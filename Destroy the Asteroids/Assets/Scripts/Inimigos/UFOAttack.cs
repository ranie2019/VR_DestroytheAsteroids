using UnityEngine;

public class UFOAttack : MonoBehaviour
{
    [Header("Configurações de Ataque")]
    public GameObject projetilPrefab; // Prefab do projétil
    public Transform pontoDeDisparo; // Ponto onde o projétil será disparado
    public float intervaloDeAtaque = 15f; // Tempo mínimo entre ataques
    public float alcanceDeAtaque = 100f; // Distância máxima para iniciar ataques
    public float velocidadeDoProjetil = 20f; // Velocidade do projétil

    private Transform jogador; // Referência ao jogador
    private float cronometroDeAtaque; // Contador para controlar o intervalo entre ataques

    private void Start()
    {
        // Procurar o jogador na cena
        GameObject playerObj = GameObject.FindWithTag("Player");
        if (playerObj != null)
        {
            jogador = playerObj.transform;
        }
        else
        {
            Debug.LogError("Jogador não encontrado! Certifique-se de que há um objeto com a tag 'Player' na cena.");
        }
    }

    private void Update()
    {
        if (jogador == null) return;

        float distanciaDoJogador = Vector3.Distance(transform.position, jogador.position);

        // Se estiver em alcance, ataca
        if (distanciaDoJogador <= alcanceDeAtaque)
        {
            Atacar();
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
        if (projetilPrefab == null)
        {
            Debug.LogError("Prefab do projétil não configurado!");
            return;
        }

        if (pontoDeDisparo == null)
        {
            Debug.LogError("Ponto de disparo não configurado!");
            return;
        }

        // Calcula a direção do jogador
        Vector3 direcao = (jogador.position - pontoDeDisparo.position).normalized;

        // Instancia o projétil no ponto de disparo
        GameObject projetil = Instantiate(projetilPrefab, pontoDeDisparo.position, Quaternion.LookRotation(direcao));

        // Configura a direção e a rotação do projétil
        Rigidbody rb = projetil.GetComponent<Rigidbody>();
        if (rb != null)
        {
            rb.velocity = direcao * velocidadeDoProjetil;
        }
        else
        {
            Debug.LogError("O prefab do projétil não possui um componente Rigidbody!");
        }
    }

    private void OnDrawGizmosSelected()
    {
        // Desenha o alcance de ataque no editor
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, alcanceDeAtaque);
    }
}
