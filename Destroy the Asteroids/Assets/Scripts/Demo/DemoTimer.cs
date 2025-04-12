using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;

public class DemoTimer : MonoBehaviour
{
    [Header("Configurações do Tempo")]
    [SerializeField] private float demoDuration = 300f;

    [Header("UI do Cronômetro")]
    [SerializeField] private TextMeshProUGUI timerText;

    private float timeRemaining;
    private float lastDisplayedTime = -1f;
    private bool isTimerActive = false;
    private bool demoEnded = false;

    private void Start()
    {
        timeRemaining = demoDuration;
        UpdateTimerUI(forceUpdate: true);
        enabled = false; // só ativa quando for começar
    }

    private void Update()
    {
        if (!isTimerActive || demoEnded) return;

        timeRemaining -= Time.unscaledDeltaTime;

        if (timeRemaining <= 0f)
        {
            timeRemaining = 0f;
            UpdateTimerUI(forceUpdate: true);
            EndDemo();
            return;
        }

        UpdateTimerUI();
    }

    private void UpdateTimerUI(bool forceUpdate = false)
    {
        if (!timerText) return;

        float roundedTime = Mathf.Floor(timeRemaining);
        if (forceUpdate || roundedTime != lastDisplayedTime)
        {
            lastDisplayedTime = roundedTime;
            timerText.text = roundedTime > 0f ? $" {FormatTime(roundedTime)}" : "";
        }
    }

    private static string FormatTime(float time)
    {
        int minutes = (int)(time / 60);
        int seconds = (int)(time % 60);
        return $"{minutes:00}:{seconds:00}";
    }

    private void EndDemo()
    {
        if (demoEnded) return;

        demoEnded = true;
        enabled = false;
        SceneManager.LoadScene("Demo");
    }

    public void StartTimer()
    {
        if (isTimerActive) return;

        isTimerActive = true;
        enabled = true;
    }
}
