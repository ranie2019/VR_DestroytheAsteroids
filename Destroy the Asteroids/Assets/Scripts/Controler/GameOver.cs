using UnityEngine;
using System.Collections;

public class GameOver : MonoBehaviour
{
    [Header("Spawners a serem congelados")]
    [Tooltip("Refer�ncia aos scripts de Spawner que ser�o desabilitados.")]
    [SerializeField] private AsteroidSpawner[] asteroidSpawnerScripts; // Array de Spawners

    [Header("Tag de Verifica��o")]
    [Tooltip("Tag do objeto que, ao colidir, causar� o congelamento dos spawners.")]
    [SerializeField] private string asteroidTag = "Asteroid";

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
        // Toca o �udio de Game Over e o �udio da explos�o da Terra
        if (audioPlayer != null)
        {
            audioPlayer.StopMainGameAudio(); // Se necess�rio, pare a m�sica principal antes de tocar o Game Over
            audioPlayer.PlayGameOverAudio(); // Toca o �udio de Game Over
        }

        // Desabilita todos os scripts de Asteroid Spawner
        foreach (var spawner in asteroidSpawnerScripts)
        {
            if (spawner != null && spawner.enabled)
            {
                spawner.enabled = false;
            }
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

    // M�todo para resetar o estado do Game Over
    public void ResetGameOverState()
    {
        // Reativa os Spawners de Asteroides
        foreach (var spawner in asteroidSpawnerScripts)
        {
            if (spawner != null && !spawner.enabled)
            {
                spawner.enabled = true;
            }
        }

        // Restaura o MeshRenderer do filho do objeto
        if (objectToDisableMeshRenderer != null)
        {
            MeshRenderer childMeshRenderer = objectToDisableMeshRenderer.GetComponentInChildren<MeshRenderer>();
            if (childMeshRenderer != null)
            {
                childMeshRenderer.enabled = true;
            }
        }

        // Reseta o estado do jogo conforme necess�rio
        // Caso haja alguma vari�vel que marque o estado do jogo, ela deve ser resetada aqui.
    }
}