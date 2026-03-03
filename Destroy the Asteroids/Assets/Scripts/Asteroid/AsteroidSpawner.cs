using UnityEngine;

public class AsteroidSpawner : MonoBehaviour
{
    [Header("Área de geração")]
    [Tooltip("Tamanho da área onde os asteroides podem nascer.")]
    [SerializeField] private Vector3 tamanhoArea = new Vector3(10, 5, 10);

    [Header("Frequência de spawn (segundos)")]
    [Tooltip("Intervalo entre spawns durante a fase ATIVA (quanto menor, mais asteroides).")]
    [SerializeField] private float intervaloSpawn = 0.6f;

    [Tooltip("Intervalo mínimo permitido (pra não ficar zero/negativo).")]
    [SerializeField] private float intervaloSpawnMinimo = 0.1f;

    [Header("Tempos do ciclo (segundos)")]
    [Tooltip("Tempo que fica spawnando (fase ATIVA).")]
    [SerializeField] private float tempoAtivo = 10f;

    [Tooltip("Tempo que fica sem spawnar (fase PAUSA).")]
    [SerializeField] private float tempoPausa = 5f;

    [Tooltip("Tempo mínimo permitido de pausa.")]
    [SerializeField] private float tempoPausaMinimo = 0.5f;

    [Header("Dificuldade por tempo (a cada 30s)")]
    [Tooltip("A cada 30 segundos, diminui o intervalo de spawn em 0.1.")]
    [SerializeField] private float reduzirIntervaloSpawn = 0.1f;

    [Tooltip("A cada 30 segundos, diminui o tempo de pausa em 0.5.")]
    [SerializeField] private float reduzirTempoPausa = 0.5f;

    [Tooltip("De quanto em quanto tempo aplica a dificuldade.")]
    [SerializeField] private float intervaloDificuldadeSegundos = 30f;

    [Header("Modelos de Asteroides")]
    [SerializeField] private GameObject[] modelosAsteroides;

    [Header("Organização (opcional)")]
    [Tooltip("Se preencher, os asteroides vão nascer como filhos desse objeto (facilita limpar/resetar).")]
    [SerializeField] private Transform paiDosAsteroides;

    private enum Estado { Ativo, Pausa }
    private Estado estado = Estado.Ativo;

    private float timerEstado = 0f;
    private float timerSpawn = 0f;

    private float timerDificuldade = 0f;

    private float intervaloSpawnOriginal;
    private float tempoAtivoOriginal;
    private float tempoPausaOriginal;

    private void Awake()
    {
        intervaloSpawnOriginal = intervaloSpawn;
        tempoAtivoOriginal = tempoAtivo;
        tempoPausaOriginal = tempoPausa;
    }

    private void OnEnable()
    {
        ResetSpawner();
    }

    private void Update()
    {
        // ===== 1) Dificuldade por tempo (a cada 30s) =====
        timerDificuldade += Time.deltaTime;
        if (timerDificuldade >= intervaloDificuldadeSegundos)
        {
            AplicarDificuldade();
            timerDificuldade = 0f;
        }

        // ===== 2) Lógica de ciclo (ATIVO / PAUSA) =====
        timerEstado += Time.deltaTime;

        if (estado == Estado.Ativo)
        {
            // Spawn contínuo enquanto estiver ATIVO
            timerSpawn += Time.deltaTime;
            if (timerSpawn >= intervaloSpawn)
            {
                SpawnAsteroid();
                timerSpawn = 0f;
            }

            // Terminou o tempo ATIVO? vai pra PAUSA
            if (timerEstado >= tempoAtivo)
            {
                estado = Estado.Pausa;
                timerEstado = 0f;
                timerSpawn = 0f;
            }
        }
        else // PAUSA
        {
            // PAUSA = não spawnar nada

            // Terminou a PAUSA? volta a ATIVO
            if (timerEstado >= tempoPausa)
            {
                estado = Estado.Ativo;
                timerEstado = 0f;
                timerSpawn = 0f;
            }
        }
    }

    private void AplicarDificuldade()
    {
        // Intervalo de spawn cai 0.1 a cada 30s
        intervaloSpawn = Mathf.Max(intervaloSpawn - reduzirIntervaloSpawn, intervaloSpawnMinimo);

        // Tempo de pausa cai 0.5 a cada 30s
        tempoPausa = Mathf.Max(tempoPausa - reduzirTempoPausa, tempoPausaMinimo);
    }

    private void SpawnAsteroid()
    {
        if (modelosAsteroides == null || modelosAsteroides.Length == 0) return;

        GameObject prefab = modelosAsteroides[Random.Range(0, modelosAsteroides.Length)];
        if (prefab == null) return;

        Vector3 pos = GetRandomSpawnPosition();

        if (paiDosAsteroides != null)
            Instantiate(prefab, pos, Quaternion.identity, paiDosAsteroides);
        else
            Instantiate(prefab, pos, Quaternion.identity);
    }

    private Vector3 GetRandomSpawnPosition()
    {
        return transform.position + new Vector3(
            Random.Range(-tamanhoArea.x / 2f, tamanhoArea.x / 2f),
            Random.Range(-tamanhoArea.y / 2f, tamanhoArea.y / 2f),
            Random.Range(-tamanhoArea.z / 2f, tamanhoArea.z / 2f)
        );
    }

    public void ResetSpawner()
    {
        estado = Estado.Ativo;
        timerEstado = 0f;
        timerSpawn = 0f;
        timerDificuldade = 0f;

        intervaloSpawn = intervaloSpawnOriginal;
        tempoAtivo = tempoAtivoOriginal;
        tempoPausa = tempoPausaOriginal;

        // Limpa asteroides se estiver usando paiDosAsteroides
        if (paiDosAsteroides != null)
        {
            for (int i = paiDosAsteroides.childCount - 1; i >= 0; i--)
                Destroy(paiDosAsteroides.GetChild(i).gameObject);
        }
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = new Color(0, 1, 0, 0.25f);
        Gizmos.DrawCube(transform.position, tamanhoArea);
    }
}