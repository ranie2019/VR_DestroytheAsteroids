using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    private GameOverUI gameOverUI;

    private void Start()
    {
        gameOverUI = FindObjectOfType<GameOverUI>();
    }

    public void RestartGame()
    {
        // Reseta o UI de Game Over
        if (gameOverUI != null)
        {
            gameOverUI.ResetGameOverUI();
        }

        // Reinicia a cena atual
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }
}
