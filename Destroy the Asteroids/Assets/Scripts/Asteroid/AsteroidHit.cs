using UnityEngine;
using TMPro;

public class AsteroidHit : MonoBehaviour
{
    [Header("Configurações de Destruição")]
    [SerializeField] private GameObject asteroidExplosion; // Prefab da explosão
    [SerializeField] private float explosionLifetime = 2f; // Tempo de vida da explosão
    [SerializeField] private float asteroidDestroyDelay = 0f; // Tempo antes de destruir o asteroide

    [Header("Áudio")]
    [SerializeField] private AudioClip explosionSound; // Som da explosão
    [SerializeField] private float audioVolume = 1f;

    [Header("Referências Externas")]
    [SerializeField] private GameObject popupCanvas; // Prefab do popup de pontuação
    private Transform playerTransform; // Transform do jogador

    private PontoController gameController;

    private void Awake()
    {
        // Tenta encontrar o GameController na cena
        gameController = FindAnyObjectByType<PontoController>();

        // Tenta localizar o Transform do jogador na cena, se não foi atribuído
        if (playerTransform == null)
        {
            playerTransform = GameObject.FindWithTag("Player")?.transform;
        }

        if (asteroidExplosion == null)
        {
            Debug.LogError("O prefab de explosão não foi atribuído ao campo 'Asteroid Explosion'!");
        }

        if (popupCanvas == null)
        {
            Debug.LogError("O prefab do popup de pontuação não foi atribuído ao campo 'Popup Canvas'!");
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Laser"))
        {
            HandleAsteroidDestruction();
            Destroy(collision.gameObject);
        }
    }

    public void HandleAsteroidDestruction()
    {
        // NOVO: toca o áudio
        PlayExplosionSound();

        // Instancia a explosão
        CreateExplosion();

        int asteroidScore = CalculateScore();
        ShowScorePopup(asteroidScore);

        if (gameController != null)
        {
            gameController.UpdatePlayerScore(asteroidScore);
        }
        else
        {
            Debug.LogWarning("GameController não encontrado. Não foi possível atualizar a pontuação!");
        }

        Destroy(gameObject, asteroidDestroyDelay);
    }

    private void CreateExplosion()
    {
        if (asteroidExplosion != null)
        {
            GameObject explosion = Instantiate(asteroidExplosion, transform.position, transform.rotation);
            Destroy(explosion, explosionLifetime);
        }
        else
        {
            Debug.LogError("Prefab de explosão ausente. Certifique-se de atribuir o campo 'Asteroid Explosion'.");
        }
    }

    // ===== NOVA FUNÇÃO DE ÁUDIO =====
    private void PlayExplosionSound()
    {
        if (explosionSound != null)
        {
            AudioSource.PlayClipAtPoint(explosionSound, transform.position, audioVolume);
        }
    }

    private int CalculateScore()
    {
        float distanceFromCenter = Vector3.Distance(transform.position, Vector3.zero);
        return Mathf.Max(1, (int)distanceFromCenter);
    }

    private void ShowScorePopup(int score)
    {
        if (popupCanvas != null && playerTransform != null)
        {
            GameObject asteroidPopup = Instantiate(popupCanvas, transform.position, Quaternion.identity);
            TextMeshProUGUI scoreText = asteroidPopup.transform.GetChild(0).GetComponent<TextMeshProUGUI>();

            scoreText.text = score.ToString();

            float scaleMultiplier = Vector3.Distance(transform.position, Vector3.zero) / 10f;
            asteroidPopup.transform.localScale = transform.localScale * (scaleMultiplier / 2f);

            Vector3 directionToPlayer = (playerTransform.position - asteroidPopup.transform.position).normalized;
            asteroidPopup.transform.forward = directionToPlayer;
        }
        else
        {
            Debug.LogError("Popup Canvas ou Player Transform não encontrados. Não foi possível exibir o popup.");
        }
    }
}