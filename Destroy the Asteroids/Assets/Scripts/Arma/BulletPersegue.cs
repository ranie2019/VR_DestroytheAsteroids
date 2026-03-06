using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
public class BulletPersegue : MonoBehaviour
{
    [Header("Alvo")]
    [Tooltip("Tag do alvo que a bala deve perseguir.")]
    [SerializeField] private string tagAlvo = "Asteroid";

    [Header("Configurações")]
    [Tooltip("Tempo máximo de vida da bala (se não colidir, ela se destrói sozinha).")]
    [SerializeField] private float tempoDeVida = 5f;

    [Tooltip("Velocidade da bala (m/s).")]
    [SerializeField] private float velocidade = 10f;

    private Rigidbody rb;
    private Transform alvo;

    private void Awake()
    {
        rb = GetComponent<Rigidbody>();
        rb.useGravity = false;

        tempoDeVida = Mathf.Max(tempoDeVida, 0.1f);
        velocidade = Mathf.Max(velocidade, 0.01f);

        Destroy(gameObject, tempoDeVida);

        alvo = EncontrarAlvoMaisProximo();
    }

    private void FixedUpdate()
    {
        if (alvo == null)
            alvo = EncontrarAlvoMaisProximo();

        if (alvo == null)
        {
            rb.linearVelocity = transform.forward * velocidade;
            return;
        }

        Vector3 direcao = (alvo.position - transform.position).normalized;
        rb.linearVelocity = direcao * velocidade;
        rb.MoveRotation(Quaternion.LookRotation(direcao, Vector3.up));
    }

    private Transform EncontrarAlvoMaisProximo()
    {
        GameObject[] alvos = GameObject.FindGameObjectsWithTag(tagAlvo);

        Transform melhor = null;
        float menorDistSqr = float.MaxValue;
        Vector3 pos = transform.position;

        foreach (var obj in alvos)
        {
            if (!obj || !obj.activeInHierarchy) continue;

            float distSqr = (obj.transform.position - pos).sqrMagnitude;
            if (distSqr < menorDistSqr)
            {
                menorDistSqr = distSqr;
                melhor = obj.transform;
            }
        }

        return melhor;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject == gameObject) return;

        // Se for o alvo: destrói alvo + bala
        if (other.CompareTag(tagAlvo))
        {
            Destroy(other.gameObject);
        }

        // Qualquer colisão: bala se destrói
        Destroy(gameObject);
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject == gameObject) return;

        // Se for o alvo: destrói alvo + bala
        if (collision.gameObject.CompareTag(tagAlvo))
        {
            Destroy(collision.gameObject);
        }

        // Qualquer colisão: bala se destrói
        Destroy(gameObject);
    }
}