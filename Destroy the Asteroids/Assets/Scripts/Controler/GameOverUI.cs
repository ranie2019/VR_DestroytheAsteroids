using UnityEngine;

public class GameOverUI : MonoBehaviour
{
    [Header("Objeto Game Over")]
    [Tooltip("Canvas de Game Over que ser� ativado e reposicionado.")]
    [SerializeField] private GameObject gameOverCanvas;

    [Header("Posi��o do Canvas Game Over")]
    [Tooltip("Posi��o p�blica para reposicionar o canvas ao sofrer uma colis�o.")]
    [SerializeField] private Vector3 canvasPosition;

    private bool isGameOver = false; // Para rastrear o estado do jogo

    private void Start()
    {
        // Garante que o Canvas comece desativado no in�cio do jogo
        if (gameOverCanvas != null)
        {
            gameOverCanvas.SetActive(false); // Desativa o canvas no in�cio
        }
        else
        {
            Debug.LogError("Game Over Canvas n�o atribu�do no Inspector!");
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        // Verifica se houve colis�o com um objeto espec�fico (como "Asteroid")
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
    /// M�todo para resetar o estado do UI de Game Over.
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
