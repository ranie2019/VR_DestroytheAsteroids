using UnityEngine;

public class GameOverUI : MonoBehaviour
{
    [Header("Objeto Game Over")]
    [SerializeField] private GameObject gameOverCanvas; // Canvas de Game Over

    [Header("Gerenciador de Asteroides")]
    [SerializeField] private AsteroidManager asteroidManager; // Gerenciador de asteroides

    private bool isGameOver = false; // Para rastrear o estado do jogo

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Asteroid") && !isGameOver)
        {
            HandleGameOver(collision);
        }
    }

    private void HandleGameOver(Collision collision)
    {
        isGameOver = true; // Marca o estado como Game Over

        // Ativa o canvas de Game Over
        if (gameOverCanvas != null)
        {
            gameOverCanvas.SetActive(true);
        }

        // Destroi todos os asteroides
        if (asteroidManager != null)
        {
            asteroidManager.DestroyAllAsteroids();
        }

        // Remove o asteroide que causou o Game Over
        Destroy(collision.gameObject);
    }

    public void ResetGameOverUI()
    {
        // Garante que o canvas seja desativado
        if (gameOverCanvas != null)
        {
            gameOverCanvas.SetActive(false);
        }

        // Reseta o estado interno
        isGameOver = false;
    }
}
