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

    [Header("Objetos a Desabilitar")]
    [Tooltip("Lista de objetos que devem ser desativados no Game Over.")]
    [SerializeField] private GameObject[] objetosParaDesabilitar;

    [Header("Áudio de Explosão")]
    [Tooltip("Áudio que será tocado quando o jogador perder toda a vida.")]
    [SerializeField] private AudioClip explosionClip;

    [Tooltip("Referência ao AudioSource que tocará o áudio.")]
    [SerializeField] private AudioSource audioSource;

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

            // Atualiza a UI com a vida atual
            AtualizarUIVida();


            // Destroi o objeto que colidiu (inimigo)
            Destroy(collision.gameObject);

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
            vidaTextMeshPro.text = vidaAtual.ToString();
        }
    }

    private void HandleGameOver()
    {
        // Toca o áudio de explosão se configurado
        if (explosionClip != null && audioSource != null)
        {
            audioSource.PlayOneShot(explosionClip);
        }

        // Habilita o painel de Game Over e seus filhos
        ShowGameOverUI();

        // Congela todos os spawners
        FreezeSpawners();

        // Destrói todos os objetos com a tag "Asteroid"
        DestroyAllAsteroids();

        // Desabilita objetos específicos
        DesabilitarObjetos();
    }

    private void ShowGameOverUI()
    {
        if (gameOverUI != null)
        {
            gameOverUI.SetActive(true); // Ativa o painel principal
            EnableAllChildren(gameOverUI); // Ativa todos os filhos do painel
        }
        else
        {
            Debug.LogWarning("GameOver UI não está atribuído.");
        }
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

    private void EnableAllChildren(GameObject parent)
    {
        // Habilita recursivamente todos os filhos de um GameObject
        foreach (Transform child in parent.transform)
        {
            child.gameObject.SetActive(true);
            EnableAllChildren(child.gameObject);
        }
    }
}
