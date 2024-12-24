using UnityEngine;

public class BuracoNegro : MonoBehaviour
{
    [Header("Configura��o de �udio")]
    [Tooltip("�udio reproduzido ao colidir com um asteroide.")]
    [SerializeField] private AudioClip collisionSound;

    [Header("Tempo de Vida")]
    [Tooltip("Tempo em segundos antes que o objeto seja destru�do.")]
    [SerializeField] private float lifeTime = 10f;

    [Header("Configura��o de Movimento")]
    [Tooltip("Dire��o do movimento do objeto.")]
    [SerializeField] private Vector3 movementDirection = Vector3.forward;
    [Tooltip("Velocidade de movimento do objeto.")]
    [SerializeField] private float movementSpeed = 5f;

    [Header("Configura��o de Atra��o")]
    [Tooltip("Raio de atra��o do buraco negro.")]
    [SerializeField] private float attractionRadius = 10f;
    [Tooltip("For�a de atra��o aplicada nos asteroides.")]
    [SerializeField] private float attractionForce = 50f;

    private GameController gameController;
    private AudioSource audioSource;

    private void Start()
    {
        // Encontra o GameController automaticamente
        gameController = gameController ?? FindObjectOfType<GameController>();

        // Inicializa ou adiciona um AudioSource, se necess�rio
        audioSource = GetComponent<AudioSource>() ?? gameObject.AddComponent<AudioSource>();

        // Destroi o objeto ap�s o tempo especificado
        Destroy(gameObject, lifeTime);
    }

    private void Update()
    {
        // Move o buraco negro na dire��o especificada
        MoveObject();

        // Aplica atra��o aos asteroides
        AttractAsteroids();
    }

    private void MoveObject()
    {
        transform.Translate(movementDirection * movementSpeed * Time.deltaTime, Space.World);
    }

    private void OnTriggerEnter(Collider other)
    {
        // Verifica se o objeto com o qual houve intera��o � um asteroide
        if (other.CompareTag("Asteroid"))
        {
            DestroyAsteroid(other.gameObject);
        }
    }

    private void DestroyAsteroid(GameObject asteroid)
    {
        if (asteroid != null)
        {
            // Reproduz som de colis�o
            PlayCollisionSound();

            // Atualiza a pontua��o
            if (gameController != null)
            {
                gameController.UpdatePlayerScore(50); // Incrementa a pontua��o
            }
            else
            {
                Debug.LogWarning("GameController n�o encontrado! N�o foi poss�vel atualizar a pontua��o.");
            }

            // Destroi o asteroide
            Destroy(asteroid);
        }
    }

    private void PlayCollisionSound()
    {
        if (audioSource != null && collisionSound != null)
        {
            audioSource.PlayOneShot(collisionSound);
        }
    }

    private void AttractAsteroids()
    {
        Collider[] colliders = Physics.OverlapSphere(transform.position, attractionRadius);

        foreach (Collider collider in colliders)
        {
            if (collider.CompareTag("Asteroid"))
            {
                Rigidbody asteroidRigidbody = collider.GetComponent<Rigidbody>();

                if (asteroidRigidbody != null)
                {
                    Vector3 directionToCenter = (transform.position - collider.transform.position).normalized;
                    asteroidRigidbody.AddForce(directionToCenter * attractionForce * Time.deltaTime, ForceMode.Acceleration);
                }
            }
        }
    }

    private void OnDrawGizmosSelected()
    {
        // Desenha a �rea de atra��o no Editor
        Gizmos.color = new Color(0, 0, 1, 0.3f); // Azul transparente
        Gizmos.DrawSphere(transform.position, attractionRadius);
    }
}
