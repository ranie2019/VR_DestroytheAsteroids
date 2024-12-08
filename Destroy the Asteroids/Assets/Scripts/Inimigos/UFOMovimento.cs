using UnityEngine;

public class UFOMovimento : MonoBehaviour
{
    [Header("Configura��es de Movimenta��o")]
    public float velocidade = 5f; // Velocidade de movimenta��o do UFO
    public float velocidadeOrbitacao = 3f; // Velocidade para orbitar ao redor do jogador
    public float distanciaOrbitacao = 100f; // Dist�ncia fixa de orbita��o ao redor do jogador

    [Header("Trajet�ria Escolhida")]
    public int trajetoriaEscolhida = 1; // Armazena a trajet�ria escolhida (2 a 4), vis�vel no Editor

    private Transform jogador; // Refer�ncia ao jogador
    private bool emAlcanceDeAtaque; // Indica se o UFO est� dentro do alcance de ataque
    private bool emMovimentacaoLateral; // Se o UFO est� movendo-se lateralmente ao redor do jogador
    private Vector3 direcaoInicial; // Dire��o inicial do movimento aleat�rio

    private void Start()
    {
        // Procurar o jogador na cena
        GameObject playerObj = GameObject.FindWithTag("Player");
        if (playerObj != null)
        {
            jogador = playerObj.transform;
        }

        // Inicializa a movimenta��o aleat�ria at� alcan�ar o jogador
        emMovimentacaoLateral = false;

        // Escolhe a trajet�ria inicial
        EscolherTrajetoria();
    }

    private void Update()
    {
        if (jogador != null)
        {
            if (emAlcanceDeAtaque)
            {
                // Quando atingir o alcance de ataque, muda para a movimenta��o lateral
                if (!emMovimentacaoLateral)
                {
                    emMovimentacaoLateral = true;
                    // Desliga a movimenta��o da trajet�ria
                    StopCoroutine("MoverNaTrajetoria");
                }

                // Se o UFO est� em alcance de ataque, orbita ao redor do jogador
                OrbitarJogador();
            }
            else
            {
                // Caso contr�rio, segue a trajet�ria normal
                if (emMovimentacaoLateral)
                {
                    emMovimentacaoLateral = false;
                    // Desliga a movimenta��o lateral e reinicia a movimenta��o de trajet�ria
                    EscolherTrajetoria();
                    StartCoroutine("MoverNaTrajetoria");
                }

                MoverNaTrajetoria();
            }
        }
    }

    public void AtualizarEstadoMovimento(bool estadoEmAlcanceDeAtaque)
    {
        emAlcanceDeAtaque = estadoEmAlcanceDeAtaque;
    }

    private void EscolherTrajetoria()
    {
        // Escolhe entre 2 trajet�rias restantes (2 e 4)
        int trajeto = Random.Range(0, 2);

        // Define a trajet�ria escolhida como um n�mero vis�vel no Editor
        trajetoriaEscolhida = trajeto == 0 ? 2 : 4;

        switch (trajeto)
        {
            case 0:
                direcaoInicial = new Vector3(0, 0, 1).normalized; // Movimento inicial para frente (trajet�ria 2)
                break;
            case 1:
                direcaoInicial = new Vector3(-1, 0, -1).normalized; // Diagonal para tr�s e esquerda (trajet�ria 4)
                break;
        }
    }

    private void MoverNaTrajetoria()
    {
        switch (trajetoriaEscolhida)
        {
            case 2:
                MoverParaFrente();
                break;
            case 4:
                MoverParaTrasESquerda();
                break;
        }
    }

    // L�gica de movimenta��o 2 (Movimento inicial para frente)
    private void MoverParaFrente()
    {
        Vector3 direcaoParaJogador = (jogador.position - transform.position).normalized;
        transform.position += direcaoParaJogador * velocidade * Time.deltaTime;

        // Rotaciona o UFO para olhar na dire��o do jogador
        if (direcaoParaJogador != Vector3.zero)
        {
            Quaternion rotacao = Quaternion.LookRotation(direcaoParaJogador);
            transform.rotation = Quaternion.Slerp(transform.rotation, rotacao, Time.deltaTime * velocidade);
        }
    }

    // L�gica de movimenta��o 4 (Diagonal para tr�s e esquerda)
    private void MoverParaTrasESquerda()
    {
        Vector3 direcaoParaJogador = (jogador.position - transform.position).normalized;
        Vector3 direcaoCombinada = (direcaoInicial + direcaoParaJogador).normalized;

        transform.position += direcaoCombinada * velocidade * Time.deltaTime;

        // Verificar se a dire��o n�o � zero antes de rotacionar
        if (direcaoCombinada != Vector3.zero)
        {
            Quaternion rotacao = Quaternion.LookRotation(direcaoCombinada);
            transform.rotation = Quaternion.Slerp(transform.rotation, rotacao, Time.deltaTime * velocidade);
        }
    }

    private void OrbitarJogador()
    {
        // Agora, ao atingir o alcance de ataque, todos os movimentos passam a ser orbita��o lateral
        if (emAlcanceDeAtaque)
        {
            Vector3 direcaoOrbitacao = (transform.position - jogador.position).normalized;
            direcaoOrbitacao = Quaternion.Euler(0, velocidadeOrbitacao * Time.deltaTime, 0) * direcaoOrbitacao;

            // Calcula a nova posi��o ao redor do jogador
            Vector3 novaPosicao = jogador.position + direcaoOrbitacao * distanciaOrbitacao;

            // Move o UFO para a nova posi��o ao redor do jogador
            transform.position = Vector3.Lerp(transform.position, novaPosicao, Time.deltaTime * velocidade);

            // Mant�m o UFO olhando para o jogador
            Quaternion rotacao = Quaternion.LookRotation(jogador.position - transform.position);
            transform.rotation = Quaternion.Slerp(transform.rotation, rotacao, Time.deltaTime * velocidade);
        }
    }
}
