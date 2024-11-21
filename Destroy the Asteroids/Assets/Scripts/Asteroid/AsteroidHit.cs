using UnityEngine;
using TMPro;

public class AsteroidHit : MonoBehaviour
{
    [Header("Configurações de Destruição")]
    [SerializeField] private GameObject asteroidExplosion;
    [SerializeField] private float explosionLifetime = 2f;
    [SerializeField] private float asteroidDestroyDelay = 0f;

    [Header("Referências Externas")]
    private GameController gameController;
    [SerializeField] private GameObject popupCanvas;

    [Header("Configurações de Tags")]
    [SerializeField] private string gravityTag = "Gravidade"; // Tag para identificar objetos de gravidade

    private void Awake()
    {
        // Tenta encontrar o GameController na cena
        gameController = FindAnyObjectByType<GameController>();
    }

    private void OnCollisionEnter(Collision collision)
    {
        // Verifica se o objeto que colidiu tem a tag "Gravidade"
        if (collision.gameObject.CompareTag(gravityTag))
        {
            Debug.Log($"Colidiu com objeto de gravidade: {collision.gameObject.name}");
            DestroyGravitationalObject();
        }
        else if (collision.gameObject.CompareTag("Laser"))
        {
            HandleAsteroidDestruction();
            Destroy(collision.gameObject); // Destroi o laser após a colisão
        }
    }

    public void HandleAsteroidDestruction()
    {
        CreateExplosion();

        int asteroidScore = CalculateScore();
        ShowScorePopup(asteroidScore);

        if (gameController != null)
        {
            gameController.UpdatePlayerScore(asteroidScore);
        }

        Destroy(gameObject, asteroidDestroyDelay);
    }

    private void DestroyGravitationalObject()
    {
        CreateExplosion();
        Destroy(gameObject); // Destroi o objeto imediatamente
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
        if (popupCanvas != null)
        {
            GameObject asteroidPopup = Instantiate(popupCanvas, transform.position, Quaternion.identity);
            TextMeshProUGUI scoreText = asteroidPopup.transform.GetChild(0).GetComponent<TextMeshProUGUI>();

            // Define o texto do popup como a pontuação calculada
            scoreText.text = score.ToString();

            // Ajusta a escala do popup com base na distância do centro
            float scaleMultiplier = Vector3.Distance(transform.position, Vector3.zero) / 10f;
            asteroidPopup.transform.localScale = transform.localScale * scaleMultiplier;
        }
    }
}
