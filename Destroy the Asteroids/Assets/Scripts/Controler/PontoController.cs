using UnityEngine;
using TMPro;

public class GameController : MonoBehaviour
{
    [Header("Componentes de Pontuação")]
    [SerializeField] private TextMeshProUGUI scoreText; // Referência ao texto que exibe a pontuação
    [SerializeField] private TextMeshProUGUI recordText; // Referência ao texto que exibe o recorde

    [Header("Efeitos de Recorde")]
    [SerializeField] private ParticleSystem recordEffect; // Efeito visual ao bater o recorde
    [SerializeField] private AudioClip recordSound; // Som ao bater o recorde
    [SerializeField] private AudioSource audioSource; // Fonte de áudio para tocar o som

    private int playerScore; // Pontuação atual do jogador
    private int recordScore; // Recorde salvo
    private bool hasPlayedRecordSound = false; // Flag para verificar se o som do recorde foi tocado

    private void Start()
    {
        // Carrega o recorde salvo usando PlayerPrefs
        recordScore = PlayerPrefs.GetInt("RecordScore", 0);
        UpdateUI(); // Atualiza os textos da interface do usuário
    }

    // Método para atualizar a pontuação do jogador
    public void UpdatePlayerScore(int points)
    {
        playerScore += points; // Incrementa a pontuação
        UpdateUI(); // Atualiza o texto da pontuação na interface

        // Verifica se a pontuação atual é maior que o recorde
        if (playerScore > recordScore)
        {
            UpdateRecord(); // Atualiza o recorde
        }
    }

    // Método que atualiza o recorde e salva a informação
    private void UpdateRecord()
    {
        recordScore = playerScore; // Define o novo recorde
        PlayerPrefs.SetInt("RecordScore", recordScore); // Salva o recorde no armazenamento persistente
        PlayerPrefs.Save(); // Garante que as alterações sejam salvas imediatamente

        TriggerRecordFeedback(); // Executa efeitos visuais e sonoros ao bater o recorde
        UpdateUI(); // Atualiza o texto da interface
    }

    // Método que atualiza os textos na interface do usuário
    private void UpdateUI()
    {
        scoreText.text = $": {playerScore:N0}"; // Atualiza o texto da pontuação
        recordText.text = $": {recordScore:N0}"; // Atualiza o texto do recorde
    }

    // Método para exibir efeitos visuais e sonoros ao bater o recorde
    private void TriggerRecordFeedback()
    {
        if (recordEffect != null)
        {
            // Instancia o efeito visual de recorde na posição atual
            Instantiate(recordEffect, transform.position, Quaternion.identity);
        }

        if (!hasPlayedRecordSound && audioSource != null && recordSound != null)
        {
            // Toca o som de recorde apenas uma vez
            audioSource.PlayOneShot(recordSound);
            hasPlayedRecordSound = true; // Marca que o som foi tocado
        }
    }

    // Método para resetar a pontuação do jogador
    public void ResetScore()
    {
        playerScore = 0; // Zera a pontuação do jogador
        hasPlayedRecordSound = false; // Reseta a flag para o áudio ser tocado no próximo recorde
        UpdateUI(); // Atualiza a interface do usuário
    }

    // Método para resetar o recorde salvo
    public void ResetRecord()
    {
        recordScore = 0; // Zera o recorde
        PlayerPrefs.SetInt("RecordScore", 0); // Salva o novo recorde (zero)
        PlayerPrefs.Save(); // Garante que o armazenamento persistente seja atualizado
        hasPlayedRecordSound = false; // Reseta a flag para o áudio ser tocado no próximo recorde
        UpdateUI(); // Atualiza o texto do recorde na interface
    }
}