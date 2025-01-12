using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class PlayerDano : MonoBehaviour
{
    [Header("Configuração de Vida")]
    [SerializeField] private int vidaInicial = 3; // Vida inicial do jogador.
    private int vidaAtual; // Vida atual do jogador.
    [SerializeField] private TextMeshProUGUI vidaTextMeshPro; // UI para exibir a vida do jogador.

    [Header("Game Over UI")]
    [SerializeField] private GameObject gameOverUI; // Referência ao painel de Game Over.

    [Header("Spawners")]
    [SerializeField] private MonoBehaviour[] asteroidSpawnerScripts; // Scripts responsáveis por spawnar asteroides.

    [Header("Objetos a Desabilitar")]
    [SerializeField] private GameObject[] objetosParaDesabilitar; // Objetos que serão desativados no Game Over.

    [Header("Áudio de Explosão")]
    [SerializeField] private AudioClip explosionClip; // Som de explosão ao perder todas as vidas.
    [SerializeField] private AudioSource audioSource; // Componente de áudio usado para tocar sons.

    [Header("Efeito de Dano")]
    [SerializeField] private Canvas danoCanvas; // Canvas usado para exibir o efeito de dano.
    [SerializeField] private float duracaoDanoImage = 0.5f; // Duração do efeito de dano.
    [SerializeField] private AudioClip danoClip; // Som tocado ao sofrer dano.

    [Header("Distância do Canvas de Dano")]
    [SerializeField] private float distanciaDanoCanvas = 2f; // Distância entre o canvas de dano e o jogador.

    private bool gameOverAtivo = false; // Indica se o jogo está no estado de Game Over.
    private Camera mainCamera; // Referência à câmera principal da cena.

    private void Start()
    {
        // Inicializa a vida do jogador com a vida inicial.
        vidaAtual = vidaInicial;

        // Atualiza a UI da vida no início do jogo.
        AtualizarUIVida();

        // Valida se todas as referências necessárias foram atribuídas.
        ValidateReferences();

        // Obtém a câmera principal.
        mainCamera = Camera.main;

        // Garante que o Canvas de dano comece desativado.
        if (danoCanvas != null)
        {
            danoCanvas.gameObject.SetActive(false);
        }
    }

    private void Update()
    {
        // Atualiza a posição e rotação do Canvas de dano para seguir o jogador.
        if (danoCanvas != null && mainCamera != null)
        {
            Vector3 playerPosition = transform.position;
            Vector3 cameraForward = mainCamera.transform.forward;

            // Posiciona o Canvas de dano à frente do jogador.
            danoCanvas.transform.position = playerPosition + cameraForward.normalized * distanciaDanoCanvas;

            // Faz o Canvas de dano olhar na direção da câmera.
            Vector3 directionToLook = (mainCamera.transform.position - danoCanvas.transform.position).normalized;
            Quaternion rotation = Quaternion.LookRotation(directionToLook);
            Vector3 eulerRotation = rotation.eulerAngles;
            eulerRotation.x = 0f; // Remove a inclinação no eixo X.
            danoCanvas.transform.rotation = Quaternion.Euler(eulerRotation);
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        // Detecta colisões com objetos marcados como "Inimigo".
        if (collision.gameObject.CompareTag("Inimigo") && !gameOverAtivo)
        {
            // Reduz a vida atual do jogador.
            vidaAtual--;

            // Atualiza a UI da vida.
            AtualizarUIVida();

            // Destrói o objeto inimigo que colidiu com o jogador.
            Destroy(collision.gameObject);

            // Exibe o efeito de dano e toca o som de dano.
            StartCoroutine(ExibirDanoCanvas());
            TocarSomDano();

            // Verifica se o jogador perdeu todas as vidas.
            if (vidaAtual <= 0)
            {
                HandleGameOver();
            }
        }
    }

    private void AtualizarUIVida()
    {
        // Atualiza o texto na UI para refletir a vida atual.
        if (vidaTextMeshPro != null)
        {
            vidaTextMeshPro.text = vidaAtual.ToString();
        }
        else
        {
            Debug.LogWarning("TextMeshProUGUI não atribuído!");
        }
    }

    private void HandleGameOver()
    {
        if (gameOverAtivo) return; // Evita múltiplas execuções do Game Over.

        gameOverAtivo = true; // Marca o jogo como finalizado.

        // Toca o som de explosão, se configurado.
        if (explosionClip != null && audioSource != null)
        {
            audioSource.PlayOneShot(explosionClip);
        }

        // Exibe a UI de Game Over.
        ShowGameOverUI();

        // Para os spawners de asteroides.
        FreezeSpawners();

        // Destrói todos os asteroides existentes na cena.
        DestroyAllAsteroids();

        // Desativa os objetos configurados.
        DesabilitarObjetos();

        Debug.Log("Game Over! O jogador perdeu todas as vidas.");
    }

    private void ShowGameOverUI()
    {
        // Ativa a UI de Game Over e todos os seus filhos.
        if (gameOverUI != null)
        {
            gameOverUI.SetActive(true);
            EnableAllChildren(gameOverUI);
        }
        else
        {
            Debug.LogWarning("GameOver UI não atribuído!");
        }
    }

    private void FreezeSpawners()
    {
        // Desativa todos os scripts de spawner de asteroides.
        foreach (var spawner in asteroidSpawnerScripts)
        {
            if (spawner != null && spawner.enabled)
            {
                spawner.enabled = false;
            }
        }
    }

    private void DestroyAllAsteroids()
    {
        // Encontra e destrói todos os objetos na cena com a tag "Asteroid".
        foreach (GameObject asteroid in GameObject.FindGameObjectsWithTag("Asteroid"))
        {
            Destroy(asteroid);
        }
    }

    private void DesabilitarObjetos()
    {
        // Desativa todos os objetos configurados na lista.
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
        // Ativa todos os filhos do objeto pai recursivamente.
        foreach (Transform child in parent.transform)
        {
            child.gameObject.SetActive(true);
            EnableAllChildren(child.gameObject);
        }
    }

    public void ResetarVida()
    {
        // Reseta a vida do jogador para o valor inicial e reinicia o jogo.
        vidaAtual = vidaInicial;
        gameOverAtivo = false;
        AtualizarUIVida();
    }

    private System.Collections.IEnumerator ExibirDanoCanvas()
    {
        // Exibe o Canvas de dano por um tempo definido.
        if (danoCanvas != null)
        {
            danoCanvas.gameObject.SetActive(true);
            yield return new WaitForSeconds(duracaoDanoImage);
            danoCanvas.gameObject.SetActive(false);
        }
    }

    private void TocarSomDano()
    {
        // Toca o som de dano, se configurado.
        if (danoClip != null && audioSource != null)
        {
            audioSource.PlayOneShot(danoClip);
        }
    }

    private void ValidateReferences()
    {
        // Valida se as referências importantes foram configuradas corretamente.
        if (asteroidSpawnerScripts.Length == 0)
            Debug.LogWarning("Nenhum spawner de asteroides atribuído.");

        if (audioSource == null)
            Debug.LogWarning("AudioSource não atribuído.");

        if (gameOverUI == null)
            Debug.LogWarning("GameOver UI não atribuído.");

        if (danoCanvas == null)
            Debug.LogWarning("Canvas de Dano não atribuído.");

        if (danoClip == null)
            Debug.LogWarning("Áudio de Dano não atribuído.");
    }
}
