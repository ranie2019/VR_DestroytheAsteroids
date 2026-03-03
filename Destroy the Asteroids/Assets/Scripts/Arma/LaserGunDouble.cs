using UnityEngine;

public class LaserGunDouble : MonoBehaviour
{
    [Header("Configura��es do Laser")]
    [Tooltip("Animator respons�vel pela anima��o de disparo.")]
    [SerializeField] private Animator laserAnimator;

    [Header("Som do Disparo")]
    [Tooltip("Som reproduzido ao disparar o laser.")]
    [SerializeField] private AudioClip laserSFX;

    [Header("Origens do Raycast")]
    [Tooltip("Transform de onde o primeiro raycast ser� emitido.")]
    [SerializeField] private Transform raycastOrigin1;

    [Tooltip("Transform de onde o segundo raycast ser� emitido.")]
    [SerializeField] private Transform raycastOrigin2;

    [Header("Configura��es da Bala")]
    [Tooltip("Prefab da bala que ser� instanciada ao disparar.")]
    [SerializeField] private GameObject bulletPrefab;

    [Tooltip("Velocidade da bala disparada.")]
    [SerializeField] private float bulletSpeed = 20f;

    [Header("Pontos de Spawn das Balas")]
    [Tooltip("Primeiro ponto de spawn da bala.")]
    [SerializeField] private Transform bulletSpawnPoint1;

    [Tooltip("Segundo ponto de spawn da bala.")]
    [SerializeField] private Transform bulletSpawnPoint2;

    private AudioSource laserAudioSource;

    private void Awake()
    {
        // Tenta obter o AudioSource automaticamente ou adiciona um novo
        laserAudioSource = GetComponent<AudioSource>();
        if (laserAudioSource == null)
        {
            laserAudioSource = gameObject.AddComponent<AudioSource>();
        }

        // Atribui o AudioClip ao AudioSource e configura as propriedades do som
        if (laserSFX != null)
        {
            laserAudioSource.clip = laserSFX;
            laserAudioSource.playOnAwake = false;
        }
    }

    public void LaserGunFire()
    {
        // Ativa a anima��o de disparo, se atribu�da
        if (laserAnimator != null)
        {
            laserAnimator.SetTrigger("Fire");
        }

        // Reproduz o som de disparo
        if (laserAudioSource != null && laserSFX != null)
        {
            laserAudioSource.Play();
        }

        // Instancia e dispara as balas a partir dos dois pontos de spawn
        FireBullets();
    }

    private void FireBullets()
    {
        // Dispara a partir do primeiro ponto de spawn
        if (bulletPrefab != null && bulletSpawnPoint1 != null)
        {
            SpawnBullet(bulletSpawnPoint1);
        }

        // Dispara a partir do segundo ponto de spawn
        if (bulletPrefab != null && bulletSpawnPoint2 != null)
        {
            SpawnBullet(bulletSpawnPoint2);
        }
    }

    private void SpawnBullet(Transform spawnPoint)
    {
        // Instancia a bala no ponto de spawn
        GameObject bullet = Instantiate(bulletPrefab, spawnPoint.position, spawnPoint.rotation);

        // Aplica uma for�a na bala para mov�-la para frente
        Rigidbody bulletRigidbody = bullet.GetComponent<Rigidbody>();
        if (bulletRigidbody != null)
        {
            bulletRigidbody.linearVelocity = spawnPoint.forward * bulletSpeed;
        }
    }
}
