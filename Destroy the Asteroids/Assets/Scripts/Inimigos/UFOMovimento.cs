using UnityEngine;
using System.Collections;
using System.Linq;

public class UFOMovimento : MonoBehaviour
{
    [Tooltip("Velocidade de movimento em linha reta at� o destino")]
    public float velocidadeMovimentoReto = 3f;

    private Transform jogador; // Refer�ncia ao jogador
    private Transform buracoNegro; // Refer�ncia ao buraco negro
    private bool emAlcanceDeAtaque; // Indica se est� em modo de ataque
    private bool sendoAtraido; // Indica se est� sendo afetado pelo buraco negro

    private Vector3 destino; // Posi��o do pr�ximo ponto de destino
    private Transform[] pontosFixos; // Lista de pontos fixos
    private Rigidbody rb; // Refer�ncia ao Rigidbody do UFO

    private void Start()
    {
        // Obt�m a refer�ncia do jogador
        jogador = GameObject.FindGameObjectWithTag("Player")?.transform;

        // Obt�m a refer�ncia do buraco negro
        buracoNegro = GameObject.FindGameObjectWithTag("Gravidade")?.transform;

        // Configura��es iniciais
        rb = GetComponent<Rigidbody>();
        if (rb == null)
        {
            Debug.LogError("Rigidbody � necess�rio para o movimento do UFO. Adicione um Rigidbody ao objeto.");
            return;
        }

        rb.useGravity = false; // Desativa a gravidade do UFO

        // Busca todos os objetos com a tag "Posicao" e os armazena no array
        pontosFixos = GameObject.FindGameObjectsWithTag("Posicao")
            .Select(go => go.transform).ToArray();

        if (pontosFixos.Length == 0)
        {
            Debug.LogError("Nenhum ponto fixo com a tag 'Posicao' foi encontrado.");
        }

        // Inicia a movimenta��o normal
        EscolherPontoAleatorio();
    }

    private void FixedUpdate()
    {
        if (sendoAtraido)
        {
            // Prioriza a atra��o do buraco negro
            AtraidoPeloBuracoNegro();
        }
        else if (emAlcanceDeAtaque && jogador != null)
        {
            // Realiza a movimenta��o orbital se em alcance de ataque
            OrbitarJogador();
        }
        else
        {
            // Movimenta��o normal
            MovimentacaoNormal();
        }
    }

    private void MovimentacaoNormal()
    {
        if (Vector3.Distance(transform.position, destino) > 0.1f)
        {
            transform.position = Vector3.MoveTowards(transform.position, destino, velocidadeMovimentoReto * Time.deltaTime);
        }
        else
        {
            EscolherPontoAleatorio();
        }
    }

    private void AtraidoPeloBuracoNegro()
    {
        if (buracoNegro == null) return;

        // Calcula a dire��o de atra��o para o buraco negro
        Vector3 direcaoAtracao = (buracoNegro.position - transform.position).normalized;

        // Aplica a for�a de atra��o (valor fixo ajustado diretamente aqui)
        float forcaAtracaoFixada = 50f;
        rb.AddForce(direcaoAtracao * forcaAtracaoFixada * Time.deltaTime, ForceMode.Acceleration);
    }

    private void OrbitarJogador()
    {
        float angle = Time.time * 0.5f;
        Vector3 direcaoOrbitacao = new Vector3(
            Mathf.Cos(angle) * 5f,
            0, // Altura removida
            Mathf.Sin(angle) * 5f
        );

        transform.position = jogador.position + direcaoOrbitacao;
    }

    private void EscolherPontoAleatorio()
    {
        if (pontosFixos.Length > 0)
        {
            Transform pontoAleatorio;
            do
            {
                pontoAleatorio = pontosFixos[Random.Range(0, pontosFixos.Length)];
            } while (pontoAleatorio.position == destino);

            destino = pontoAleatorio.position;
        }
    }

    public void AtualizarEstadoMovimento(bool estadoEmAlcanceDeAtaque)
    {
        emAlcanceDeAtaque = estadoEmAlcanceDeAtaque;
    }

    public void SetSendoAtraido(bool estado)
    {
        sendoAtraido = estado;

        if (estado)
        {
            // Garante que o UFO seja controlado pela f�sica durante a atra��o
            rb.isKinematic = false;
        }
        else
        {
            // Retorna ao estado normal ap�s a atra��o
            rb.isKinematic = true;
        }
    }
}
