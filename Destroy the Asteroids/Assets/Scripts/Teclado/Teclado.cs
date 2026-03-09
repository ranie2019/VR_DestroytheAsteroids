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

    [Header("Sistema do placar")]
    public PlacarRecords placarRecords;

    [Header("Configuração")]
    public int limiteMaximo = 10;

    private Vector3 escalaOriginal;
    private Vector3 posicaoOriginal;
    private bool processandoEnter = false;

    private void Start()
    {
        escalaOriginal = transform.localScale;
        posicaoOriginal = transform.localPosition;
    }

    private void OnCollisionEnter(Collision collision)
    {
        StartCoroutine(AnimarBotao());

        if (alvoDisplay == null)
            return;

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
            if (processandoEnter)
                return;

            processandoEnter = true;
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

    public void ConfirmarNome()
    {
        if (alvoDisplay == null)
        {
            processandoEnter = false;
            return;
        }

        TextMeshProUGUI textoAlvo = alvoDisplay.GetComponentInChildren<TextMeshProUGUI>();
        if (textoAlvo == null)
        {
            processandoEnter = false;
            return;
        }

        string nomeLimpo = textoAlvo.text.Trim();
        if (string.IsNullOrEmpty(nomeLimpo))
        {
            processandoEnter = false;
            return;
        }

        if (placarRecords == null)
        {
            Debug.LogError("⚠️ Teclado: PlacarRecords não foi atribuído.");
            processandoEnter = false;
            return;
        }

        // Aqui está a ligação certa com o sistema novo
        placarRecords.ConfirmarNomeDoJogador(nomeLimpo);

        if (botaoEnter && tecladoRaiz != null)
        {
            StartCoroutine(FinalizarTecladoComDelay());
        }
        else
        {
            processandoEnter = false;
        }
    }

    private IEnumerator FinalizarTecladoComDelay()
    {
        yield return new WaitForSeconds(0.7f);

        ResetarTextoDigitado();

        if (tecladoRaiz != null)
        {
            tecladoRaiz.SetActive(false);
        }

        processandoEnter = false;
    }

    private void ResetarTextoDigitado()
    {
        if (alvoDisplay == null)
            return;

        TextMeshProUGUI textoAlvo = alvoDisplay.GetComponentInChildren<TextMeshProUGUI>();
        if (textoAlvo != null)
        {
            textoAlvo.text = "";
        }
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
                EditorGUILayout.LabelField("Configurações Exclusivas do ENTER", EditorStyles.boldLabel);

                SerializedProperty tecladoRaiz = serializedObject.FindProperty("tecladoRaiz");
                EditorGUILayout.PropertyField(tecladoRaiz, new GUIContent("Objeto do Teclado"));

                SerializedProperty placarRecords = serializedObject.FindProperty("placarRecords");
                EditorGUILayout.PropertyField(placarRecords, new GUIContent("Placar Records"));

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