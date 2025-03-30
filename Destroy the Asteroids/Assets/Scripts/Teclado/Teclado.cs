using UnityEngine;
using TMPro;

public class Teclado : MonoBehaviour
{
    public GameObject alvoDisplay; // Objeto que contém o TextMeshPro onde o texto será escrito
    public bool botaoApagar = false; // Define se este objeto é o botão de apagar
    public bool botaoEspaco = false; // Define se este objeto é o botão de espaço
    public int limiteMaximo = 10; // Limite máximo de caracteres permitidos

    private void OnCollisionEnter(Collision collision)
    {
        if (alvoDisplay == null)
        {
            return;
        }

        if (botaoApagar)
        {
            ApagarUltimaLetra();
        }
        else if (botaoEspaco)
        {
            AdicionarEspaco();
        }
        else
        {
            AdicionarLetra();
        }
    }

    private void AdicionarLetra()
    {
        TextMeshProUGUI textoFilho = GetComponentInChildren<TextMeshProUGUI>();

        if (textoFilho != null)
        {
            TextMeshProUGUI textoAlvo = alvoDisplay.GetComponentInChildren<TextMeshProUGUI>();

            if (textoAlvo != null && textoAlvo.text.Length < limiteMaximo)
            {
                textoAlvo.text += textoFilho.text;
            }
        }
    }

    private void ApagarUltimaLetra()
    {
        TextMeshProUGUI textoAlvo = alvoDisplay.GetComponentInChildren<TextMeshProUGUI>();

        if (textoAlvo != null && textoAlvo.text.Length > 0)
        {
            textoAlvo.text = textoAlvo.text.Substring(0, textoAlvo.text.Length - 1);
        }
    }

    private void AdicionarEspaco()
    {
        TextMeshProUGUI textoAlvo = alvoDisplay.GetComponentInChildren<TextMeshProUGUI>();

        if (textoAlvo != null && textoAlvo.text.Length < limiteMaximo)
        {
            textoAlvo.text += " ";
        }
    }

    public void DefinirTextoAlvo(TextMeshProUGUI novoTextoAlvo)
    {
        if (novoTextoAlvo != null)
        {
            alvoDisplay = novoTextoAlvo.gameObject;
        }
    }
}
