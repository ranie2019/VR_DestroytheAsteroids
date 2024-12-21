using UnityEngine;
using UnityEngine.UI; // Para trabalhar com o Text UI
using TMPro; // Se você estiver usando o TextMeshPro
using System.Collections.Generic;

public class GameOver2 : MonoBehaviour
{
    [Header("Spawners a serem congelados")]
    [Tooltip("Lista de referências aos scripts de Spawners que serão desabilitados.")]
    [SerializeField] private List<AsteroidSpawner> asteroidSpawnerScripts;

    [Header("Tag de Verificação")]
    [Tooltip("Tag do objeto que, ao colidir, causará o congelamento dos spawners.")]
    [SerializeField] private string asteroidTag = "Asteroid";

    [Header("Objeto Game Over")]
    [Tooltip("Referência ao objeto que será habilitado ao ocorrer o Game Over.")]
    [SerializeField] private GameObject gameOverUI;

    [Header("Audio Player")]
    [Tooltip("Referência ao script AudioPlayer.")]
    [SerializeField] private AudioPlayer audioPlayer;

    [Header("Particle Systems Prefabs")]
    [Tooltip("Prefabs dos Particle Systems que serão ativados no Game Over.")]
    [SerializeField] private List<ParticleSystem> particleSystemPrefabs;

    [Header("Objeto a ser alterado")]
    [Tooltip("Objeto que terá o MeshRenderer do filho desativado manualmente ao ocorrer o Game Over.")]
    [SerializeField] private GameObject objectToDisableMeshRenderer;

    [Header("Armas a serem desativadas")]
    [Tooltip("Lista de objetos representando as armas que terão seus MeshRenderers, BoxColliders e filhos desativados no Game Over.")]
    [SerializeField] private List<GameObject> weaponObjects;

    [Header("Vida Inicial")]
    [Tooltip("Valor da vida inicial do jogador.")]
    [SerializeField] private int initialLives = 3;

    [Header("UI de Vida")]
    [Tooltip("Referência ao texto da UI que exibirá o número de vidas restantes.")]
    [SerializeField] private TextMeshProUGUI livesText; // Usando TextMeshPro, mas pode usar Text se preferir

    private int currentLives;

    private void Start()
    {
        // Inicializa as vidas do jogador com o valor definido em initialLives
        currentLives = initialLives;
        UpdateLivesUI();
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag(asteroidTag))
        {
            HandleGameOver(collision);
        }
    }

    private void HandleGameOver(Collision collision)
    {
        FreezeSpawners();
        ShowGameOverUI();
        DestroyAllAsteroids();
        ActivateParticleSystems();
        DisableObjectMeshRenderer(objectToDisableMeshRenderer);
        DisableWeapons();
        Destroy(collision.gameObject);
        PlayGameOverAudio();

        // Subtrai uma vida após o Game Over
        LoseLife();
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

    private void ShowGameOverUI()
    {
        if (gameOverUI != null)
        {
            gameOverUI.SetActive(true);
            EnableAllChildren(gameOverUI);
        }
        else
        {
            Debug.LogWarning("GameOver UI não está atribuído.");
        }
    }

    private void DestroyAllAsteroids()
    {
        foreach (GameObject asteroid in GameObject.FindGameObjectsWithTag(asteroidTag))
        {
            Destroy(asteroid);
        }
    }

    private void ActivateParticleSystems()
    {
        if (objectToDisableMeshRenderer != null)
        {
            Vector3 position = objectToDisableMeshRenderer.transform.position;
            Quaternion rotation = objectToDisableMeshRenderer.transform.rotation;

            foreach (var prefab in particleSystemPrefabs)
            {
                if (prefab != null)
                {
                    ParticleSystem particleClone = Instantiate(prefab, position, rotation);
                    particleClone.Play();
                    Destroy(particleClone.gameObject, particleClone.main.duration);
                }
            }
        }
        else
        {
            Debug.LogWarning("Objeto para partículas não está atribuído.");
        }
    }

    private void DisableObjectMeshRenderer(GameObject obj)
    {
        if (obj != null)
        {
            MeshRenderer childMeshRenderer = obj.GetComponentInChildren<MeshRenderer>();
            if (childMeshRenderer != null)
            {
                childMeshRenderer.enabled = false;
            }
        }
    }

    private void DisableWeapons()
    {
        foreach (var weapon in weaponObjects)
        {
            if (weapon != null)
            {
                DisableComponentsInChildren<MeshRenderer>(weapon);
                DisableComponentsInChildren<BoxCollider>(weapon);

                foreach (Transform child in weapon.transform)
                {
                    child.gameObject.SetActive(false);
                }
            }
        }
    }

    private void DisableComponentsInChildren<T>(GameObject obj) where T : Component
    {
        T[] components = obj.GetComponentsInChildren<T>();
        foreach (var component in components)
        {
            if (component is Renderer renderer)
            {
                renderer.enabled = false;
            }
            else if (component is Collider collider)
            {
                collider.enabled = false;
            }
        }
    }

    private void PlayGameOverAudio()
    {
        if (audioPlayer != null)
        {
            audioPlayer.StopMainGameAudio();
            audioPlayer.PlayGameOverAudio();
        }
        else
        {
            Debug.LogWarning("AudioPlayer não está atribuído.");
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

    private void LoseLife()
    {
        // Reduz uma vida
        currentLives--;

        // Atualiza a UI de vidas
        UpdateLivesUI();

        // Verifica se o jogador ainda tem vidas
        if (currentLives <= 0)
        {
            // Caso não tenha mais vidas, aciona a lógica de Game Over
            TriggerGameOver();
        }
        else
        {
            Debug.Log($"Vidas restantes: {currentLives}");
            // Aqui você pode adicionar a lógica para restaurar o jogador ou reiniciar o nível.
        }
    }

    private void TriggerGameOver()
    {
        // Aqui você pode adicionar a lógica que aciona o game over total,
        // como reiniciar o jogo, exibir uma tela de game over, etc.
        Debug.Log("Game Over! Você perdeu todas as vidas.");
        // Exemplo: reiniciar o nível
        // SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }

    private void UpdateLivesUI()
    {
        if (livesText != null)
        {
            livesText.text = "Vidas: " + currentLives;
        }
        else
        {
            Debug.LogWarning("Referência ao texto de vidas não está atribuída.");
        }
    }
}
