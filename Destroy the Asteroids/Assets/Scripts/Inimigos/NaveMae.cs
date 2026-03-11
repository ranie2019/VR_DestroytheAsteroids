using UnityEngine;

public class NaveMae : MonoBehaviour
{
    [Header("Movimento (Loop nos pontos)")]
    public Transform[] pontos;              // 1,2,3,4...
    public float velocidade = 3f;
    public float velocidadeRotacao = 8f;
    public float distanciaParaTrocar = 0.3f;

    [Header("Suavização da curva")]
    [Tooltip("Distância antes do ponto em que a nave começa a preparar a curva.")]
    public float distanciaAntecipacaoCurva = 4f;

    [Tooltip("Velocidade com que a direção real do movimento gira para a nova direção.")]
    public float velocidadeCurva = 90f;

    [Header("Correção de eixo (se a nave anda de lado)")]
    public Vector3 modeloForwardLocal = new Vector3(0, 0, 1);
    public Vector3 modeloUpLocal = new Vector3(0, 1, 0);

    [Header("Visual (opcional)")]
    public Transform visual;

    [Header("Barbatanas / Pás (onda de peixe)")]
    public Transform[] pasEsquerda; // 3
    public Transform[] pasDireita;  // 3

    [Range(0f, 50f)] public float anguloMax = 50f;
    public float frequencia = 2f;
    [Range(0f, 3f)] public float atrasoEntrePas = 0.25f;

    public Vector3 eixoLocalRotacao = Vector3.right;
    public bool inverterDireita = true;

    [Header("Objetos que giram no eixo Z (OUTROS, NÃO SÃO AS PÁS)")]
    public Transform objetoRotacaoZ1;
    public Transform objetoRotacaoZ2;

    [Tooltip("Velocidade de rotação (graus por segundo) no eixo Z.")]
    public float velocidadeRotacaoZ = 200f;

    [Header("Spawn ao chegar no Ponto 1")]
    [Tooltip("Prefab do objeto que vai ser spawnado quando chegar no ponto 1.")]
    public GameObject prefabParaSpawnar;

    [Tooltip("Quantidade de objetos para spawnar toda vez que chegar no ponto 1.")]
    public int quantidadeSpawn = 10;

    [Tooltip("Ponto exato onde os drones vão nascer. Se vazio, usa o offset da nave.")]
    public Transform pontoDeSpawnDosDrones;

    [Tooltip("Offset (posição) a partir da nave para spawnar (usado só se não houver ponto de spawn).")]
    public Vector3 offsetSpawn = new Vector3(0f, 0.5f, 0f);

    [Tooltip("Raio aleatório ao redor do ponto de spawn (0 = sempre no mesmo lugar).")]
    public float raioAleatorio = 0f;

    [Tooltip("Se preencher, os objetos spawnados viram filhos desse Transform.")]
    public Transform paiDosSpawns;

    private bool jaSpawnouNoPonto1NestaChegada = false;
    private int indice = 0;

    private Quaternion[] rotInicialEsq;
    private Quaternion[] rotInicialDir;

    // NOVO: direção real usada no movimento
    private Vector3 direcaoMovimentoAtual;

    void Awake()
    {
        if (visual == null) visual = transform;

        rotInicialEsq = SalvarRotInicial(pasEsquerda);
        rotInicialDir = SalvarRotInicial(pasDireita);

        if (pontos != null && pontos.Length > 0)
            indice = Mathf.Clamp(indice, 0, pontos.Length - 1);

        // Inicializa a direção de movimento
        if (pontos != null && pontos.Length > 0 && pontos[indice] != null)
        {
            Vector3 dirInicial = (pontos[indice].position - transform.position);
            if (dirInicial.sqrMagnitude > 0.0001f)
                direcaoMovimentoAtual = dirInicial.normalized;
            else
                direcaoMovimentoAtual = transform.forward;
        }
        else
        {
            direcaoMovimentoAtual = transform.forward;
        }
    }

