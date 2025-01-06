using UnityEngine;
using System.Collections.Generic;

public class GameOver : MonoBehaviour
{
    [Header("Spawners a serem congelados")]
    [Tooltip("Lista de referências aos scripts de Spawners que serão desabilitados. Esses spawners são responsáveis por gerar asteroides durante o jogo.")]
    [SerializeField] private List<AsteroidSpawner> asteroidSpawnerScripts;

    [Header("Tag de Verificação")]
    [Tooltip("Tag do objeto que, ao colidir com o jogador, causará o congelamento dos spawners e o desencadeamento do Game Over.")]
    [SerializeField] private string asteroidTag = "Asteroid";

    [Header("Objeto Game Over")]
    [Tooltip("Referência ao objeto que será habilitado quando ocorrer o Game Over. Geralmente é um painel de UI que exibe a tela de Game Over.")]
    [SerializeField] private GameObject gameOverUI;

    [Header("Audio Player")]
    [Tooltip("Referência ao script AudioPlayer. Este script é responsável por controlar os efeitos sonoros do jogo.")]
    [SerializeField] private AudioPlayer audioPlayer;

    [Header("Particle Systems Prefabs")]
    [Tooltip("Prefabs dos Particle Systems que serão ativados quando o Game Over ocorrer. Usados para gerar efeitos visuais, como explosões ou faíscas.")]
    [SerializeField] private List<ParticleSystem> particleSystemPrefabs;

    [Header("Objeto a ser alterado")]
    [Tooltip("Objeto que terá o MeshRenderer do filho desativado manualmente. Pode ser utilizado para ocultar a nave ou outras partes do jogo após a colisão.")]
    [SerializeField] private GameObject objectToDisableMeshRenderer;

    [Header("Armas a serem desativadas")]
    [Tooltip("Lista de objetos representando as armas que terão seus MeshRenderers, BoxColliders e filhos desativados durante o Game Over.")]
    [SerializeField] private List<GameObject> weaponObjects;

    [Header("Referências aos Scripts Inativo")]
    [Tooltip("Lista de referências aos scripts Inativo. Esses scripts são congelados para parar o tempo de inatividade do jogo durante o Game Over.")]
    [SerializeField] private List<Inativo> inativoScripts;

    /// <summary>
    /// Detecta colisões com objetos com a tag específica para acionar o Game Over.
    /// Este método é chamado automaticamente sempre que um objeto colide com o objeto que possui este script.
    /// </summary>
    private void OnCollisionEnter(Collision collision)
    {
        // Verifica se o objeto que causou a colisão tem a tag 'Asteroid'
        if (collision.gameObject.CompareTag(asteroidTag))
        {
            // Chama o método responsável por gerenciar o Game Over
            HandleGameOver(collision);
        }
    }

    /// <summary>
    /// Gerencia todas as ações necessárias durante o Game Over.
    /// Este método é chamado quando a colisão com um asteroide é detectada.
    /// </summary>
    private void HandleGameOver(Collision collision)
    {
        FreezeInativoScripts();          // Congela os scripts de inatividade.
        FreezeSpawners();                // Desativa os spawners de asteroides.
        ShowGameOverUI();                // Exibe a interface do Game Over.
        DestroyAllAsteroids();           // Destroi todos os asteroides presentes na cena.
        ActivateParticleSystems();       // Ativa os efeitos visuais de partículas.
        DisableObjectMeshRenderer(objectToDisableMeshRenderer); // Desativa o MeshRenderer de objetos específicos.
        DisableWeapons();                // Desativa as armas do jogador.
        Destroy(collision.gameObject);   // Destrói o asteroide que causou o Game Over.
        PlayGameOverAudio();             // Reproduz o áudio relacionado ao Game Over.
    }

    /// <summary>
    /// Congela os scripts de inatividade.
    /// Este método percorre todos os scripts 'Inativo' e congela o tempo de inatividade para parar qualquer ação que não deva ocorrer durante o Game Over.
    /// </summary>
    private void FreezeInativoScripts()
    {
        foreach (var inativo in inativoScripts)
        {
            if (inativo != null)
            {
                inativo.CongelarTempoDeInatividade(); // Congela o tempo de inatividade do script.
            }
        }
    }

    /// <summary>
    /// Desabilita os scripts de spawners de asteroides.
    /// Desativa os spawners de asteroides para que novos asteroides não sejam gerados após o Game Over.
    /// </summary>
    private void FreezeSpawners()
    {
        foreach (var spawner in asteroidSpawnerScripts)
        {
            if (spawner != null && spawner.enabled)
            {
                spawner.enabled = false; // Desativa o spawner de asteroides.
            }
        }
    }

