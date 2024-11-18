using UnityEngine;
using TMPro;
using UnityEngine.InputSystem;

public class MunicaoLimite : MonoBehaviour
{
    [Header("Configura��es de Muni��o")]
    [Tooltip("Prefab da bala que ser� disparada.")]
    [SerializeField] private GameObject bulletPrefab;

    [Tooltip("Ponto de spawn das balas.")]
    [SerializeField] private Transform bulletSpawnPoint;

    [Tooltip("Velocidade da bala disparada.")]
    [SerializeField] private float bulletSpeed = 20f;

    [Tooltip("Quantidade m�xima de muni��o.")]
    [SerializeField] private int municaoMaxima = 40;

    [Tooltip("Tempo de recarga (em segundos).")]
    [SerializeField] private float tempoRecarga = 5f;

    [Tooltip("Texto para exibir a quantidade de muni��o.")]
    [SerializeField] private TMP_Text municaoText;

    [Header("Configura��es de �udio")]
    [Tooltip("�udio do disparo da arma.")]
    [SerializeField] private AudioClip disparoSFX;

    private int municaoAtual;
    private bool recarregando = false;
    private AudioSource audioSource;

    // Novo sistema de entrada
    private InputAction dispararAction;

    private void OnEnable()
    {
        // Configura o Input Action para disparar
        dispararAction = new InputAction(type: InputActionType.Button, binding: "<Mouse>/leftButton");
        dispararAction.Enable();
    }

    private void Start()
    {
        // Inicializa o AudioSource
        audioSource = GetComponent<AudioSource>();
        if (audioSource == null)
        {
            audioSource = gameObject.AddComponent<AudioSource>();
        }

        // Inicializa a quantidade de muni��o e atualiza o texto
        municaoAtual = municaoMaxima;
        AtualizarTextoMunicao();
    }

    private void Update()
    {
        if (dispararAction.WasPressedThisFrame() && !recarregando)
        {
            Disparar();
        }
    }

    public void Disparar()
    {
        if (municaoAtual > 0)
        {
            // Dispara a bala
            GameObject bullet = Instantiate(bulletPrefab, bulletSpawnPoint.position, bulletSpawnPoint.rotation);
            Rigidbody bulletRigidbody = bullet.GetComponent<Rigidbody>();
            if (bulletRigidbody != null)
            {
                bulletRigidbody.velocity = bulletSpawnPoint.forward * bulletSpeed;
            }

            // Reproduz o som de disparo
            if (disparoSFX != null)
            {
                audioSource.PlayOneShot(disparoSFX);
            }

            // Reduz a muni��o e atualiza o texto
            municaoAtual--;
            AtualizarTextoMunicao();

            // Inicia o recarregamento se a muni��o chegar a 0
            if (municaoAtual <= 0)
            {
                Invoke(nameof(IniciarRecarga), tempoRecarga);
            }
        }
    }

    private void IniciarRecarga()
    {
        recarregando = true;
        StartCoroutine(Recarregar());
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
        // Desabilita o Input Action
        dispararAction.Disable();
    }
}
