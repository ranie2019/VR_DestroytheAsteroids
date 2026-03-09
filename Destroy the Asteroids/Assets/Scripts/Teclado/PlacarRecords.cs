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
            Debug.LogError("⚠️ PlacarRecords: SaveRecords não foi atribuído.");
            return;
        }

        saveRecords.CarregarRecordsNoPlacar();
        Debug.Log("📥 Placar carregado no início.");
    }

    /// <summary>
    /// Chame essa função quando o jogo acabar.
    /// Ela captura a pontuação final da partida e deixa pronta para salvar.
    /// </summary>
    public void RegistrarPontuacaoFinal()
    {
        if (pontoController == null)
        {
            Debug.LogError("⚠️ PlacarRecords: PontoController não foi atribuído.");
            return;
        }

        pontuacaoFinalPendente = pontoController.GetPontosFinais();
        aguardandoNome = true;

        Debug.Log($"✅ Pontuação final capturada: {pontuacaoFinalPendente}");
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
            Debug.LogError("⚠️ PlacarRecords: SaveRecords não foi atribuído.");
            return;
        }

        if (!aguardandoNome)
        {
            Debug.LogWarning("⚠️ PlacarRecords: não existe pontuação pendente para salvar.");
            return;
        }

        if (pontuacaoFinalPendente < 0)
        {
            Debug.LogWarning("⚠️ PlacarRecords: pontuação pendente inválida.");
            return;
        }

        if (string.IsNullOrWhiteSpace(nomeJogador))
            nomeJogador = "Name";

        saveRecords.RegistrarNovoRecord(nomeJogador.Trim(), pontuacaoFinalPendente);

        Debug.Log($"💾 Record salvo: {nomeJogador} - {pontuacaoFinalPendente}");

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

        Debug.Log("❌ Pontuação pendente cancelada.");
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
            Debug.LogWarning("⚠️ PlacarRecords: SaveRecords não foi atribuído.");
            return;
        }

        saveRecords.CarregarRecordsNoPlacar();
        Debug.Log("🔄 Placar recarregado manualmente.");
    }
}