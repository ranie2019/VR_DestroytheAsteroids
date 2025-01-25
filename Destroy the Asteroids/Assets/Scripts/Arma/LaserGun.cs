using UnityEngine;

public class LaserGun : MonoBehaviour
{
    [Header("Configura��es do Laser")]
    [Tooltip("Som reproduzido ao disparar o laser.")]
    [SerializeField] private AudioClip laserSFX; // Som que ser� reproduzido ao disparar a arma.

    [Header("Configura��es da Bala")]
    [Tooltip("Prefab da bala que ser� instanciada ao disparar.")]
    [SerializeField] private GameObject bulletPrefab; // Prefab da bala que ser� instanciada.

    [Tooltip("Velocidade da bala disparada.")]
    [SerializeField] private float bulletSpeed = 20f; // Velocidade com que a bala ser� disparada.

    [Header("Ponto de Spawn da Bala")]
    [Tooltip("Transform que define o ponto de spawn da bala.")]
    [SerializeField] private Transform bulletSpawnPoint; // Transform que define o local onde a bala ser� criada.

    private AudioSource laserAudioSource; // Componente de �udio usado para reproduzir o som do disparo.

    private void Awake()
    {
        // Configura o AudioSource para reproduzir o som do disparo.
        laserAudioSource = GetComponent<AudioSource>();
        if (laserAudioSource == null)
        {
            // Caso n�o haja um AudioSource no GameObject, um novo ser� adicionado automaticamente.
            laserAudioSource = gameObject.AddComponent<AudioSource>();
        }

        if (laserSFX != null)
        {
            // Configura o AudioSource com o clipe de som atribu�do no Inspector.
            laserAudioSource.clip = laserSFX;
            laserAudioSource.playOnAwake = false; // O som n�o ser� reproduzido automaticamente ao iniciar a cena.
        }

        // Valida��o para garantir que os componentes obrigat�rios foram atribu�dos.
        if (bulletPrefab == null)
        {
            Debug.LogError("O prefab da bala n�o foi atribu�do! Certifique-se de configurar o campo no Inspector.");
        }

        if (bulletSpawnPoint == null)
        {
            Debug.LogError("O ponto de spawn da bala n�o foi atribu�do! Certifique-se de configurar o campo no Inspector.");
        }
    }

    /// <summary>
    /// Dispara o laser.
    /// Este m�todo deve ser chamado quando o jogador realizar a a��o de disparo.
    /// </summary>
    public void LaserGunFire()
    {
        // Reproduz o som do disparo, caso o �udio esteja configurado corretamente.
        if (laserAudioSource != null && laserSFX != null)
        {
            laserAudioSource.Play();
        }

        // Instancia e dispara a bala.
        FireBullet();
    }

    /// <summary>
    /// Instancia a bala e aplica a velocidade para que ela se mova na dire��o desejada.
    /// </summary>
    private void FireBullet()
    {
        if (bulletPrefab != null && bulletSpawnPoint != null)
        {
            // Instancia a bala no ponto de spawn definido, com a mesma rota��o do ponto.
            GameObject bullet = Instantiate(bulletPrefab, bulletSpawnPoint.position, bulletSpawnPoint.rotation);

            // Obt�m o componente Rigidbody da bala para aplicar movimento f�sico.
            Rigidbody bulletRigidbody = bullet.GetComponent<Rigidbody>();
            if (bulletRigidbody != null)
            {
                // Define a velocidade da bala na dire��o para onde o spawn point est� apontando.
                bulletRigidbody.velocity = bulletSpawnPoint.forward * bulletSpeed;
            }
            else
            {
                // Caso a bala n�o tenha um Rigidbody, exibe um aviso no console.
                Debug.LogWarning("O prefab da bala n�o possui um componente Rigidbody. A bala n�o se mover�!");
            }
        }
        else
        {
            // Exibe um erro caso o prefab ou o ponto de spawn n�o tenham sido configurados corretamente.
            Debug.LogError("BulletPrefab ou BulletSpawnPoint n�o foram atribu�dos. Verifique as configura��es no Inspector.");
        }
    }
}