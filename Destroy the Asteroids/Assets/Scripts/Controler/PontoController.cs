using UnityEngine;
using TMPro;
using System.Collections.Generic;

public class PontoController : MonoBehaviour
{
    [Header("Referência do placar principal (UI)")]
    public TextMeshProUGUI placarTexto;

    [Header("Elementos do ranking (UI)")]
    public GameObject[] recordesUI; // Lista de objetos UI que mostram o ranking

    // Dados internos do jogo
    private List<int> rankingPontos = new List<int>();       // Lista de pontuações
    private List<string> nomesJogadores = new List<string>(); // Lista de nomes
    private int pontosAtuais = 0;                            // Pontuação atual do jogador
    private int ultimoIndiceSalvo = -1;                      // Índice do último jogador salvo no ranking

    private void Start()
    {
        AtualizarPlacarUI(); // Garante que o placar mostre o valor inicial
    }

    /// <summary>
    /// Adiciona pontos ao placar atual
    /// </summary>
    public void AddPontos(int pontos)
    {
        pontosAtuais += pontos;
        AtualizarPlacarUI();
    }

    /// <summary>
    /// Reseta a pontuação atual
    /// </summary>
    public void ResetScore()
    {
        pontosAtuais = 0;
        AtualizarPlacarUI();
    }

    /// <summary>
    /// Atualiza o texto da UI com os pontos atuais
    /// </summary>
    private void AtualizarPlacarUI()
    {
        if (placarTexto != null)
            placarTexto.text = pontosAtuais.ToString();
    }

    /// <summary>
    /// Método externo para atualizar o score (usado por outros scripts)
    /// </summary>
    public void UpdatePlayerScore(int pontos)
    {
        AddPontos(pontos);
    }

    /// <summary>
    /// Salva os pontos no ranking local e atualiza o ranking na tela
    /// </summary>
    public void PlacarOffline()
    {
        rankingPontos.Add(pontosAtuais);  // Adiciona a pontuação atual à lista
        nomesJogadores.Add("Anon");       // Nome temporário padrão
        ultimoIndiceSalvo = rankingPontos.Count - 1; // Guarda o índice salvo

        AtualizarUIRanking(); // Atualiza visualmente o ranking
    }

    /// <summary>
    /// Atualiza o nome do jogador no ranking
    /// </summary>
    public void SetNomePlayer(string nome)
    {
        if (ultimoIndiceSalvo >= 0 && ultimoIndiceSalvo < nomesJogadores.Count)
        {
            nomesJogadores[ultimoIndiceSalvo] = nome;
        }
    }

    /// <summary>
    /// Retorna o índice do último jogador salvo (usado pelo Teclado)
    /// </summary>
    public int GetUltimoIndiceSalvo()
    {
        return ultimoIndiceSalvo;
    }

    /// <summary>
    /// Retorna o objeto de UI do ranking correspondente ao índice
    /// </summary>
    public GameObject GetRecordObject(int indice)
    {
        if (indice >= 0 && indice < recordesUI.Length)
            return recordesUI[indice];

        return null;
    }

    /// <summary>
    /// Retorna os pontos finais atuais do jogador (usado ao salvar o ranking)
    /// </summary>
    public int GetPontosFinais()
    {
        return pontosAtuais;
    }

    /// <summary>
    /// Chamada pelo botão ENTER do teclado
    /// </summary>
    public void RegistrarColisaoEnter(string tagObjeto)
    {
        Debug.Log("Colisão ENTER registrada pelo objeto: " + tagObjeto);
        // Aqui você pode adicionar lógica extra de acordo com a tag recebida
    }

    /// <summary>
    /// Atualiza os textos da UI com os dados do ranking
    /// </summary>
    private void AtualizarUIRanking()
    {
        for (int i = 0; i < recordesUI.Length; i++)
        {
            if (i < rankingPontos.Count)
            {
                // Busca os componentes dentro do objeto de UI
                Transform nomeTextObj = recordesUI[i].transform.Find("Nome");
                Transform pontosTextObj = recordesUI[i].transform.Find("Pontos");

                // Atualiza o nome do jogador
                if (nomeTextObj != null && nomeTextObj.TryGetComponent(out TextMeshProUGUI nomeTMP))
                {
                    nomeTMP.text = nomesJogadores[i];
                }

                // Atualiza a pontuação
                if (pontosTextObj != null && pontosTextObj.TryGetComponent(out TextMeshProUGUI pontosTMP))
                {
                    pontosTMP.text = rankingPontos[i].ToString();
                }
            }
        }
    }
}
