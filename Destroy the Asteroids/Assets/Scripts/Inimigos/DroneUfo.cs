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
        if (alvo == null) { EscolherNovoAlvo(); return; }

        Vector3 direcao = (alvo.position - transform.position);
        float dist = direcao.magnitude;

        if (dist <= distanciaParaChegar)
        {
            EscolherNovoAlvo();
            return;
        }

        transform.position += direcao.normalized * velocidade * Time.deltaTime;

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
            if (arr[i] == null) return false;
        return true;
    }

    private int NomeParaNumero(string nome)
    {
        // se o ponto chama "1", "2", "3" ele ordena certo.
        // se não for número, joga pra final.
        int n;
        if (int.TryParse(nome, out n)) return n;
        return int.MaxValue;
    }

    private void EscolherNovoAlvo()
    {
        if (pontos == null || pontos.Length == 0) return;

        int novoIndice = Random.Range(0, pontos.Length);

        if (evitarRepetirMesmoPonto && pontos.Length > 1)
        {
            while (novoIndice == indiceAtual)
                novoIndice = Random.Range(0, pontos.Length);
        }

        indiceAtual = novoIndice;
        alvo = pontos[indiceAtual];
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