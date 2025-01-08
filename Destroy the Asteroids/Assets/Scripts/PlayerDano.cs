using UnityEngine;
using TMPro;

public class PlayerDano : MonoBehaviour
{
    [Header("Configura��o de Vida")]
    [Tooltip("Vida inicial do jogador. A quantidade de vidas que o jogador come�a com.")]
    [SerializeField] private int vidaInicial = 3;
    private int vidaAtual;

    // Propriedade p�blica para acessar a vida atual
    public int VidaAtual => vidaAtual;  // Agora, a vida atual � acess�vel de outros scripts
    public int VidaInicial => vidaInicial;

    [Header("UI de Vida")]
    [Tooltip("Refer�ncia para exibir a vida do jogador na interface usando TextMeshPro.")]
    [SerializeField] private TextMeshProUGUI vidaTextMeshPro;

    [Header("Game Over UI")]
    [Tooltip("Refer�ncia para o objeto de Game Over que ser� exibido quando o jogador perder toda a vida.")]
    [SerializeField] private GameObject gameOverUI;

    [Header("Spawners")]
    [Tooltip("Refer�ncia aos spawners de asteroides.")]
    [SerializeField] private MonoBehaviour[] asteroidSpawnerScripts;

    [Header("Objetos a Desabilitar")]
    [Tooltip("Lista de objetos que devem ser desativados no Game Over.")]
    [SerializeField] private GameObject[] objetosParaDesabilitar;

    [Header("�udio de Explos�o")]
    [Tooltip("�udio que ser� tocado quando o jogador perder toda a vida.")]
    [SerializeField] private AudioClip explosionClip;
    [Tooltip("Refer�ncia ao AudioSource que tocar� o �udio.")]
    [SerializeField] private AudioSource audioSource;

    [Header("Spawner de Armas")]
    [Tooltip("Lista de refer�ncias para os spawners de armas.")]
    [SerializeField] private MonoBehaviour[] weaponSpawnerScripts;

    private bool gameOverAtivo = false;

    private void Start()
    {
        vidaAtual = vidaInicial;
        AtualizarUIVida();

        // Valida se o AudioSource foi atribu�do corretamente
        if (audioSource == null)
        {
            Debug.LogWarning("AudioSource n�o est� atribu�do. O �udio de explos�o n�o ser� tocado.");
        }

        // Verifica se as refer�ncias essenciais foram atribu�das
        ValidateReferences();
    }

    private void OnCollisionEnter(Collision collision)
    {
        // Verifica colis�o com inimigos e se o game over ainda n�o foi ativado
        if (collision.gameObject.CompareTag("Inimigo") && !gameOverAtivo)
        {
            vidaAtual--;
            AtualizarUIVida();
            Destroy(collision.gameObject);

            // Se a vida atingir 0 ou menos, inicia o processo de game over
            if (vidaAtual <= 0)
            {
                HandleGameOver();
            }
        }
    }

    private void AtualizarUIVida()
    {
        if (vidaTextMeshPro != null)
        {
            vidaTextMeshPro.text = vidaAtual.ToString();
        }
        else
        {
            Debug.LogWarning("TextMeshProUGUI n�o atribu�do! A vida n�o ser� exibida corretamente.");
        }
    }

    private void HandleGameOver()
    {
        // Impede que o Game Over seja chamado mais de uma vez
        if (gameOverAtivo) return;

        gameOverAtivo = true;

        // Toca o �udio de explos�o se estiver configurado
        if (explosionClip != null && audioSource != null)
        {
            audioSource.PlayOneShot(explosionClip);
        }

        // Exibe a interface de Game Over
        ShowGameOverUI();

        // Congela todos os spawners de asteroides e armas
        FreezeSpawners();
        FreezeWeaponSpawners();

        // Destroi todos os asteroides e desabilita objetos
        DestroyAllAsteroids();
        DesabilitarObjetos();

        Debug.Log("Game Over! O jogador perdeu todas as vidas.");
    }

    private void ShowGameOverUI()
    {
        if (gameOverUI != null)
        {
            gameOverUI.SetActive(true);
            EnableAllChildren(gameOverUI);
        }
        else
        {
            Debug.LogWarning("GameOver UI n�o atribu�do!");
        }
    }

    private void FreezeSpawners()
    {
        foreach (var spawner in asteroidSpawnerScripts)
        {
            if (spawner != null && spawner.enabled)
            {
                spawner.enabled = false;
            }
        }
    }

    private void FreezeWeaponSpawners()
    {
        foreach (MonoBehaviour spawner in weaponSpawnerScripts)
        {
            if (spawner != null)
            {
                spawner.enabled = false;
                Debug.Log($"Spawner de Arma {spawner.name} desativado.");
            }
        }
    }

    private void DestroyAllAsteroids()
    {
        foreach (GameObject asteroid in GameObject.FindGameObjectsWithTag("Asteroid"))
        {
            Destroy(asteroid);
        }
    }

    private void DesabilitarObjetos()
    {
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
        foreach (Transform child in parent.transform)
        {
            child.gameObject.SetActive(true);
            EnableAllChildren(child.gameObject);
        }
    }

    /// <summary>
    /// Reinicia o jogo e redefine a vida.
    /// </summary>
    public void OnRestartButtonPressed()
    {
        RestartGame();
    }

    private void RestartGame()
    {
        Debug.Log("Reiniciando o jogo...");

        ResetarVida(); // Vida � redefinida aqui, no in�cio do novo jogo
        gameOverAtivo = false;
        AtualizarUIVida();

        // Ativa os spawners novamente
        foreach (MonoBehaviour spawner in asteroidSpawnerScripts)
        {
            if (spawner != null)
            {
                spawner.enabled = true;
            }
        }

        // Ativa os spawners de armas novamente
        foreach (MonoBehaviour spawner in weaponSpawnerScripts)
        {
            if (spawner != null)
            {
                spawner.enabled = true;
            }
        }

        // Desativa o UI de Game Over
        if (gameOverUI != null)
        {
            gameOverUI.SetActive(false);
        }

        // Habilita objetos que foram desabilitados
        foreach (GameObject obj in objetosParaDesabilitar)
        {
            if (obj != null)
            {
                obj.SetActive(true);
            }
        }
    }

    /// <summary>
    /// Redefine a vida para o valor inicial.
    /// </summary>
    public void ResetarVida()
    {
        // Se a vida atual for menor que a vida inicial, reseta para o valor inicial
        if (vidaAtual < vidaInicial)
        {
            vidaAtual = vidaInicial;
            Debug.Log("Vida redefinida para o valor inicial.");
        }
        else
        {
            Debug.Log("Vida j� est� no valor inicial.");
        }
        gameOverAtivo = false;
        AtualizarUIVida();
    }

    /// <summary>
    /// Valida as refer�ncias atribu�das no inspetor do Unity.
    /// </summary>
    private void ValidateReferences()
    {
        if (asteroidSpawnerScripts.Length == 0)
            Debug.LogWarning("Nenhum spawner de asteroides atribu�do.");

        if (weaponSpawnerScripts.Length == 0)
            Debug.LogWarning("Nenhum spawner de armas atribu�do.");

        if (audioSource == null)
            Debug.LogWarning("AudioSource n�o atribu�do.");

        if (gameOverUI == null)
            Debug.LogWarning("GameOver UI n�o atribu�do.");
    }
}
