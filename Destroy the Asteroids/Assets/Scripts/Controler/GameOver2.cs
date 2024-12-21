using UnityEngine;
using UnityEngine.UI; // Para trabalhar com o Text UI
using TMPro; // Se voc� estiver usando o TextMeshPro
using System.Collections.Generic;

public class GameOver2 : MonoBehaviour
{
    [Header("Spawners a serem congelados")]
    [Tooltip("Lista de refer�ncias aos scripts de Spawners que ser�o desabilitados.")]
    [SerializeField] private List<AsteroidSpawner> asteroidSpawnerScripts;

    [Header("Tag de Verifica��o")]
    [Tooltip("Tag do objeto que, ao colidir, causar� o congelamento dos spawners.")]
    [SerializeField] private string asteroidTag = "Asteroid";

    [Header("Objeto Game Over")]
    [Tooltip("Refer�ncia ao objeto que ser� habilitado ao ocorrer o Game Over.")]
    [SerializeField] private GameObject gameOverUI;

    [Header("Audio Player")]
    [Tooltip("Refer�ncia ao script AudioPlayer.")]
    [SerializeField] private AudioPlayer audioPlayer;

    [Header("Particle Systems Prefabs")]
    [Tooltip("Prefabs dos Particle Systems que ser�o ativados no Game Over.")]
    [SerializeField] private List<ParticleSystem> particleSystemPrefabs;

    [Header("Objeto a ser alterado")]
    [Tooltip("Objeto que ter� o MeshRenderer do filho desativado manualmente ao ocorrer o Game Over.")]
    [SerializeField] private GameObject objectToDisableMeshRenderer;

    [Header("Armas a serem desativadas")]
    [Tooltip("Lista de objetos representando as armas que ter�o seus MeshRenderers, BoxColliders e filhos desativados no Game Over.")]
    [SerializeField] private List<GameObject> weaponObjects;

    [Header("Vida Inicial")]
    [Tooltip("Valor da vida inicial do jogador.")]
    [SerializeField] private int initialLives = 3;

    [Header("UI de Vida")]
    [Tooltip("Refer�ncia ao texto da UI que exibir� o n�mero de vidas restantes.")]
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

        // Subtrai uma vida ap�s o Game Over
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
            Debug.LogWarning("GameOver UI n�o est� atribu�do.");
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
            Debug.LogWarning("Objeto para part�culas n�o est� atribu�do.");
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
            Debug.LogWarning("AudioPlayer n�o est� atribu�do.");
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
            // Caso n�o tenha mais vidas, aciona a l�gica de Game Over
            TriggerGameOver();
        }
        else
        {
            Debug.Log($"Vidas restantes: {currentLives}");
            // Aqui voc� pode adicionar a l�gica para restaurar o jogador ou reiniciar o n�vel.
        }
    }

    private void TriggerGameOver()
    {
        // Aqui voc� pode adicionar a l�gica que aciona o game over total,
        // como reiniciar o jogo, exibir uma tela de game over, etc.
        Debug.Log("Game Over! Voc� perdeu todas as vidas.");
        // Exemplo: reiniciar o n�vel
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
            Debug.LogWarning("Refer�ncia ao texto de vidas n�o est� atribu�da.");
        }
    }
}
