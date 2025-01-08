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
    [SerializeField] private AsteroidSpawner[] asteroidSpawnerScripts;  // Referências aos spawners de asteroides que serão ativados

    [Header("Particle System")]
    [Tooltip("Referência ao ParticleSystem que será ativado.")]
    [SerializeField] private ParticleSystem particleSystem;  // Referência ao sistema de partículas (ex: explosão)

    [Header("Game Controller")]
    [Tooltip("Referência ao script GameController.")]
    [SerializeField] private GameController gameController;  // Referência ao script que controla a pontuação e o estado do jogo

    [Header("Objetos Inativos")]
    [Tooltip("Referências aos objetos inativos que devem começar a contagem.")]
    [SerializeField] private Inativo[] objetosInativos;  // Referências aos objetos que devem ser ativados ao reiniciar o jogo

    [Header("Mesh Renderer")]
    [Tooltip("Referência ao MeshRenderer do objeto filho.")]
    [SerializeField] private MeshRenderer childMeshRenderer;  // Referência ao MeshRenderer do objeto filho que deve ser ativado

    [Header("Armas")]
    [Tooltip("Referências aos objetos de armas que devem ser ativados.")]
    [SerializeField] private GameObject[] armas;  // Referências aos objetos de armas que devem ser ativados

    // Função chamada quando o script é iniciado
    private void Start()
    {
        ValidateReferences();  // Valida se todas as referências foram atribuídas corretamente
    }

    // Função chamada quando uma colisão ocorre
    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Laser"))
        {
            HandleCollisionWithLaser();  // Lida com a colisão com o laser
        }
    }

    // Lida com a colisão do jogador com o laser
    private void HandleCollisionWithLaser()
    {
        // Se a vida do jogador for menor que o valor inicial, reseta a vida
        if (playerDano != null && playerDano.VidaAtual < playerDano.VidaInicial)
        {
            playerDano.ResetarVida();  // Reseta a vida do jogador
        }

        // Reseta a pontuação do jogo
        if (gameController != null)
        {
            gameController.ResetScore();  // Reseta a pontuação do jogo
        }

        // Ativa todos os spawners de asteroides
        foreach (var spawner in asteroidSpawnerScripts)
        {
            if (spawner != null)
            {
                spawner.enabled = true;  // Ativa o spawner
            }
        }

        // Troca o áudio de introdução para o áudio principal do jogo
        if (audioPlayer != null)
        {
            audioPlayer.PlayRandomMainGameAudio();  // Toca o áudio principal
        }

        // Ativa o sistema de partículas (ex: explosão)
        if (particleSystem != null)
        {
            particleSystem.Play();  // Inicia o sistema de partículas
        }

        // Habilita o MeshRenderer do objeto filho
        if (childMeshRenderer != null)
        {
            childMeshRenderer.enabled = true;  // Torna o MeshRenderer visível
        }

        // Ativa as armas
        AtivarArmas();

        // Ativa os objetos inativos
        foreach (var objInativo in objetosInativos)
        {
            if (objInativo != null)
            {
                objInativo.NotificarInicioDoJogo();  // Notifica o objeto inativo para iniciar a contagem ou processo
                objInativo.enabled = true;  // Habilita o script do objeto inativo
            }
        }

        // Desativa o script GameOverStart após reiniciar o jogo
        gameObject.SetActive(false);
    }

    // Função para ativar as armas do jogador
    private void AtivarArmas()
    {
        foreach (var arma in armas)
        {
            if (arma != null)
            {
                arma.SetActive(true);  // Ativa o GameObject da arma
            }

            // Ativa o script Inativo da arma
            Inativo inativoScript = arma.GetComponent<Inativo>();
            if (inativoScript != null)
            {
                inativoScript.enabled = true;  // Habilita o script Inativo da arma
            }
        }
    }

    // Valida se todas as referências foram atribuídas no inspetor do Unity
    private void ValidateReferences()
    {
        if (playerDano == null)
            Debug.LogWarning("PlayerDano não está atribuído!");  // Aviso se a referência não foi atribuída

        if (gameController == null)
            Debug.LogWarning("GameController não está atribuído!");  // Aviso se a referência não foi atribuída

        if (audioPlayer == null)
            Debug.LogWarning("AudioPlayer não está atribuído!");  // Aviso se a referência não foi atribuída
    }
}
