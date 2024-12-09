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
}