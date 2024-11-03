using UnityEngine;

public class AsteroidSpawner : MonoBehaviour
{
    [Header("Tamanho da �rea de gera��o")]
    [SerializeField] private Vector3 spawnerSize;

    [Header("Taxa de gera��o (segundos)")]
    [SerializeField] private float spawnRate = 1.0f;

    [Header("Modelos de Asteroides")]
    [SerializeField] private GameObject[] asteroidModels;

    private float spawnTimer;

    private void Update()
    {
        // Atualiza o temporizador de spawn
        spawnTimer += Time.deltaTime;

        // Verifica se � hora de gerar um novo asteroide
        if (spawnTimer >= spawnRate)
        {
            SpawnAsteroid();
            spawnTimer = 0f; // Reinicia o temporizador
        }
    }

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

    private Vector3 GetRandomSpawnPosition()
    {
        // Ajusta a gera��o para o eixo X como a faixa principal
        return transform.position + new Vector3(
            Random.Range(-spawnerSize.x / 2, spawnerSize.x / 2), // Eixo X
            Random.Range(-spawnerSize.y / 2, spawnerSize.y / 2), // Eixo Y
            Random.Range(-spawnerSize.z / 2, spawnerSize.z / 2)  // Eixo Z
        );
    }

    private GameObject GetRandomAsteroidModel()
    {
        if (asteroidModels.Length == 0)
        {
            return null;
        }

        int randomIndex = Random.Range(0, asteroidModels.Length);
        return asteroidModels[randomIndex];
    }

    private void OnDrawGizmos()
    {
        // Desenha a �rea do spawner no editor para visualiza��o
        Gizmos.color = new Color(0, 1, 0, 0.5f);
        Gizmos.DrawCube(transform.position, spawnerSize);
    }
}
