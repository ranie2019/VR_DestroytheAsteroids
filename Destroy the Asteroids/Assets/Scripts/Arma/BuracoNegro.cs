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

    private GameController gameController;
    private AudioSource audioSource;

    private void Start()
    {
        // Encontra o GameController automaticamente
        gameController = gameController ?? FindObjectOfType<GameController>();

        // Inicializa ou adiciona um AudioSource, se necessário
        audioSource = GetComponent<AudioSource>() ?? gameObject.AddComponent<AudioSource>();

        // Destroi o objeto após o tempo especificado
        Destroy(gameObject, lifeTime);
    }

    private void Update()
    {
        // Move o buraco negro na direção especificada
        MoveObject();
    }

    private void MoveObject()
    {
        transform.Translate(movementDirection * movementSpeed * Time.deltaTime, Space.World);
    }

    private void OnTriggerEnter(Collider other)
    {
        // Verifica se o objeto com o qual houve interação é um asteroide
        if (other.CompareTag("Asteroid"))
        {
            DestroyAsteroid(other.gameObject);
        }
    }

    private void DestroyAsteroid(GameObject asteroid)
    {
        if (asteroid != null)
        {
            // Reproduz som de colisão
            PlayCollisionSound();

            // Atualiza a pontuação
            if (gameController != null)
            {
                gameController.UpdatePlayerScore(50); // Incrementa a pontuação
            }
            else
            {
                Debug.LogWarning("GameController não encontrado! Não foi possível atualizar a pontuação.");
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