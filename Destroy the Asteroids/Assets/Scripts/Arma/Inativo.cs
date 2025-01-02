using UnityEngine;
using System.Collections;

public class Inativo : MonoBehaviour
{
    [Header("Configurações de Inatividade")]
    [Tooltip("Tempo de inatividade antes da ativação.")]
    [SerializeField] private float tempoDeInatividade = 5f;

    private bool jogoFinalizado = false; // Indica se o jogo foi finalizado
    private float spawnTimer; // Timer para controlar o tempo de inatividade
    private bool contagemEmAndamento = false; // Flag para evitar múltiplas corrotinas

    private MeshRenderer meshRenderer;
    private BoxCollider boxCollider;
    private Canvas canvasFilho;
    private Transform[] filhos;

    [Header("Configuração de Áudio")]
    [Tooltip("Áudio reproduzido quando o tempo de inatividade chega a zero.")]
    [SerializeField] private AudioClip ativacaoSound;
    private AudioSource audioSource;

    private void Awake()
    {
        InicializarComponentes();
        DesativarObjetos();
    }

    private void InicializarComponentes()
    {
        meshRenderer = GetComponent<MeshRenderer>();
        boxCollider = GetComponent<BoxCollider>();
        canvasFilho = GetComponentInChildren<Canvas>();
        audioSource = GetComponent<AudioSource>() ?? gameObject.AddComponent<AudioSource>();

        filhos = GetComponentsInChildren<Transform>();
    }

    private void DesativarObjetos()
    {
        if (meshRenderer != null) meshRenderer.enabled = false;
        if (boxCollider != null) boxCollider.enabled = false;

        foreach (var filho in filhos)
        {
            if (filho != transform) filho.gameObject.SetActive(false);
        }
    }

    public void Configurar(float tempo)
    {
        tempoDeInatividade = tempo;
    }

    public void IniciarContagem()
    {
        if (!gameObject.activeInHierarchy || jogoFinalizado || contagemEmAndamento) return;

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

        contagemEmAndamento = false; // Permite reiniciar contagem posteriormente
    }

    private void AtivarObjetos()
    {
        if (meshRenderer != null) meshRenderer.enabled = true;
        if (boxCollider != null) boxCollider.enabled = true;

        foreach (var filho in filhos)
        {
            if (filho != transform) filho.gameObject.SetActive(true);
        }

        PlayActivationSound();
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
    }

    public void ReiniciarTempoDeInatividade()
    {
        jogoFinalizado = false;
        IniciarContagem();
    }

    public void GameOverStart()
    {
        jogoFinalizado = false;
        contagemEmAndamento = false; // Garante que podemos reiniciar a contagem
        IniciarContagem();
    }
}
