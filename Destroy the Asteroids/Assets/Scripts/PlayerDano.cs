using UnityEngine;
using TMPro; // Importa o namespace TextMeshPro
using System.Collections.Generic;

public class PlayerDano : MonoBehaviour
{
    [Header("Configura��o de Vida")]
    [Tooltip("Vida inicial do jogador. A quantidade de vidas que o jogador come�a com.")]
    [SerializeField] private int vidaInicial = 3; // Defini��o da quantidade de vidas iniciais do jogador
    private int vidaAtual; // Vari�vel que armazena a quantidade atual de vida do jogador

    [Header("UI de Vida")]
    [Tooltip("Refer�ncia para exibir a vida do jogador na interface usando TextMeshPro.")]
    [SerializeField] private TextMeshProUGUI vidaTextMeshPro; // Refer�ncia ao componente TextMeshProUGUI para exibir a vida na UI

    [Header("Game Over UI")]
    [Tooltip("Refer�ncia para o objeto de Game Over que ser� exibido quando o jogador perder toda a vida.")]
    [SerializeField] private GameObject gameOverUI; // Objeto que representa a tela de Game Over

    [Header("Spawners")]
    [Tooltip("Refer�ncia aos spawners de asteroides.")]
    [SerializeField] private MonoBehaviour[] asteroidSpawnerScripts; // Scripts dos spawners de asteroides espec�ficos

    [Header("Objetos a Desabilitar")]
    [Tooltip("Lista de objetos que devem ser desativados no Game Over.")]
    [SerializeField] private GameObject[] objetosParaDesabilitar; // Lista de objetos que devem ser desativados durante o Game Over

    [Header("�udio de Explos�o")]
    [Tooltip("�udio que ser� tocado quando o jogador perder toda a vida.")]
    [SerializeField] private AudioClip explosionClip; // �udio de explos�o que ser� tocado no Game Over
    [Tooltip("Refer�ncia ao AudioSource que tocar� o �udio.")]
    [SerializeField] private AudioSource audioSource; // Refer�ncia ao AudioSource para tocar o �udio

    [Header("Spawner de Armas")]
    [Tooltip("Lista de refer�ncias para os spawners de armas.")]
    [SerializeField] private MonoBehaviour[] weaponSpawnerScripts; // Scripts dos spawners de armas a serem desativados no Game Over

    private bool gameOverAtivo = false; // Vari�vel para verificar se o Game Over j� foi ativado

    /// <summary>
    /// M�todo chamado ao iniciar o jogo.
    /// Inicializa a quantidade de vida e configura a UI de vida.
    /// </summary>
    private void Start()
    {
        vidaAtual = vidaInicial; // Inicializa a vida atual com a vida inicial
        AtualizarUIVida(); // Atualiza a exibi��o da vida na UI

        // Verifica se o AudioSource est� atribu�do, caso contr�rio exibe um aviso.
        if (audioSource == null)
        {
            Debug.LogWarning("AudioSource n�o est� atribu�do. O �udio de explos�o n�o ser� tocado.");
        }
    }