    /// <summary>
    /// Ativa a interface do Game Over.
    /// Exibe o painel de UI de Game Over na tela.
    /// </summary>
    private void ShowGameOverUI()
    {
        if (gameOverUI != null)
        {
            gameOverUI.SetActive(true);         // Torna o painel de Game Over visível.
            EnableAllChildren(gameOverUI);      // Garante que todos os filhos do painel de Game Over também sejam visíveis.
        }
        else
        {
            Debug.LogWarning("GameOver UI não está atribuído."); // Exibe um aviso se o UI de Game Over não estiver atribuído.
        }
    }

    /// <summary>
    /// Destroi todos os objetos com a tag de asteroide.
    /// Esse método é responsável por limpar todos os asteroides da cena após o Game Over.
    /// </summary>
    private void DestroyAllAsteroids()
    {
        foreach (GameObject asteroid in GameObject.FindGameObjectsWithTag(asteroidTag))
        {
            Destroy(asteroid); // Destroi o asteroide da cena.
        }
    }

    /// <summary>
    /// Ativa os efeitos de partículas durante o Game Over.
    /// Este método instância e ativa os prefabs de partículas para criar efeitos visuais, como explosões ou faíscas.
    /// </summary>
    private void ActivateParticleSystems()
    {
        if (objectToDisableMeshRenderer != null)
        {
            Vector3 position = objectToDisableMeshRenderer.transform.position; // Posição do objeto.
            Quaternion rotation = objectToDisableMeshRenderer.transform.rotation; // Rotação do objeto.

            foreach (var prefab in particleSystemPrefabs)
            {
                if (prefab != null)
                {
                    ParticleSystem particleClone = Instantiate(prefab, position, rotation); // Cria uma cópia do prefab da partícula.
                    particleClone.Play(); // Reproduz o sistema de partículas.
                    Destroy(particleClone.gameObject, particleClone.main.duration); // Destroi o objeto de partícula após a duração.
                }
            }
        }
        else
        {
            Debug.LogWarning("Objeto para partículas não está atribuído."); // Exibe um aviso se o objeto de partículas não estiver atribuído.
        }
    }

    /// <summary>
    /// Desativa o MeshRenderer de um objeto.
    /// Este método desativa o componente MeshRenderer, fazendo com que o objeto se torne invisível.
    /// </summary>
    private void DisableObjectMeshRenderer(GameObject obj)
    {
        if (obj != null)
        {
            MeshRenderer childMeshRenderer = obj.GetComponentInChildren<MeshRenderer>(); // Obtém o MeshRenderer do filho.
            if (childMeshRenderer != null)
            {
                childMeshRenderer.enabled = false; // Desativa o MeshRenderer.
            }
        }
    }

    /// <summary>
    /// Desativa os componentes das armas do jogador.
    /// Este método desativa os MeshRenderers, BoxColliders e filhos das armas, tornando-as inutilizáveis após o Game Over.
    /// </summary>
    private void DisableWeapons()
    {
        foreach (var weapon in weaponObjects)
        {
            if (weapon != null)
            {
                DisableComponentsInChildren<MeshRenderer>(weapon); // Desativa o MeshRenderer das armas.
                DisableComponentsInChildren<BoxCollider>(weapon); // Desativa o BoxCollider das armas.

                // Desativa todos os filhos da arma.
                foreach (Transform child in weapon.transform)
                {
                    child.gameObject.SetActive(false);
                }
            }
        }
    }

    /// <summary>
    /// Desativa componentes específicos (MeshRenderer ou BoxCollider) em todos os filhos de um objeto.
    /// </summary>
    private void DisableComponentsInChildren<T>(GameObject obj) where T : Component
    {
        T[] components = obj.GetComponentsInChildren<T>(); // Obtém todos os componentes do tipo especificado nos filhos.
        foreach (var component in components)
        {
            if (component is Renderer renderer)
            {
                renderer.enabled = false; // Desativa o Renderer.
            }
            else if (component is Collider collider)
            {
                collider.enabled = false; // Desativa o Collider.
            }
        }
    }

    /// <summary>
    /// Reproduz o áudio relacionado ao Game Over.
    /// Este método para a música principal e toca o áudio do Game Over.
    /// </summary>
    private void PlayGameOverAudio()
    {
        if (audioPlayer != null)
        {
            audioPlayer.StopMainGameAudio(); // Para o áudio principal.
            audioPlayer.PlayGameOverAudio(); // Toca o áudio do Game Over.
        }
        else
        {
            Debug.LogWarning("AudioPlayer não está atribuído."); // Exibe um aviso se o AudioPlayer não estiver atribuído.
        }
    }

    /// <summary>
    /// Ativa todos os filhos de um objeto.
    /// Esse método garante que todos os filhos do objeto sejam visíveis (usado para a UI de Game Over).
    /// </summary>
    private void EnableAllChildren(GameObject parent)
    {
        foreach (Transform child in parent.transform)
        {
            child.gameObject.SetActive(true); // Torna o filho visível.
            EnableAllChildren(child.gameObject); // Recursivamente ativa os filhos.
        }
    }
}
