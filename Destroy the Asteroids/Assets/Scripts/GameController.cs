using UnityEngine;
using TMPro;

public class GameController : MonoBehaviour
{
    [Header("Score Components")]
    [SerializeField] private TextMeshProUGUI scoreText; // Exibi��o da pontua��o atual
    [SerializeField] private TextMeshProUGUI recordText; // Exibi��o do recorde

    private int playerScore; // Pontua��o atual do jogador
    private int recordScore; // Recorde salvo

    private void Start()
    {
        // Carrega o recorde salvo (se existir) ao iniciar o jogo
        recordScore = PlayerPrefs.GetInt("RecordScore", 0);
        recordText.text = recordScore.ToString(); // Atualiza o texto do recorde na UI
    }

    // M�todo para atualizar a pontua��o do jogador
    public void UpdatePlayerScore(int points)
    {
        playerScore += points; // Incrementa a pontua��o do jogador
        scoreText.text = playerScore.ToString(); // Atualiza o texto da pontua��o na UI

        // Verifica se o jogador bateu o recorde
        if (playerScore > recordScore)
        {
            recordScore = playerScore; // Atualiza o recorde
            recordText.text = recordScore.ToString(); // Atualiza o texto do recorde na UI

            // Salva o novo recorde
            PlayerPrefs.SetInt("RecordScore", recordScore);
            PlayerPrefs.Save();
        }
    }

    // M�todo para zerar a pontua��o do jogador
    public void ResetScore()
    {
        playerScore = 0; // Reseta a pontua��o
        scoreText.text = playerScore.ToString(); // Atualiza o texto da pontua��o na UI
    }

    // M�todo opcional para zerar o recorde (caso necess�rio)
    public void ResetRecord()
    {
        recordScore = 0; // Reseta o recorde
        PlayerPrefs.SetInt("RecordScore", 0); // Salva o novo recorde (zero)
        PlayerPrefs.Save();
        recordText.text = recordScore.ToString(); // Atualiza o texto do recorde na UI
    }
}
