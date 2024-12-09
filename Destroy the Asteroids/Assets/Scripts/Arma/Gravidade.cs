using UnityEngine;

public class Gravidade : MonoBehaviour
{
    [Header("�rea de Atra��o")]
    [Tooltip("Raio da �rea de atra��o onde o objeto atrai asteroides.")]
    [SerializeField] private float attractionRadius = 10f;

    [Header("For�a de Atra��o")]
    [Tooltip("For�a com que o buraco negro atrai os asteroides.")]
    [SerializeField] private float attractionForce = 100f;

    [Header("Multiplicador de Gravidade")]
    [Tooltip("Multiplicador para ajustar a for�a de atra��o simulando gravidade.")]
    [SerializeField] private float gravityMultiplier = 1f;

    [Header("Tempo de Vida")]
    [Tooltip("Tempo em segundos antes que o objeto seja destru�do.")]
    [SerializeField] private float lifeTime = 10f;

    [Header("Configura��o de �udio")]
    [Tooltip("�udio reproduzido ao colidir com um asteroide.")]
    [SerializeField] private AudioClip collisionSound;

    private const string AsteroidTag = "Asteroid"; // Tag dos asteroides
    private GameController gameController;
    private AudioSource audioSource;

    private void Start()
    {
        // Encontra o GameController automaticamente, caso n�o esteja atribu�do
        gameController = gameController ?? FindObjectOfType<GameController>();

        // Inicializa ou adiciona um AudioSource, se necess�rio
        audioSource = GetComponent<AudioSource>() ?? gameObject.AddComponent<AudioSource>();

        // Inicia o temporizador para destruir o objeto ap�s `lifeTime`
        Invoke(nameof(DestroyObject), lifeTime);
    }

    private void FixedUpdate()
    {
        AttractAsteroids();
    }

    private void AttractAsteroids()
    {
        // Encontra todos os objetos dentro do raio de atra��o
        Collider[] collidersInRange = Physics.OverlapSphere(transform.position, attractionRadius);

        foreach (var collider in collidersInRange)
        {
            if (collider.CompareTag(AsteroidTag))
            {
                // Movimenta��o manual do asteroide
                Vector3 directionToCenter = (transform.position - collider.transform.position).normalized;
                float distanceSquared = Mathf.Max(Vector3.SqrMagnitude(transform.position - collider.transform.position), 0.1f); // Evita divis�o por zero
                float scaledForce = (attractionForce * gravityMultiplier) / distanceSquared;

                // Aplica a atra��o manualmente
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
            // Calcula a pontua��o com base na posi��o do asteroide
            int score = CalculateScore(asteroid.transform.position);

            // Atualiza o placar do jogador
            if (gameController != null)
            {
                gameController.UpdatePlayerScore(score);
            }
            else
            {
                Debug.LogWarning("GameController n�o encontrado! N�o foi poss�vel atualizar a pontua��o.");
            }

            // Destroi o asteroide
            Destroy(asteroid);
        }
    }

    private int CalculateScore(Vector3 asteroidPosition)
    {
        // C�lculo de pontua��o com base na dist�ncia do asteroide
        float distanceFromCenter = Vector3.Distance(transform.position, asteroidPosition);
        int score = Mathf.Max(1, Mathf.CeilToInt(distanceFromCenter)); // Garantir que a pontua��o n�o seja zero
        return score;
    }

    private void DestroyObject()
    {
        // Destroi o objeto de gravidade ap�s o tempo de vida
        Destroy(gameObject);
    }

    private void OnDrawGizmosSelected()
    {
        // Visualiza a �rea de atra��o no Editor
        Gizmos.color = Color.green;
        Gizmos.DrawWireSphere(transform.position, attractionRadius);
    }
}
