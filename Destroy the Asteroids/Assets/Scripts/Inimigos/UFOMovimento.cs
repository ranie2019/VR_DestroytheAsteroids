using UnityEngine;
using System.Collections;

public class UFOMovimento : MonoBehaviour
{
    [Tooltip("Velocidade de órbita ao redor do jogador")]
    public float orbitSpeed = 5f;

    [Tooltip("Velocidade de movimento em linha reta até o destino")]
    public float velocidadeMovimentoReto = 3f;

    [Tooltip("Raio da órbita ao redor do jogador")]
    public float orbitRadius = 10f;

    [Tooltip("Altura do UFO durante a órbita")]
    public float altura = 5f;

    private Transform jogador; // Referência ao jogador
    private bool emAlcanceDeAtaque; // Indica se está em modo de ataque

    private Vector3 initialPosition; // Posição inicial do UFO
    private Vector3 destinoInicial; // Posição de origem para o movimento reto
    private bool chegouNoDestino = false; // Flag para indicar que o UFO chegou ao destino
    private Vector3 pontoDeOrbita; // Ponto onde o UFO começará a orbitar

    private Coroutine movimentoCoroutine; // Coroutine para controlar o movimento em linha reta

    private void Start()
    {
        jogador = GameObject.FindGameObjectWithTag("Player")?.transform;
        initialPosition = transform.position;
        destinoInicial = new Vector3(jogador.position.x, altura, jogador.position.z); // Definindo destino no plano horizontal

        // Inicializando a posição do UFO, caso seja instanciado em algum lugar diferente da origem
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
        // Movendo o UFO em linha reta até a posição de origem com suavização
        float distancia = Vector3.Distance(transform.position, destinoInicial);
        float tempoMovimento = distancia / velocidadeMovimentoReto;

        float tempoGasto = 0f;
        Vector3 posicaoInicial = transform.position;

        while (tempoGasto < tempoMovimento)
        {
            tempoGasto += Time.deltaTime;
            transform.position = Vector3.Lerp(posicaoInicial, destinoInicial, tempoGasto / tempoMovimento);

            // Verifica se o UFO está dentro do raio de órbita
            if (Vector3.Distance(transform.position, jogador.position) <= orbitRadius && !chegouNoDestino)
            {
                pontoDeOrbita = transform.position; // Salva a posição de orbita
                chegouNoDestino = true;

                // Se a posição do UFO for diferente da posição inicial, significa que é um clone
                if (transform.position != initialPosition)
                {
                    // Gerar um valor aleatório para o orbitSpeed entre 0.1 e 2 para clones
                    orbitSpeed = Random.Range(0.1f, 2f);
                }

                break;
            }

            yield return null;
        }

        // Garantir que o UFO termine na posição inicial, se não tenha atingido o limite do orbitRadius
        if (!chegouNoDestino)
        {
            transform.position = destinoInicial;
            pontoDeOrbita = destinoInicial; // No caso de não ter atingido o orbitRadius
            chegouNoDestino = true;
        }
    }

    private void MovimentacaoNormal()
    {
        // Movimentação normal (orbital) quando o UFO não está em ataque
        float angle = Time.time * orbitSpeed;
        float x = Mathf.Cos(angle) * orbitRadius;
        float z = Mathf.Sin(angle) * orbitRadius;

        transform.position = new Vector3(x, altura, z);
    }

    private void OrbitarJogador()
    {
        // Movimentação orbital quando o UFO está em modo de ataque
        float angle = Time.time * orbitSpeed;
        Vector3 direcaoOrbitacao = new Vector3(
            Mathf.Cos(angle) * orbitRadius,
            altura,
            Mathf.Sin(angle) * orbitRadius
        );

        // Usa a posição armazenada como ponto de órbita para começar a orbitar
        transform.position = pontoDeOrbita + direcaoOrbitacao;
    }

    /// <summary>
    /// Atualiza o estado de movimento do UFO.
    /// </summary>
    /// <param name="estadoEmAlcanceDeAtaque">Define se está em modo de ataque.</param>
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
