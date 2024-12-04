using UnityEngine;
using TMPro;

public class AsteroidHit : MonoBehaviour
{
    [Header("Configurações de Destruição")]
    [SerializeField] private GameObject asteroidExplosion;
    [SerializeField] private float explosionLifetime = 2f;
    [SerializeField] private float asteroidDestroyDelay = 0f;

    [Header("Referências Externas")]
    [SerializeField] private GameObject popupCanvas;
    [SerializeField] private Transform playerTransform; // Transform do jogador

    private GameController gameController;

    private void Awake()
    {
        // Tenta encontrar o GameController na cena
        gameController = FindAnyObjectByType<GameController>();

        // Verifica se o GameController foi encontrado
        if (gameController == null)
        {
            Debug.LogError("GameController não encontrado!");
        }

        // Tenta localizar o Transform do jogador na cena, se não foi atribuído
        if (playerTransform == null)
        {
            playerTransform = GameObject.FindWithTag("Player")?.transform;

            if (playerTransform == null)
            {
                Debug.LogError("Transform do jogador não foi encontrado!");
            }
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
        // Conteúdo do método permanece o mesmo
        CreateExplosion();

        int asteroidScore = CalculateScore();
        Debug.Log("Pontuação calculada: " + asteroidScore);
        ShowScorePopup(asteroidScore);

        if (gameController != null)
        {
            gameController.UpdatePlayerScore(asteroidScore);
            Debug.Log("Pontuação do jogador atualizada.");
        }
        else
        {
            Debug.LogError("GameController não foi encontrado na cena.");
        }

        Destroy(gameObject, asteroidDestroyDelay);
    }

    private void CreateExplosion()
    {
        // Instancia a explosão do asteroide se o prefab foi atribuído
        if (asteroidExplosion != null)
        {
            GameObject explosion = Instantiate(asteroidExplosion, transform.position, transform.rotation);
            Destroy(explosion, explosionLifetime); // Destroi a explosão após o tempo definido
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
        // Mostra o popup com a pontuação se o prefab foi atribuído
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
            Debug.LogError("Popup Canvas ou Transform do jogador está faltando!");
        }
    }
}
