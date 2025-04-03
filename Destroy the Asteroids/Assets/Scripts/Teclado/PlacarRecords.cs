using UnityEngine;
using TMPro;
using System.Collections.Generic;

public class PlacarRecords : MonoBehaviour
{
    [Header("Objetos de Recorde")]
    [SerializeField] private List<GameObject> recordObjects; // Lista de objetos pais que contêm o filho "Record"

    private int[] recordScores = new int[10]; // Array fixo para evitar redimensionamentos
    private GameController gameController; // Referência ao GameController

    private void Start()
    {
        gameController = FindObjectOfType<GameController>(); // Busca o GameController na cena
        AtualizarRecords();
    }

    public void AtualizarRecords()
    {
        // Carrega os recordes salvos no PlayerPrefs
        for (int i = 0; i < 10; i++)
        {
            recordScores[i] = PlayerPrefs.GetInt($"RecordScore_{i}", 0);
        }

        // Ordena os recordes do maior para o menor
        System.Array.Sort(recordScores, (a, b) => b.CompareTo(a));

        // Atualiza a interface gráfica
        AtualizarUI();
    }

    public void RegistrarPontuacaoFinal()
    {
        if (gameController == null) return;

        // Encontra a menor pontuação na tabela
        int menorPontuacao = int.MaxValue;
        int indiceMenor = -1;

        for (int i = 0; i < recordScores.Length; i++)
        {
            if (recordScores[i] < menorPontuacao)
            {
                menorPontuacao = recordScores[i];
                indiceMenor = i;
            }
        }


        // Salva os novos recordes no PlayerPrefs
        for (int i = 0; i < 10; i++)
        {
            PlayerPrefs.SetInt($"RecordScore_{i}", recordScores[i]);
        }
        PlayerPrefs.Save();

        AtualizarUI(); // Atualiza a interface gráfica após a substituição
    }

    private void AtualizarUI()
    {
        int count = Mathf.Min(recordScores.Length, recordObjects.Count); // Evita acessar elementos fora do limite

        for (int i = 0; i < count; i++)
        {
            if (recordObjects[i] != null)
            {
                Transform recordChild = recordObjects[i].transform.Find("Record");

                if (recordChild != null && recordChild.TryGetComponent(out TextMeshProUGUI recordText))
                {
                    recordText.text = $"{recordScores[i]:N0}"; // Exibe a pontuação formatada
                }

                if (!recordObjects[i].activeSelf)
                {
                    recordObjects[i].SetActive(true); // Ativa o objeto apenas se estiver desativado
                }
            }
        }
    }
}
