using UnityEngine;
using TMPro; // Importa o namespace TextMeshPro

public class PlayerDano : MonoBehaviour
{
    [Header("Configura��o de Vida")]
    [Tooltip("Vida inicial do jogador.")]
    [SerializeField] private int vidaInicial = 3;
    private int vidaAtual;

    [Header("UI de Vida")]
    [Tooltip("Refer�ncia para exibir a vida do jogador na interface usando TextMeshPro.")]
    [SerializeField] private TextMeshProUGUI vidaTextMeshPro;

    [Header("Game Over UI")]
    [Tooltip("Refer�ncia para o objeto de Game Over que ser� exibido quando o jogador perder toda a vida.")]
    [SerializeField] private GameObject gameOverUI;  // Refer�ncia ao painel de Game Over

    [Header("Spawners")]
    [Tooltip("Refer�ncia aos spawners que ser�o desativados no Game Over.")]
    [SerializeField] private GameObject[] spawners; // Refer�ncia aos spawners (como GameObjects)

    [Header("Objetos a Desabilitar")]
    [Tooltip("Lista de objetos que devem ser desativados no Game Over.")]
    [SerializeField] private GameObject[] objetosParaDesabilitar;

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
            // Reduz a vida ao sofrer uma colis�o com um inimigo
            vidaAtual--;
            AtualizarUIVida();

            // Exibe uma mensagem de debug com a vida atual
            Debug.Log($"Vida Atual: {vidaAtual}");

            // Verifica se a vida chegou a zero
            if (vidaAtual <= 0)
            {
                // Chama o m�todo de Game Over
                HandleGameOver();
            }
        }
    }

    private void AtualizarUIVida()
    {
        // Atualiza a UI com a vida atual usando TextMeshPro
        if (vidaTextMeshPro != null)
        {
            vidaTextMeshPro.text = vidaAtual.ToString();
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

        // Destr�i todos os objetos com a tag "Asteroid"
        DestroyAllAsteroids();

        // Desabilita objetos espec�ficos
        DesabilitarObjetos();

        // Desabilita o jogador ou implementa outras a��es de Game Over (como parar o movimento do jogador)
        gameObject.SetActive(false);  // Desabilita o jogador
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

    private void DesabilitarObjetos()
    {
        // Desativa todos os objetos na lista "objetosParaDesabilitar"
        foreach (GameObject obj in objetosParaDesabilitar)
        {
            if (obj != null)
            {
                obj.SetActive(false);
            }
        }
    }
}
