using UnityEngine;
using System.Collections;

public class UFOMovimento : MonoBehaviour
{
    [Tooltip("Velocidade de �rbita ao redor do jogador")]
    public float orbitSpeed = 5f;

    [Tooltip("Velocidade de movimento em linha reta at� o destino")]
    public float velocidadeMovimentoReto = 3f;

    [Tooltip("Raio da �rbita ao redor do jogador")]
    public float orbitRadius = 10f;

    [Tooltip("Altura do UFO durante a �rbita")]
    public float altura = 5f;

    private Transform jogador; // Refer�ncia ao jogador
    private bool emAlcanceDeAtaque; // Indica se est� em modo de ataque

    private Vector3 initialPosition; // Posi��o inicial do UFO
    private Vector3 destinoInicial; // Posi��o de origem para o movimento reto
    private bool chegouNoDestino = false; // Flag para indicar que o UFO chegou ao destino
    private Vector3 pontoDeOrbita; // Ponto onde o UFO come�ar� a orbitar

    private Coroutine movimentoCoroutine; // Coroutine para controlar o movimento em linha reta

    private void Start()
    {
        jogador = GameObject.FindGameObjectWithTag("Player")?.transform;
        initialPosition = transform.position;
        destinoInicial = new Vector3(jogador.position.x, altura, jogador.position.z); // Definindo destino no plano horizontal

        // Inicializando a posi��o do UFO, caso seja instanciado em algum lugar diferente da origem
        transform.position = initialPosition;

        // Iniciando o movimento em linha reta usando uma corrotina
        movimentoCoroutine = StartCoroutine(MoverParaOrigem());
    }

    private void Update()
    {
        if (jogador != null)
        {
            if (chegouNoDestino)
            {
                if (emAlcanceDeAtaque)
                {
                    OrbitarJogador();
                }
                else
                {
                    MovimentacaoNormal();
                }
            }
        }
    }

    private IEnumerator MoverParaOrigem()
    {
        // Movendo o UFO em linha reta at� a posi��o de origem com suaviza��o
        float distancia = Vector3.Distance(transform.position, destinoInicial);
        float tempoMovimento = distancia / velocidadeMovimentoReto;

        float tempoGasto = 0f;
        Vector3 posicaoInicial = transform.position;

        while (tempoGasto < tempoMovimento)
        {
            tempoGasto += Time.deltaTime;
            transform.position = Vector3.Lerp(posicaoInicial, destinoInicial, tempoGasto / tempoMovimento);

            // Verifica se o UFO est� dentro do raio de �rbita
            if (Vector3.Distance(transform.position, jogador.position) <= orbitRadius && !chegouNoDestino)
            {
                pontoDeOrbita = transform.position; // Salva a posi��o de orbita
                chegouNoDestino = true;

                // Se a posi��o do UFO for diferente da posi��o inicial, significa que � um clone
                if (transform.position != initialPosition)
                {
                    // Gerar um valor aleat�rio para o orbitSpeed entre 0.1 e 2 para clones
                    orbitSpeed = Random.Range(0.1f, 2f);
                }

                break;
            }

            yield return null;
        }

        // Garantir que o UFO termine na posi��o inicial, se n�o tenha atingido o limite do orbitRadius
        if (!chegouNoDestino)
        {
            transform.position = destinoInicial;
            pontoDeOrbita = destinoInicial; // No caso de n�o ter atingido o orbitRadius
            chegouNoDestino = true;
        }
    }

    private void MovimentacaoNormal()
    {
        // Movimenta��o normal (orbital) quando o UFO n�o est� em ataque
        float angle = Time.time * orbitSpeed;
        float x = Mathf.Cos(angle) * orbitRadius;
        float z = Mathf.Sin(angle) * orbitRadius;

        transform.position = new Vector3(x, altura, z);
    }

    private void OrbitarJogador()
    {
        // Movimenta��o orbital quando o UFO est� em modo de ataque
        float angle = Time.time * orbitSpeed;
        Vector3 direcaoOrbitacao = new Vector3(
            Mathf.Cos(angle) * orbitRadius,
            altura,
            Mathf.Sin(angle) * orbitRadius
        );

        // Usa a posi��o armazenada como ponto de �rbita para come�ar a orbitar
        transform.position = pontoDeOrbita + direcaoOrbitacao;
    }

    /// <summary>
    /// Atualiza o estado de movimento do UFO.
    /// </summary>
    /// <param name="estadoEmAlcanceDeAtaque">Define se est� em modo de ataque.</param>
    public void AtualizarEstadoMovimento(bool estadoEmAlcanceDeAtaque)
    {
        emAlcanceDeAtaque = estadoEmAlcanceDeAtaque;

        if (emAlcanceDeAtaque)
        {
            orbitRadius = 5f;
            orbitSpeed = 8f;
        }
        else
        {
            orbitRadius = 10f;
            orbitSpeed = 5f;
        }
    }

    /// <summary>
    /// Define a altura do UFO.
    /// </summary>
    /// <param name="novaAltura">Nova altura desejada para o UFO.</param>
    public void DefinirAltura(float novaAltura)
    {
        altura = novaAltura;
    }
}
