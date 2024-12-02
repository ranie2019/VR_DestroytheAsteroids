using UnityEngine;

public class GameOverUI : MonoBehaviour
{
    [Header("Objeto Game Over")]
    [Tooltip("Canvas de Game Over que ser� ativado.")]
    [SerializeField] private GameObject gameOverCanvas;

    [Header("Prefabs de Asteroides")]
    [Tooltip("Lista de prefabs de asteroides que ser�o instanciados.")]
    [SerializeField] private GameObject[] asteroidPrefabs;

    [Header("Gerenciador de Asteroides")]
    [Tooltip("Gerenciador de asteroides para gerenciar as posi��es iniciais.")]
    [SerializeField] private AsteroidManager asteroidManager;

    private bool isGameOver = false; // Para rastrear o estado do jogo

    private void Start()
    {
        // Valida os objetos configurados no Inspector
        if (gameOverCanvas == null)
        {
            Debug.LogError("Game Over Canvas n�o foi atribu�do!");
        }
        if (asteroidPrefabs == null || asteroidPrefabs.Length == 0)
        {
            Debug.LogError("Nenhum prefab de asteroide foi atribu�do!");
        }
        if (asteroidManager == null)
        {
            Debug.LogError("AsteroidManager n�o foi atribu�do!");
        }
    }

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

        // Destroi todos os asteroides na cena
        if (asteroidManager != null)
        {
            asteroidManager.DestroyAllAsteroids();
        }

        // Remove o asteroide que causou o Game Over
        Destroy(collision.gameObject);

        // Instancia novos asteroides a partir dos prefabs
        SpawnAsteroids();
    }

    private void SpawnAsteroids()
    {
        if (asteroidPrefabs != null && asteroidPrefabs.Length > 0 && asteroidManager != null)
        {
            foreach (Vector3 position in asteroidManager.GetSpawnPositions())
            {
                // Seleciona aleatoriamente um prefab da lista
                GameObject selectedPrefab = asteroidPrefabs[Random.Range(0, asteroidPrefabs.Length)];

                // Instancia o prefab selecionado
                Instantiate(selectedPrefab, position, Quaternion.identity);
            }
        }
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