    void Update()
    {
        if (pontos == null || pontos.Length == 0 || pontos[indice] == null)
            return;

        // ===== MOVIMENTO =====
        Vector3 pos = transform.position;
        Vector3 alvoAtual = pontos[indice].position;
        Vector3 vetorParaAlvo = alvoAtual - pos;
        float dist = vetorParaAlvo.magnitude;

        // índice do próximo ponto (para antecipar a curva)
        int proximoIndice = (indice + 1) % pontos.Length;
        Vector3 direcaoDesejada;

        if (dist > 0.0001f)
        {
            direcaoDesejada = vetorParaAlvo.normalized;
        }
        else
        {
            direcaoDesejada = direcaoMovimentoAtual.sqrMagnitude > 0.0001f ? direcaoMovimentoAtual : transform.forward;
        }

        // ===== ANTECIPAÇÃO DA CURVA =====
        // Quando estiver perto do ponto atual, mistura a direção do ponto atual com a do próximo trecho
        if (pontos.Length > 1 && dist <= distanciaAntecipacaoCurva && pontos[proximoIndice] != null)
        {
            Vector3 dirTrechoAtual = vetorParaAlvo.sqrMagnitude > 0.0001f ? vetorParaAlvo.normalized : direcaoDesejada;
            Vector3 dirProximoTrecho = (pontos[proximoIndice].position - alvoAtual).normalized;

            // blend = 0 longe da curva / blend = 1 perto do ponto
            float blend = 1f - Mathf.InverseLerp(distanciaParaTrocar, distanciaAntecipacaoCurva, dist);
            blend = Mathf.Clamp01(blend);

            direcaoDesejada = Vector3.Slerp(dirTrechoAtual, dirProximoTrecho, blend).normalized;
        }

        // ===== SUAVIZAÇÃO REAL DA DIREÇÃO DE MOVIMENTO =====
        if (direcaoMovimentoAtual.sqrMagnitude < 0.0001f)
            direcaoMovimentoAtual = direcaoDesejada;

        direcaoMovimentoAtual = Vector3.RotateTowards(
            direcaoMovimentoAtual,
            direcaoDesejada,
            velocidadeCurva * Mathf.Deg2Rad * Time.deltaTime,
            0f
        ).normalized;

        // anda usando a direção suavizada
        transform.position += direcaoMovimentoAtual * (velocidade * Time.deltaTime);

        // ===== TROCA DE PONTO =====
        // troca quando chega perto OU quando já passou da região do ponto
        Vector3 novoVetorParaAlvo = alvoAtual - transform.position;
        float novaDist = novoVetorParaAlvo.magnitude;

        bool chegouNoPonto = novaDist <= distanciaParaTrocar;

        // detecta se passou pelo ponto enquanto já estava muito perto dele
        bool passouDoPonto = false;
        if (dist <= distanciaAntecipacaoCurva)
        {
            float dot = Vector3.Dot(novoVetorParaAlvo.normalized, direcaoMovimentoAtual);
            passouDoPonto = dot < -0.15f;
        }

        if (chegouNoPonto || passouDoPonto)
        {
            if (indice == 0 && !jaSpawnouNoPonto1NestaChegada)
            {
                SpawnarNoPonto1();
                jaSpawnouNoPonto1NestaChegada = true;
            }

            indice = (indice + 1) % pontos.Length;

            if (indice != 0)
                jaSpawnouNoPonto1NestaChegada = false;
        }

        // ===== ROTAÇÃO VISUAL =====
        // Usa a direção real do movimento, não a direção crua do alvo
        if (direcaoMovimentoAtual.sqrMagnitude > 0.0001f)
        {
            Quaternion look = Quaternion.LookRotation(direcaoMovimentoAtual, Vector3.up);

            Quaternion correcaoModelo =
                Quaternion.Inverse(
                    Quaternion.LookRotation(modeloForwardLocal.normalized, modeloUpLocal.normalized)
                );

            Quaternion rotFinal = look * correcaoModelo;
            visual.rotation = Quaternion.Slerp(visual.rotation, rotFinal, velocidadeRotacao * Time.deltaTime);
        }

        // ===== BARBATANAS (NÃO ALTERADO) =====
        float t = Time.time * frequencia;
        AnimarLado(pasEsquerda, rotInicialEsq, t, invert: false);
        AnimarLado(pasDireita, rotInicialDir, t, invert: inverterDireita);

        // ===== ROTAÇÃO EIXO Z (APENAS NOS 2 OBJETOS) =====
        GirarNoEixoZ(objetoRotacaoZ1);
        GirarNoEixoZ(objetoRotacaoZ2);
    }

    private void SpawnarNoPonto1()
    {
        if (prefabParaSpawnar == null) return;

        int qtd = Mathf.Max(0, quantidadeSpawn);

        Vector3 basePos;

        // Se existir um ponto específico, usa ele
        if (pontoDeSpawnDosDrones != null)
            basePos = pontoDeSpawnDosDrones.position;
        else
            basePos = transform.TransformPoint(offsetSpawn);

        for (int i = 0; i < qtd; i++)
        {
            Vector3 pos = basePos;

            if (raioAleatorio > 0f)
            {
                Vector2 r = Random.insideUnitCircle * raioAleatorio;
                pos += new Vector3(r.x, 0f, r.y);
            }

            if (paiDosSpawns != null)
                Instantiate(prefabParaSpawnar, pos, Quaternion.identity, paiDosSpawns);
            else
                Instantiate(prefabParaSpawnar, pos, Quaternion.identity);
        }
    }

    private void GirarNoEixoZ(Transform obj)
    {
        if (obj == null) return;
        obj.Rotate(0f, 0f, velocidadeRotacaoZ * Time.deltaTime, Space.Self);
    }

    private void AnimarLado(Transform[] pas, Quaternion[] rotIni, float t, bool invert)
    {
        if (pas == null || rotIni == null) return;

        for (int i = 0; i < pas.Length; i++)
        {
            var p = pas[i];
            if (!p) continue;

            float fase = i * atrasoEntrePas;
            float s = Mathf.Sin(t - fase);

            float ang = s * anguloMax;
            if (invert) ang = -ang;

            Quaternion rotAlvo = rotIni[i] * Quaternion.AngleAxis(ang, eixoLocalRotacao.normalized);
            p.localRotation = rotAlvo;
        }
    }

    private Quaternion[] SalvarRotInicial(Transform[] pas)
    {
        if (pas == null) return null;

        var arr = new Quaternion[pas.Length];
        for (int i = 0; i < pas.Length; i++)
            arr[i] = pas[i] ? pas[i].localRotation : Quaternion.identity;

        return arr;
    }
}