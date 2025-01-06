using UnityEngine;

public class GameOverStart : MonoBehaviour
{
    [Header("Audio Player")]
    [Tooltip("Referência ao script AudioPlayer.")]
    [SerializeField] private AudioPlayer audioPlayer;

    [Header("Spawners a serem ativados")]
    [Tooltip("Referências aos scripts de Spawner que serão ativados.")]
    [SerializeField] private AsteroidSpawner[] asteroidSpawnerScripts;

    [Header("Particle System")]
    [Tooltip("Referência ao ParticleSystem que será ativado.")]
    [SerializeField] private ParticleSystem particleSystem;

    [Header("Game Controller")]
    [Tooltip("Referência ao script GameController.")]
    [SerializeField] private GameController gameController;

    [Header("Objetos Inativos")]
    [Tooltip("Referências aos objetos inativos que devem começar a contagem.")]
    [SerializeField] private Inativo[] objetosInativos;

    [Header("Mesh Renderer")]
    [Tooltip("Referência ao MeshRenderer do objeto filho.")]
    [SerializeField] private MeshRenderer childMeshRenderer;

    [Header("Armas")]
    [Tooltip("Referências aos objetos de armas que devem ser ativados.")]
    [SerializeField] private GameObject[] armas; // Armas que devem ser ativadas

    private void Start()
    {
        ValidateReferences();
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Laser"))
        {
            HandleCollisionWithLaser();
        }
    }

    private void HandleCollisionWithLaser()
    {
        // Reseta a pontuação do jogador
        if (gameController != null)
        {
            gameController.ResetScore();
        }

        // Ativa todos os Spawners
        foreach (var spawner in asteroidSpawnerScripts)
        {
            if (spawner != null)
            {
                spawner.enabled = true;
            }
        }

        // Troca o áudio de introdução para o áudio principal do jogo
        if (audioPlayer != null)
        {
            audioPlayer.PlayRandomMainGameAudio();
        }

        // Ativa o ParticleSystem
        if (particleSystem != null)
        {
            particleSystem.Play();
        }

        // Habilita o MeshRenderer do objeto filho
        if (childMeshRenderer != null)
        {
            childMeshRenderer.enabled = true;
        }

        // Ativa as armas (caso já estejam atribuídas como GameObjects)
        AtivarArmas();

        // Notifica os objetos inativos para iniciar a contagem e desativa o script Inativo
        foreach (var objInativo in objetosInativos)
        {
            if (objInativo != null)
            {
                objInativo.NotificarInicioDoJogo();
                objInativo.enabled = true; // Habilita o script Inativo no Inspector
            }
        }


        // Desativa o objeto GameOverStart
        gameObject.SetActive(false);

        // Desativa o objeto pai (caso necessário)
        if (transform.parent != null)
        {
            var parentCollider = transform.parent.GetComponent<Collider>();
            if (parentCollider != null)
            {
                parentCollider.enabled = false;
            }
        }
    }

    // Método para ativar as armas
    private void AtivarArmas()
    {
        foreach (var arma in armas)
        {
            if (arma != null)
            {
                arma.SetActive(true); // Ativa as armas, caso sejam GameObjects
            }

            // Habilita o script Inativo nos objetos de arma
            Inativo inativoScript = arma.GetComponent<Inativo>();
            if (inativoScript != null)
            {
                inativoScript.enabled = true;
            }
        }

        // Ativa todos os scripts desativados
        MonoBehaviour[] allScripts = FindObjectsOfType<MonoBehaviour>(true);
        foreach (var script in allScripts)
        {
            if (!script.enabled)
            {
                script.enabled = true;
            }
        }
    }

    private void ValidateReferences()
    {
        if (gameController == null)
            Debug.LogWarning("GameController não está atribuído!");

        if (audioPlayer == null)
            Debug.LogWarning("AudioPlayer não está atribuído!");

        if (particleSystem == null)
            Debug.LogWarning("ParticleSystem não está atribuído!");

        if (childMeshRenderer == null)
            Debug.LogWarning("Child MeshRenderer não está atribuído!");

        if (asteroidSpawnerScripts.Length == 0)
            Debug.LogWarning("Nenhum AsteroidSpawner foi atribuído!");

        if (objetosInativos.Length == 0)
            Debug.LogWarning("Nenhum objeto inativo foi atribuído!");

        if (armas.Length == 0)
            Debug.LogWarning("Nenhuma arma foi atribuída!");
    }
}
