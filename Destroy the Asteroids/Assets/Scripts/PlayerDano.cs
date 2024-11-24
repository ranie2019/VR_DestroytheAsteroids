using UnityEngine;

public class PlayerDano : MonoBehaviour
{
    [Header("Spawners a serem congelados")]
    [Tooltip("Referências aos scripts de Spawner que serão desabilitados.")]
    [SerializeField] private AsteroidSpawner asteroidSpawner1;
    [SerializeField] private AsteroidSpawner asteroidSpawner2;
    [SerializeField] private AsteroidSpawner asteroidSpawner3;

    [Header("Tag de Verificação")]
    [Tooltip("Tag do objeto que, ao colidir, causará o Game Over.")]
    [SerializeField] private string damagingObjectTag = "Asteroid";

    [Header("Objeto Game Over")]
    [Tooltip("Referência ao objeto que será habilitado ao ocorrer o Game Over.")]
    [SerializeField] private GameObject gameOverUI;

    [Header("Audio Player")]
    [Tooltip("Referência ao script AudioPlayer.")]
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
        // Desabilita os scripts dos Spawners, se as referências estiverem corretas
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

        // Destrói todos os objetos com a tag "Asteroid"
        DestroyAllDamagingObjects();

        // Destrói o objeto que causou o Game Over
        Destroy(collision.gameObject);

        // Troca o áudio do jogo para o áudio de Game Over
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
