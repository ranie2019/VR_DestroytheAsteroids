using UnityEngine;
using TMPro;

public class PopupController : MonoBehaviour
{
    [Header("Configuração de Animação")]
    [Tooltip("Velocidade de movimento para cima do popup.")]
    [SerializeField] private float floatSpeed = 2.0f;

    [Tooltip("Tempo de vida do popup.")]
    [SerializeField] private float lifetime = 1.5f;

    private TextMeshProUGUI scoreText;

    private void Awake()
    {
        // Busca automática do componente TextMeshProUGUI
        scoreText = GetComponentInChildren<TextMeshProUGUI>();
    }

    private void Start()
    {
        // Destroi o popup após o tempo de vida
        Destroy(gameObject, lifetime);
    }

    private void Update()
    {
        // Move o popup para cima ao longo do tempo
        transform.Translate(Vector3.up * floatSpeed * Time.deltaTime);
    }

    /// <summary>
    /// Define o texto do popup para exibir a pontuação.
    /// </summary>
    /// <param name="text">Texto a ser exibido.</param>
    public void SetScoreText(string text)
    {
        if (scoreText != null)
        {
            scoreText.text = text;
        }
    }
}
