using UnityEngine;
using TMPro;

public class AsteroidHit : MonoBehaviour
{
    [Header("Configurações de Destruição")]
    [SerializeField] private GameObject asteroidExplosion; // Prefab da explosão
    [SerializeField] private float explosionLifetime = 2f; // Tempo de vida da explosão
    [SerializeField] private float asteroidDestroyDelay = 0f; // Tempo antes de destruir o asteroide

    [Header("Referências Externas")]
    [SerializeField] private GameObject popupCanvas; // Prefab do popup de pontuação
    private Transform playerTransform; // Transform do jogador

    private GameController gameController;

    private void Awake()
    {
        // Tenta encontrar o GameController na cena
        gameController = FindAnyObjectByType<GameController>();

        // Tenta localizar o Transform do jogador na cena, se não foi atribuído
        if (playerTransform == null)
        {
            playerTransform = GameObject.FindWithTag("Player")?.transform;
        }

        // Verificação adicional para prefabs obrigatórios
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
        // Verifica se o objeto que colidiu tem a tag "Laser"
        if (collision.gameObject.CompareTag("Laser"))
        {
            HandleAsteroidDestruction();
            Destroy(collision.gameObject); // Destrói o laser após a colisão
        }
    }

    public void HandleAsteroidDestruction()
    {
        // Instancia a explosão do asteroide
        CreateExplosion();

        // Calcula e exibe a pontuação
        int asteroidScore = CalculateScore();
        ShowScorePopup(asteroidScore);

        // Atualiza a pontuação do jogador
        if (gameController != null)
        {
            gameController.UpdatePlayerScore(asteroidScore);
        }
        else
        {
            Debug.LogWarning("GameController não encontrado. Não foi possível atualizar a pontuação!");
        }

        // Destroi o asteroide após o tempo de delay
        Destroy(gameObject, asteroidDestroyDelay);
    }

    private void CreateExplosion()
    {
        // Verifica se o prefab da explosão foi atribuído antes de instanciar
        if (asteroidExplosion != null)
        {
            GameObject explosion = Instantiate(asteroidExplosion, transform.position, transform.rotation);
            Destroy(explosion, explosionLifetime); // Destroi a explosão após o tempo definido
        }
        else
        {
            Debug.LogError("Prefab de explosão ausente. Certifique-se de atribuir o campo 'Asteroid Explosion'.");
        }
    }

    private int CalculateScore()
    {
        // Calcula a pontuação com base na distância do centro da cena (Vector3.zero)
        float distanceFromCenter = Vector3.Distance(transform.position, Vector3.zero);

        // Garante que a pontuação mínima seja 1
        return Mathf.Max(1, (int)distanceFromCenter);
    }

    private void ShowScorePopup(int score)
    {
        // Verifica se o prefab do popup foi atribuído antes de instanciar
        if (popupCanvas != null && playerTransform != null)
        {
            GameObject asteroidPopup = Instantiate(popupCanvas, transform.position, Quaternion.identity);
            TextMeshProUGUI scoreText = asteroidPopup.transform.GetChild(0).GetComponent<TextMeshProUGUI>();

            // Define o texto do popup como a pontuação calculada
            scoreText.text = score.ToString();

            // Ajusta a escala do popup com base na distância do centro
            float scaleMultiplier = Vector3.Distance(transform.position, Vector3.zero) / 10f;
            asteroidPopup.transform.localScale = transform.localScale * scaleMultiplier;

            // Aponta o popup para o jogador
            Vector3 directionToPlayer = (playerTransform.position - asteroidPopup.transform.position).normalized;
            asteroidPopup.transform.forward = directionToPlayer;
        }
        else
        {
            Debug.LogError("Popup Canvas ou Player Transform não encontrados. Não foi possível exibir o popup.");
        }
    }
}
