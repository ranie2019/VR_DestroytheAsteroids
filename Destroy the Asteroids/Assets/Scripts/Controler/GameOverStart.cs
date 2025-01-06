using UnityEngine;

public class GameOverStart : MonoBehaviour
{
    [Header("Audio Player")]
    [Tooltip("Refer�ncia ao script AudioPlayer.")]
    [SerializeField] private AudioPlayer audioPlayer;

    [Header("Spawners a serem ativados")]
    [Tooltip("Refer�ncias aos scripts de Spawner que ser�o ativados.")]
    [SerializeField] private AsteroidSpawner[] asteroidSpawnerScripts;

    [Header("Particle System")]
    [Tooltip("Refer�ncia ao ParticleSystem que ser� ativado.")]
    [SerializeField] private ParticleSystem particleSystem;

    [Header("Game Controller")]
    [Tooltip("Refer�ncia ao script GameController.")]
    [SerializeField] private GameController gameController;

    [Header("Objetos Inativos")]
    [Tooltip("Refer�ncias aos objetos inativos que devem come�ar a contagem.")]
    [SerializeField] private Inativo[] objetosInativos;

    [Header("Mesh Renderer")]
    [Tooltip("Refer�ncia ao MeshRenderer do objeto filho.")]
    [SerializeField] private MeshRenderer childMeshRenderer;

    [Header("Armas")]
    [Tooltip("Refer�ncias aos objetos de armas que devem ser ativados.")]
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
        // Reseta a pontua��o do jogador
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

        // Troca o �udio de introdu��o para o �udio principal do jogo
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

        // Ativa as armas (caso j� estejam atribu�das como GameObjects)
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

        // Desativa o objeto pai (caso necess�rio)
        if (transform.parent != null)
        {
            var parentCollider = transform.parent.GetComponent<Collider>();
            if (parentCollider != null)
            {
                parentCollider.enabled = false;
            }
        }
    }

    // M�todo para ativar as armas
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
            Debug.LogWarning("GameController n�o est� atribu�do!");

        if (audioPlayer == null)
            Debug.LogWarning("AudioPlayer n�o est� atribu�do!");

        if (particleSystem == null)
            Debug.LogWarning("ParticleSystem n�o est� atribu�do!");

        if (childMeshRenderer == null)
            Debug.LogWarning("Child MeshRenderer n�o est� atribu�do!");

        if (asteroidSpawnerScripts.Length == 0)
            Debug.LogWarning("Nenhum AsteroidSpawner foi atribu�do!");

        if (objetosInativos.Length == 0)
            Debug.LogWarning("Nenhum objeto inativo foi atribu�do!");

        if (armas.Length == 0)
            Debug.LogWarning("Nenhuma arma foi atribu�da!");
    }
}
