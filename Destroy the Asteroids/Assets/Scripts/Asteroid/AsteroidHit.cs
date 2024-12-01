using UnityEngine;

public class AsteroidHit : MonoBehaviour
{
    [Header("Game Controller")]
    [Tooltip("Referência ao GameController para atualizar a pontuação.")]
    [SerializeField] private GameController gameController;

    [Header("Pontuação")]
    [Tooltip("Pontuação atribuída ao destruir este asteroide.")]
    [SerializeField] private int scoreValue = 10;

    // Tornar o método público para acesso de outros scripts
    public void HandleAsteroidDestruction()
    {
        // Atualiza a pontuação do jogador
        if (gameController != null)
        {
            gameController.UpdatePlayerScore(scoreValue);
        }

        // Destroi o asteroide
        Destroy(gameObject);
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Laser"))
        {
            HandleAsteroidDestruction();
        }
    }
}
