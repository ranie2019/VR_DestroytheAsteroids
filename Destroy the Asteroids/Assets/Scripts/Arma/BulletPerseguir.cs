using UnityEngine;

public class BulletPerseguir : MonoBehaviour
{
    [Header("Configura��es de Velocidade")]
    [Tooltip("Velocidade da bala enquanto persegue o alvo.")]
    [SerializeField] private float velocidade = 20f;

    [Header("Configura��es de Detec��o")]
    [Tooltip("Tag do alvo que ser� perseguido.")]
    [SerializeField] private string tagAlvo = "Asteroid";

    [Tooltip("Tempo de vida da bala em segundos.")]
    [SerializeField] private float tempoDeVida = 5f;

    private Transform alvo;

    private void Start()
    {
        // Encontra o alvo mais pr�ximo com a tag especificada
        AtualizarAlvo();

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
            AtualizarAlvo();
        }
    }

    private void AtualizarAlvo()
    {
        alvo = EncontrarAlvoMaisProximo();
    }

    private Transform EncontrarAlvoMaisProximo()
    {
        GameObject[] alvos = GameObject.FindGameObjectsWithTag(tagAlvo);
        Transform alvoMaisProximo = null;
        float menorDistancia = Mathf.Infinity;

        foreach (GameObject obj in alvos)
        {
            // Certifique-se de ignorar objetos inativos
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

    private void OnTriggerEnter(Collider other)
    {
        // Destr�i a bala ao colidir com o alvo
        if (other.CompareTag(tagAlvo))
        {
            Destroy(gameObject);
        }
    }
}
