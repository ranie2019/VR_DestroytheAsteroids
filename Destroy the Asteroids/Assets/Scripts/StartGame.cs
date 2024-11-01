using UnityEngine;

public class StartGame : MonoBehaviour
{
    [Header("Audio Player")]
    [Tooltip("Refer�ncia ao script AudioPlayer.")]
    [SerializeField] private AudioPlayer audioPlayer;

    [Header("Spawner a ser ativado")]
    [Tooltip("Refer�ncia ao script de Spawner que ser� ativado.")]
    [SerializeField] private AsteroidSpawner asteroidSpawnerScript;

    [Header("Particle System")]
    [Tooltip("Refer�ncia ao ParticleSystem que ser� ativado.")]
    [SerializeField] private ParticleSystem particleSystem;

    [Header("Game Controller")]
    [Tooltip("Refer�ncia ao script GameController.")]
    [SerializeField] private GameController gameController;

    [Header("Mesh Renderer")]
    [Tooltip("Refer�ncia ao MeshRenderer do objeto filho.")]
    [SerializeField] private MeshRenderer childMeshRenderer;

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Laser"))
        {
            HandleCollisionWithLaser();
        }
    }

    private void HandleCollisionWithLaser()
    {
        // Reseta a pontua��o do jogador
        if (gameController != null)
        {
            gameController.ResetScore();
        }

        // Ativa o Spawner
        if (asteroidSpawnerScript != null)
        {
            asteroidSpawnerScript.enabled = true;
        }

        // Troca o �udio de introdu��o para o �udio principal do jogo
        if (audioPlayer != null)
        {
            audioPlayer.PlayRandomMainGameAudio();
        }

        // Ativa o ParticleSystem
        if (particleSystem != null)
        {
            particleSystem.Play();
        }

        // Habilita o MeshRenderer do objeto filho
        if (childMeshRenderer != null)
        {
            childMeshRenderer.enabled = true;
        }

        // Desativa o objeto StartGame
        gameObject.SetActive(false);
    }
}
