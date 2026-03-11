using UnityEngine;
using System.Linq;

public class DroneUfo : MonoBehaviour
{
    [Header("Pontos do caminho (Transforms)")]
    public Transform[] pontos;

    [Header("Auto-herdar pontos (para clones)")]
    [Tooltip("Se true, quando o clone nascer e estiver sem pontos, ele tenta herdar automaticamente.")]
    public bool autoHerdarPontos = true;

    [Tooltip("Nome do objeto na cena que contém os pontos como filhos (ex: 'Drone Caminho').")]
    public string nomeDoCaminhoNaCena = "Drone Caminho";

    [Header("Movimento")]
    public float velocidade = 5f;
    public float distanciaParaChegar = 0.2f;

    [Header("Rotação (opcional)")]
    public bool rotacionarParaDirecao = true;
    public float velocidadeRotacao = 8f;

    [Header("Regras de escolha")]
    public bool evitarRepetirMesmoPonto = true;

    [Tooltip("Evita escolher um novo ponto muito perto da posição atual do drone.")]
    public float distanciaMinimaNovoAlvo = 1f;

    [Tooltip("Número máximo de tentativas para achar um ponto melhor.")]
    public int maxTentativasEscolha = 10;

    private int indiceAtual = -1;
    private Transform alvo;

    // Cache global: se um Drone tiver pontos preenchidos, os clones usam.
    private static Transform[] pontosCache;

    void Awake()
    {
        PrepararPontosSePrecisar();
    }

    void OnEnable()
    {
        PrepararPontosSePrecisar();
    }

    void Start()
    {
        if (pontos != null && pontos.Length > 0)
            EscolherNovoAlvo();
    }

    void Update()
    {
        if (pontos == null || pontos.Length == 0) return;

        if (alvo == null)
        {
            EscolherNovoAlvo();
            if (alvo == null) return;
        }

        Vector3 direcao = alvo.position - transform.position;
        float dist = direcao.magnitude;

        // Se chegou, escolhe outro alvo
        if (dist <= distanciaParaChegar)
        {
            EscolherNovoAlvo();

            if (alvo == null) return;

            direcao = alvo.position - transform.position;
            dist = direcao.magnitude;

            // Se ainda assim estiver colado no novo alvo, sai
            if (dist <= 0.0001f) return;
        }

        // Move sem passar do alvo
        transform.position = Vector3.MoveTowards(
            transform.position,
            alvo.position,
            velocidade * Time.deltaTime
        );

        if (rotacionarParaDirecao && direcao.sqrMagnitude > 0.0001f)
        {
            Quaternion rot = Quaternion.LookRotation(direcao.normalized, Vector3.up);
            transform.rotation = Quaternion.Slerp(transform.rotation, rot, velocidadeRotacao * Time.deltaTime);
        }
    }

    // ====== IMPORTANTE: clones herdam pontos aqui ======
    private void PrepararPontosSePrecisar()
    {
        // Se este Drone já tem pontos válidos, salva no cache e pronto.
        if (TemPontosValidos(pontos))
        {
            pontosCache = pontos;
            return;
        }

        if (!autoHerdarPontos) return;

        // 1) Tenta usar cache
        if (TemPontosValidos(pontosCache))
        {
            pontos = pontosCache;
            return;
        }

        // 2) Tenta copiar de outro DroneUfo na cena que tenha pontos
        var outro = FindObjectsOfType<DroneUfo>(true)
            .FirstOrDefault(d => d != this && TemPontosValidos(d.pontos));

        if (outro != null)
        {
            pontos = outro.pontos;
            pontosCache = pontos;
            return;
        }

        // 3) Tenta pegar do objeto "Drone Caminho" (filhos viram pontos)
        var caminho = GameObject.Find(nomeDoCaminhoNaCena);
        if (caminho != null)
        {
            var filhos = caminho.GetComponentsInChildren<Transform>(true)
                .Where(t => t != caminho.transform)
                .OrderBy(t => NomeParaNumero(t.name))
                .ToArray();

            if (TemPontosValidos(filhos))
            {
                pontos = filhos;
                pontosCache = pontos;
            }
        }
    }

    private bool TemPontosValidos(Transform[] arr)
    {
        if (arr == null || arr.Length == 0) return false;

        for (int i = 0; i < arr.Length; i++)
        {
            if (arr[i] == null) return false;
        }

        return true;
    }

    private int NomeParaNumero(string nome)
    {
        int n;
        if (int.TryParse(nome, out n)) return n;
        return int.MaxValue;
    }

    private void EscolherNovoAlvo()
    {
        if (pontos == null || pontos.Length == 0) return;

        Transform melhorPonto = null;
        int melhorIndice = -1;

        for (int tentativa = 0; tentativa < maxTentativasEscolha; tentativa++)
        {
            int novoIndice = Random.Range(0, pontos.Length);

            if (evitarRepetirMesmoPonto && pontos.Length > 1 && novoIndice == indiceAtual)
                continue;

            Transform candidato = pontos[novoIndice];
            if (candidato == null) continue;

            float distDoDrone = Vector3.Distance(transform.position, candidato.position);

            // tenta evitar pontos muito perto
            if (distDoDrone >= distanciaMinimaNovoAlvo)
            {
                melhorPonto = candidato;
                melhorIndice = novoIndice;
                break;
            }

            // guarda pelo menos alguma opção caso não ache uma ideal
            if (melhorPonto == null)
            {
                melhorPonto = candidato;
                melhorIndice = novoIndice;
            }
        }

        if (melhorPonto != null)
        {
            indiceAtual = melhorIndice;
            alvo = melhorPonto;
        }
    }

    // Opcional: se você quiser setar via spawner também
    public void SetPontos(Transform[] novosPontos)
    {
        pontos = novosPontos;

        if (TemPontosValidos(pontos))
        {
            pontosCache = pontos;
            EscolherNovoAlvo();
        }
    }
}