using UnityEngine;
using System.Collections;

public class Inativo : MonoBehaviour
{
    [Header("Configurações de Inatividade")]
    [Tooltip("Tempo de inatividade antes da ativação.")]
    [SerializeField] private float tempoDeInatividade = 5f;

    [Header("Travamento absoluto")]
    [Tooltip("Enquanto o jogo não começar, trava totalmente posição e rotação.")]
    [SerializeField] private bool travarTransformTotalmente = true;

    [Tooltip("Se true, trava também o Rigidbody enquanto estiver inativo.")]
    [SerializeField] private bool travarRigidbody = true;

    private bool jogoFinalizado = false;
    private bool jogoIniciado = false;
    private float spawnTimer;
    private bool contagemEmAndamento = false;

    private MeshRenderer meshRenderer;
    private BoxCollider boxCollider;
    private Canvas canvasFilho;
    private Transform[] filhos;

    [Header("Configuração de Áudio")]
    [Tooltip("Áudio reproduzido quando o tempo de inatividade chega a zero.")]
    [SerializeField] private AudioClip ativacaoSound;
    private AudioSource audioSource;

    private Rigidbody rb;

    // posição/rotação travadas
    private Vector3 posicaoInicial;
    private Quaternion rotacaoInicial;

    // constraints originais do rigidbody
    private RigidbodyConstraints rbConstraintsOriginais;
    private bool rbKinematicOriginal;

    private void Awake()
    {
        InicializarComponentes();

        posicaoInicial = transform.position;
        rotacaoInicial = transform.rotation;

        if (rb != null)
        {
            rbConstraintsOriginais = rb.constraints;
            rbKinematicOriginal = rb.isKinematic;
        }

        DesativarObjetos();
        AplicarTravamentoTotal();
    }

    private void InicializarComponentes()
    {
        meshRenderer = GetComponent<MeshRenderer>();
        boxCollider = GetComponent<BoxCollider>();
        canvasFilho = GetComponentInChildren<Canvas>(true);
        audioSource = GetComponent<AudioSource>() ?? gameObject.AddComponent<AudioSource>();
        rb = GetComponent<Rigidbody>();

        filhos = GetComponentsInChildren<Transform>(true);
    }

    private void DesativarObjetos()
    {
        if (meshRenderer != null) meshRenderer.enabled = false;
        if (boxCollider != null) boxCollider.enabled = false;
        if (canvasFilho != null) canvasFilho.enabled = false;

        foreach (var filho in filhos)
        {
            if (filho != transform)
                filho.gameObject.SetActive(false);
        }
    }

    private void AtivarObjetos()
    {
        if (meshRenderer != null) meshRenderer.enabled = true;
        if (boxCollider != null) boxCollider.enabled = true;
        if (canvasFilho != null) canvasFilho.enabled = true;

        foreach (var filho in filhos)
        {
            if (filho != transform)
                filho.gameObject.SetActive(true);
        }

        LiberarTravamentoTotal();
        PlayActivationSound();
    }

    private void AplicarTravamentoTotal()
    {
        jogoIniciado = false;

        if (travarTransformTotalmente)
        {
            transform.position = posicaoInicial;
            transform.rotation = rotacaoInicial;
        }

        if (rb != null && travarRigidbody)
        {
            rb.linearVelocity = Vector3.zero;
            rb.angularVelocity = Vector3.zero;
            rb.isKinematic = true;
            rb.constraints = RigidbodyConstraints.FreezeAll;
        }
    }

    private void LiberarTravamentoTotal()
    {
        jogoIniciado = true;

        if (rb != null && travarRigidbody)
        {
            rb.isKinematic = rbKinematicOriginal;
            rb.constraints = rbConstraintsOriginais;
            rb.linearVelocity = Vector3.zero;
            rb.angularVelocity = Vector3.zero;
        }
    }

    private void LateUpdate()
    {
        // enquanto o jogo não começou, força voltar pro lugar
        if (!jogoIniciado && travarTransformTotalmente)
        {
            transform.position = posicaoInicial;
            transform.rotation = rotacaoInicial;

            if (rb != null && travarRigidbody)
            {
                rb.linearVelocity = Vector3.zero;
                rb.angularVelocity = Vector3.zero;
            }
        }
    }

    public void Configurar(float tempo)
    {
        tempoDeInatividade = tempo;
    }

    public void IniciarContagem()
    {
        if (!gameObject.activeInHierarchy || jogoFinalizado || contagemEmAndamento)
            return;

        contagemEmAndamento = true;
        spawnTimer = tempoDeInatividade;
        StartCoroutine(ContagemRegressiva());
    }

    private IEnumerator ContagemRegressiva()
    {
        while (spawnTimer > 0 && !jogoFinalizado)
        {
            spawnTimer -= Time.deltaTime;
            yield return null;
        }

        if (!jogoFinalizado)
        {
            AtivarObjetos();
        }

        contagemEmAndamento = false;
    }

    private void PlayActivationSound()
    {
        if (audioSource != null && ativacaoSound != null)
        {
            audioSource.PlayOneShot(ativacaoSound);
        }
    }

    public void NotificarInicioDoJogo()
    {
        jogoFinalizado = false;
        IniciarContagem();
    }

    public void CongelarTempoDeInatividade()
    {
        jogoFinalizado = true;
        AplicarTravamentoTotal();
    }

    public void ReiniciarTempoDeInatividade()
    {
        jogoFinalizado = false;
        AplicarTravamentoTotal();
        IniciarContagem();
    }

    public void GameOverStart()
    {
        jogoFinalizado = false;
        contagemEmAndamento = false;
        AplicarTravamentoTotal();
        IniciarContagem();
    }
}