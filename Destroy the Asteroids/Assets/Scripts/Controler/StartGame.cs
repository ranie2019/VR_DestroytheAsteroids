using UnityEngine;

public class StartGame : MonoBehaviour
{
    [Header("Audio Player")]
    [Tooltip("Referência ao script AudioPlayer.")]
    [SerializeField] private AudioPlayer audioPlayer;

    [Header("Spawners a serem ativados")]
    [Tooltip("Referências aos scripts de Spawner que serão ativados.")]
    [SerializeField] private AsteroidSpawner[] asteroidSpawnerScripts; // Array para múltiplos spawners

    [Header("Particle System")]
    [Tooltip("Referência ao ParticleSystem que será ativado.")]
    [SerializeField] private ParticleSystem particleSystem;

    [Header("Game Controller")]
    [Tooltip("Referência ao script GameController.")]
    [SerializeField] private GameController gameController;

    [Header("Objetos Inativos")]
    [Tooltip("Referências aos objetos Inativos que devem começar a contagem.")]
    [SerializeField] private Inativo[] objetosInativos;

    [Header("Mesh Renderer")]
    [Tooltip("Referência ao MeshRenderer do objeto filho.")]
    [SerializeField] private MeshRenderer childMeshRenderer;  // Adicionada a referência aqui

    [Header("Referência ao Script GameOver")]
    [SerializeField] private GameOver gameOverScript;


    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Laser"))
        {
            HandleCollisionWithLaser();
        }
    }

    private void HandleCollisionWithLaser()
    {
        // Reseta a pontuação do jogador
        if (gameController != null)
        {
            gameController.ResetScore();
        }

        // Ativa todos os Spawners
        foreach (var spawner in asteroidSpawnerScripts)
        {
            if (spawner != null)
            {
                spawner.enabled = true;
            }
        }

        // Troca o áudio de introdução para o áudio principal do jogo
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

        // Notifica os objetos inativos para iniciar a contagem
        foreach (var objInativo in objetosInativos)
        {
            objInativo.NotificarInicioDoJogo();  // Chama NotificarInicioDoJogo para cada objeto Inativo
        }

        // Desativa o objeto StartGame
        gameObject.SetActive(false);
    }
}