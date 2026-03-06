using UnityEngine;

public class TerraGravidade : MonoBehaviour
{
    [Header("Configurações da Gravidade")]
    [Tooltip("Raio de influência da gravidade (em metros).")]
    [SerializeField] private float raioInfluencia = 60f;

    [Tooltip("Força base da atração gravitacional. Aumente para puxar mais forte.")]
    [SerializeField] private float forcaGravidade = 30f;

    [Tooltip("Distância mínima usada no cálculo para evitar força infinita quando o meteoro encosta na Terra.")]
    [SerializeField] private float distanciaMinima = 2f;

    [Header("Filtro de Alvos")]
    [Tooltip("Somente objetos com essa tag serão atraídos. Deixe vazio para atrair qualquer Rigidbody.")]
    [SerializeField] private string tagAlvo = "Asteroid";

    [Tooltip("Se marcado, puxa somente rigidbodies não-kinematic.")]
    [SerializeField] private bool ignorarKinematic = true;

    [Header("Modo do Cálculo")]
    [Tooltip("Se ligado, a força aumenta quando está mais perto (estilo 1/d²). Se desligado, força constante.")]
    [SerializeField] private bool usarForcaPorDistancia = true;

    [Tooltip("Com que frequência (em segundos) a Terra reaplica a atração. 0 = todo FixedUpdate.")]
    [SerializeField] private float intervaloAtualizacao = 0f;

    private float proximaAtualizacao = 0f;

    private void FixedUpdate()
    {
        if (intervaloAtualizacao > 0f)
        {
            if (Time.time < proximaAtualizacao) return;
            proximaAtualizacao = Time.time + intervaloAtualizacao;
        }

        AplicarGravidadeNosProximos();
    }

    /// <summary>
    /// Encontra rigidbodies próximos e aplica a atração gravitacional.
    /// </summary>
    private void AplicarGravidadeNosProximos()
    {
        Collider[] colisoes = Physics.OverlapSphere(transform.position, raioInfluencia);

        for (int i = 0; i < colisoes.Length; i++)
        {
            Rigidbody rb = colisoes[i].attachedRigidbody;
            if (rb == null) continue;

            // não puxa a própria Terra
            if (rb.gameObject == gameObject) continue;

            if (ignorarKinematic && rb.isKinematic) continue;

            if (!string.IsNullOrEmpty(tagAlvo) && !rb.CompareTag(tagAlvo)) continue;

            AtrairObjeto(rb);
        }
    }

    /// <summary>
    /// Aplica força no Rigidbody na direção do centro da Terra.
    /// </summary>
    private void AtrairObjeto(Rigidbody rb)
    {
        Vector3 direcao = (transform.position - rb.worldCenterOfMass);
        float distancia = direcao.magnitude;

        // evita divisão por zero / força absurda
        distancia = Mathf.Max(distancia, distanciaMinima);

        Vector3 direcaoNormalizada = direcao / distancia;

        float forcaFinal = forcaGravidade;

        if (usarForcaPorDistancia)
        {
            // força ~ 1 / d² (quanto mais perto, mais puxa)
            forcaFinal = forcaGravidade / (distancia * distancia);
        }

        rb.AddForce(direcaoNormalizada * forcaFinal, ForceMode.Acceleration);
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.color = new Color(0.2f, 0.6f, 1f, 0.25f);
        Gizmos.DrawSphere(transform.position, raioInfluencia);
    }
}