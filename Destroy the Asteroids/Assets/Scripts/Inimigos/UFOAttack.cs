using UnityEngine;

public class UFOAttack : MonoBehaviour
{
    [Header("Configurações de Ataque")]
    public GameObject projetilPrefab; // Prefab do projétil
    public Transform pontoRotacaoY; // Ponto que rotaciona no eixo Y
    public Transform pontoRotacaoX; // Ponto que rotaciona no eixo X
    public Transform pontoDeDisparo; // Ponto onde o projétil será disparado
    public float intervaloDeAtaque = 15f; // Tempo mínimo entre ataques
    public float alcanceDeAtaque = 100f; // Distância máxima para iniciar ataques
    public float velocidadeDoProjetil = 20f; // Velocidade do projétil (adicionada)

    private Transform jogador; // Referência ao jogador
    private float cronometroDeAtaque; // Contador para controlar o intervalo entre ataques

    private UFOMovimento ufoMovimento; // Referência ao script de movimento

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
        // Atualiza o cronômetro de ataque
        cronometroDeAtaque += Time.deltaTime;

        if (cronometroDeAtaque >= intervaloDeAtaque)
        {
            DispararProjetil();
            cronometroDeAtaque = 0f; // Reinicia o cronômetro
        }
    }

    private void DispararProjetil()
    {
        if (projetilPrefab != null && pontoDeDisparo != null && jogador != null)
        {
            // Calcula a direção do jogador
            Vector3 direcao = (jogador.position - pontoDeDisparo.position).normalized;

            // Instancia o projétil no ponto de disparo
            GameObject projetil = Instantiate(projetilPrefab, pontoDeDisparo.position, Quaternion.LookRotation(direcao));

            // Configura a direção e a rotação do projétil
            Rigidbody rb = projetil.GetComponent<Rigidbody>();
            if (rb != null)
            {
                // Configura a velocidade do projétil
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
