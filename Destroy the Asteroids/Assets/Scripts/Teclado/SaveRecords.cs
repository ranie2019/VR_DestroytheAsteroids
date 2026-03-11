using UnityEngine;
using TMPro;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;

public class SaveRecords : MonoBehaviour
{
    [System.Serializable]
    public class RecordData
    {
        public string nome;
        public int pontuacao;

        public RecordData(string nome, int pontuacao)
        {
            this.nome = nome;
            this.pontuacao = pontuacao;
        }
    }

    [Header("Objetos do placar na ordem do Top 1 ao Top 10")]
    [SerializeField] private List<GameObject> recordObjects;

    [Header("Configuração")]
    [SerializeField] private int maxRecords = 10;
    [SerializeField] private string nomePadrao = "Name";

    private const string PrefixoNome = "record_nome_";
    private const string PrefixoScore = "record_score_";

    private readonly CultureInfo culturaPtBr = new CultureInfo("pt-BR");

    private void Start()
    {
        CarregarRecordsNoPlacar();
    }

    /// <summary>
    /// Adiciona um novo record, ordena por pontuação e mantém apenas o Top 10.
    /// </summary>
    public void RegistrarNovoRecord(string nomeJogador, int novaPontuacao)
    {
        if (string.IsNullOrWhiteSpace(nomeJogador))
            nomeJogador = nomePadrao;

        if (novaPontuacao < 0)
        {
            return;
        }

        List<RecordData> records = CarregarRecordsDaMemoria();

        records.Add(new RecordData(nomeJogador.Trim(), novaPontuacao));

        records = records
            .OrderByDescending(r => r.pontuacao)
            .Take(maxRecords)
            .ToList();

        SalvarListaNaMemoria(records);
        AplicarListaNaUI(records);

    }

    /// <summary>
    /// Carrega os records salvos no PlayerPrefs e aplica no placar.
    /// </summary>
    public void CarregarRecordsNoPlacar()
    {
        if (recordObjects == null || recordObjects.Count == 0)
        {
            return;
        }

        List<RecordData> records = CarregarRecordsDaMemoria();
        AplicarListaNaUI(records);

    }

    /// <summary>
    /// Salva o estado atual visível do placar no PlayerPrefs.
    /// Mantido apenas para compatibilidade com fluxos antigos.
    /// </summary>
    public void SalvarEstadoAtualDoPlacar()
    {
        if (recordObjects == null || recordObjects.Count == 0)
        {
            return;
        }

        List<RecordData> records = new List<RecordData>();

        for (int i = 0; i < recordObjects.Count && i < maxRecords; i++)
        {
            GameObject slot = recordObjects[i];
            if (slot == null)
                continue;

            Transform nomeChild = slot.transform.Find("Nome");
            Transform recordChild = slot.transform.Find("Record");

            string nome = nomePadrao;
            int pontuacao = 0;

            if (nomeChild != null && nomeChild.TryGetComponent(out TextMeshProUGUI nomeTMP))
            {
                nome = nomeTMP.text.Trim();
                if (string.IsNullOrWhiteSpace(nome))
                    nome = nomePadrao;
            }

            if (recordChild != null && recordChild.TryGetComponent(out TextMeshProUGUI scoreTMP))
            {
                string textoScore = scoreTMP.text
                    .Replace(".", "")
                    .Replace(",", "")
                    .Trim();

                if (!int.TryParse(textoScore, out pontuacao))
                    pontuacao = 0;
            }

            // Só salva se tiver conteúdo real ou pontuação
            if (!string.IsNullOrWhiteSpace(nome) || pontuacao > 0)
            {
                records.Add(new RecordData(nome, pontuacao));
            }
        }

        records = records
            .OrderByDescending(r => r.pontuacao)
            .Take(maxRecords)
            .ToList();

        SalvarListaNaMemoria(records);
        AplicarListaNaUI(records);

    }

    /// <summary>
    /// Apaga todos os records salvos e limpa a UI.
    /// </summary>
    [ContextMenu("Apagar Todos os Records")]
    public void ApagarTodosOsRecords()
    {
        for (int i = 0; i < maxRecords; i++)
        {
            PlayerPrefs.DeleteKey(PrefixoNome + i);
            PlayerPrefs.DeleteKey(PrefixoScore + i);
        }

        PlayerPrefs.Save();
        AplicarListaNaUI(new List<RecordData>());

    }

    /// <summary>
    /// Lê os records salvos da memória.
    /// </summary>
    private List<RecordData> CarregarRecordsDaMemoria()
    {
        List<RecordData> records = new List<RecordData>();

        for (int i = 0; i < maxRecords; i++)
        {
            string nome = PlayerPrefs.GetString(PrefixoNome + i, nomePadrao);
            int pontuacao = PlayerPrefs.GetInt(PrefixoScore + i, -1);

            if (pontuacao >= 0)
            {
                if (string.IsNullOrWhiteSpace(nome))
                    nome = nomePadrao;

                records.Add(new RecordData(nome, pontuacao));
            }
        }

        return records
            .OrderByDescending(r => r.pontuacao)
            .Take(maxRecords)
            .ToList();
    }

    /// <summary>
    /// Salva a lista no PlayerPrefs, limpando os dados antigos antes.
    /// </summary>
    private void SalvarListaNaMemoria(List<RecordData> records)
    {
        for (int i = 0; i < maxRecords; i++)
        {
            PlayerPrefs.DeleteKey(PrefixoNome + i);
            PlayerPrefs.DeleteKey(PrefixoScore + i);
        }

        for (int i = 0; i < records.Count && i < maxRecords; i++)
        {
            string nome = string.IsNullOrWhiteSpace(records[i].nome) ? nomePadrao : records[i].nome.Trim();
            int pontuacao = Mathf.Max(0, records[i].pontuacao);

            PlayerPrefs.SetString(PrefixoNome + i, nome);
            PlayerPrefs.SetInt(PrefixoScore + i, pontuacao);

        }

        PlayerPrefs.Save();
    }

    /// <summary>
    /// Aplica a lista nos slots visuais do placar.
    /// A ordem da lista recordObjects define Top 1, Top 2, Top 3...
    /// </summary>
    private void AplicarListaNaUI(List<RecordData> records)
    {
        for (int i = 0; i < recordObjects.Count && i < maxRecords; i++)
        {
            GameObject slot = recordObjects[i];
            if (slot == null)
                continue;

            Transform nomeChild = slot.transform.Find("Nome");
            Transform recordChild = slot.transform.Find("Record");

            if (i < records.Count)
            {
                if (nomeChild != null && nomeChild.TryGetComponent(out TextMeshProUGUI nomeTMP))
                    nomeTMP.text = records[i].nome;

                if (recordChild != null && recordChild.TryGetComponent(out TextMeshProUGUI scoreTMP))
                    scoreTMP.text = records[i].pontuacao.ToString("N0", culturaPtBr);

            }
            else
            {
                if (nomeChild != null && nomeChild.TryGetComponent(out TextMeshProUGUI nomeTMP))
                    nomeTMP.text = nomePadrao;

                if (recordChild != null && recordChild.TryGetComponent(out TextMeshProUGUI scoreTMP))
                    scoreTMP.text = "";

            }
        }
    }
}