    /// <summary>
    /// M�todo chamado ao ocorrer uma colis�o com outro objeto.
    /// Diminui a vida do jogador ao colidir com um inimigo e checa se o Game Over deve ser acionado.
    /// </summary>
    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Inimigo") && !gameOverAtivo) // Verifica se o objeto colidido � um inimigo e se o Game Over n�o foi ativado
        {
            vidaAtual--; // Diminui a vida do jogador
            AtualizarUIVida(); // Atualiza a UI de vida ap�s o dano

            Destroy(collision.gameObject); // Destr�i o inimigo que causou o dano

            // Verifica se o jogador perdeu todas as vidas e aciona o Game Over
            if (vidaAtual <= 0)
            {
                HandleGameOver();
            }
        }
    }

    /// <summary>
    /// Atualiza a exibi��o da vida na UI.
    /// Exibe a quantidade atual de vida na tela usando TextMeshPro.
    /// </summary>
    private void AtualizarUIVida()
    {
        if (vidaTextMeshPro != null)
        {
            vidaTextMeshPro.text = vidaAtual.ToString(); // Atualiza o texto da UI com a quantidade de vida atual
        }
        else
        {
            Debug.LogWarning("TextMeshProUGUI n�o atribu�do! A vida n�o ser� exibida corretamente.");
        }
    }

    /// <summary>
    /// Trata o Game Over quando o jogador perde todas as vidas.
    /// Aciona o �udio, desativa spawners, destr�i asteroides e desabilita objetos relacionados ao Game Over.
    /// </summary>
    private void HandleGameOver()
    {
        gameOverAtivo = true; // Marca que o Game Over foi ativado

        // Toca o �udio de explos�o, se o �udio e o AudioSource estiverem configurados
        if (explosionClip != null && audioSource != null)
        {
            audioSource.PlayOneShot(explosionClip);
        }

        ShowGameOverUI(); // Exibe a UI de Game Over
        FreezeSpawners(); // Desativa os spawners de asteroides
        FreezeWeaponSpawners(); // Desativa os spawners de armas
        DestroyAllAsteroids(); // Destroi todos os asteroides presentes na cena (Nova l�gica adicionada)
        DesabilitarObjetos(); // Desativa os objetos que devem ser desabilitados no Game Over

        Debug.Log("Game Over! O jogador perdeu todas as vidas.");
    }

    /// <summary>
    /// Exibe a interface de Game Over na tela.
    /// Ativa o objeto de UI do Game Over e seus filhos.
    /// </summary>
    private void ShowGameOverUI()
    {
        if (gameOverUI != null)
        {
            gameOverUI.SetActive(true); // Torna o painel de Game Over vis�vel
            EnableAllChildren(gameOverUI); // Ativa todos os filhos do painel de Game Over
        }
        else
        {
            Debug.LogWarning("GameOver UI n�o atribu�do!"); // Exibe um aviso caso o painel de Game Over n�o esteja atribu�do
        }
    }

    /// <summary>
    /// Congela (desativa) todos os spawners de asteroides.
    /// Esse m�todo verifica se o spawner n�o � nulo e se est� habilitado antes de desativ�-lo.
    /// </summary>
    private void FreezeSpawners()
    {
        foreach (var spawner in asteroidSpawnerScripts)
        {
            if (spawner != null && spawner.enabled)
            {
                spawner.enabled = false; // Desativa o spawner de asteroides.
            }
        }
    }

    /// <summary>
    /// Congela (desativa) todos os spawners de armas.
    /// </summary>
    private void FreezeWeaponSpawners()
    {
        foreach (MonoBehaviour spawner in weaponSpawnerScripts)
        {
            if (spawner != null)
            {
                spawner.enabled = false; // Desativa o script do spawner de armas
                Debug.Log($"Spawner de Arma {spawner.name} desativado."); // Exibe uma mensagem indicando que o spawner de arma foi desativado
            }
        }
    }

    /// <summary>
    /// Destroi todos os asteroides presentes na cena.
    /// Esse m�todo � respons�vel por limpar todos os asteroides da cena ap�s o Game Over.
    /// </summary>
    private void DestroyAllAsteroids()
    {
        foreach (GameObject asteroid in GameObject.FindGameObjectsWithTag("Asteroid"))
        {
            Destroy(asteroid); // Destroi o asteroide da cena.
        }
    }

    /// <summary>
    /// Desabilita os objetos listados no array de objetosParaDesabilitar.
    /// Estes objetos s�o desativados durante o Game Over.
    /// </summary>
    private void DesabilitarObjetos()
    {
        foreach (GameObject obj in objetosParaDesabilitar)
        {
            if (obj != null)
            {
                obj.SetActive(false); // Desativa o objeto
            }
        }
    }

    /// <summary>
    /// Ativa todos os filhos de um objeto.
    /// Utilizado para garantir que todos os elementos da UI de Game Over sejam vis�veis.
    /// </summary>
    private void EnableAllChildren(GameObject parent)
    {
        foreach (Transform child in parent.transform)
        {
            child.gameObject.SetActive(true); // Ativa o filho
            EnableAllChildren(child.gameObject); // Recursivamente ativa os filhos de todos os filhos
        }
    }

    /// <summary>
    /// M�todo chamado ao pressionar o bot�o de reiniciar o jogo.
    /// </summary>
    public void OnRestartButtonPressed()
    {
        RestartGame(); // Chama o m�todo de rein�cio do jogo
    }

    /// <summary>
    /// Reinicia o jogo, restaurando a vida, desativando a UI de Game Over, e ativando spawners.
    /// </summary>
    private void RestartGame()
    {
        Debug.Log("Reiniciando o jogo...");

        vidaAtual = vidaInicial; // Restaura a vida inicial
        gameOverAtivo = false; // Desmarca o estado de Game Over
        AtualizarUIVida(); // Atualiza a UI com a vida inicial

        // Reativa os spawners de asteroides e armas
        foreach (MonoBehaviour spawner in asteroidSpawnerScripts)
        {
            if (spawner != null)
            {
                spawner.enabled = true;
            }
        }

        foreach (MonoBehaviour spawner in weaponSpawnerScripts)
        {
            if (spawner != null)
            {
                spawner.enabled = true;
            }
        }

        if (gameOverUI != null)
        {
            gameOverUI.SetActive(false); // Desativa a UI de Game Over
        }

        // Reativa todos os objetos que foram desativados no Game Over
        foreach (GameObject obj in objetosParaDesabilitar)
        {
            if (obj != null)
            {
                obj.SetActive(true);
            }
        }
    }
}
