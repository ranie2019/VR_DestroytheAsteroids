using UnityEngine;
using System.Collections;
using UnityEngine.Events;

public class Inativo2 : MonoBehaviour
{
    [Header("Configurações de Inatividade")]
    [Tooltip("Tempo de inatividade antes da ativação.")]
    [SerializeField] private float tempoDeInatividade = 5f; // Tempo antes da ativação do objeto

    private bool jogoFinalizado = false; // Indica se o jogo foi finalizado (pausa)
    private float spawnTimer; // Timer para controlar a contagem regressiva
    private Coroutine contagemCorrotina; // Referência à corrotina ativa

    private MeshRenderer meshRenderer;
    private BoxCollider boxCollider;
    private Transform[] filhos;

    [Header("Configuração de Áudio")]
    [Tooltip("Áudio reproduzido quando o tempo de inatividade chega a zero.")]
    [SerializeField] private AudioClip ativacaoSound; // Som tocado na ativação
    private AudioSource audioSource;

    [Header("Configurações de Animação")]
    [Tooltip("Posição inicial da arma.")]
    [SerializeField] private Vector3 startPosition; // Posição inicial da animação
    [Tooltip("Posição final da arma.")]
    [SerializeField] private Vector3 endPosition; // Posição final da animação
    [Tooltip("Tempo da animação.")]
    [SerializeField] private float animationDuration = 2f; // Duração da animação
    [Tooltip("Curva para suavizar a animação.")]
    [SerializeField] private AnimationCurve sizeCurve; // Curva para ajustar suavidade

    private Vector3 originalScale; // Escala original do objeto

    [Header("Efeitos Visuais e Eventos")]
    [Tooltip("Partículas ativadas quando o objeto é ativado.")]
    [SerializeField] private ParticleSystem ativacaoParticle; // Partículas na ativação
    public UnityEvent onActivated; // Evento chamado na ativação
    public UnityEvent onDeactivated; // Evento chamado na desativação

    private void Awake()
    {
        // Inicializa os componentes e configura o estado inicial do objeto
        InicializarComponentes();
        DesativarObjetos();
    }

    private void InicializarComponentes()
    {
        // Busca os componentes necessários
        meshRenderer = GetComponent<MeshRenderer>();
        boxCollider = GetComponent<BoxCollider>();
        audioSource = GetComponent<AudioSource>() ?? gameObject.AddComponent<AudioSource>();
        filhos = GetComponentsInChildren<Transform>();

        // Salva a escala original do objeto
        originalScale = transform.localScale;
    }

    private void DesativarObjetos()
    {
        // Desativa renderização e colisor do objeto principal
        if (meshRenderer != null) meshRenderer.enabled = false;
        if (boxCollider != null) boxCollider.enabled = false;

        // Desativa todos os objetos filhos
        foreach (var filho in filhos)
        {
            if (filho != transform) filho.gameObject.SetActive(false);
        }

        // Define a posição inicial e escala reduzida
        transform.localPosition = startPosition;
        transform.localScale = Vector3.zero;

        // Dispara o evento de desativação
        onDeactivated?.Invoke();
    }

    public void Configurar(float tempo)
    {
        // Permite ajustar o tempo de inatividade dinamicamente
        tempoDeInatividade = tempo;
    }

    public void IniciarContagem()
    {
        // Inicia a contagem regressiva para ativação
        if (!gameObject.activeInHierarchy || jogoFinalizado || contagemCorrotina != null) return;

        spawnTimer = tempoDeInatividade;
        contagemCorrotina = StartCoroutine(ContagemRegressiva());
    }

    private IEnumerator ContagemRegressiva()
    {
        // Contagem regressiva antes de ativar o objeto
        while (spawnTimer > 0 && !jogoFinalizado)
        {
            spawnTimer -= Time.deltaTime; // Decrementa o tempo
            yield return null; // Espera o próximo frame
        }

        // Quando o tempo acaba, inicia a animação de ativação
        if (!jogoFinalizado)
        {
            StartCoroutine(AnimarAtivacao());
        }

        contagemCorrotina = null; // Reseta a referência da corrotina
    }

    private IEnumerator AnimarAtivacao()
    {
        float elapsedTime = 0f;

        // Realiza a animação de movimento e redimensionamento
        while (elapsedTime < animationDuration)
        {
            elapsedTime += Time.deltaTime;
            float progress = Mathf.SmoothStep(0f, 1f, elapsedTime / animationDuration);

            // Atualiza posição e escala com base no progresso
            transform.localPosition = Vector3.Lerp(startPosition, endPosition, progress);
            transform.localScale = Vector3.Lerp(Vector3.zero, originalScale, sizeCurve.Evaluate(progress));

            yield return null; // Espera o próximo frame
        }

        // Garante que os valores finais sejam precisos
        transform.localPosition = endPosition;
        transform.localScale = originalScale;

        AtivarObjetos(); // Ativa os objetos após a animação
    }

    private void AtivarObjetos()
    {
        // Ativa renderização e colisor do objeto principal
        if (meshRenderer != null) meshRenderer.enabled = true;
        if (boxCollider != null) boxCollider.enabled = true;

        // Ativa todos os objetos filhos
        foreach (var filho in filhos)
        {
            if (filho != transform) filho.gameObject.SetActive(true);
        }

        // Dispara o evento de ativação
        onActivated?.Invoke();

        // Toca o som de ativação
        PlayActivationSound();

        // Exibe partículas, se configuradas
        PlayActivationEffect();
    }

    private void PlayActivationSound()
    {
        if (audioSource != null && ativacaoSound != null)
        {
            audioSource.PlayOneShot(ativacaoSound);
        }
    }

    private void PlayActivationEffect()
    {
        if (ativacaoParticle != null)
        {
            ativacaoParticle.Play();
        }
    }

    public void NotificarInicioDoJogo()
    {
        // Reinicia o estado para permitir ativação
        jogoFinalizado = false;
        IniciarContagem();
    }

    public void CongelarTempoDeInatividade()
    {
        // Pausa o temporizador de ativação
        jogoFinalizado = true;
    }

    public void ReiniciarTempoDeInatividade()
    {
        // Reinicia o temporizador após pausa
        jogoFinalizado = false;
        IniciarContagem();
    }

    public void GameOverStart()
    {
        // Reseta o temporizador e reinicia a contagem
        jogoFinalizado = false;
        if (contagemCorrotina != null)
            StopCoroutine(contagemCorrotina);

        contagemCorrotina = null;
        IniciarContagem();
    }
}
