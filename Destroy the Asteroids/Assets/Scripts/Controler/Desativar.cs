using UnityEngine;

public class Desativar : MonoBehaviour
{
    [Header("Dependência")]
    [Tooltip("Script StartGame que controla o início do jogo.")]
    [SerializeField] private StartGame startGameScript;

    [Header("Tempo de Espera")]
    [Tooltip("Tempo em segundos antes de desativar o Canvas.")]
    [SerializeField] private float delayBeforeDeactivate = 3f;

    private bool startGameCalled = false; // Flag para verificar se StartGame foi chamado

    private void Update()
    {
        // Verifica se o StartGame foi chamado
        if (startGameCalled)
        {
            // Inicia a Coroutine para desativar o Canvas após o tempo de espera
            StartCoroutine(DeactivateCanvasAfterDelay());
        }
    }

    // Método público chamado pelo script StartGame
    public void OnStartGameCalled()
    {
        startGameCalled = true;
    }

    private System.Collections.IEnumerator DeactivateCanvasAfterDelay()
    {
        // Impede que múltiplas chamadas ocorram ao mesmo tempo
        startGameCalled = false;

        // Aguarda o tempo configurado
        yield return new WaitForSeconds(delayBeforeDeactivate);

        // Desativa o Canvas (o GameObject que contém este script)
        GameObject canvasGameObject = this.gameObject;
        if (canvasGameObject.activeSelf)
        {
            canvasGameObject.SetActive(false);
            Debug.Log("Canvas desativado.");
        }
    }
}
