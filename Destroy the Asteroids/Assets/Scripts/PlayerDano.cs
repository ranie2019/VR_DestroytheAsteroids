using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class PlayerDano : MonoBehaviour
{
    [Header("Configura��o de Vida")]
    [SerializeField] private int vidaInicial = 3;
    private int vidaAtual;
    [SerializeField] private TextMeshProUGUI vidaTextMeshPro;

    [Header("Game Over UI")]
    [SerializeField] private GameObject gameOverUI;

    [Header("Spawners")]
    [SerializeField] private MonoBehaviour[] asteroidSpawnerScripts;
    [SerializeField] private MonoBehaviour[] weaponSpawnerScripts;

    [Header("Objetos a Desabilitar")]
    [SerializeField] private GameObject[] objetosParaDesabilitar;

    [Header("�udio de Explos�o")]
    [SerializeField] private AudioClip explosionClip;
    [SerializeField] private AudioSource audioSource;

    [Header("Efeito de Dano")]
    [SerializeField] private Canvas danoCanvas;
    [SerializeField] private float duracaoDanoImage = 0.5f;
    [SerializeField] private AudioClip danoClip;

    [Header("Dist�ncia do Canvas de Dano")]
    [SerializeField] private float distanciaDanoCanvas = 2f;

    private bool gameOverAtivo = false;
    private Camera mainCamera;

    private void Start()
    {
        vidaAtual = vidaInicial;
        AtualizarUIVida();
        ValidateReferences();

        mainCamera = Camera.main;

        // Garante que o Canvas de dano comece desabilitado
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
            Debug.LogWarning("TextMeshProUGUI n�o atribu�do!");
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
        FreezeWeaponSpawners();
        DestroyAllAsteroids();
        DesabilitarObjetos();

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
            Debug.LogWarning("GameOver UI n�o atribu�do!");
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

    private void FreezeWeaponSpawners()
    {
        foreach (MonoBehaviour spawner in weaponSpawnerScripts)
        {
            if (spawner != null)
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
            Debug.LogWarning("Nenhum spawner de asteroides atribu�do.");

        if (weaponSpawnerScripts.Length == 0)
            Debug.LogWarning("Nenhum spawner de armas atribu�do.");

        if (audioSource == null)
            Debug.LogWarning("AudioSource n�o atribu�do.");

        if (gameOverUI == null)
            Debug.LogWarning("GameOver UI n�o atribu�do.");

        if (danoCanvas == null)
            Debug.LogWarning("Canvas de Dano n�o atribu�do.");

        if (danoClip == null)
            Debug.LogWarning("�udio de Dano n�o atribu�do.");
    }
}
