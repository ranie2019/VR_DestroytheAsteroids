using UnityEngine;
using TMPro;

public class AsteroidHit : MonoBehaviour
{
    [Header("Configura��es de Destrui��o")]
    [SerializeField] private GameObject asteroidExplosion; // Prefab da explos�o
    [SerializeField] private float explosionLifetime = 2f; // Tempo de vida da explos�o
    [SerializeField] private float asteroidDestroyDelay = 0f; // Tempo antes de destruir o asteroide

    [Header("Refer�ncias Externas")]
    [SerializeField] private GameObject popupCanvas; // Prefab do popup de pontua��o
    private Transform playerTransform; // Transform do jogador

    private GameController gameController;

    private void Awake()
    {
        // Tenta encontrar o GameController na cena
        gameController = FindAnyObjectByType<GameController>();

        // Tenta localizar o Transform do jogador na cena, se n�o foi atribu�do
        if (playerTransform == null)
        {
            playerTransform = GameObject.FindWithTag("Player")?.transform;
        }

        // Verifica��o adicional para prefabs obrigat�rios
        if (asteroidExplosion == null)
        {
            Debug.LogError("O prefab de explos�o n�o foi atribu�do ao campo 'Asteroid Explosion'!");
        }

        if (popupCanvas == null)
        {
            Debug.LogError("O prefab do popup de pontua��o n�o foi atribu�do ao campo 'Popup Canvas'!");
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        // Verifica se o objeto que colidiu tem a tag "Laser"
        if (collision.gameObject.CompareTag("Laser"))
        {
            HandleAsteroidDestruction();
            Destroy(collision.gameObject); // Destr�i o laser ap�s a colis�o
        }
    }

    public void HandleAsteroidDestruction()
    {
        // Instancia a explos�o do asteroide
        CreateExplosion();

        // Calcula e exibe a pontua��o
        int asteroidScore = CalculateScore();
        ShowScorePopup(asteroidScore);

        // Atualiza a pontua��o do jogador
        if (gameController != null)
        {
            gameController.UpdatePlayerScore(asteroidScore);
        }
        else
        {
            Debug.LogWarning("GameController n�o encontrado. N�o foi poss�vel atualizar a pontua��o!");
        }

        // Destroi o asteroide ap�s o tempo de delay
        Destroy(gameObject, asteroidDestroyDelay);
    }

    private void CreateExplosion()
    {
        // Verifica se o prefab da explos�o foi atribu�do antes de instanciar
        if (asteroidExplosion != null)
        {
            GameObject explosion = Instantiate(asteroidExplosion, transform.position, transform.rotation);
            Destroy(explosion, explosionLifetime); // Destroi a explos�o ap�s o tempo definido
        }
        else
        {
            Debug.LogError("Prefab de explos�o ausente. Certifique-se de atribuir o campo 'Asteroid Explosion'.");
        }
    }

    private int CalculateScore()
    {
        // Calcula a pontua��o com base na dist�ncia do centro da cena (Vector3.zero)
        float distanceFromCenter = Vector3.Distance(transform.position, Vector3.zero);

        // Garante que a pontua��o m�nima seja 1
        return Mathf.Max(1, (int)distanceFromCenter);
    }

    private void ShowScorePopup(int score)
    {
        // Verifica se o prefab do popup foi atribu�do antes de instanciar
        if (popupCanvas != null && playerTransform != null)
        {
            GameObject asteroidPopup = Instantiate(popupCanvas, transform.position, Quaternion.identity);
            TextMeshProUGUI scoreText = asteroidPopup.transform.GetChild(0).GetComponent<TextMeshProUGUI>();

            // Define o texto do popup como a pontua��o calculada
            scoreText.text = score.ToString();

            // Ajusta a escala do popup com base na dist�ncia do centro
            float scaleMultiplier = Vector3.Distance(transform.position, Vector3.zero) / 10f;
            asteroidPopup.transform.localScale = transform.localScale * scaleMultiplier;

            // Aponta o popup para o jogador
            Vector3 directionToPlayer = (playerTransform.position - asteroidPopup.transform.position).normalized;
            asteroidPopup.transform.forward = directionToPlayer;
        }
        else
        {
            Debug.LogError("Popup Canvas ou Player Transform n�o encontrados. N�o foi poss�vel exibir o popup.");
        }
    }
}
