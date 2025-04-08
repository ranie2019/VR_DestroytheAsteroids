using UnityEngine;
using TMPro;
using System.Collections;

#if UNITY_EDITOR
using UnityEditor;
#endif

public class Teclado : MonoBehaviour
{
    [Header("Configurações do Botão")]
    public GameObject alvoDisplay;
    public bool botaoApagar = false;
    public bool botaoEspaco = false;
    public bool botaoEnter = false;

    [Header("Referência ao teclado completo")]
    public GameObject tecladoRaiz;

    public PontoController pontoController;
    public int limiteMaximo = 10;

    private Vector3 escalaOriginal;
    private Vector3 posicaoOriginal;

    private void Start()
    {
        escalaOriginal = transform.localScale;
        posicaoOriginal = transform.localPosition;

        if (tecladoRaiz != null)
            tecladoRaiz.SetActive(false);
    }

    private void OnCollisionEnter(Collision collision)
    {
        StartCoroutine(AnimarBotao());

        if (alvoDisplay == null) return;

        if (botaoApagar)
        {
            ApagarUltimaLetra();
        }
        else if (botaoEspaco)
        {
            AdicionarEspaco();
        }
        else if (botaoEnter)
        {
            AtivarETeleportarTeclado();

            if (pontoController != null)
            {
                pontoController.RegistrarColisaoEnter(gameObject.tag);
            }

            ConfirmarNome();
        }
        else
        {
            AdicionarLetra();
        }
    }

    private IEnumerator AnimarBotao()
    {
        Vector3 novaEscala;

        if (botaoEspaco)
            novaEscala = new Vector3(2f, 0.4f, 0.1f);
        else if (botaoEnter)
            novaEscala = new Vector3(0.6183478f, 0.4f, 0.1f);
        else if (botaoApagar)
            novaEscala = new Vector3(0.5950938f, 0.4f, 0.1f);
        else
            novaEscala = new Vector3(0.8f, 0.8f, 0.1f);

        Vector3 novaPosicao = new Vector3(posicaoOriginal.x, posicaoOriginal.y, -0.2f);

        transform.localScale = novaEscala;
        transform.localPosition = novaPosicao;

        yield return new WaitForSeconds(0.5f);

        transform.localScale = escalaOriginal;
        transform.localPosition = posicaoOriginal;
    }

    private void AdicionarLetra()
    {
        TextMeshProUGUI letraTMP = GetComponentInChildren<TextMeshProUGUI>();
        TextMeshProUGUI textoAlvo = alvoDisplay.GetComponentInChildren<TextMeshProUGUI>();

        if (letraTMP != null && textoAlvo != null && textoAlvo.text.Length < limiteMaximo)
        {
            textoAlvo.text += letraTMP.text;
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

    private void AtivarETeleportarTeclado()
    {
        if (tecladoRaiz != null)
        {
            tecladoRaiz.SetActive(true);
            tecladoRaiz.transform.position = new Vector3(0f, -5f, 3.69f);
            Debug.Log("Teclado ativado e movido pelo botão Enter.");
        }
    }

    public void ConfirmarNome()
    {
        if (alvoDisplay == null || pontoController == null) return;

        TextMeshProUGUI textoAlvo = alvoDisplay.GetComponentInChildren<TextMeshProUGUI>();
        if (textoAlvo == null) return;

        string nomeLimpo = textoAlvo.text.Trim();
        if (string.IsNullOrEmpty(nomeLimpo)) return;

        pontoController.SetNomePlayer(nomeLimpo);

        // Atualiza o UI diretamente
        int indice = pontoController.GetUltimoIndiceSalvo();
        GameObject objetoRecord = pontoController.GetRecordObject(indice);

        if (objetoRecord != null)
        {
            Transform nomeChild = objetoRecord.transform.Find("Nome");
            if (nomeChild != null && nomeChild.TryGetComponent(out TextMeshProUGUI nomeText))
            {
                nomeText.text = nomeLimpo;
            }
        }

        if (botaoEnter && tecladoRaiz != null)
        {
            StartCoroutine(DesativarTecladoComDelay());
        }
    }

    private IEnumerator DesativarTecladoComDelay()
    {
        yield return new WaitForSeconds(0.7f);
        tecladoRaiz.SetActive(false);
    }

    public void DefinirTextoAlvo(TextMeshProUGUI novoTextoAlvo)
    {
        if (novoTextoAlvo != null)
        {
            alvoDisplay = novoTextoAlvo.gameObject;
        }
    }

#if UNITY_EDITOR
    [CustomEditor(typeof(Teclado))]
    public class TecladoEditor : Editor
    {
        public override void OnInspectorGUI()
        {
            DrawDefaultInspector();

            Teclado teclado = (Teclado)target;

            if (teclado.botaoEnter)
            {
                EditorGUILayout.Space();
                EditorGUILayout.LabelField("↪️ Configurações Exclusivas do ENTER", EditorStyles.boldLabel);

                SerializedProperty tecladoRaiz = serializedObject.FindProperty("tecladoRaiz");
                EditorGUILayout.PropertyField(tecladoRaiz, new GUIContent("Objeto do Teclado"));

                SerializedProperty pontoController = serializedObject.FindProperty("pontoController");
                EditorGUILayout.PropertyField(pontoController, new GUIContent("Ponto Controller"));

                serializedObject.ApplyModifiedProperties();
            }

            if (GUI.changed)
            {
                EditorUtility.SetDirty(teclado);
            }
        }
    }
#endif
}
