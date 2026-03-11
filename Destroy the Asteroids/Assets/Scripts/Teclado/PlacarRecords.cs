using UnityEngine;

public class PlacarRecords : MonoBehaviour
{
    [Header("Fonte da Pontuação")]
    [SerializeField] private PontoController pontoController;

    [Header("Persistência dos records")]
    [SerializeField] private SaveRecords saveRecords;

    private int pontuacaoFinalPendente = -1;
    private bool aguardandoNome = false;

    private void Start()
    {
        if (saveRecords == null)
        {
            return;
        }

        saveRecords.CarregarRecordsNoPlacar();
    }

    /// <summary>
    /// Chame essa função quando o jogo acabar.
    /// Ela captura a pontuação final da partida e deixa pronta para salvar.
    /// </summary>
    public void RegistrarPontuacaoFinal()
    {
        if (pontoController == null)
        {
            return;
        }

        pontuacaoFinalPendente = pontoController.GetPontosFinais();
        aguardandoNome = true;

    }

    /// <summary>
    /// O teclado deve chamar essa função passando o nome digitado.
    /// Exemplo:
    /// placarRecords.ConfirmarNomeDoJogador("RANIE");
    /// </summary>
    public void ConfirmarNomeDoJogador(string nomeJogador)
    {
        if (saveRecords == null)
        {
            return;
        }

        if (!aguardandoNome)
        {
            return;
        }

        if (pontuacaoFinalPendente < 0)
        {
            return;
        }

        if (string.IsNullOrWhiteSpace(nomeJogador))
            nomeJogador = "Name";

        saveRecords.RegistrarNovoRecord(nomeJogador.Trim(), pontuacaoFinalPendente);


        pontuacaoFinalPendente = -1;
        aguardandoNome = false;
    }

    /// <summary>
    /// Cancela a pontuação pendente atual.
    /// </summary>
    public void CancelarPontuacaoPendente()
    {
        pontuacaoFinalPendente = -1;
        aguardandoNome = false;

    }

    /// <summary>
    /// Retorna se existe pontuação aguardando nome.
    /// </summary>
    public bool EstaAguardandoNome()
    {
        return aguardandoNome;
    }

    /// <summary>
    /// Retorna a pontuação final pendente.
    /// </summary>
    public int GetPontuacaoFinalPendente()
    {
        return pontuacaoFinalPendente;
    }

    /// <summary>
    /// Recarrega manualmente o placar salvo.
    /// </summary>
    public void RecarregarPlacar()
    {
        if (saveRecords == null)
        {
            return;
        }

        saveRecords.CarregarRecordsNoPlacar();
    }
}