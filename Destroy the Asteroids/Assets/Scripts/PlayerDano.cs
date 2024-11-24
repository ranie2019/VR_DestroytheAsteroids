using UnityEngine;

public class PlayerDano : MonoBehaviour
{
    [Header("Spawners a serem congelados")]
    [Tooltip("Refer�ncias aos scripts de Spawner que ser�o desabilitados.")]
    [SerializeField] private AsteroidSpawner asteroidSpawner1;
    [SerializeField] private AsteroidSpawner asteroidSpawner2;
    [SerializeField] private AsteroidSpawner asteroidSpawner3;

    [Header("Tag de Verifica��o")]
    [Tooltip("Tag do objeto que, ao colidir, causar� o Game Over.")]
    [SerializeField] private string damagingObjectTag = "Asteroid";

    [Header("Objeto Game Over")]
    [Tooltip("Refer�ncia ao objeto que ser� habilitado ao ocorrer o Game Over.")]
    [SerializeField] private GameObject gameOverUI;

    [Header("Audio Player")]
    [Tooltip("Refer�ncia ao script AudioPlayer.")]
    [SerializeField] private AudioPlayer audioPlayer;

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag(damagingObjectTag))
        {
            HandleGameOver(collision);
        }
    }

    private void HandleGameOver(Collision collision)
    {
        // Desabilita os scripts dos Spawners, se as refer�ncias estiverem corretas
        if (asteroidSpawner1 != null && asteroidSpawner1.enabled)
        {
            asteroidSpawner1.enabled = false;
        }
        if (asteroidSpawner2 != null && asteroidSpawner2.enabled)
        {
            asteroidSpawner2.enabled = false;
        }
        if (asteroidSpawner3 != null && asteroidSpawner3.enabled)
        {
            asteroidSpawner3.enabled = false;
        }

        // Habilita o objeto de Game Over
        if (gameOverUI != null)
        {
            gameOverUI.SetActive(true);
        }

        // Destr�i todos os objetos com a tag "Asteroid"
        DestroyAllDamagingObjects();

        // Destr�i o objeto que causou o Game Over
        Destroy(collision.gameObject);

        // Troca o �udio do jogo para o �udio de Game Over
        if (audioPlayer != null)
        {
            audioPlayer.StopMainGameAudio();
            audioPlayer.PlayGameOverAudio();
        }
    }

    private void DestroyAllDamagingObjects()
    {
        foreach (GameObject damagingObject in GameObject.FindGameObjectsWithTag(damagingObjectTag))
        {
            Destroy(damagingObject);
        }
    }
}
