using UnityEngine;
using TMPro;

public class AsteroidHit : MonoBehaviour
{
    [Header("Configura��es de Destrui��o")]
    [SerializeField] private GameObject asteroidExplosion;
    [SerializeField] private float explosionLifetime = 2f;
    [SerializeField] private float asteroidDestroyDelay = 0f;

    [Header("Refer�ncias Externas")]
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
            Debug.LogError("GameController n�o encontrado!");
        }

        // Tenta localizar o Transform do jogador na cena, se n�o foi atribu�do
        if (playerTransform == null)
        {
            playerTransform = GameObject.FindWithTag("Player")?.transform;

            if (playerTransform == null)
            {
                Debug.LogError("Transform do jogador n�o foi encontrado!");
            }
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
        // Conte�do do m�todo permanece o mesmo
        CreateExplosion();

        int asteroidScore = CalculateScore();
        Debug.Log("Pontua��o calculada: " + asteroidScore);
        ShowScorePopup(asteroidScore);

        if (gameController != null)
        {
            gameController.UpdatePlayerScore(asteroidScore);
            Debug.Log("Pontua��o do jogador atualizada.");
        }
        else
        {
            Debug.LogError("GameController n�o foi encontrado na cena.");
        }

        Destroy(gameObject, asteroidDestroyDelay);
    }

    private void CreateExplosion()
    {
        // Instancia a explos�o do asteroide se o prefab foi atribu�do
        if (asteroidExplosion != null)
        {
            GameObject explosion = Instantiate(asteroidExplosion, transform.position, transform.rotation);
            Destroy(explosion, explosionLifetime); // Destroi a explos�o ap�s o tempo definido
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
        // Mostra o popup com a pontua��o se o prefab foi atribu�do
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
            Debug.LogError("Popup Canvas ou Transform do jogador est� faltando!");
        }
    }
}
