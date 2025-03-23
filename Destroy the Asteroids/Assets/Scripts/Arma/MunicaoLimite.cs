using UnityEngine;
using TMPro;
using UnityEngine.InputSystem;
using System.Collections;
using System.Collections.Generic;

public class MunicaoLimite : MonoBehaviour
{
    [Header("Configurações de Munição")]
    [SerializeField] private GameObject bulletPrefab;
    [SerializeField] private List<Transform> bulletSpawnPoints;
    [SerializeField] private float bulletSpeed = 20f;
    [SerializeField] private int municaoMaxima = 40;
    [SerializeField] private float tempoRecarga = 5f;
    [SerializeField] private TMP_Text municaoText;

    [Header("Configurações de Áudio")]
    [SerializeField] private AudioClip disparoSFX;
    [SerializeField] private int audioSourcePoolSize = 5;

    private int municaoAtual;
    private bool recarregando = false;
    private List<AudioSource> audioSourcePool;
    private int currentAudioSourceIndex = 0;
    private InputAction dispararAction;

    private void OnEnable()
    {
        dispararAction = new InputAction(type: InputActionType.Button, binding: "<Mouse>/leftButton");
        dispararAction.Enable();
    }

    private void Start()
    {
        municaoAtual = municaoMaxima;
        AtualizarTextoMunicao();
        InicializarAudioPool();
    }

    private void Update()
    {
        if (dispararAction.WasPressedThisFrame() && !recarregando)
        {
            Disparar();
        }

        if (!recarregando && municaoAtual < municaoMaxima)
        {
            IniciarRecarga();
        }
    }

    private void InicializarAudioPool()
    {
        audioSourcePool = new List<AudioSource>();
        for (int i = 0; i < audioSourcePoolSize; i++)
        {
            AudioSource newAudioSource = gameObject.AddComponent<AudioSource>();
            newAudioSource.clip = disparoSFX;
            newAudioSource.playOnAwake = false;
            audioSourcePool.Add(newAudioSource);
        }
    }

    private void Disparar()
    {
        if (municaoAtual > 0)
        {
            foreach (Transform spawnPoint in bulletSpawnPoints)
            {
                if (spawnPoint != null)
                {
                    GameObject bullet = Instantiate(bulletPrefab, spawnPoint.position, spawnPoint.rotation);
                    Rigidbody bulletRigidbody = bullet.GetComponent<Rigidbody>();
                    if (bulletRigidbody != null)
                    {
                        bulletRigidbody.velocity = spawnPoint.forward * bulletSpeed;
                    }
                }
            }
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

    private IEnumerator Recarregar()
    {
        yield return new WaitForSeconds(tempoRecarga);
        municaoAtual = municaoMaxima;
        recarregando = false;
        AtualizarTextoMunicao();
    }

    private void AtualizarTextoMunicao()
    {
        if (municaoText != null)
        {
            municaoText.text = municaoAtual.ToString();
        }
    }

    private void OnDisable()
    {
        dispararAction.Disable();
    }
}
