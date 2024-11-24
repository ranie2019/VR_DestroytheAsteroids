using UnityEngine;

public class UFOAttack : MonoBehaviour
{
    [Header("Configura��es de Ataque")]
    public GameObject projetilPrefab; // Prefab do proj�til
    public Transform pontoRotacaoY; // Ponto que rotaciona no eixo Y
    public Transform pontoRotacaoX; // Ponto que rotaciona no eixo X
    public Transform pontoDeDisparo; // Ponto onde o proj�til ser� disparado
    public float intervaloDeAtaque = 15f; // Tempo m�nimo entre ataques
    public float alcanceDeAtaque = 100f; // Dist�ncia m�xima para iniciar ataques
    public float velocidadeDoProjetil = 20f; // Velocidade do proj�til (adicionada)

    private Transform jogador; // Refer�ncia ao jogador
    private float cronometroDeAtaque; // Contador para controlar o intervalo entre ataques

    private UFOMovimento ufoMovimento; // Refer�ncia ao script de movimento

    private void Start()
    {
        // Procurar o jogador na cena
        GameObject playerObj = GameObject.FindWithTag("Player");
        if (playerObj != null)
        {
            jogador = playerObj.transform;
        }

        // Obter o componente de movimento
        ufoMovimento = GetComponent<UFOMovimento>();
    }

    private void Update()
    {
        if (jogador != null)
        {
            float distanciaDoJogador = Vector3.Distance(transform.position, jogador.position);

            // Atualiza o estado de movimento
            bool emAlcanceDeAtaque = distanciaDoJogador <= alcanceDeAtaque;
            ufoMovimento.AtualizarEstadoMovimento(emAlcanceDeAtaque);

            // Se estiver em alcance, ataca
            if (emAlcanceDeAtaque)
            {
                Atacar();
            }
        }
    }

    private void Atacar()
    {
        // Atualiza o cron�metro de ataque
        cronometroDeAtaque += Time.deltaTime;

        if (cronometroDeAtaque >= intervaloDeAtaque)
        {
            DispararProjetil();
            cronometroDeAtaque = 0f; // Reinicia o cron�metro
        }
    }

    private void DispararProjetil()
    {
        if (projetilPrefab != null && pontoDeDisparo != null && jogador != null)
        {
            // Calcula a dire��o do jogador
            Vector3 direcao = (jogador.position - pontoDeDisparo.position).normalized;

            // Instancia o proj�til no ponto de disparo
            GameObject projetil = Instantiate(projetilPrefab, pontoDeDisparo.position, Quaternion.LookRotation(direcao));

            // Configura a dire��o e a rota��o do proj�til
            Rigidbody rb = projetil.GetComponent<Rigidbody>();
            if (rb != null)
            {
                // Configura a velocidade do proj�til
                rb.velocity = direcao * velocidadeDoProjetil;
            }
        }
    }

    private void OnDrawGizmosSelected()
    {
        // Desenha o alcance de ataque no editor
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, alcanceDeAtaque);
    }
}
