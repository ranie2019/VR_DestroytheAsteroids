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

    [Header("Teclado")]
    [SerializeField] private GameObject teclado; // Referência ao teclado, que será ativado

    [Header("Record")]
    [SerializeField] private GameObject record; // Referência ao Record, que será ativado

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
        AtivarTeclado(); // Chama a função para ativar o teclado
        AtivarRecord(); // Chama a função para ativar o Record
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
    }

    private void EnableAllChildren(GameObject parent)
    {
        foreach (Transform child in parent.transform)
        {
            child.gameObject.SetActive(true);
            EnableAllChildren(child.gameObject);
        }
    }

    // Função para ativar o teclado
    private void AtivarTeclado()
    {
        if (teclado != null)
        {
            teclado.SetActive(true); // Ativa o teclado
        }
        else
        {
            Debug.LogWarning("Teclado não está atribuído.");
        }
    }

    // Função para ativar o Record
    private void AtivarRecord()
    {
        if (record != null)
        {
            record.SetActive(true); // Ativa o Record
        }
        else
        {
            Debug.LogWarning("Record não está atribuído.");
        }
    }
}
