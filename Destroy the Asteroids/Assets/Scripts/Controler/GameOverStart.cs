using UnityEngine;

public class GameOverStart : MonoBehaviour
{
    [Header("Player Dano")]
    [Tooltip("Referência ao script PlayerDano.")]
    [SerializeField] private PlayerDano playerDano;

    [Header("Audio Player")]
    [Tooltip("Referência ao script AudioPlayer.")]
    [SerializeField] private AudioPlayer audioPlayer;

    [Header("Spawners a serem ativados")]
    [Tooltip("Referências aos scripts de Spawner que serão ativados.")]
    [SerializeField] private AsteroidSpawner[] asteroidSpawnerScripts;

    [Header("Sistema Particulas")]
    [Tooltip("Referência ao ParticleSystem que será ativado.")]
    [SerializeField] private ParticleSystem Particulas;

    [Header("Game Controller")]
    [Tooltip("Referência ao script GameController.")]
    [SerializeField] private PontoController gameController;

    [Header("Objetos Inativos")]
    [Tooltip("Referências aos objetos inativos que devem começar a contagem.")]
    [SerializeField] private Inativo[] objetosInativos;

    [Header("Mesh Renderer")]
    [Tooltip("Referência ao MeshRenderer do objeto filho.")]
    [SerializeField] private MeshRenderer childMeshRenderer;

    [Header("Armas")]
    [Tooltip("Referências aos objetos de armas que devem ser ativados.")]
    [SerializeField] private GameObject[] armas;

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
        // Reseta a vida do jogador
        if (playerDano != null)
        {
            playerDano.ResetarVida();
        }
        else
        {
            Debug.LogError("PlayerDano não foi atribuído corretamente no GameOverStart!");
        }

        // Ativa Spawners de Asteroides
        foreach (var spawner in asteroidSpawnerScripts)
        {
            if (spawner != null)
            {
                spawner.enabled = true;
            }
        }

        // Toca o áudio principal
        if (audioPlayer != null)
        {
            audioPlayer.PlayRandomMainGameAudio();
        }

        // Ativa sistema de partículas
        if (GetComponent<ParticleSystem>() != null)
        {
            GetComponent<ParticleSystem>().Play();
        }

        // Habilita MeshRenderer do objeto filho
        if (childMeshRenderer != null)
        {
            childMeshRenderer.enabled = true;
        }

        // Ativa armas
        AtivarArmas();

        // Ativa objetos inativos
        foreach (var objInativo in objetosInativos)
        {
            if (objInativo != null)
            {
                objInativo.NotificarInicioDoJogo();
                objInativo.enabled = true;
            }
        }

        // Move o objeto "Records" para a posição fixa
        GameObject recordsObj = GameObject.Find("Records");
        if (recordsObj != null)
        {
            recordsObj.transform.position = new Vector3(-5.2f, -8.9f, 5.1f);
        }
        else
        {
            Debug.LogWarning("Objeto 'Records' não encontrado na cena.");
        }

        // Desativa este script após reiniciar o jogo
        gameObject.SetActive(false);
    }

    private void AtivarArmas()
    {
        foreach (var arma in armas)
        {
            if (arma != null)
            {
                arma.SetActive(true);
                AtivarMeshRenderersDosFilhos(arma);

                if (arma.TryGetComponent<Inativo>(out Inativo inativoScript))
                {
                    inativoScript.enabled = true;
                }
            }
        }
    }

    private void AtivarMeshRenderersDosFilhos(GameObject arma)
    {
        MeshRenderer[] meshRenderers = arma.GetComponentsInChildren<MeshRenderer>(true);

        foreach (var meshRenderer in meshRenderers)
        {
            meshRenderer.enabled = true;
        }
    }

    private void ValidateReferences()
    {
        if (playerDano == null)
            Debug.LogWarning("PlayerDano não está atribuído!");

        if (gameController == null)
            Debug.LogWarning("GameController não está atribuído!");

        if (audioPlayer == null)
            Debug.LogWarning("AudioPlayer não está atribuído!");

        if (asteroidSpawnerScripts.Length == 0)
            Debug.LogWarning("Nenhum AsteroidSpawner foi atribuído!");

        if (objetosInativos.Length == 0)
            Debug.LogWarning("Nenhum objeto inativo foi atribuído!");

        if (armas.Length == 0)
            Debug.LogWarning("Nenhuma arma foi atribuída!");
    }
}
