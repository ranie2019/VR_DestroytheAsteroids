using UnityEngine;

public class Disparo : MonoBehaviour
{
    [Header("Configurações do Audio")]
    [SerializeField] private AudioClip audio; // Som do disparo

    [Header("Configurações da Bala")]
    [SerializeField] private GameObject bulletPrefab; // Prefab da bala
    [SerializeField] private float bulletSpeed = 20f; // Velocidade da bala

    [Header("Ponto de Spawn da Bala")]
    [SerializeField] private Transform bulletSpawnPoint; // Ponto de spawn da bala (geralmente na ponta do cano da arma)

    private AudioSource audioSource;

    [Header("Configurações de Disparo")]
    [SerializeField] private float fireRate = 0.2f; // Intervalo entre disparos rápidos
    private float nextFireTime = 0f; // Controla o tempo de disparo

    private Vector3 latestSpawnPosition;
    private Quaternion latestSpawnRotation;
    private bool shouldFire = false;

    private void Awake()
    {
        // Verifica se o AudioSource existe, senão cria um novo
        audioSource = GetComponent<AudioSource>() ?? gameObject.AddComponent<AudioSource>();
        audioSource.playOnAwake = false;
    }

    /// <summary>
    /// Método para disparar a arma.
    /// </summary>
    public void LaserGunFire()
    {
        if (Time.time >= nextFireTime)
        {
            nextFireTime = Time.time + fireRate; // Ajusta o tempo para o próximo disparo
            PlayLaserSound();

            // Guarda a posição e rotação corretas no momento do disparo
            latestSpawnPosition = bulletSpawnPoint.position;
            latestSpawnRotation = Quaternion.LookRotation(bulletSpawnPoint.forward);
            shouldFire = true;
        }
    }

    /// <summary>
    /// Reproduz o som do disparo.
    /// </summary>
    private void PlayLaserSound()
    {
        if (audio != null)
        {
            audioSource.PlayOneShot(audio);
        }
    }

    /// <summary>
    /// Garante que o disparo ocorra no último momento do frame, evitando problemas de interpolação.
    /// </summary>
    private void LateUpdate()
    {
        if (shouldFire)
        {
            FireBullet();
            shouldFire = false;
        }
    }

    /// <summary>
    /// Instancia a bala e aplica a velocidade.
    /// </summary>
    private void FireBullet()
    {
        if (bulletPrefab != null)
        {
            // Instancia a bala na posição correta do último frame
            GameObject bullet = Instantiate(bulletPrefab, latestSpawnPosition, latestSpawnRotation);
            Rigidbody bulletRigidbody = bullet.GetComponent<Rigidbody>();

            if (bulletRigidbody != null)
            {
                // Aplica a velocidade à bala
                bulletRigidbody.velocity = latestSpawnRotation * Vector3.forward * bulletSpeed;
            }
        }
    }
}
