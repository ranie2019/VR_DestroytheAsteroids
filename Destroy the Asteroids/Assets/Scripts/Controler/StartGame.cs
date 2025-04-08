using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;

public class StartGame : MonoBehaviour
{
    [Header("Audio Player")]
    [SerializeField] private AudioPlayer audioPlayer;

    [Header("Spawners a serem ativados")]
    [SerializeField] private AsteroidSpawner[] asteroidSpawnerScripts;

    [Header("Particle System")]
    [SerializeField] private ParticleSystem particleSystem;

    [Header("Game Controller")]
    [SerializeField] private PontoController pontoController;

    [Header("Objetos Inativos")]
    [SerializeField] private Inativo[] objetosInativos;

    [Header("Mesh Renderer")]
    [SerializeField] private MeshRenderer childMeshRenderer;

    [Header("Cronômetro")]
    [SerializeField] private DemoTimer demoTimer;

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Laser"))
        {
            Debug.Log("Colisão com laser detectada!");
            HandleCollisionWithLaser();
        }
    }

    private void HandleCollisionWithLaser()
    {
        if (asteroidSpawnerScripts != null && asteroidSpawnerScripts.Length > 0)
        {
            foreach (var spawner in asteroidSpawnerScripts)
            {
                if (spawner != null)
                {
                    spawner.enabled = true;
                }
                else
                {
                    Debug.LogWarning("Spawner está nulo!");
                }
            }
        }
        else
        {
            Debug.LogWarning("Nenhum Spawner atribuído!");
        }

        if (audioPlayer != null)
        {
            audioPlayer.PlayRandomMainGameAudio();
        }
        else
        {
            Debug.LogWarning("audioPlayer está nulo!");
        }

        if (particleSystem != null)
        {
            particleSystem.Play();
        }
        else
        {
            Debug.LogWarning("particleSystem está nulo!");
        }

        if (childMeshRenderer != null)
        {
            childMeshRenderer.enabled = true;
        }
        else
        {
            Debug.LogWarning("childMeshRenderer está nulo!");
        }

        if (objetosInativos != null && objetosInativos.Length > 0)
        {
            foreach (var objInativo in objetosInativos)
            {
                if (objInativo != null)
                {
                    objInativo.NotificarInicioDoJogo();
                }
                else
                {
                    Debug.LogWarning("objInativo está nulo!");
                }
            }
        }
        else
        {
            Debug.LogWarning("Nenhum objeto inativo atribuído!");
        }

        if (demoTimer != null)
        {
            demoTimer.StartTimer();
        }
        else
        {
            Debug.LogWarning("demoTimer está nulo!");
        }

        gameObject.SetActive(false);
    }
}
