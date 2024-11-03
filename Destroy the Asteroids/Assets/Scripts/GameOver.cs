using UnityEngine;

public class GameOver : MonoBehaviour
{
    [Header("Spawner a ser congelado")]
    [Tooltip("Refer�ncia ao script de Spawner que ser� desabilitado.")]
    [SerializeField] private AsteroidSpawner asteroidSpawnerScript;

    [Header("Tag de Verifica��o")]
    [Tooltip("Tag do objeto que, ao colidir, causar� o congelamento do spawner.")]
    [SerializeField] private string asteroidTag = "Asteroid";

    [Header("Objeto Game Over")]
    [Tooltip("Refer�ncia ao objeto que ser� habilitado ao ocorrer o Game Over.")]
    [SerializeField] private GameObject gameOverUI;

    [Header("Audio Player")]
    [Tooltip("Refer�ncia ao script AudioPlayer.")]
    [SerializeField] private AudioPlayer audioPlayer;

    [Header("Particle Systems Prefabs")]
    [Tooltip("Prefabs dos Particle Systems que ser�o ativados no Game Over.")]
    [SerializeField] private ParticleSystem particleSystemPrefab1;
    [SerializeField] private ParticleSystem particleSystemPrefab2;
    [SerializeField] private ParticleSystem particleSystemPrefab3;

    [Header("Objeto a ser alterado")]
    [Tooltip("Objeto que ter� o MeshRenderer do filho desativado manualmente ao ocorrer o Game Over.")]
    [SerializeField] private GameObject objectToDisableMeshRenderer;

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag(asteroidTag))
        {
            HandleGameOver(collision);
        }
    }

    private void HandleGameOver(Collision collision)
    {
        // Desabilita o script Asteroid Spawner se a refer�ncia estiver correta
        if (asteroidSpawnerScript != null && asteroidSpawnerScript.enabled)
        {
            asteroidSpawnerScript.enabled = false;
        }

        // Habilita o objeto de Game Over
        if (gameOverUI != null)
        {
            gameOverUI.SetActive(true);
        }

        // Destr�i todos os objetos com a tag "Asteroid"
        DestroyAllAsteroids();

        // Posiciona e ativa os Particle Systems clonados
        ActivateParticleSystems();

        // Desativa o MeshRenderer do filho do objeto manualmente escolhido
        if (objectToDisableMeshRenderer != null)
        {
            MeshRenderer childMeshRenderer = objectToDisableMeshRenderer.GetComponentInChildren<MeshRenderer>();
            if (childMeshRenderer != null)
            {
                childMeshRenderer.enabled = false;
            }
        }

        // Destr�i o objeto que causou o Game Over
        Destroy(collision.gameObject);

        // Troca o �udio do jogo para o �udio de Game Over
        if (audioPlayer != null)
        {
            audioPlayer.StopMainGameAudio();
            audioPlayer.PlayGameOverAudio();
        }
    }

    private void DestroyAllAsteroids()
    {
        foreach (GameObject asteroid in GameObject.FindGameObjectsWithTag(asteroidTag))
        {
            Destroy(asteroid);
        }
    }

    private void ActivateParticleSystems()
    {
        if (objectToDisableMeshRenderer != null)
        {
            Vector3 objectPosition = objectToDisableMeshRenderer.transform.position;
            Quaternion objectRotation = objectToDisableMeshRenderer.transform.rotation;

            // Instancia e ativa os Particle Systems na posi��o e rota��o do objeto a ser alterado
            if (particleSystemPrefab1 != null)
            {
                ParticleSystem particleClone1 = Instantiate(particleSystemPrefab1, objectPosition, objectRotation);
                particleClone1.Play();
                Destroy(particleClone1.gameObject, particleClone1.main.duration); // Destroi o clone ap�s a dura��o das part�culas
            }
            if (particleSystemPrefab2 != null)
            {
                ParticleSystem particleClone2 = Instantiate(particleSystemPrefab2, objectPosition, objectRotation);
                particleClone2.Play();
                Destroy(particleClone2.gameObject, particleClone2.main.duration); // Destroi o clone ap�s a dura��o das part�culas
            }
            if (particleSystemPrefab3 != null)
            {
                ParticleSystem particleClone3 = Instantiate(particleSystemPrefab3, objectPosition, objectRotation);
                particleClone3.Play();
                Destroy(particleClone3.gameObject, particleClone3.main.duration); // Destroi o clone ap�s a dura��o das part�culas
            }
        }
    }
}
