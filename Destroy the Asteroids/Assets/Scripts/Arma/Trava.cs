using UnityEngine;

public class Trava : MonoBehaviour
{
    [Header("Refer�ncia ao Script Inativo")]
    [Tooltip("Componente Inativo a ser resetado.")]
    private Inativo scriptInativo;

    [Header("Game Over Canvas")]
    [Tooltip("Refer�ncia ao Canvas de Game Over.")]
    [SerializeField] private GameObject gameOverCanvas;

    private void Awake()
    {
        // Busca o componente Inativo no mesmo GameObject
        scriptInativo = GetComponent<Inativo>();

        if (scriptInativo == null)
        {
            Debug.LogError("O componente 'Inativo' n�o foi encontrado neste GameObject!");
        }

        if (gameOverCanvas == null)
        {
            Debug.LogError("Game Over Canvas n�o foi atribu�do no Inspector!");
        }
    }

    private void Update()
    {
        // Verifica se o Game Over Canvas est� ativo
        if (gameOverCanvas != null && gameOverCanvas.activeInHierarchy)
        {
            ResetarContador();
            DesativarFilhos();  // Chama a fun��o para desativar os filhos
        }
    }

    /// <summary>
    /// Reseta o contador do script Inativo.
    /// </summary>
    private void ResetarContador()
    {
        if (scriptInativo != null)
        {
            scriptInativo.NotificarInicioDoJogo(); // Reinicia o contador
            Debug.Log("Contador do script Inativo foi resetado devido ao Game Over!");
        }
        else
        {
            Debug.LogWarning("N�o foi poss�vel resetar o contador porque o script Inativo n�o est� atribu�do.");
        }
    }

    /// <summary>
    /// Desativa todos os objetos filhos do GameObject que cont�m este script.
    /// </summary>
    private void DesativarFilhos()
    {
        foreach (Transform filho in transform) // Itera sobre todos os filhos do GameObject
        {
            filho.gameObject.SetActive(false); // Desativa cada filho
        }

        Debug.Log("Todos os objetos filhos foram desativados.");
    }
}
