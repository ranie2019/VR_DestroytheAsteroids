using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class PlayerDano : MonoBehaviour
{
    [Header("Configuração de Vida")]
    [SerializeField] private int vidaInicial = 3;
    private int vidaAtual;
    [SerializeField] private TextMeshProUGUI vidaTextMeshPro;

    [Header("Game Over UI")]
    [SerializeField] private GameObject gameOverUI;

    [Header("Spawners")]
    [SerializeField] private MonoBehaviour[] asteroidSpawnerScripts;

    [Header("Objetos a Desabilitar")]
    [SerializeField] private GameObject[] objetosParaDesabilitar;

    [Header("Áudio de Explosão")]
    [SerializeField] private AudioClip explosionClip;
    [SerializeField] private AudioSource audioSource;

    [Header("Efeito de Dano")]
    [SerializeField] private Canvas danoCanvas;
    [SerializeField] private float duracaoDanoImage = 0.5f;
    [SerializeField] private AudioClip danoClip;

    [Header("Distância do Canvas de Dano")]
    [SerializeField] private float distanciaDanoCanvas = 2f;

    [Header("Sistema de Placar")]
    [SerializeField] private GameObject record;
    [SerializeField] private GameObject teclado;

    private bool gameOverAtivo = false;
    private Camera mainCamera;

    private void Start()
    {
        vidaAtual = vidaInicial;
        AtualizarUIVida();
        ValidateReferences();

        mainCamera = Camera.main;

        if (danoCanvas != null)
        {
            danoCanvas.gameObject.SetActive(false);
        }
    }

    private void Update()
    {
        if (danoCanvas != null && mainCamera != null)
        {
            Vector3 playerPosition = transform.position;
            Vector3 cameraForward = mainCamera.transform.forward;

            danoCanvas.transform.position = playerPosition + cameraForward.normalized * distanciaDanoCanvas;

            Vector3 directionToLook = (mainCamera.transform.position - danoCanvas.transform.position).normalized;
            Quaternion rotation = Quaternion.LookRotation(directionToLook);
            Vector3 eulerRotation = rotation.eulerAngles;
            eulerRotation.x = 0f;
            danoCanvas.transform.rotation = Quaternion.Euler(eulerRotation);
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Inimigo") && !gameOverAtivo)
        {
            vidaAtual--;
            AtualizarUIVida();

            Destroy(collision.gameObject);

            StartCoroutine(ExibirDanoCanvas());
            TocarSomDano();

            if (vidaAtual <= 0)
            {
                HandleGameOver();
            }
        }
    }

    private void AtualizarUIVida()
    {
        if (vidaTextMeshPro != null)
        {
            vidaTextMeshPro.text = vidaAtual.ToString();
        }
        else
        {
            Debug.LogWarning("TextMeshProUGUI não atribuído!");
        }
    }

    private void HandleGameOver()
    {
        if (gameOverAtivo) return;

        gameOverAtivo = true;

        if (explosionClip != null && audioSource != null)
        {
            audioSource.PlayOneShot(explosionClip);
        }

        ShowGameOverUI();
        FreezeSpawners();
        DestroyAllAsteroids();
        DesabilitarObjetos();

        // ATIVA TECLADO
        if (teclado != null)
        {
            teclado.SetActive(true);
            teclado.transform.position = new Vector3(0f, 2.88f, 3.69f);
        }

        // ATIVA PLACAR
        if (record != null)
        {
            record.SetActive(true);
            record.transform.position = new Vector3(-5.2f, 3.5f, 5.1f);
        }

        // PREPARA SISTEMA DE PONTUAÇÃO
        FindObjectOfType<PontoController>()?.PlacarOffline();

        // REGISTRA PONTUAÇÃO FINAL
        FindObjectOfType<PlacarRecords>()?.RegistrarPontuacaoFinal();

        Debug.Log("Game Over! O jogador perdeu todas as vidas.");
    }

    private void ShowGameOverUI()
    {
        if (gameOverUI != null)
        {
            gameOverUI.SetActive(true);
            EnableAllChildren(gameOverUI);
        }
        else
        {
            Debug.LogWarning("GameOver UI não atribuído!");
        }
    }

    private void FreezeSpawners()
    {
        foreach (var spawner in asteroidSpawnerScripts)
        {
            if (spawner != null && spawner.enabled)
            {
                spawner.enabled = false;
            }
        }
    }

    private void DestroyAllAsteroids()
    {
        foreach (GameObject asteroid in GameObject.FindGameObjectsWithTag("Asteroid"))
        {
            Destroy(asteroid);
        }
    }

    private void DesabilitarObjetos()
    {
        foreach (GameObject obj in objetosParaDesabilitar)
        {
            if (obj != null)
            {
                obj.SetActive(false);
            }
        }
    }

    private void EnableAllChildren(GameObject parent)
    {
        foreach (Transform child in parent.transform)
        {
            child.gameObject.SetActive(true);
            EnableAllChildren(child.gameObject);
        }
    }

    public void ResetarVida()
    {
        vidaAtual = vidaInicial;
        gameOverAtivo = false;
        AtualizarUIVida();
    }

    private System.Collections.IEnumerator ExibirDanoCanvas()
    {
        if (danoCanvas != null)
        {
            danoCanvas.gameObject.SetActive(true);
            yield return new WaitForSeconds(duracaoDanoImage);
            danoCanvas.gameObject.SetActive(false);
        }
    }

    private void TocarSomDano()
    {
        if (danoClip != null && audioSource != null)
        {
            audioSource.PlayOneShot(danoClip);
        }
    }

    private void ValidateReferences()
    {
        if (asteroidSpawnerScripts.Length == 0)
            Debug.LogWarning("Nenhum spawner de asteroides atribuído.");

        if (audioSource == null)
            Debug.LogWarning("AudioSource não atribuído.");

        if (gameOverUI == null)
            Debug.LogWarning("GameOver UI não atribuído.");

        if (danoCanvas == null)
            Debug.LogWarning("Canvas de Dano não atribuído.");

        if (danoClip == null)
            Debug.LogWarning("Áudio de Dano não atribuído.");
    }
}