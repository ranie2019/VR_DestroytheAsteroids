using UnityEngine;
using TMPro;
using System.Collections.Generic;

public class PontoController : MonoBehaviour
{
    [Header("Referência do placar principal (UI)")]
    public TextMeshProUGUI placarTexto;

    [Header("Referência do recorde (UI)")]
    public TextMeshProUGUI recordeTexto;

    [Header("Elementos do ranking (UI)")]
    public GameObject[] recordesUI; // Lista de objetos UI que mostram o ranking

    // Dados internos do jogo
    private List<int> rankingPontos = new List<int>();        // Lista de pontuações
    private List<string> nomesJogadores = new List<string>(); // Lista de nomes
    private int pontosAtuais = 0;                              // Pontuação atual do jogador
    private int maiorPontuacao = 0;                            // Maior pontuação registrada
    private int ultimoIndiceSalvo = -1;                        // Índice do último jogador salvo no ranking

    private void Start()
    {
        maiorPontuacao = PlayerPrefs.GetInt("recordeSalvo", 0);
        AtualizarPlacarUI();
        AtualizarRecordeUI();
    }

    public void AddPontos(int pontos)
    {
        pontosAtuais += pontos;
        AtualizarPlacarUI();

        if (pontosAtuais > maiorPontuacao)
        {
            maiorPontuacao = pontosAtuais;
            PlayerPrefs.SetInt("recordeSalvo", maiorPontuacao);
            AtualizarRecordeUI();
        }
    }

    private void AtualizarPlacarUI()
    {
        if (placarTexto != null)
            placarTexto.text = pontosAtuais.ToString("N0"); // Formato 1.000.000
    }

    private void AtualizarRecordeUI()
    {
        if (recordeTexto != null)
            recordeTexto.text = maiorPontuacao.ToString("N0"); // Formato 1.000.000
    }

    public void UpdatePlayerScore(int pontos)
    {
        AddPontos(pontos);
    }

    public void PlacarOffline()
    {
        rankingPontos.Add(pontosAtuais);
        nomesJogadores.Add("Anon");
        ultimoIndiceSalvo = rankingPontos.Count - 1;

        if (pontosAtuais > maiorPontuacao)
        {
            maiorPontuacao = pontosAtuais;
            PlayerPrefs.SetInt("recordeSalvo", maiorPontuacao);
            AtualizarRecordeUI();
        }

        AtualizarUIRanking();
    }

    public void SetNomePlayer(string nome)
    {
        if (ultimoIndiceSalvo >= 0 && ultimoIndiceSalvo < nomesJogadores.Count)
        {
            nomesJogadores[ultimoIndiceSalvo] = nome;
        }
    }

    public int GetUltimoIndiceSalvo()
    {
        return ultimoIndiceSalvo;
    }

    public GameObject GetRecordObject(int indice)
    {
        if (indice >= 0 && indice < recordesUI.Length)
            return recordesUI[indice];

        return null;
    }

    public int GetPontosFinais()
    {
        return pontosAtuais;
    }

    public void RegistrarColisaoEnter(string tagObjeto)
    {
        Debug.Log("Colisão ENTER registrada pelo objeto: " + tagObjeto);
    }

    private void AtualizarUIRanking()
    {
        for (int i = 0; i < recordesUI.Length; i++)
        {
            if (i < rankingPontos.Count)
            {
                Transform nomeTextObj = recordesUI[i].transform.Find("Nome");
                Transform pontosTextObj = recordesUI[i].transform.Find("Pontos");

                if (nomeTextObj != null && nomeTextObj.TryGetComponent(out TextMeshProUGUI nomeTMP))
                {
                    nomeTMP.text = nomesJogadores[i];
                }

                if (pontosTextObj != null && pontosTextObj.TryGetComponent(out TextMeshProUGUI pontosTMP))
                {
                    pontosTMP.text = rankingPontos[i].ToString("N0");
                }
            }
        }
    }

    public void MostrarPontuacaoNaTela()
    {
        Debug.Log("Pontuação atual: " + pontosAtuais.ToString("N0"));
    }

    public void MostrarRecordeNaTela()
    {
        Debug.Log("Recorde atual: " + maiorPontuacao.ToString("N0"));
    }
}
