using UnityEngine;
using TMPro;

public class PontoController : MonoBehaviour
{
    [Header("Referência do placar principal (UI)")]
    [SerializeField] private TextMeshProUGUI placarTexto;

    [Header("Referência do recorde (UI)")]
    [SerializeField] private TextMeshProUGUI recordeTexto;

    [Header("Compatibilidade antiga")]
    [SerializeField] private GameObject[] recordesUI;

    [Header("Configuração")]
    [SerializeField] private bool resetarPontuacaoAoIniciar = true;
    [SerializeField] private string chaveRecordeSalvo = "recordeSalvo";

    private int pontosAtuais = 0;
    private int maiorPontuacao = 0;

    // Mantido apenas para compatibilidade com scripts antigos
    private int ultimoIndiceSalvo = -1;

    private void Start()
    {
        if (resetarPontuacaoAoIniciar)
            pontosAtuais = 0;

        maiorPontuacao = PlayerPrefs.GetInt(chaveRecordeSalvo, 0);

        AtualizarPlacarUI();
        AtualizarRecordeUI();

    }

    /// <summary>
    /// Adiciona pontos à pontuação atual do jogador.
    /// </summary>
    public void AddPontos(int pontos)
    {
        pontosAtuais += pontos;

        if (pontosAtuais < 0)
            pontosAtuais = 0;

        AtualizarPlacarUI();

        if (pontosAtuais > maiorPontuacao)
        {
            maiorPontuacao = pontosAtuais;
            PlayerPrefs.SetInt(chaveRecordeSalvo, maiorPontuacao);
            PlayerPrefs.Save();

            AtualizarRecordeUI();

        }
    }

    /// <summary>
    /// Compatibilidade com chamadas antigas.
    /// </summary>
    public void UpdatePlayerScore(int pontos)
    {
        AddPontos(pontos);
    }

    /// <summary>
    /// Atualiza o texto da pontuação atual na UI.
    /// </summary>
    private void AtualizarPlacarUI()
    {
        if (placarTexto != null)
            placarTexto.text = pontosAtuais.ToString("N0");
    }

    /// <summary>
    /// Atualiza o texto do recorde na UI.
    /// </summary>
    private void AtualizarRecordeUI()
    {
        if (recordeTexto != null)
            recordeTexto.text = maiorPontuacao.ToString("N0");
    }

    /// <summary>
    /// Retorna a pontuação final da partida.
    /// </summary>
    public int GetPontosFinais()
    {
        return pontosAtuais;
    }

    /// <summary>
    /// Retorna a pontuação atual.
    /// </summary>
    public int GetPontuacaoAtual()
    {
        return pontosAtuais;
    }

    /// <summary>
    /// Retorna o recorde atual salvo.
    /// </summary>
    public int GetMaiorPontuacao()
    {
        return maiorPontuacao;
    }

    /// <summary>
    /// Define manualmente a pontuação atual.
    /// Útil para testes ou integração com outros sistemas.
    /// </summary>
    public void SetPontuacaoAtual(int novaPontuacao)
    {
        pontosAtuais = Mathf.Max(0, novaPontuacao);

        AtualizarPlacarUI();

        if (pontosAtuais > maiorPontuacao)
        {
            maiorPontuacao = pontosAtuais;
            PlayerPrefs.SetInt(chaveRecordeSalvo, maiorPontuacao);
            PlayerPrefs.Save();

            AtualizarRecordeUI();
        }

    }

    /// <summary>
    /// Reseta a pontuação atual da partida.
    /// </summary>
    public void ResetarPontuacao()
    {
        pontosAtuais = 0;
        AtualizarPlacarUI();

    }

    /// <summary>
    /// Reseta o recorde salvo.
    /// Use apenas para teste.
    /// </summary>
    [ContextMenu("Resetar Recorde Salvo")]
    public void ResetarRecorde()
    {
        maiorPontuacao = 0;
        PlayerPrefs.SetInt(chaveRecordeSalvo, 0);
        PlayerPrefs.Save();

        AtualizarRecordeUI();

    }

    /// <summary>
    /// Atualiza manualmente os textos da UI.
    /// </summary>
    public void AtualizarTodaUI()
    {
        AtualizarPlacarUI();
        AtualizarRecordeUI();

    }

    /// <summary>
    /// Mantido só para compatibilidade com scripts antigos.
    /// No sistema novo ele não controla mais o ranking.
    /// </summary>
    public void PlacarOffline()
    {
        ultimoIndiceSalvo = -1;
    }

    /// <summary>
    /// Mantido só para compatibilidade com scripts antigos.
    /// </summary>
    public void SetNomePlayer(string nome)
    {

    }

    /// <summary>
    /// Mantido só para compatibilidade com scripts antigos.
    /// Não deve mais ser usado como lógica principal.
    /// </summary>
    public int GetUltimoIndiceSalvo()
    {
        return ultimoIndiceSalvo;
    }

    /// <summary>
    /// Mantido só para compatibilidade com scripts antigos.
    /// </summary>
    public GameObject GetRecordObject(int indice)
    {
        if (recordesUI != null && indice >= 0 && indice < recordesUI.Length)
            return recordesUI[indice];

        return null;
    }

    /// <summary>
    /// Debug simples.
    /// </summary>
    public void MostrarPontuacaoNaTela()
    {

    }

    /// <summary>
    /// Debug simples.
    /// </summary>
    public void MostrarRecordeNaTela()
    {

    }

    /// <summary>
    /// Mantido por compatibilidade com algum sistema que registre colisão do Enter.
    /// </summary>
    public void RegistrarColisaoEnter(string tagObjeto)
    {

    }
}