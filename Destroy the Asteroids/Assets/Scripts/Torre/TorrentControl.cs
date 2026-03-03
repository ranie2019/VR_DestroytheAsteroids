using UnityEngine;
using System.Collections;

public class TorrentControl : MonoBehaviour
{
    [Header("Detecção do Alvo")]
    [Tooltip("Tag dos alvos que a torreta pode detectar e atacar.")]
    [SerializeField] private string tagAlvo = "Asteroid";

    [Tooltip("Alcance máximo da torreta.")]
    [SerializeField] private float alcance = 50f;

    [Tooltip("Intervalo (segundos) para procurar o alvo novamente (evita custo todo frame).")]
    [SerializeField] private float intervaloBuscaAlvo = 0.25f;

    [Header("Partes da Torreta")]
    [Tooltip("Base que gira no eixo Y.")]
    [SerializeField] private Transform baseY;

    [Tooltip("Cabeça/cano que gira no eixo X.")]
    [SerializeField] private Transform cabecaX;

    [Tooltip("Pontos de disparo (múltiplos canos).")]
    [SerializeField] private Transform[] pontosDisparo;

    [Header("Disparo")]
    [Tooltip("Prefab do projétil.")]
    [SerializeField] private GameObject projetilPrefab;

    [Tooltip("Tiros por segundo.")]
    [SerializeField] private float taxaDisparo = 5f;

    [Tooltip("Velocidade do projétil (m/s).")]
    [SerializeField] private float velocidadeProjetil = 25f;

    [Tooltip("Velocidade de rotação da base (graus/s).")]
    [SerializeField] private float velocidadeRotacaoBase = 180f;

    [Tooltip("Velocidade de rotação da cabeça (graus/s).")]
    [SerializeField] private float velocidadeRotacaoCabeca = 180f;

    [Header("Áudio")]
    [SerializeField] private AudioClip laserSFX;

    private AudioSource audioSource;
    private TurrentAnimator anim;

    private Transform alvoAtual;
    private int indicePonto = 0;
    private float proximoDisparoEm = 0f;
    private float proximaBuscaEm = 0f;

    private void Awake()
    {
        audioSource = GetComponent<AudioSource>() ?? gameObject.AddComponent<AudioSource>();
        audioSource.playOnAwake = false;

        anim = GetComponentInChildren<TurrentAnimator>();
    }

    private void Update()
    {
        // Buscar alvo em intervalos (evita custo todo frame)
        if (Time.time >= proximaBuscaEm)
        {
            proximaBuscaEm = Time.time + intervaloBuscaAlvo;
            alvoAtual = EncontrarAlvoMaisProximo();
        }

        // Se tem alvo, mirar e atirar
        if (alvoAtual != null)
        {
            MirarNoAlvo(alvoAtual);

            if (Time.time >= proximoDisparoEm)
            {
                proximoDisparoEm = Time.time + (1f / Mathf.Max(0.01f, taxaDisparo));
                Disparar();
            }
        }
    }

    private Transform EncontrarAlvoMaisProximo()
    {
        GameObject[] alvos = GameObject.FindGameObjectsWithTag(tagAlvo);
        Transform melhor = null;
        float melhorDistSqr = float.MaxValue;

        Vector3 origem = transform.position;
        float alcanceSqr = alcance * alcance;

        for (int i = 0; i < alvos.Length; i++)
        {
            GameObject go = alvos[i];
            if (!go || !go.activeInHierarchy) continue;

            float distSqr = (go.transform.position - origem).sqrMagnitude;
            if (distSqr <= alcanceSqr && distSqr < melhorDistSqr)
            {
                melhorDistSqr = distSqr;
                melhor = go.transform;
            }
        }

        return melhor;
    }

    private void MirarNoAlvo(Transform alvo)
    {
        if (!baseY || !cabecaX) return;

        Vector3 alvoPos = alvo.position;

        // Base: gira só no Y
        Vector3 dirBase = alvoPos - baseY.position;
        dirBase.y = 0f;

        if (dirBase.sqrMagnitude > 0.0001f)
        {
            Quaternion rotBaseDesejada = Quaternion.LookRotation(dirBase.normalized, Vector3.up);
            baseY.rotation = Quaternion.RotateTowards(
                baseY.rotation,
                rotBaseDesejada,
                velocidadeRotacaoBase * Time.deltaTime
            );
        }

        // Cabeça: gira no X (pitch)  ✅ CORRIGIDO (invertido para apontar pra cima)
        Vector3 dirLocal = baseY.InverseTransformDirection((alvoPos - cabecaX.position).normalized);

        // inversão do pitch resolve cabeça apontando para baixo (eixo do modelo invertido)
        float pitch = -Mathf.Atan2(dirLocal.y, dirLocal.z) * Mathf.Rad2Deg;

        Quaternion rotCabecaDesejada = Quaternion.Euler(pitch, 0f, 0f);
        cabecaX.localRotation = Quaternion.RotateTowards(
            cabecaX.localRotation,
            rotCabecaDesejada,
            velocidadeRotacaoCabeca * Time.deltaTime
        );
    }

    private void Disparar()
    {
        if (projetilPrefab == null || pontosDisparo == null || pontosDisparo.Length == 0)
            return;

        Transform ponto = pontosDisparo[indicePonto];
        if (ponto == null || alvoAtual == null) return;

        // Direção real do alvo (não depende do eixo do modelo)
        Vector3 dir = (alvoAtual.position - ponto.position).normalized;

        // Instancia o projétil olhando para o alvo
        GameObject bala = Instantiate(projetilPrefab, ponto.position, Quaternion.LookRotation(dir, Vector3.up));

        // Velocidade consistente na direção do alvo
        Rigidbody rb = bala.GetComponent<Rigidbody>();
        if (rb != null)
        {
            rb.useGravity = false;
            rb.linearVelocity = dir * velocidadeProjetil;
        }

        // Som
        if (laserSFX != null) audioSource.PlayOneShot(laserSFX);

        // Animação (se existir)
        if (anim != null) StartCoroutine(RotinaAnimDisparo());

        // Próximo cano
        indicePonto = (indicePonto + 1) % pontosDisparo.Length;
    }

    private IEnumerator RotinaAnimDisparo()
    {
        if (anim == null) yield break;

        anim.ActivateFireAnimation();
        yield return new WaitForSeconds(0.15f);
        anim.DeactivateFireAnimation();
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.green;
        Gizmos.DrawWireSphere(transform.position, alcance);

        if (alvoAtual != null && pontosDisparo != null && pontosDisparo.Length > 0)
        {
            Transform p = pontosDisparo[Mathf.Clamp(indicePonto, 0, pontosDisparo.Length - 1)];
            if (p != null)
            {
                Gizmos.color = Color.red;
                Gizmos.DrawLine(p.position, alvoAtual.position);
            }
        }
    }
}