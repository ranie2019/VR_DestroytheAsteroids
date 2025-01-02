using UnityEngine;

public class AsteroidSpawner : MonoBehaviour
{
    [Header("Tamanho da �rea de gera��o")]
    [Tooltip("Define a �rea em que os asteroides podem ser gerados.")]
    [SerializeField] private Vector3 spawnerSize;

    [Header("Taxa de gera��o (segundos)")]
    [Tooltip("Tempo inicial entre a gera��o de asteroides.")]
    [SerializeField] private float spawnRate = 1.0f; // Taxa inicial de gera��o
    [Tooltip("Taxa m�nima de gera��o para evitar spawn muito r�pido.")]
    [SerializeField] private float minimumSpawnRate = 0.1f; // Valor m�nimo do spawnRate

    [Header("Modelos de Asteroides")]
    [Tooltip("Array de modelos de asteroides que podem ser instanciados.")]
    [SerializeField] private GameObject[] asteroidModels;

    private float originalSpawnRate; // Armazena o valor original de spawnRate
    private float spawnTimer;        // Temporizador para controlar o spawn
    private float rateReductionTimer; // Temporizador para reduzir a taxa de spawn

    private void Awake()
    {
        // Armazena o valor original de spawnRate e inicializa a taxa de gera��o
        originalSpawnRate = spawnRate;
    }

    private void OnEnable()
    {
        // Reinicia o spawnRate para o valor original sempre que o script � reiniciado
        spawnRate = originalSpawnRate;
    }

    private void Update()
    {
        // Atualiza o temporizador de spawn
        spawnTimer += Time.deltaTime;

        // Verifica se � hora de gerar um novo asteroide
        if (spawnTimer >= spawnRate)
        {
            SpawnAsteroid();
            spawnTimer = 0f; // Reinicia o temporizador de spawn
        }

        // Atualiza o temporizador para reduzir o spawnRate
        rateReductionTimer += Time.deltaTime;
        if (rateReductionTimer >= 1f) // Reduz a cada 1 segundo
        {
            ReduceSpawnRate();
            rateReductionTimer = 0f; // Reinicia o temporizador de redu��o
        }
    }

    /// <summary>
    /// Instancia um asteroide em uma posi��o aleat�ria dentro da �rea do spawner.
    /// </summary>
    private void SpawnAsteroid()
    {
        // Gera uma posi��o aleat�ria dentro da �rea do spawner
        Vector3 spawnPosition = GetRandomSpawnPosition();

        // Seleciona aleatoriamente um dos modelos de asteroide
        GameObject selectedAsteroid = GetRandomAsteroidModel();

        // Instancia o asteroide selecionado na posi��o gerada com rota��o padr�o
        if (selectedAsteroid != null)
        {
            Instantiate(selectedAsteroid, spawnPosition, Quaternion.identity);
        }
    }

    /// <summary>
    /// Retorna uma posi��o aleat�ria dentro da �rea do spawner.
    /// </summary>
    /// <returns>Posi��o 3D dentro da �rea do spawner.</returns>
    private Vector3 GetRandomSpawnPosition()
    {
        return transform.position + new Vector3(
            Random.Range(-spawnerSize.x / 2, spawnerSize.x / 2), // Eixo X
            Random.Range(-spawnerSize.y / 2, spawnerSize.y / 2), // Eixo Y
            Random.Range(-spawnerSize.z / 2, spawnerSize.z / 2)  // Eixo Z
        );
    }

    /// <summary>
    /// Seleciona aleatoriamente um modelo de asteroide do array.
    /// </summary>
    /// <returns>GameObject do asteroide selecionado.</returns>
    private GameObject GetRandomAsteroidModel()
    {
        if (asteroidModels.Length == 0)
        {
            return null;
        }

        int randomIndex = Random.Range(0, asteroidModels.Length);
        return asteroidModels[randomIndex];
    }

    /// <summary>
    /// Reduz a taxa de gera��o dos asteroides, respeitando o valor m�nimo configurado.
    /// </summary>
    private void ReduceSpawnRate()
    {
        // Reduz o spawnRate, mas garante que ele n�o fique menor que o valor m�nimo
        spawnRate = Mathf.Max(spawnRate - 0.01f, minimumSpawnRate);
    }

    /// <summary>
    /// Reseta o estado do AsteroidSpawner.
    /// </summary>
    public void ResetSpawner()
    {
        // Reinicia os temporizadores
        spawnTimer = 0f;
        rateReductionTimer = 0f;

        // Restaura a taxa de spawn original
        spawnRate = originalSpawnRate;

        // (Opcional) Destruir todos os asteroides existentes ou realizar outras a��es de reset
        foreach (Transform child in transform)
        {
            Destroy(child.gameObject); // Destruir todos os asteroides filhos
        }

        // Voc� pode adicionar outras a��es de reset aqui, se necess�rio
    }

    private void OnDrawGizmos()
    {
        // Desenha a �rea do spawner no editor para visualiza��o
        Gizmos.color = new Color(0, 1, 0, 0.5f);
        Gizmos.DrawCube(transform.position, spawnerSize);
    }
}
