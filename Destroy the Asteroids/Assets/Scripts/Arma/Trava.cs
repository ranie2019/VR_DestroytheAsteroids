using UnityEngine;

public class Trava : MonoBehaviour
{
    [Header("Referência ao Script Inativo")]
    [Tooltip("Componente Inativo a ser resetado.")]
    private Inativo scriptInativo;

    [Header("Game Over Canvas")]
    [Tooltip("Referência ao Canvas de Game Over.")]
    [SerializeField] private GameObject gameOverCanvas;

    private void Awake()
    {
        // Busca o componente Inativo no mesmo GameObject
        scriptInativo = GetComponent<Inativo>();

        if (scriptInativo == null)
        {
            Debug.LogError("O componente 'Inativo' não foi encontrado neste GameObject!");
        }

        if (gameOverCanvas == null)
        {
            Debug.LogError("Game Over Canvas não foi atribuído no Inspector!");
        }
    }

    private void Update()
    {
        // Verifica se o Game Over Canvas está ativo
        if (gameOverCanvas != null && gameOverCanvas.activeInHierarchy)
        {
            ResetarContador();
            DesativarFilhos();  // Chama a função para desativar os filhos
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
            Debug.LogWarning("Não foi possível resetar o contador porque o script Inativo não está atribuído.");
        }
    }

    /// <summary>
    /// Desativa todos os objetos filhos do GameObject que contém este script.
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
