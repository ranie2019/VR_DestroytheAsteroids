using UnityEngine;
using TMPro;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

public class PlacarRecords : MonoBehaviour
{
    [Header("Fonte da Pontuação")]
    [SerializeField] private PontoController pontoController;

    [Header("Objetos do Ranking (com filho 'Record')")]
    [SerializeField] private List<GameObject> recordObjects;

    [Header("Tempo de espera (em segundos) após preencher textos")]
    [SerializeField] private float tempoDeEspera = 0.5f;

    private void Start()
    {
        Debug.Log("📌 PlacarRecords iniciado.");
    }

    // Chame essa função ao final do jogo
    public void RegistrarPontuacaoFinal()
    {
        if (pontoController == null)
        {
            Debug.LogError("⚠️ ERRO: PontoController não atribuído!");
            return;
        }

        int playerScore = pontoController.GetPontosFinais();
        Debug.Log($"✅ Pontuação capturada: {playerScore:N0}");

        // Espera os textos atualizarem antes de ordenar
        StartCoroutine(OrdenarDepoisDeAtualizar());
    }

    private IEnumerator OrdenarDepoisDeAtualizar()
    {
        yield return new WaitForSeconds(tempoDeEspera);
        OrdenarPlacarPorPontuacao();
    }

    private void OrdenarPlacarPorPontuacao()
    {
        // Guarda a posição original de cada objeto
        List<Vector3> posicoesOrdenadas = recordObjects
            .Select(obj => obj.transform.localPosition)
            .OrderByDescending(pos => pos.y) // Ordem vertical do canvas
            .ToList();

        // Cria lista com a pontuação lida do filho "Record"
        List<(GameObject obj, int score)> ranking = new List<(GameObject, int)>();

        foreach (var obj in recordObjects)
        {
            if (obj == null) continue;

            Transform recordChild = obj.transform.Find("Record");

            if (recordChild != null && recordChild.TryGetComponent(out TextMeshProUGUI recordText))
            {
                string texto = recordText.text.Replace(".", "").Replace(",", "").Trim();
                int score = int.TryParse(texto, out int s) ? s : 0;
                ranking.Add((obj, score));
            }
            else
            {
                ranking.Add((obj, 0)); // fallback
            }
        }

        // Ordena a lista com base na pontuação (maior para cima)
        var rankingOrdenado = ranking.OrderByDescending(x => x.score).ToList();

        // Atribui a nova posição com base na ordem do ranking
        for (int i = 0; i < rankingOrdenado.Count; i++)
        {
            rankingOrdenado[i].obj.transform.localPosition = posicoesOrdenadas[i];
            Debug.Log($"[{i + 1}] {rankingOrdenado[i].obj.name} => {rankingOrdenado[i].score} pontos => Posição Y: {posicoesOrdenadas[i].y}");
        }

        Debug.Log("✅ Placar reorganizado visualmente por pontuação.");
    }
}
