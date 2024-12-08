using UnityEngine;
using System.Collections.Generic;

public class GameOver : MonoBehaviour
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
}
