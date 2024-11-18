using UnityEngine;

public class BulletExplosao : MonoBehaviour
{
    [Header("Configura��es de Persegui��o")]
    [Tooltip("Velocidade da bala enquanto persegue o alvo.")]
    [SerializeField] private float velocidade = 20f;

    [Tooltip("Tag do alvo que ser� perseguido.")]
    [SerializeField] private string tagAlvo = "Asteroid";

    [Header("Configura��es de Explos�o")]
    [Tooltip("Prefab dos fragmentos gerados na explos�o.")]
    [SerializeField] private GameObject prefabFragmento;

    [Tooltip("N�mero de fragmentos gerados na explos�o.")]
    [SerializeField] private int quantidadeFragmentos = 5;

    [Tooltip("For�a aplicada aos fragmentos ao serem criados.")]
    [SerializeField] private float forcaFragmentos = 5f;

    [Tooltip("Tempo de vida da bala em segundos.")]
    [SerializeField] private float tempoDeVida = 5f;

    private Transform alvo;

    private void Start()
    {
        // Encontra o alvo mais pr�ximo com a tag especificada
        alvo = EncontrarAlvoMaisProximo();

        // Destroi a bala ap�s o tempo de vida
        Destroy(gameObject, tempoDeVida);
    }

    private void Update()
    {
        if (alvo != null)
        {
            // Move a bala na dire��o do alvo
            Vector3 direcao = (alvo.position - transform.position).normalized;
            transform.position += direcao * velocidade * Time.deltaTime;

            // Rotaciona a bala para olhar na dire��o do alvo
            transform.LookAt(alvo);
        }
        else
        {
            // Caso o alvo seja destru�do, tenta encontrar outro alvo
            alvo = EncontrarAlvoMaisProximo();
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        // Verifica se a bala colidiu com um alvo v�lido
        if (collision.gameObject.CompareTag(tagAlvo))
        {
            // Instancia os fragmentos
            Explodir();

            // Destroi a bala
            Destroy(gameObject);
        }
    }

    private void Explodir()
    {
        for (int i = 0; i < quantidadeFragmentos; i++)
        {
            // Cria um fragmento no local da explos�o
            GameObject fragmento = Instantiate(prefabFragmento, transform.position, Quaternion.identity);

            // Configura um nome �nico para o fragmento (opcional para depura��o)
            fragmento.name = $"Fragmento_{i + 1}";

            // Adiciona uma for�a aleat�ria ao fragmento
            if (fragmento.TryGetComponent(out Rigidbody rb))
            {
                Vector3 direcaoAleatoria = Random.onUnitSphere; // Dire��o aleat�ria
                rb.AddForce(direcaoAleatoria * forcaFragmentos, ForceMode.Impulse);
            }

            // (Opcional) Configura tempo de vida dos fragmentos para evitar excesso na cena
            Destroy(fragmento, 5f);
        }
    }

    private Transform EncontrarAlvoMaisProximo()
    {
        GameObject[] alvos = GameObject.FindGameObjectsWithTag(tagAlvo);
        Transform alvoMaisProximo = null;
        float menorDistancia = Mathf.Infinity;

        foreach (GameObject obj in alvos)
        {
            float distancia = Vector3.Distance(transform.position, obj.transform.position);
            if (distancia < menorDistancia)
            {
                menorDistancia = distancia;
                alvoMaisProximo = obj.transform;
            }
        }

        return alvoMaisProximo;
    }
}
