using UnityEngine;

public class GameOverUI : MonoBehaviour
{
    [Header("Objeto Game Over")]
    [Tooltip("Referência ao objeto que será habilitado ao ocorrer o Game Over.")]
    [SerializeField] private GameObject gameOverUI;

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Asteroid"))
        {
            HandleGameOver(collision);
        }
    }

    private void HandleGameOver(Collision collision)
    {
        // Habilita o objeto de Game Over
        if (gameOverUI != null)
        {
            gameOverUI.SetActive(true);
        }

        // Destrói todos os objetos com a tag "Asteroid"
        DestroyAllAsteroids();

        // Destrói o objeto que causou o Game Over
        Destroy(collision.gameObject);
    }

    private void DestroyAllAsteroids()
    {
        foreach (GameObject asteroid in GameObject.FindGameObjectsWithTag("Asteroid"))
        {
            Destroy(asteroid);
        }
    }

    // Método para resetar o estado do Game Over
    public void ResetGameOverUI()
    {
        // Desabilita a UI de Game Over
        if (gameOverUI != null)
        {
            gameOverUI.SetActive(false);
        }

        // Reseta o estado de Game Over
        // Caso haja alguma variável que marque o estado do jogo, ela deve ser resetada aqui.
    }
}