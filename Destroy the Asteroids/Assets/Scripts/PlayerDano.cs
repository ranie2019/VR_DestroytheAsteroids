using UnityEngine;
using TMPro; // Importa o namespace TextMeshPro

public class PlayerDano : MonoBehaviour
{
    [Header("Configuração de Vida")]
    [Tooltip("Vida inicial do jogador.")]
    [SerializeField] private int vidaInicial = 3;
    private int vidaAtual;

    [Header("UI de Vida")]
    [Tooltip("Referência para exibir a vida do jogador na interface usando TextMeshPro.")]
    [SerializeField] private TextMeshProUGUI vidaTextMeshPro;

    [Header("Game Over UI")]
    [Tooltip("Referência para o objeto de Game Over que será exibido quando o jogador perder toda a vida.")]
    [SerializeField] private GameObject gameOverUI;  // Referência ao painel de Game Over

    [Header("Spawners")]
    [Tooltip("Referência aos spawners que serão desativados no Game Over.")]
    [SerializeField] private GameObject[] spawners; // Referência aos spawners (como GameObjects)

    private void Start()
    {
        // Define a vida inicial
        vidaAtual = vidaInicial;
        AtualizarUIVida();
    }

    private void OnCollisionEnter(Collision collision)
    {
        // Verifica se o objeto com o qual colidimos tem a tag "Inimigo"
        if (collision.gameObject.CompareTag("Inimigo"))
        {
            // Reduz a vida ao sofrer uma colisão com um inimigo
            vidaAtual--;
            AtualizarUIVida();

            // Exibe uma mensagem de debug com a vida atual
            Debug.Log($" {vidaAtual}");

            // Verifica se a vida chegou a zero
            if (vidaAtual <= 0)
            {
                // Chama o método de Game Over
                HandleGameOver();
            }
        }
    }

    private void AtualizarUIVida()
    {
        // Atualiza a UI com a vida atual usando TextMeshPro
        if (vidaTextMeshPro != null)
        {
            vidaTextMeshPro.text = "" + vidaAtual;
        }
    }

    private void HandleGameOver()
    {
        // Habilita o painel de Game Over
        if (gameOverUI != null)
        {
            gameOverUI.SetActive(true);  // Mostra a tela de Game Over
        }

        // Congela todos os spawners
        FreezeSpawners();

        // Destrói todos os objetos com a tag "Asteroid"
        DestroyAllAsteroids();

        // Desabilita o jogador ou implementa outras ações de Game Over (como parar o movimento do jogador)
        gameObject.SetActive(false);  // Desabilita o jogador

        // Aqui você pode adicionar mais lógica de Game Over, como exibir pontuação, reiniciar o jogo, etc.
    }

    private void FreezeSpawners()
    {
        // Desabilita todos os spawners (fazendo com que parem de gerar inimigos)
        foreach (GameObject spawner in spawners)
        {
            if (spawner != null)
            {
                spawner.SetActive(false);  // Desativa o spawner
            }
        }
    }

    private void DestroyAllAsteroids()
    {
        // Encontra todos os objetos com a tag "Asteroid"
        GameObject[] asteroids = GameObject.FindGameObjectsWithTag("Asteroid");

        // Destroi cada asteroide encontrado
        foreach (GameObject asteroid in asteroids)
        {
            Destroy(asteroid);
        }
    }
}
