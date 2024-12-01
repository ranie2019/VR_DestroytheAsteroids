using UnityEngine;

public class AsteroidHit : MonoBehaviour
{
    [Header("Game Controller")]
    [Tooltip("Refer�ncia ao GameController para atualizar a pontua��o.")]
    [SerializeField] private GameController gameController;

    [Header("Pontua��o")]
    [Tooltip("Pontua��o atribu�da ao destruir este asteroide.")]
    [SerializeField] private int scoreValue = 10;

    // Tornar o m�todo p�blico para acesso de outros scripts
    public void HandleAsteroidDestruction()
    {
        // Atualiza a pontua��o do jogador
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
