using UnityEngine;
using System.Collections;
using UnityEngine.Events;

public class Inativo2 : MonoBehaviour
{
    [Header("Configura��es de Inatividade")]
    [Tooltip("Tempo de inatividade antes de mover o objeto.")]
    [SerializeField] private float tempoDeInatividade = 5f; // Tempo antes do movimento

    private bool jogoFinalizado = false; // Indica se o jogo foi pausado
    private float spawnTimer; // Timer para controlar a contagem regressiva
    private Coroutine contagemCorrotina; // Refer�ncia � corrotina ativa

    [Header("Configura��es de Posi��o")]
    [Tooltip("Posi��o inicial do objeto.")]
    [SerializeField] private Vector3 startPosition; // Posi��o inicial
    [Tooltip("Posi��o final do objeto.")]
    [SerializeField] private Vector3 endPosition; // Posi��o final

    [Header("Eventos")]
    [Tooltip("Evento disparado ao concluir o movimento.")]
    public UnityEvent onMoveComplete;

    private void Awake()
    {
        // Configura a posi��o inicial do objeto
        transform.localPosition = startPosition;
    }

    public void Configurar(float tempo)
    {
        // Permite ajustar o tempo de inatividade dinamicamente
        tempoDeInatividade = tempo;
    }

    public void IniciarContagem()
    {
        // Inicia a contagem regressiva para o movimento
        if (!gameObject.activeInHierarchy || jogoFinalizado || contagemCorrotina != null) return;

        spawnTimer = tempoDeInatividade;
        contagemCorrotina = StartCoroutine(ContagemRegressiva());
    }

    private IEnumerator ContagemRegressiva()
    {
        // Contagem regressiva antes de mover o objeto
        while (spawnTimer > 0 && !jogoFinalizado)
        {
            spawnTimer -= Time.deltaTime; // Decrementa o tempo
            yield return null; // Espera o pr�ximo frame
        }

        // Quando o tempo acaba, move o objeto para a posi��o final
        if (!jogoFinalizado)
        {
            MoverParaPosicaoFinal();
        }

        contagemCorrotina = null; // Reseta a refer�ncia da corrotina
    }

    private void MoverParaPosicaoFinal()
    {
        // Atualiza a posi��o do objeto para a posi��o final
        transform.localPosition = endPosition;

        // Dispara o evento de conclus�o do movimento
        onMoveComplete?.Invoke();
    }

    public void NotificarInicioDoJogo()
    {
        // Reinicia o estado para permitir movimento
        jogoFinalizado = false;
        IniciarContagem();
    }

    public void CongelarTempoDeInatividade()
    {
        // Pausa o temporizador de movimento
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
