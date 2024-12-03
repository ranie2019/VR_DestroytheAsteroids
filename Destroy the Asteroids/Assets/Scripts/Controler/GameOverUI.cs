using UnityEngine;

public class GameOverUI : MonoBehaviour
{
    [Header("Objeto Game Over")]
    [Tooltip("Canvas de Game Over que será ativado e reposicionado.")]
    [SerializeField] private GameObject gameOverCanvas;

    [Header("Posição do Canvas Game Over")]
    [Tooltip("Posição pública para reposicionar o canvas ao sofrer uma colisão.")]
    [SerializeField] private Vector3 canvasPosition;

    private bool isGameOver = false; // Para rastrear o estado do jogo

    private void Start()
    {
        // Garante que o Canvas comece desativado no início do jogo
        if (gameOverCanvas != null)
        {
            gameOverCanvas.SetActive(false); // Desativa o canvas no início
        }
        else
        {
            Debug.LogError("Game Over Canvas não atribuído no Inspector!");
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        // Verifica se houve colisão com um objeto específico (como "Asteroid")
        if (collision.gameObject.CompareTag("Asteroid"))
        {
            HandleGameOver();
        }
    }

    private void HandleGameOver()
    {
        if (!isGameOver && gameOverCanvas != null)
        {
            isGameOver = true;

            // Ativa e reposiciona o Canvas de Game Over
            gameOverCanvas.SetActive(true);
            gameOverCanvas.transform.position = canvasPosition;

            Debug.Log("Canvas Game Over ativado e reposicionado em: " + canvasPosition);
        }
    }

    /// <summary>
    /// Método para resetar o estado do UI de Game Over.
    /// </summary>
    public void ResetGameOverUI()
    {
        if (gameOverCanvas != null)
        {
            gameOverCanvas.SetActive(false); // Desativa o Canvas
        }

        isGameOver = false; // Reseta o estado de Game Over
        Debug.Log("Game Over UI resetado.");
    }
}
