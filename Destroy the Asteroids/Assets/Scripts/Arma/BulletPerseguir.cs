using UnityEngine;

public class BulletPerseguir : MonoBehaviour
{
    [Header("Configura��es de Detec��o")]
    [Tooltip("Tag do alvo que ser� perseguido.")]
    [SerializeField] private string tagAlvo = "Asteroid";

    [Tooltip("Tempo de vida da bala em segundos.")]
    [SerializeField] private float tempoDeVida = 5f;

    [Tooltip("Tempo estimado para a bala alcan�ar o alvo (em segundos).")]
    [SerializeField] private float tempoDeChegada = 1f;

    [Tooltip("Tempo de atraso antes de come�ar a perseguir o alvo.")]
    [SerializeField] private float tempoDeDelay = 0.5f;

    [Tooltip("Velocidade da bala em linha reta durante o tempo de delay.")]
    [SerializeField] private float velocidadeInicial = 10f;

    private Transform alvo;
    private float velocidadeDinamica;
    private bool podePerseguir = false;

    private void Start()
    {
        // Valida as vari�veis de configura��o
        if (tempoDeChegada <= 0) tempoDeChegada = 1f;
        if (tempoDeVida <= 0) tempoDeVida = 5f;
        if (tempoDeDelay < 0) tempoDeDelay = 0;

        // Encontra o alvo mais pr�ximo com a tag especificada
        AtualizarAlvo();

        // Calcula a velocidade dinamicamente com base no tempo de chegada
        CalcularVelocidade();

        // Inicia o atraso antes de perseguir
        StartCoroutine(DelayParaPerseguir());

        // Destroi a bala ap�s o tempo de vida
        Destroy(gameObject, tempoDeVida);
    }

    private void Update()
    {
        if (!podePerseguir)
        {
            // Movimento inicial durante o delay (em linha reta)
            transform.position += transform.forward * velocidadeInicial * Time.deltaTime;
        }
        else if (alvo != null)
        {
            // Move a bala na dire��o do alvo
            Vector3 direcao = (alvo.position - transform.position).normalized;
            transform.position += direcao * velocidadeDinamica * Time.deltaTime;

            // Rotaciona a bala para olhar na dire��o do alvo
            Quaternion rotacaoDesejada = Quaternion.LookRotation(direcao);
            transform.rotation = Quaternion.Slerp(transform.rotation, rotacaoDesejada, Time.deltaTime * 5f);
        }
        else if (podePerseguir && alvo == null)
        {
            // Caso o alvo seja destru�do, tenta encontrar outro alvo
            AtualizarAlvo();
        }
    }

    /// <summary>
    /// Corrotina que aplica o atraso antes de come�ar a perseguir o alvo.
    /// </summary>
    private System.Collections.IEnumerator DelayParaPerseguir()
    {
        yield return new WaitForSeconds(tempoDeDelay);
        podePerseguir = true;
    }

    /// <summary>
    /// Atualiza o alvo mais pr�ximo.
    /// </summary>
    private void AtualizarAlvo()
    {
        alvo = EncontrarAlvoMaisProximo();
        CalcularVelocidade(); // Recalcula a velocidade caso um novo alvo seja encontrado
    }

    /// <summary>
    /// Encontra o alvo mais pr�ximo dentro do alcance.
    /// </summary>
    /// <returns>Transform do alvo mais pr�ximo ou null se nenhum for encontrado.</returns>
    private Transform EncontrarAlvoMaisProximo()
    {
        GameObject[] alvos = GameObject.FindGameObjectsWithTag(tagAlvo);
        Transform alvoMaisProximo = null;
        float menorDistancia = Mathf.Infinity;

        foreach (GameObject obj in alvos)
        {
            // Ignora objetos inativos
            if (!obj.activeInHierarchy)
                continue;

            float distancia = Vector3.Distance(transform.position, obj.transform.position);
            if (distancia < menorDistancia)
            {
                menorDistancia = distancia;
                alvoMaisProximo = obj.transform;
            }
        }

        return alvoMaisProximo;
    }

    /// <summary>
    /// Calcula a velocidade necess�ria para atingir o alvo no tempo especificado.
    /// </summary>
    private void CalcularVelocidade()
    {
        if (alvo != null)
        {
            float distancia = Vector3.Distance(transform.position, alvo.position);
            velocidadeDinamica = distancia / tempoDeChegada;
        }
        else
        {
            velocidadeDinamica = 0f; // Para evitar movimento indesejado se n�o houver alvo
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        // Destr�i a bala ao colidir com o alvo
        if (other.CompareTag(tagAlvo))
        {
            Destroy(gameObject);
        }
    }
}
