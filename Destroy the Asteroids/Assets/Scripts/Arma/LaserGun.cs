using UnityEngine;

public class LaserGun : MonoBehaviour
{
    [Header("Configurações do Laser")]
    [Tooltip("Som reproduzido ao disparar o laser.")]
    [SerializeField] private AudioClip laserSFX; // Som que será reproduzido ao disparar a arma.

    [Header("Configurações da Bala")]
    [Tooltip("Prefab da bala que será instanciada ao disparar.")]
    [SerializeField] private GameObject bulletPrefab; // Prefab da bala que será instanciada.

    [Tooltip("Velocidade da bala disparada.")]
    [SerializeField] private float bulletSpeed = 20f; // Velocidade com que a bala será disparada.

    [Header("Ponto de Spawn da Bala")]
    [Tooltip("Transform que define o ponto de spawn da bala.")]
    [SerializeField] private Transform bulletSpawnPoint; // Transform que define o local onde a bala será criada.

    private AudioSource laserAudioSource; // Componente de áudio usado para reproduzir o som do disparo.

    private void Awake()
    {
        // Configura o AudioSource para reproduzir o som do disparo.
        laserAudioSource = GetComponent<AudioSource>();
        if (laserAudioSource == null)
        {
            // Caso não haja um AudioSource no GameObject, um novo será adicionado automaticamente.
            laserAudioSource = gameObject.AddComponent<AudioSource>();
        }

        if (laserSFX != null)
        {
            // Configura o AudioSource com o clipe de som atribuído no Inspector.
            laserAudioSource.clip = laserSFX;
            laserAudioSource.playOnAwake = false; // O som não será reproduzido automaticamente ao iniciar a cena.
        }

        // Validação para garantir que os componentes obrigatórios foram atribuídos.
        if (bulletPrefab == null)
        {
            Debug.LogError("O prefab da bala não foi atribuído! Certifique-se de configurar o campo no Inspector.");
        }

        if (bulletSpawnPoint == null)
        {
            Debug.LogError("O ponto de spawn da bala não foi atribuído! Certifique-se de configurar o campo no Inspector.");
        }
    }

    /// <summary>
    /// Dispara o laser.
    /// Este método deve ser chamado quando o jogador realizar a ação de disparo.
    /// </summary>
    public void LaserGunFire()
    {
        // Reproduz o som do disparo, caso o áudio esteja configurado corretamente.
        if (laserAudioSource != null && laserSFX != null)
        {
            laserAudioSource.Play();
        }

        // Instancia e dispara a bala.
        FireBullet();
    }

    /// <summary>
    /// Instancia a bala e aplica a velocidade para que ela se mova na direção desejada.
    /// </summary>
    private void FireBullet()
    {
        if (bulletPrefab != null && bulletSpawnPoint != null)
        {
            // Instancia a bala no ponto de spawn definido, com a mesma rotação do ponto.
            GameObject bullet = Instantiate(bulletPrefab, bulletSpawnPoint.position, bulletSpawnPoint.rotation);

            // Obtém o componente Rigidbody da bala para aplicar movimento físico.
            Rigidbody bulletRigidbody = bullet.GetComponent<Rigidbody>();
            if (bulletRigidbody != null)
            {
                // Define a velocidade da bala na direção para onde o spawn point está apontando.
                bulletRigidbody.velocity = bulletSpawnPoint.forward * bulletSpeed;
            }
            else
            {
                // Caso a bala não tenha um Rigidbody, exibe um aviso no console.
                Debug.LogWarning("O prefab da bala não possui um componente Rigidbody. A bala não se moverá!");
            }
        }
        else
        {
            // Exibe um erro caso o prefab ou o ponto de spawn não tenham sido configurados corretamente.
            Debug.LogError("BulletPrefab ou BulletSpawnPoint não foram atribuídos. Verifique as configurações no Inspector.");
        }
    }
}