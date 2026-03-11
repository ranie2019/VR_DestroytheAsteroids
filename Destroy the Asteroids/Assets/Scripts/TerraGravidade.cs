using UnityEngine;

public class TerraGravidade : MonoBehaviour
{
    [Header("Centro da Gravidade")]
    [Tooltip("Ponto central real da gravidade. Se vazio, usa o transform deste objeto.")]
    [SerializeField] private Transform centroGravidade;

    [Header("Configurações da Gravidade")]
    [Tooltip("Raio de influência da gravidade.")]
    [SerializeField] private float raioInfluencia = 3000f;

    [Tooltip("Força base da gravidade.")]
    [SerializeField] private float forcaGravidade = 30f;

    [Tooltip("Distância mínima para evitar força infinita perto demais do centro.")]
    [SerializeField] private float distanciaMinima = 5f;

    [Header("Filtro de Alvos")]
    [Tooltip("Somente objetos com essa tag serão atraídos.")]
    [SerializeField] private string tagAlvo = "Asteroid";

    [Tooltip("Se marcado, ignora rigidbodies kinematic.")]
    [SerializeField] private bool ignorarKinematic = true;

    [Header("Modo do Cálculo")]
    [Tooltip("Se marcado, a força aumenta conforme o objeto se aproxima do centro.")]
    [SerializeField] private bool usarForcaPorDistancia = true;

    [Tooltip("Com que frequência reaplica a gravidade. 0 = todo FixedUpdate.")]
    [SerializeField] private float intervaloAtualizacao = 0f;

    [Header("Limites")]
    [Tooltip("Força mínima aplicada quando o objeto está no limite do raio.")]
    [SerializeField] private float forcaMinima = 2f;

    [Tooltip("Força máxima para evitar aceleração absurda perto demais do centro.")]
    [SerializeField] private float forcaMaxima = 300f;

    [Header("Performance")]
    [Tooltip("Quantidade máxima de colliders verificados por atualização.")]
    [SerializeField] private int maxColisoes = 256;

    private float proximaAtualizacao = 0f;
    private Collider[] resultadosOverlap;

    private void Awake()
    {
        if (maxColisoes < 1)
            maxColisoes = 1;

        resultadosOverlap = new Collider[maxColisoes];
    }

    private void FixedUpdate()
    {
        if (intervaloAtualizacao > 0f)
        {
            if (Time.time < proximaAtualizacao)
                return;

            proximaAtualizacao = Time.time + intervaloAtualizacao;
        }

        AplicarGravidadeNosProximos();
    }

    private Vector3 ObterCentroGravidade()
    {
        if (centroGravidade != null)
            return centroGravidade.position;

        return transform.position;
    }

    private void AplicarGravidadeNosProximos()
    {
        Vector3 centro = ObterCentroGravidade();

        int quantidade = Physics.OverlapSphereNonAlloc(
            centro,
            raioInfluencia,
            resultadosOverlap
        );

        for (int i = 0; i < quantidade; i++)
        {
            Collider col = resultadosOverlap[i];

            if (col == null)
                continue;

            Rigidbody rb = col.attachedRigidbody;

            if (rb == null)
                continue;

            if (rb.gameObject == gameObject)
                continue;

            if (ignorarKinematic && rb.isKinematic)
                continue;

            if (!string.IsNullOrEmpty(tagAlvo) && !rb.CompareTag(tagAlvo))
                continue;

            AtrairObjeto(rb, centro);
        }
    }

    private void AtrairObjeto(Rigidbody rb, Vector3 centro)
    {
        Vector3 direcao = centro - rb.worldCenterOfMass;
        float distancia = direcao.magnitude;

        if (distancia <= 0.0001f)
            return;

        distancia = Mathf.Max(distancia, distanciaMinima);

        Vector3 direcaoNormalizada = direcao / distancia;

        float forcaFinal = forcaGravidade;

        if (usarForcaPorDistancia)
        {
            float distanciaNormalizada = Mathf.Clamp01(distancia / raioInfluencia);

            float fator = 1f / Mathf.Max(distanciaNormalizada * distanciaNormalizada, 0.001f);
            forcaFinal = forcaGravidade * fator;
        }

        forcaFinal = Mathf.Clamp(forcaFinal, forcaMinima, forcaMaxima);

        rb.AddForce(direcaoNormalizada * forcaFinal, ForceMode.Acceleration);
    }

    private void OnDrawGizmosSelected()
    {
        Vector3 centro = centroGravidade != null ? centroGravidade.position : transform.position;

        Gizmos.color = new Color(0.2f, 0.6f, 1f, 0.25f);
        Gizmos.DrawSphere(centro, raioInfluencia);

        Gizmos.color = Color.cyan;
        Gizmos.DrawWireSphere(centro, 0.5f);
    }
}