using UnityEngine;

public class Gravidade : MonoBehaviour
{
    [Header("Área de Atração")]
    [Tooltip("Raio da área de atração onde o objeto atrai asteroides.")]
    [SerializeField] private float attractionRadius = 10f;

    [Header("Força de Atração")]
    [Tooltip("Força com que o buraco negro atrai os asteroides.")]
    [SerializeField] private float attractionForce = 100f;

    [Header("Multiplicador de Gravidade")]
    [Tooltip("Multiplicador para ajustar a força de atração simulando gravidade.")]
    [SerializeField] private float gravityMultiplier = 1f;

    [Header("Tempo de Vida")]
    [Tooltip("Tempo em segundos antes que o objeto seja destruído.")]
    [SerializeField] private float lifeTime = 10f;

    private string asteroidTag = "Asteroid";

    private GameController gameController;

    [Header("Configuração de Áudio")]
    [Tooltip("Áudio reproduzido ao colidir com um asteroide.")]
    [SerializeField] private AudioClip collisionSound; // Som de colisão
    private AudioSource audioSource;

    private void Start()
    {
        // Encontra o GameController na cena, caso não esteja atribuído manualmente
        if (gameController == null)
        {
            gameController = FindObjectOfType<GameController>();
        }

        // Inicializa o AudioSource
        audioSource = GetComponent<AudioSource>();
        if (audioSource == null)
        {
            Debug.LogWarning("AudioSource não encontrado! Adicione um componente AudioSource ao GameObject.");
        }

        // Inicia a contagem regressiva para destruir o objeto
        Invoke(nameof(DestroyObject), lifeTime);
    }

    private void FixedUpdate()
    {
        AttractAsteroids();
    }

    private void AttractAsteroids()
    {
        // Encontra todos os asteroides dentro da área de atração
        Collider[] asteroidsInRange = Physics.OverlapSphere(transform.position, attractionRadius);

        foreach (Collider collider in asteroidsInRange)
        {
            if (collider.CompareTag(asteroidTag))
            {
                Rigidbody asteroidRigidbody = collider.GetComponent<Rigidbody>();
                if (asteroidRigidbody != null)
                {
                    // Aplica a atração
                    Vector3 directionToCenter = (transform.position - collider.transform.position).normalized;
                    float distanceToCenter = Vector3.Distance(transform.position, collider.transform.position);
                    float scaledForce = attractionForce / Mathf.Pow(distanceToCenter, 2) * gravityMultiplier;
                    asteroidRigidbody.AddForce(directionToCenter * scaledForce, ForceMode.Acceleration);
                }
            }
        }
    }

    private void DestroyAsteroid(GameObject asteroid)
    {
        if (asteroid != null)
        {
            int asteroidScore = CalculateScore(asteroid.transform.position);

            if (gameController != null)
            {
                gameController.UpdatePlayerScore(asteroidScore);
            }

            Destroy(asteroid);
        }
    }

    private int CalculateScore(Vector3 asteroidPosition)
    {
        float distanceFromCenter = Vector3.Distance(transform.position, asteroidPosition);
        return Mathf.Max(1, (int)distanceFromCenter);
    }

    private void DestroyObject()
    {
        Destroy(gameObject);
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag(asteroidTag))
        {
            Debug.Log($"Colisão com {collision.gameObject.name}");

            // Reproduz o som de colisão
            if (audioSource != null && collisionSound != null)
            {
                audioSource.PlayOneShot(collisionSound);
            }

            // Destrói o asteroide após a colisão
            DestroyAsteroid(collision.gameObject);
        }
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.green;
        Gizmos.DrawWireSphere(transform.position, attractionRadius);
    }
}
