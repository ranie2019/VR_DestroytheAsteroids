using UnityEngine;
using System.Collections.Generic;

public class GameOver : MonoBehaviour
{
    [Header("Spawners a serem congelados")]
    [SerializeField] private List<AsteroidSpawner> asteroidSpawnerScripts;

    [Header("Tag de Verificação")]
    [SerializeField] private string asteroidTag = "Asteroid";

    [Header("Objeto Game Over")]
    [SerializeField] private GameObject gameOverUI;

    [Header("Audio Player")]
    [SerializeField] private AudioPlayer audioPlayer;

    [Header("Particle Systems Prefabs")]
    [SerializeField] private List<ParticleSystem> particleSystemPrefabs;

    [Header("Objeto a ser alterado")]
    [SerializeField] private GameObject objectToDisableMeshRenderer;

    [Header("Armas a serem desativadas")]
    [SerializeField] private List<GameObject> weaponObjects;

    [Header("Referências aos Scripts Inativo")]
    [SerializeField] private List<Inativo> inativoScripts;

    private void OnCollisionEnter(Collision collision)
    {
        // Verifica se a colisão é com um asteroide
        if (collision.gameObject.CompareTag(asteroidTag))
        {
            HandleGameOver(collision);
        }
    }

    private void HandleGameOver(Collision collision)
    {
        FreezeInativoScripts();
        FreezeSpawners();
        ShowGameOverUI();
        DestroyAllAsteroids();
        ActivateParticleSystems();
        DisableObjectMeshRenderer();
        DisableWeapons();
        Destroy(collision.gameObject);
        PlayGameOverAudio();
    }

    private void FreezeInativoScripts()
    {
        foreach (var inativo in inativoScripts)
        {
            inativo?.CongelarTempoDeInatividade();
        }
    }

    private void FreezeSpawners()
    {
        foreach (var spawner in asteroidSpawnerScripts)
        {
            if (spawner != null)
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

    private void DisableObjectMeshRenderer()
    {
        if (objectToDisableMeshRenderer != null)
        {
            MeshRenderer childMeshRenderer = objectToDisableMeshRenderer.GetComponentInChildren<MeshRenderer>();

            if (childMeshRenderer != null)
            {
                childMeshRenderer.enabled = false;
            }
            else
            {
                Debug.LogWarning("MeshRenderer não encontrado no objeto ou nos filhos.");
            }
        }
        else
        {
            Debug.LogWarning("Objeto para desativar o MeshRenderer não está atribuído.");
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
