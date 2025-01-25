using UnityEngine;
using TMPro;
using UnityEngine.InputSystem;
using System.Collections.Generic;

public class MunicaoLimite : MonoBehaviour
{
    [Header("Configurações de Munição")]
    [Tooltip("Prefab da bala que será disparada.")]
    [SerializeField] private GameObject bulletPrefab;

    [Tooltip("Pontos de spawn das balas.")]
    [SerializeField] private List<Transform> bulletSpawnPoints;

    [Tooltip("Velocidade da bala disparada.")]
    [SerializeField] private float bulletSpeed = 20f;

    [Tooltip("Quantidade máxima de munição.")]
    [SerializeField] private int municaoMaxima = 40;

    [Tooltip("Tempo de recarga (em segundos).")]
    [SerializeField] private float tempoRecarga = 5f;

    [Tooltip("Texto para exibir a quantidade de munição.")]
    [SerializeField] private TMP_Text municaoText;

    [Header("Configurações de Áudio")]
    [Tooltip("Áudio do disparo da arma.")]
    [SerializeField] private AudioClip disparoSFX;

    [Tooltip("Quantidade de fontes de áudio para disparos.")]
    [SerializeField] private int audioSourcePoolSize = 5;

    private int municaoAtual; // Quantidade atual de munição disponível
    private bool recarregando = false; // Indica se está no processo de recarga

    private List<AudioSource> audioSourcePool; // Pool de fontes de áudio
    private int currentAudioSourceIndex = 0; // Índice atual no pool de áudio

    private InputAction dispararAction;

    private void OnEnable()
    {
        dispararAction = new InputAction(type: InputActionType.Button, binding: "<Mouse>/leftButton");
        dispararAction.Enable();
    }

    private void Start()
    {
        // Configura a munição inicial
        municaoAtual = municaoMaxima;
        AtualizarTextoMunicao();

        // Inicializa o pool de fontes de áudio
        audioSourcePool = new List<AudioSource>();
        for (int i = 0; i < audioSourcePoolSize; i++)
        {
            AudioSource newAudioSource = gameObject.AddComponent<AudioSource>();
            newAudioSource.clip = disparoSFX;
            newAudioSource.playOnAwake = false;
            audioSourcePool.Add(newAudioSource);
        }
    }

    private void Update()
    {
        // Verifica se o jogador tentou disparar
        if (dispararAction.WasPressedThisFrame() && !recarregando)
        {
            Disparar();
        }

        // Verifica se deve iniciar a recarga automática
        if (!recarregando && municaoAtual < municaoMaxima)
        {
            IniciarRecarga();
        }
    }

    public void Disparar()
    {
        if (municaoAtual > 0)
        {
            foreach (Transform spawnPoint in bulletSpawnPoints)
            {
                GameObject bullet = Instantiate(bulletPrefab, spawnPoint.position, spawnPoint.rotation);
                Rigidbody bulletRigidbody = bullet.GetComponent<Rigidbody>();
                if (bulletRigidbody != null)
                {
                    bulletRigidbody.velocity = spawnPoint.forward * bulletSpeed;
                }
            }

            // Reproduz o som do disparo usando o pool
            PlayDisparoSFX();

            municaoAtual--;
            AtualizarTextoMunicao();
        }
    }

    private void PlayDisparoSFX()
    {
        if (disparoSFX != null && audioSourcePool.Count > 0)
        {
            AudioSource currentSource = audioSourcePool[currentAudioSourceIndex];
            currentSource.Play();
            currentAudioSourceIndex = (currentAudioSourceIndex + 1) % audioSourcePool.Count;
        }
    }

    private void IniciarRecarga()
    {
        if (!recarregando && gameObject.activeInHierarchy)
        {
            recarregando = true;
            StartCoroutine(Recarregar());
        }
    }

    private System.Collections.IEnumerator Recarregar()
    {
        yield return new WaitForSeconds(tempoRecarga);
        municaoAtual = municaoMaxima;
        recarregando = false;
        AtualizarTextoMunicao();
    }

    private void AtualizarTextoMunicao()
    {
        municaoText.text = municaoAtual.ToString();
    }

    private void OnDisable()
    {
        dispararAction.Disable();
    }
}
