using UnityEngine;
using TMPro;

public class GameController : MonoBehaviour
{
    [Header("Componentes de Pontua��o")]
    [SerializeField] private TextMeshProUGUI scoreText; // Refer�ncia ao texto que exibe a pontua��o
    [SerializeField] private TextMeshProUGUI recordText; // Refer�ncia ao texto que exibe o recorde

    [Header("Efeitos de Recorde")]
    [SerializeField] private ParticleSystem recordEffect; // Efeito visual ao bater o recorde
    [SerializeField] private AudioClip recordSound; // Som ao bater o recorde
    [SerializeField] private AudioSource audioSource; // Fonte de �udio para tocar o som

    private int playerScore; // Pontua��o atual do jogador
    private int recordScore; // Recorde salvo
    private bool hasPlayedRecordSound = false; // Flag para verificar se o som do recorde foi tocado

    private void Start()
    {
        // Carrega o recorde salvo usando PlayerPrefs
        recordScore = PlayerPrefs.GetInt("RecordScore", 0);
        UpdateUI(); // Atualiza os textos da interface do usu�rio
    }

    // M�todo para atualizar a pontua��o do jogador
    public void UpdatePlayerScore(int points)
    {
        playerScore += points; // Incrementa a pontua��o
        UpdateUI(); // Atualiza o texto da pontua��o na interface

        // Verifica se a pontua��o atual � maior que o recorde
        if (playerScore > recordScore)
        {
            UpdateRecord(); // Atualiza o recorde
        }
    }

    // M�todo que atualiza o recorde e salva a informa��o
    private void UpdateRecord()
    {
        recordScore = playerScore; // Define o novo recorde
        PlayerPrefs.SetInt("RecordScore", recordScore); // Salva o recorde no armazenamento persistente
        PlayerPrefs.Save(); // Garante que as altera��es sejam salvas imediatamente

        TriggerRecordFeedback(); // Executa efeitos visuais e sonoros ao bater o recorde
        UpdateUI(); // Atualiza o texto da interface
    }

    // M�todo que atualiza os textos na interface do usu�rio
    private void UpdateUI()
    {
        scoreText.text = $": {playerScore:N0}"; // Atualiza o texto da pontua��o
        recordText.text = $": {recordScore:N0}"; // Atualiza o texto do recorde
    }

    // M�todo para exibir efeitos visuais e sonoros ao bater o recorde
    private void TriggerRecordFeedback()
    {
        if (recordEffect != null)
        {
            // Instancia o efeito visual de recorde na posi��o atual
            Instantiate(recordEffect, transform.position, Quaternion.identity);
        }

        if (!hasPlayedRecordSound && audioSource != null && recordSound != null)
        {
            // Toca o som de recorde apenas uma vez
            audioSource.PlayOneShot(recordSound);
            hasPlayedRecordSound = true; // Marca que o som foi tocado
        }
    }

    // M�todo para resetar a pontua��o do jogador
    public void ResetScore()
    {
        playerScore = 0; // Zera a pontua��o do jogador
        hasPlayedRecordSound = false; // Reseta a flag para o �udio ser tocado no pr�ximo recorde
        UpdateUI(); // Atualiza a interface do usu�rio
    }

    // M�todo para resetar o recorde salvo
    public void ResetRecord()
    {
        recordScore = 0; // Zera o recorde
        PlayerPrefs.SetInt("RecordScore", 0); // Salva o novo recorde (zero)
        PlayerPrefs.Save(); // Garante que o armazenamento persistente seja atualizado
        hasPlayedRecordSound = false; // Reseta a flag para o �udio ser tocado no pr�ximo recorde
        UpdateUI(); // Atualiza o texto do recorde na interface
    }
}