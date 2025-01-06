using UnityEngine;

public class LaserGun : MonoBehaviour
{
    [Header("Configurações do Laser")]
    [Tooltip("Som reproduzido ao disparar o laser.")]
    [SerializeField] private AudioClip laserSFX;

    [Header("Origem do Raycast")]
    [Tooltip("Transform de onde o raycast será emitido.")]
    [SerializeField] private Transform raycastOrigin;

    [Header("Configurações da Bala")]
    [Tooltip("Prefab da bala que será instanciada ao disparar.")]
    [SerializeField] private GameObject bulletPrefab;

    [Tooltip("Velocidade da bala disparada.")]
    [SerializeField] private float bulletSpeed = 20f;

    [Header("Ponto de Spawn da Bala")]
    [Tooltip("Transform que define o ponto de spawn da bala.")]
    [SerializeField] private Transform bulletSpawnPoint;

    private AudioSource laserAudioSource;

    private void Awake()
    {
        // Configura o AudioSource
        laserAudioSource = GetComponent<AudioSource>();
        if (laserAudioSource == null)
        {
            laserAudioSource = gameObject.AddComponent<AudioSource>();
        }

        if (laserSFX != null)
        {
            laserAudioSource.clip = laserSFX;
            laserAudioSource.playOnAwake = false;
        }
    }

    /// <summary>
    /// Dispara o laser.
    /// Chamado pelo XR Grab Interactable ? Activate.
    /// </summary>
    public void LaserGunFire()
    {
        // Reproduz o som de disparo
        if (laserAudioSource != null && laserSFX != null)
        {
            laserAudioSource.Play();
        }

        // Instancia e dispara a bala
        FireBullet();
    }

    private void FireBullet()
    {
        if (bulletPrefab != null && bulletSpawnPoint != null)
        {
            GameObject bullet = Instantiate(bulletPrefab, bulletSpawnPoint.position, bulletSpawnPoint.rotation);

            Rigidbody bulletRigidbody = bullet.GetComponent<Rigidbody>();
            if (bulletRigidbody != null)
            {
                bulletRigidbody.velocity = bulletSpawnPoint.forward * bulletSpeed;
            }
        }
    }
}
