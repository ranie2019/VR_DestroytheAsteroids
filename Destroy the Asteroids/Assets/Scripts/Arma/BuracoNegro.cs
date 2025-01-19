using UnityEngine;

public class BuracoNegro : MonoBehaviour
{
    [Header("Configuração de Áudio")]
    [Tooltip("Áudio reproduzido ao colidir com um asteroide.")]
    [SerializeField] private AudioClip collisionSound;

    [Header("Tempo de Vida")]
    [Tooltip("Tempo em segundos antes que o objeto seja destruído.")]
    [SerializeField] private float lifeTime = 10f;

    [Header("Configuração de Movimento")]
    [Tooltip("Direção do movimento do objeto.")]
    [SerializeField] private Vector3 movementDirection = Vector3.forward;
    [Tooltip("Velocidade de movimento do objeto.")]
    [SerializeField] private float movementSpeed = 5f;

    [Header("Configuração de Atração")]
    [Tooltip("Raio de atração do buraco negro.")]
    [SerializeField] private float attractionRadius = 10f;
    [Tooltip("Força de atração aplicada nos asteroides.")]
    [SerializeField] private float attractionForce = 50f;

    private GameController gameController;
    private AudioSource audioSource;

    private void Start()
    {
        gameController = FindObjectOfType<GameController>();
        if (gameController == null)
        {
            Debug.LogError("GameController não encontrado! Certifique-se de que há um GameController na cena.");
        }

        audioSource = GetComponent<AudioSource>() ?? gameObject.AddComponent<AudioSource>();

        Destroy(gameObject, lifeTime);

        // Inicia atração com intervalo otimizado
        InvokeRepeating(nameof(AttractAsteroids), 0f, 0.2f);
    }

    private void Update()
    {
        MoveObject();
    }

    private void MoveObject()
    {
        transform.Translate(movementDirection * movementSpeed * Time.deltaTime, Space.World);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Asteroid"))
        {
            DestroyAsteroid(other.gameObject);
        }
    }

    private void DestroyAsteroid(GameObject asteroid)
    {
        if (asteroid != null)
        {
            PlayCollisionSound();

            gameController?.UpdatePlayerScore(50);

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
                if (collider.TryGetComponent<Rigidbody>(out var asteroidRigidbody))
                {
                    Vector3 directionToCenter = (transform.position - collider.transform.position).normalized;
                    asteroidRigidbody.AddForce(directionToCenter * attractionForce * Time.deltaTime, ForceMode.Acceleration);

                }
            }
        }
    }

    private void OnValidate()
    {
        lifeTime = Mathf.Max(0.1f, lifeTime);
        movementSpeed = Mathf.Max(0f, movementSpeed);
        attractionRadius = Mathf.Max(0f, attractionRadius);
        attractionForce = Mathf.Max(0f, attractionForce);
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.color = new Color(0, 0, 1, 0.3f); // Azul transparente
        Gizmos.DrawSphere(transform.position, attractionRadius);
    }
}
