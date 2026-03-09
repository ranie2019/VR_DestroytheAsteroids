using UnityEngine;

public class TerraGravidade : MonoBehaviour
{
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

    private float proximaAtualizacao = 0f;

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

    private void AplicarGravidadeNosProximos()
    {
        Collider[] colisoes = Physics.OverlapSphere(transform.position, raioInfluencia);

        for (int i = 0; i < colisoes.Length; i++)
        {
            Rigidbody rb = colisoes[i].attachedRigidbody;
            if (rb == null)
                continue;

            if (rb.gameObject == gameObject)
                continue;

            if (ignorarKinematic && rb.isKinematic)
                continue;

            if (!string.IsNullOrEmpty(tagAlvo) && !rb.CompareTag(tagAlvo))
                continue;

            AtrairObjeto(rb);
        }
    }

    private void AtrairObjeto(Rigidbody rb)
    {
        Vector3 direcao = transform.position - rb.worldCenterOfMass;
        float distancia = direcao.magnitude;

        distancia = Mathf.Max(distancia, distanciaMinima);

        Vector3 direcaoNormalizada = direcao / distancia;

        float forcaFinal = forcaGravidade;

        if (usarForcaPorDistancia)
        {
            // Distância normalizada dentro do raio de influência
            float distanciaNormalizada = Mathf.Clamp01(distancia / raioInfluencia);

            // Quanto menor a distância, maior a força
            // No limite do raio: quase forcaMinima
            // Perto do centro: aproxima de forcaMaxima
            float fator = 1f / Mathf.Max(distanciaNormalizada * distanciaNormalizada, 0.001f);

            forcaFinal = forcaGravidade * fator;
        }

        forcaFinal = Mathf.Clamp(forcaFinal, forcaMinima, forcaMaxima);

        rb.AddForce(direcaoNormalizada * forcaFinal, ForceMode.Acceleration);
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.color = new Color(0.2f, 0.6f, 1f, 0.25f);
        Gizmos.DrawSphere(transform.position, raioInfluencia);
    }
}