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
            }
        }

        if (audioPlayer != null)
        {
            audioPlayer.PlayRandomMainGameAudio();
        }

        if (particleSystem != null)
        {
            particleSystem.Play();
        }

        if (childMeshRenderer != null)
        {
            childMeshRenderer.enabled = true;
        }

        if (objetosInativos != null && objetosInativos.Length > 0)
        {
            foreach (var objInativo in objetosInativos)
            {
                if (objInativo != null)
                {
                    objInativo.NotificarInicioDoJogo();
                }
            }
        }

        if (demoTimer != null)
        {
            demoTimer.StartTimer();
        }

        gameObject.SetActive(false);
    }
}
