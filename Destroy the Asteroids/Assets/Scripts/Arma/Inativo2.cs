using UnityEngine;
using System.Collections;
using UnityEngine.Events;

public class Inativo2 : MonoBehaviour
{
    [Header("Configura��es de Inatividade")]
    [Tooltip("Tempo de inatividade antes da ativa��o.")]
    [SerializeField] private float tempoDeInatividade = 5f; // Tempo antes da ativa��o do objeto

    private bool jogoFinalizado = false; // Indica se o jogo foi finalizado (pausa)
    private float spawnTimer; // Timer para controlar a contagem regressiva
    private Coroutine contagemCorrotina; // Refer�ncia � corrotina ativa

    private MeshRenderer meshRenderer;
    private BoxCollider boxCollider;
    private Transform[] filhos;

    [Header("Configura��o de �udio")]
    [Tooltip("�udio reproduzido quando o tempo de inatividade chega a zero.")]
    [SerializeField] private AudioClip ativacaoSound; // Som tocado na ativa��o
    private AudioSource audioSource;

    [Header("Configura��es de Anima��o")]
    [Tooltip("Posi��o inicial da arma.")]
    [SerializeField] private Vector3 startPosition; // Posi��o inicial da anima��o
    [Tooltip("Posi��o final da arma.")]
    [SerializeField] private Vector3 endPosition; // Posi��o final da anima��o
    [Tooltip("Tempo da anima��o.")]
    [SerializeField] private float animationDuration = 2f; // Dura��o da anima��o
    [Tooltip("Curva para suavizar a anima��o.")]
    [SerializeField] private AnimationCurve sizeCurve; // Curva para ajustar suavidade

    private Vector3 originalScale; // Escala original do objeto

    [Header("Efeitos Visuais e Eventos")]
    [Tooltip("Part�culas ativadas quando o objeto � ativado.")]
    [SerializeField] private ParticleSystem ativacaoParticle; // Part�culas na ativa��o
    public UnityEvent onActivated; // Evento chamado na ativa��o
    public UnityEvent onDeactivated; // Evento chamado na desativa��o

    private void Awake()
    {
        // Inicializa os componentes e configura o estado inicial do objeto
        InicializarComponentes();
        DesativarObjetos();
    }

    private void InicializarComponentes()
    {
        // Busca os componentes necess�rios
        meshRenderer = GetComponent<MeshRenderer>();
        boxCollider = GetComponent<BoxCollider>();
        audioSource = GetComponent<AudioSource>() ?? gameObject.AddComponent<AudioSource>();
        filhos = GetComponentsInChildren<Transform>();

        // Salva a escala original do objeto
        originalScale = transform.localScale;
    }

    private void DesativarObjetos()
    {
        // Desativa renderiza��o e colisor do objeto principal
        if (meshRenderer != null) meshRenderer.enabled = false;
        if (boxCollider != null) boxCollider.enabled = false;

        // Desativa todos os objetos filhos
        foreach (var filho in filhos)
        {
            if (filho != transform) filho.gameObject.SetActive(false);
        }

        // Define a posi��o inicial e escala reduzida
        transform.localPosition = startPosition;
        transform.localScale = Vector3.zero;

        // Dispara o evento de desativa��o
        onDeactivated?.Invoke();
    }

    public void Configurar(float tempo)
    {
        // Permite ajustar o tempo de inatividade dinamicamente
        tempoDeInatividade = tempo;
    }

    public void IniciarContagem()
    {
        // Inicia a contagem regressiva para ativa��o
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
            yield return null; // Espera o pr�ximo frame
        }

        // Quando o tempo acaba, inicia a anima��o de ativa��o
        if (!jogoFinalizado)
        {
            StartCoroutine(AnimarAtivacao());
        }

        contagemCorrotina = null; // Reseta a refer�ncia da corrotina
    }

    private IEnumerator AnimarAtivacao()
    {
        float elapsedTime = 0f;

        // Realiza a anima��o de movimento e redimensionamento
        while (elapsedTime < animationDuration)
        {
            elapsedTime += Time.deltaTime;
            float progress = Mathf.SmoothStep(0f, 1f, elapsedTime / animationDuration);

            // Atualiza posi��o e escala com base no progresso
            transform.localPosition = Vector3.Lerp(startPosition, endPosition, progress);
            transform.localScale = Vector3.Lerp(Vector3.zero, originalScale, sizeCurve.Evaluate(progress));

            yield return null; // Espera o pr�ximo frame
        }

        // Garante que os valores finais sejam precisos
        transform.localPosition = endPosition;
        transform.localScale = originalScale;

        AtivarObjetos(); // Ativa os objetos ap�s a anima��o
    }

    private void AtivarObjetos()
    {
        // Ativa renderiza��o e colisor do objeto principal
        if (meshRenderer != null) meshRenderer.enabled = true;
        if (boxCollider != null) boxCollider.enabled = true;

        // Ativa todos os objetos filhos
        foreach (var filho in filhos)
        {
            if (filho != transform) filho.gameObject.SetActive(true);
        }

        // Dispara o evento de ativa��o
        onActivated?.Invoke();

        // Toca o som de ativa��o
        PlayActivationSound();

        // Exibe part�culas, se configuradas
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
        // Reinicia o estado para permitir ativa��o
        jogoFinalizado = false;
        IniciarContagem();
    }

    public void CongelarTempoDeInatividade()
    {
        // Pausa o temporizador de ativa��o
        jogoFinalizado = true;
    }

    public void ReiniciarTempoDeInatividade()
    {
        // Reinicia o temporizador ap�s pausa
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
