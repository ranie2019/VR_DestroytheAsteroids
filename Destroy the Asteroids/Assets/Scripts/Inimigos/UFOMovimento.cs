using UnityEngine;

public class UFOMovimento : MonoBehaviour
{
    [Header("Configurações de Movimentação")]
    public float velocidade = 5f; // Velocidade de movimentação do UFO
    public float velocidadeOrbitacao = 3f; // Velocidade para orbitar ao redor do jogador
    public float distanciaOrbitacao = 100f; // Distância fixa de orbitação ao redor do jogador

    [Header("Trajetória Escolhida")]
    public int trajetoriaEscolhida = 1; // Armazena a trajetória escolhida (2 a 4), visível no Editor

    private Transform jogador; // Referência ao jogador
    private bool emAlcanceDeAtaque; // Indica se o UFO está dentro do alcance de ataque
    private bool emMovimentacaoLateral; // Se o UFO está movendo-se lateralmente ao redor do jogador
    private Vector3 direcaoInicial; // Direção inicial do movimento aleatório

    private void Start()
    {
        // Procurar o jogador na cena
        GameObject playerObj = GameObject.FindWithTag("Player");
        if (playerObj != null)
        {
            jogador = playerObj.transform;
        }

        // Inicializa a movimentação aleatória até alcançar o jogador
        emMovimentacaoLateral = false;

        // Escolhe a trajetória inicial
        EscolherTrajetoria();
    }

    private void Update()
    {
        if (jogador != null)
        {
            if (emAlcanceDeAtaque)
            {
                // Quando atingir o alcance de ataque, muda para a movimentação lateral
                if (!emMovimentacaoLateral)
                {
                    emMovimentacaoLateral = true;
                    // Desliga a movimentação da trajetória
                    StopCoroutine("MoverNaTrajetoria");
                }

                // Se o UFO está em alcance de ataque, orbita ao redor do jogador
                OrbitarJogador();
            }
            else
            {
                // Caso contrário, segue a trajetória normal
                if (emMovimentacaoLateral)
                {
                    emMovimentacaoLateral = false;
                    // Desliga a movimentação lateral e reinicia a movimentação de trajetória
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
        // Escolhe entre 2 trajetórias restantes (2 e 4)
        int trajeto = Random.Range(0, 2);

        // Define a trajetória escolhida como um número visível no Editor
        trajetoriaEscolhida = trajeto == 0 ? 2 : 4;

        switch (trajeto)
        {
            case 0:
                direcaoInicial = new Vector3(0, 0, 1).normalized; // Movimento inicial para frente (trajetória 2)
                break;
            case 1:
                direcaoInicial = new Vector3(-1, 0, -1).normalized; // Diagonal para trás e esquerda (trajetória 4)
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

    // Lógica de movimentação 2 (Movimento inicial para frente)
    private void MoverParaFrente()
    {
        Vector3 direcaoParaJogador = (jogador.position - transform.position).normalized;
        transform.position += direcaoParaJogador * velocidade * Time.deltaTime;

        // Rotaciona o UFO para olhar na direção do jogador
        if (direcaoParaJogador != Vector3.zero)
        {
            Quaternion rotacao = Quaternion.LookRotation(direcaoParaJogador);
            transform.rotation = Quaternion.Slerp(transform.rotation, rotacao, Time.deltaTime * velocidade);
        }
    }

    // Lógica de movimentação 4 (Diagonal para trás e esquerda)
    private void MoverParaTrasESquerda()
    {
        Vector3 direcaoParaJogador = (jogador.position - transform.position).normalized;
        Vector3 direcaoCombinada = (direcaoInicial + direcaoParaJogador).normalized;

        transform.position += direcaoCombinada * velocidade * Time.deltaTime;

        // Verificar se a direção não é zero antes de rotacionar
        if (direcaoCombinada != Vector3.zero)
        {
            Quaternion rotacao = Quaternion.LookRotation(direcaoCombinada);
            transform.rotation = Quaternion.Slerp(transform.rotation, rotacao, Time.deltaTime * velocidade);
        }
    }

    private void OrbitarJogador()
    {
        // Agora, ao atingir o alcance de ataque, todos os movimentos passam a ser orbitação lateral
        if (emAlcanceDeAtaque)
        {
            Vector3 direcaoOrbitacao = (transform.position - jogador.position).normalized;
            direcaoOrbitacao = Quaternion.Euler(0, velocidadeOrbitacao * Time.deltaTime, 0) * direcaoOrbitacao;

            // Calcula a nova posição ao redor do jogador
            Vector3 novaPosicao = jogador.position + direcaoOrbitacao * distanciaOrbitacao;

            // Move o UFO para a nova posição ao redor do jogador
            transform.position = Vector3.Lerp(transform.position, novaPosicao, Time.deltaTime * velocidade);

            // Mantém o UFO olhando para o jogador
            Quaternion rotacao = Quaternion.LookRotation(jogador.position - transform.position);
            transform.rotation = Quaternion.Slerp(transform.rotation, rotacao, Time.deltaTime * velocidade);
        }
    }
}
