using UnityEngine;

public class Disparo : MonoBehaviour
{
    [Header("Configura��es do Audio")]
    [SerializeField] private AudioClip audio; // Som do disparo

    [Header("Configura��es da Bala")]
    [SerializeField] private GameObject bulletPrefab; // Prefab da bala
    [SerializeField] private float bulletSpeed = 20f; // Velocidade da bala

    [Header("Ponto de Spawn da Bala")]
    [SerializeField] private Transform bulletSpawnPoint; // Ponto de spawn da bala (geralmente na ponta do cano da arma)

    private AudioSource audioSource;

    [Header("Configura��es de Disparo")]
    [SerializeField] private float fireRate = 0.2f; // Intervalo entre disparos r�pidos
    private float nextFireTime = 0f; // Controla o tempo de disparo

    private Vector3 latestSpawnPosition;
    private Quaternion latestSpawnRotation;
    private bool shouldFire = false;

    private void Awake()
    {
        // Verifica se o AudioSource existe, sen�o cria um novo
        audioSource = GetComponent<AudioSource>() ?? gameObject.AddComponent<AudioSource>();
        audioSource.playOnAwake = false;
    }

    /// <summary>
    /// M�todo para disparar a arma.
    /// </summary>
    public void LaserGunFire()
    {
        if (Time.time >= nextFireTime)
        {
            nextFireTime = Time.time + fireRate; // Ajusta o tempo para o pr�ximo disparo
            PlayLaserSound();

            // Guarda a posi��o e rota��o corretas no momento do disparo
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
    /// Garante que o disparo ocorra no �ltimo momento do frame, evitando problemas de interpola��o.
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
            // Instancia a bala na posi��o correta do �ltimo frame
            GameObject bullet = Instantiate(bulletPrefab, latestSpawnPosition, latestSpawnRotation);
            Rigidbody bulletRigidbody = bullet.GetComponent<Rigidbody>();

            if (bulletRigidbody != null)
            {
                // Aplica a velocidade � bala
                bulletRigidbody.linearVelocity = latestSpawnRotation * Vector3.forward * bulletSpeed;
            }
        }
    }
}
