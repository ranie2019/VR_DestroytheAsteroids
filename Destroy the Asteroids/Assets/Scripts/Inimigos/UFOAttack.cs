using UnityEngine;

public class UFOAttack : MonoBehaviour
{
    [Header("Configura��es de Ataque")]
    public GameObject projetilPrefab; // Prefab do proj�til
    public Transform pontoDeDisparo; // Ponto onde o proj�til ser� disparado
    public float intervaloDeAtaque = 15f; // Tempo m�nimo entre ataques
    public float alcanceDeAtaque = 100f; // Dist�ncia m�xima para iniciar ataques
    public float velocidadeDoProjetil = 20f; // Velocidade do proj�til

    private Transform jogador; // Refer�ncia ao jogador
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
            Debug.LogError("Jogador n�o encontrado! Certifique-se de que h� um objeto com a tag 'Player' na cena.");
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
        if (projetilPrefab == null)
        {
            Debug.LogError("Prefab do proj�til n�o configurado!");
            return;
        }

        if (pontoDeDisparo == null)
        {
            Debug.LogError("Ponto de disparo n�o configurado!");
            return;
        }

        // Calcula a dire��o do jogador
        Vector3 direcao = (jogador.position - pontoDeDisparo.position).normalized;

        // Instancia o proj�til no ponto de disparo
        GameObject projetil = Instantiate(projetilPrefab, pontoDeDisparo.position, Quaternion.LookRotation(direcao));

        // Configura a dire��o e a rota��o do proj�til
        Rigidbody rb = projetil.GetComponent<Rigidbody>();
        if (rb != null)
        {
            rb.linearVelocity = direcao * velocidadeDoProjetil;
        }
        else
        {
            Debug.LogError("O prefab do proj�til n�o possui um componente Rigidbody!");
        }
    }

    private void OnDrawGizmosSelected()
    {
        // Desenha o alcance de ataque no editor
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, alcanceDeAtaque);
    }
}
