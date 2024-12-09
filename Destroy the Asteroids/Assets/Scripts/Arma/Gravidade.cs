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

    [Header("Configuração de Áudio")]
    [Tooltip("Áudio reproduzido ao colidir com um asteroide.")]
    [SerializeField] private AudioClip collisionSound;

    private const string AsteroidTag = "Asteroid"; // Tag dos asteroides
    private GameController gameController;
    private AudioSource audioSource;

    private void Start()
    {
        // Encontra o GameController automaticamente, caso não esteja atribuído
        gameController = gameController ?? FindObjectOfType<GameController>();

        // Inicializa ou adiciona um AudioSource, se necessário
        audioSource = GetComponent<AudioSource>() ?? gameObject.AddComponent<AudioSource>();

        // Inicia o temporizador para destruir o objeto após `lifeTime`
        Invoke(nameof(DestroyObject), lifeTime);
    }

    private void FixedUpdate()
    {
        AttractAsteroids();
    }

    private void AttractAsteroids()
    {
        // Encontra todos os objetos dentro do raio de atração
        Collider[] collidersInRange = Physics.OverlapSphere(transform.position, attractionRadius);

        foreach (var collider in collidersInRange)
        {
            if (collider.CompareTag(AsteroidTag))
            {
                // Movimentação manual do asteroide
                Vector3 directionToCenter = (transform.position - collider.transform.position).normalized;
                float distanceSquared = Mathf.Max(Vector3.SqrMagnitude(transform.position - collider.transform.position), 0.1f); // Evita divisão por zero
                float scaledForce = (attractionForce * gravityMultiplier) / distanceSquared;

                // Aplica a atração manualmente
                collider.transform.position += directionToCenter * scaledForce * Time.fixedDeltaTime;
            }
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        // Evita que o objeto de gravidade se mova ao colidir com um asteroide
        if (collision.gameObject.CompareTag(AsteroidTag))
        {
            PlayCollisionSound();
            DestroyAsteroid(collision.gameObject);
        }
    }

    private void PlayCollisionSound()
    {
        if (audioSource != null && collisionSound != null)
        {
            audioSource.PlayOneShot(collisionSound);
        }
    }

    private void DestroyAsteroid(GameObject asteroid)
    {
        if (asteroid != null)
        {
            // Calcula a pontuação com base na posição do asteroide
            int score = CalculateScore(asteroid.transform.position);

            // Atualiza o placar do jogador
            if (gameController != null)
            {
                gameController.UpdatePlayerScore(score);
            }
            else
            {
                Debug.LogWarning("GameController não encontrado! Não foi possível atualizar a pontuação.");
            }

            // Destroi o asteroide
            Destroy(asteroid);
        }
    }

    private int CalculateScore(Vector3 asteroidPosition)
    {
        // Cálculo de pontuação com base na distância do asteroide
        float distanceFromCenter = Vector3.Distance(transform.position, asteroidPosition);
        int score = Mathf.Max(1, Mathf.CeilToInt(distanceFromCenter)); // Garantir que a pontuação não seja zero
        return score;
    }

    private void DestroyObject()
    {
        // Destroi o objeto de gravidade após o tempo de vida
        Destroy(gameObject);
    }

    private void OnDrawGizmosSelected()
    {
        // Visualiza a área de atração no Editor
        Gizmos.color = Color.green;
        Gizmos.DrawWireSphere(transform.position, attractionRadius);
    }
}
