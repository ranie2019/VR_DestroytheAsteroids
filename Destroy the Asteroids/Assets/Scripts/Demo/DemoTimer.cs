using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;

public class DemoTimer : MonoBehaviour
{
    [Header("Configurações do Tempo")]
    [SerializeField] private float demoDuration = 300f;
    private float timeRemaining;
    private bool isTimerActive = false;
    private bool demoEnded = false;

    [Header("UI do Cronômetro")]
    [SerializeField] private TextMeshProUGUI timerText;

    private void Start()
    {
        timeRemaining = demoDuration;
        UpdateTimerUI(); // Apenas atualiza o texto, sem iniciar o cronômetro
    }

    private void Update()
    {
        if (isTimerActive && timeRemaining > 0)
        {
            timeRemaining -= Time.deltaTime;

            if (timeRemaining <= 0)
            {
                timeRemaining = 0;
                UpdateTimerUI();
                EndDemo();
            }
            else
            {
                UpdateTimerUI();
            }
        }
    }

    private void UpdateTimerUI()
    {
        if (timerText != null)
        {
            timerText.text = timeRemaining > 0 ? $"End of Demo: {FormatTime(timeRemaining)}" : "";
        }
    }

    private string FormatTime(float time)
    {
        int minutes = Mathf.FloorToInt(time / 60);
        int seconds = Mathf.FloorToInt(time % 60);
        return $"{minutes:00}:{seconds:00}";
    }

    private void EndDemo()
    {
        if (demoEnded) return;
        demoEnded = true;
        SceneManager.LoadScene("Demo");
    }

    /// <summary>
    /// Inicia o cronômetro quando chamado pelo StartGame.
    /// </summary>
    public void StartTimer()
    {
        if (!isTimerActive)
        {
            isTimerActive = true;
        }
    }
}
