using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
public class BulletReta : MonoBehaviour
{
    [Header("Alvo")]
    [Tooltip("Tag do alvo que pode ser destruído.")]
    [SerializeField] private string tagAlvo = "Asteroid";

    [Header("Configurações")]
    [SerializeField] private float tempoDeVida = 5f;
    [SerializeField] private float velocidade = 10f;

    private Rigidbody rb;

    private void Awake()
    {
        rb = GetComponent<Rigidbody>();
        rb.useGravity = false;
        rb.collisionDetectionMode = CollisionDetectionMode.Continuous;

        tempoDeVida = Mathf.Max(tempoDeVida, 0.1f);
        velocidade = Mathf.Max(velocidade, 0.01f);

        Destroy(gameObject, tempoDeVida);
    }

    private void FixedUpdate()
    {
        rb.linearVelocity = transform.forward * velocidade;
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject == gameObject) return;

        // Se for o alvo, destrói o alvo
        if (collision.gameObject.CompareTag(tagAlvo))
        {
            Destroy(collision.gameObject);
        }

        // Independente do que for, a bala se destrói
        Destroy(gameObject);
    }
}