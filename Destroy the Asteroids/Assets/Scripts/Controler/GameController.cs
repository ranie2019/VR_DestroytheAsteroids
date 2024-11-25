using UnityEngine;
using TMPro;

public class GameController : MonoBehaviour
{
    [Header("Score Components")]
    [SerializeField] private TextMeshProUGUI scoreText; // Exibição da pontuação atual
    [SerializeField] private TextMeshProUGUI recordText; // Exibição do recorde

    private int playerScore; // Pontuação atual do jogador
    private int recordScore; // Recorde salvo

    private void Start()
    {
        // Carrega o recorde salvo (se existir) ao iniciar o jogo
        recordScore = PlayerPrefs.GetInt("RecordScore", 0);
        recordText.text = recordScore.ToString(); // Atualiza o texto do recorde na UI
    }

    // Método para atualizar a pontuação do jogador
    public void UpdatePlayerScore(int points)
    {
        playerScore += points; // Incrementa a pontuação do jogador
        scoreText.text = playerScore.ToString(); // Atualiza o texto da pontuação na UI

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

    // Método para zerar a pontuação do jogador
    public void ResetScore()
    {
        playerScore = 0; // Reseta a pontuação
        scoreText.text = playerScore.ToString(); // Atualiza o texto da pontuação na UI
    }

    // Método opcional para zerar o recorde (caso necessário)
    public void ResetRecord()
    {
        recordScore = 0; // Reseta o recorde
        PlayerPrefs.SetInt("RecordScore", 0); // Salva o novo recorde (zero)
        PlayerPrefs.Save();
        recordText.text = recordScore.ToString(); // Atualiza o texto do recorde na UI
    }
}
