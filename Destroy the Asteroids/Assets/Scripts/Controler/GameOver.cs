using UnityEngine;
using System.Collections.Generic;

public class GameOver : MonoBehaviour
{
    [Header("Spawners a serem congelados")]
    [Tooltip("Lista de refer�ncias aos scripts de Spawners que ser�o desabilitados. Esses spawners s�o respons�veis por gerar asteroides durante o jogo.")]
    [SerializeField] private List<AsteroidSpawner> asteroidSpawnerScripts;

    [Header("Tag de Verifica��o")]
    [Tooltip("Tag do objeto que, ao colidir com o jogador, causar� o congelamento dos spawners e o desencadeamento do Game Over.")]
    [SerializeField] private string asteroidTag = "Asteroid";

    [Header("Objeto Game Over")]
    [Tooltip("Refer�ncia ao objeto que ser� habilitado quando ocorrer o Game Over. Geralmente � um painel de UI que exibe a tela de Game Over.")]
    [SerializeField] private GameObject gameOverUI;

    [Header("Audio Player")]
    [Tooltip("Refer�ncia ao script AudioPlayer. Este script � respons�vel por controlar os efeitos sonoros do jogo.")]
    [SerializeField] private AudioPlayer audioPlayer;

    [Header("Particle Systems Prefabs")]
    [Tooltip("Prefabs dos Particle Systems que ser�o ativados quando o Game Over ocorrer. Usados para gerar efeitos visuais, como explos�es ou fa�scas.")]
    [SerializeField] private List<ParticleSystem> particleSystemPrefabs;

    [Header("Objeto a ser alterado")]
    [Tooltip("Objeto que ter� o MeshRenderer do filho desativado manualmente. Pode ser utilizado para ocultar a nave ou outras partes do jogo ap�s a colis�o.")]
    [SerializeField] private GameObject objectToDisableMeshRenderer;

    [Header("Armas a serem desativadas")]
    [Tooltip("Lista de objetos representando as armas que ter�o seus MeshRenderers, BoxColliders e filhos desativados durante o Game Over.")]
    [SerializeField] private List<GameObject> weaponObjects;

    [Header("Refer�ncias aos Scripts Inativo")]
    [Tooltip("Lista de refer�ncias aos scripts Inativo. Esses scripts s�o congelados para parar o tempo de inatividade do jogo durante o Game Over.")]
    [SerializeField] private List<Inativo> inativoScripts;

    /// <summary>
    /// Detecta colis�es com objetos com a tag espec�fica para acionar o Game Over.
    /// Este m�todo � chamado automaticamente sempre que um objeto colide com o objeto que possui este script.
    /// </summary>
    private void OnCollisionEnter(Collision collision)
    {
        // Verifica se o objeto que causou a colis�o tem a tag 'Asteroid'
        if (collision.gameObject.CompareTag(asteroidTag))
        {
            // Chama o m�todo respons�vel por gerenciar o Game Over
            HandleGameOver(collision);
        }
    }

    /// <summary>
    /// Gerencia todas as a��es necess�rias durante o Game Over.
    /// Este m�todo � chamado quando a colis�o com um asteroide � detectada.
    /// </summary>
    private void HandleGameOver(Collision collision)
    {
        FreezeInativoScripts();          // Congela os scripts de inatividade.
        FreezeSpawners();                // Desativa os spawners de asteroides.
        ShowGameOverUI();                // Exibe a interface do Game Over.
        DestroyAllAsteroids();           // Destroi todos os asteroides presentes na cena.
        ActivateParticleSystems();       // Ativa os efeitos visuais de part�culas.
        DisableObjectMeshRenderer(objectToDisableMeshRenderer); // Desativa o MeshRenderer de objetos espec�ficos.
        DisableWeapons();                // Desativa as armas do jogador.
        Destroy(collision.gameObject);   // Destr�i o asteroide que causou o Game Over.
        PlayGameOverAudio();             // Reproduz o �udio relacionado ao Game Over.
    }

