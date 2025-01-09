using UnityEngine;

public class GameOverStart : MonoBehaviour
{
    [Header("Player Dano")]
    [Tooltip("Referência ao script PlayerDano.")]
    [SerializeField] private PlayerDano playerDano;  // Referência ao script que controla a vida do jogador

    [Header("Audio Player")]
    [Tooltip("Referência ao script AudioPlayer.")]
    [SerializeField] private AudioPlayer audioPlayer;  // Referência ao script responsável pelo áudio

    [Header("Spawners a serem ativados")]
    [Tooltip("Referências aos scripts de Spawner que serão ativados.")]
    [SerializeField] private AsteroidSpawner[] asteroidSpawnerScripts;  // Referências aos spawners de asteroides

    [Header("Particle System")]
    [Tooltip("Referência ao ParticleSystem que será ativado.")]
    [SerializeField] private ParticleSystem particleSystem;  // Sistema de partículas

    [Header("Game Controller")]
    [Tooltip("Referência ao script GameController.")]
    [SerializeField] private GameController gameController;  // Script que controla o jogo

    [Header("Objetos Inativos")]
    [Tooltip("Referências aos objetos inativos que devem começar a contagem.")]
    [SerializeField] private Inativo[] objetosInativos;  // Objetos a serem ativados

    [Header("Mesh Renderer")]
    [Tooltip("Referência ao MeshRenderer do objeto filho.")]
    [SerializeField] private MeshRenderer childMeshRenderer;  // Renderização do objeto filho

    [Header("Armas")]
    [Tooltip("Referências aos objetos de armas que devem ser ativados.")]
    [SerializeField] private GameObject[] armas;  // Objetos de armas

    private void Start()
    {
        ValidateReferences();  // Valida referências
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Laser"))
        {
            HandleCollisionWithLaser();
        }
    }

    private void HandleCollisionWithLaser()
    {
        // Reseta a vida do jogador
        if (playerDano != null)
        {
            playerDano.ResetarVida(); // Método ResetarVida chamado corretamente
        }
        else
        {
            Debug.LogError("PlayerDano não foi atribuído corretamente no GameOverStart!");
        }

        // Reseta a pontuação
        if (gameController != null)
        {
            gameController.ResetScore();
        }

        // Ativa Spawners de Asteroides
        foreach (var spawner in asteroidSpawnerScripts)
        {
            if (spawner != null)
            {
                spawner.enabled = true;
            }
        }

        // Toca o áudio principal
        if (audioPlayer != null)
        {
            audioPlayer.PlayRandomMainGameAudio();
        }

        // Ativa sistema de partículas
        if (particleSystem != null)
        {
            particleSystem.Play();
        }

        // Habilita MeshRenderer do objeto filho
        if (childMeshRenderer != null)
        {
            childMeshRenderer.enabled = true;
        }

        // Ativa armas
        AtivarArmas();

        // Ativa objetos inativos
        foreach (var objInativo in objetosInativos)
        {
            if (objInativo != null)
            {
                objInativo.NotificarInicioDoJogo();
                objInativo.enabled = true;
            }
        }

        // Desativa este script após reiniciar o jogo
        gameObject.SetActive(false);
    }

    private void AtivarArmas()
    {
        foreach (var arma in armas)
        {
            if (arma != null)
            {
                arma.SetActive(true);
            }

            Inativo inativoScript = arma.GetComponent<Inativo>();
            if (inativoScript != null)
            {
                inativoScript.enabled = true;
            }
        }
    }

    private void ValidateReferences()
    {
        if (playerDano == null)
            Debug.LogWarning("PlayerDano não está atribuído!");

        if (gameController == null)
            Debug.LogWarning("GameController não está atribuído!");

        if (audioPlayer == null)
            Debug.LogWarning("AudioPlayer não está atribuído!");

        if (asteroidSpawnerScripts.Length == 0)
            Debug.LogWarning("Nenhum AsteroidSpawner foi atribuído!");

        if (objetosInativos.Length == 0)
            Debug.LogWarning("Nenhum objeto inativo foi atribuído!");

        if (armas.Length == 0)
            Debug.LogWarning("Nenhuma arma foi atribuída!");
    }
}