    /// <summary>
    /// Congela os scripts de inatividade.
    /// Este m�todo percorre todos os scripts 'Inativo' e congela o tempo de inatividade para parar qualquer a��o que n�o deva ocorrer durante o Game Over.
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
    /// Desativa os spawners de asteroides para que novos asteroides n�o sejam gerados ap�s o Game Over.
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
            gameOverUI.SetActive(true);         // Torna o painel de Game Over vis�vel.
            EnableAllChildren(gameOverUI);      // Garante que todos os filhos do painel de Game Over tamb�m sejam vis�veis.
        }
        else
        {
            Debug.LogWarning("GameOver UI n�o est� atribu�do."); // Exibe um aviso se o UI de Game Over n�o estiver atribu�do.
        }
    }

    /// <summary>
    /// Destroi todos os objetos com a tag de asteroide.
    /// Esse m�todo � respons�vel por limpar todos os asteroides da cena ap�s o Game Over.
    /// </summary>
    private void DestroyAllAsteroids()
    {
        foreach (GameObject asteroid in GameObject.FindGameObjectsWithTag(asteroidTag))
        {
            Destroy(asteroid); // Destroi o asteroide da cena.
        }
    }

    /// <summary>
    /// Ativa os efeitos de part�culas durante o Game Over.
    /// Este m�todo inst�ncia e ativa os prefabs de part�culas para criar efeitos visuais, como explos�es ou fa�scas.
    /// </summary>
    private void ActivateParticleSystems()
    {
        if (objectToDisableMeshRenderer != null)
        {
            Vector3 position = objectToDisableMeshRenderer.transform.position; // Posi��o do objeto.
            Quaternion rotation = objectToDisableMeshRenderer.transform.rotation; // Rota��o do objeto.

            foreach (var prefab in particleSystemPrefabs)
            {
                if (prefab != null)
                {
                    ParticleSystem particleClone = Instantiate(prefab, position, rotation); // Cria uma c�pia do prefab da part�cula.
                    particleClone.Play(); // Reproduz o sistema de part�culas.
                    Destroy(particleClone.gameObject, particleClone.main.duration); // Destroi o objeto de part�cula ap�s a dura��o.
                }
            }
        }
        else
        {
            Debug.LogWarning("Objeto para part�culas n�o est� atribu�do."); // Exibe um aviso se o objeto de part�culas n�o estiver atribu�do.
        }
    }

    /// <summary>
    /// Desativa o MeshRenderer de um objeto.
    /// Este m�todo desativa o componente MeshRenderer, fazendo com que o objeto se torne invis�vel.
    /// </summary>
    private void DisableObjectMeshRenderer(GameObject obj)
    {
        if (obj != null)
        {
            MeshRenderer childMeshRenderer = obj.GetComponentInChildren<MeshRenderer>(); // Obt�m o MeshRenderer do filho.
            if (childMeshRenderer != null)
            {
                childMeshRenderer.enabled = false; // Desativa o MeshRenderer.
            }
        }
    }

    /// <summary>
    /// Desativa os componentes das armas do jogador.
    /// Este m�todo desativa os MeshRenderers, BoxColliders e filhos das armas, tornando-as inutiliz�veis ap�s o Game Over.
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
    /// Desativa componentes espec�ficos (MeshRenderer ou BoxCollider) em todos os filhos de um objeto.
    /// </summary>
    private void DisableComponentsInChildren<T>(GameObject obj) where T : Component
    {
        T[] components = obj.GetComponentsInChildren<T>(); // Obt�m todos os componentes do tipo especificado nos filhos.
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
    /// Reproduz o �udio relacionado ao Game Over.
    /// Este m�todo para a m�sica principal e toca o �udio do Game Over.
    /// </summary>
    private void PlayGameOverAudio()
    {
        if (audioPlayer != null)
        {
            audioPlayer.StopMainGameAudio(); // Para o �udio principal.
            audioPlayer.PlayGameOverAudio(); // Toca o �udio do Game Over.
        }
        else
        {
            Debug.LogWarning("AudioPlayer n�o est� atribu�do."); // Exibe um aviso se o AudioPlayer n�o estiver atribu�do.
        }
    }

    /// <summary>
    /// Ativa todos os filhos de um objeto.
    /// Esse m�todo garante que todos os filhos do objeto sejam vis�veis (usado para a UI de Game Over).
    /// </summary>
    private void EnableAllChildren(GameObject parent)
    {
        foreach (Transform child in parent.transform)
        {
            child.gameObject.SetActive(true); // Torna o filho vis�vel.
            EnableAllChildren(child.gameObject); // Recursivamente ativa os filhos.
        }
    }
}
