using UnityEngine;
using UnityEngine.UI;

public class ContinuousLaser : MonoBehaviour
{
    [Header("Configura��es do Laser")]
    [SerializeField] private Transform firePoint;
    [SerializeField] private float maxLaserDistance = 100f;
    [SerializeField] private LineRenderer lineRenderer;
    [SerializeField] private GameController gameController;
    [SerializeField] private Image energyBar; // Barra de energia do laser

    [Header("Configura��es de Energia")]
    [SerializeField] private float maxEnergy = 100f; // Energia m�xima do laser
    [SerializeField] private float depletionRate = 10f; // Taxa de decaimento de energia por segundo
    [SerializeField] private float rechargeDelay = 5f; // Tempo de espera para iniciar o recarregamento
    [SerializeField] private float rechargeDuration = 10f; // Tempo total de recarregamento

    [Header("Configura��o da Anima��o")]
    [SerializeField] private AnimacaoArma animacaoArma; // Refer�ncia ao script de anima��o

    [Header("Configura��es de �udio")]
    [SerializeField] private AudioClip laserSound; // Som do laser
    private AudioSource audioSource;

    private bool isLaserActive = false;
    private float currentEnergy;
    private bool isRecharging = false;
    private float rechargeTimer = 0f;

    private void Start()
    {
        currentEnergy = maxEnergy;
        lineRenderer.enabled = false;

        // Inicializa o AudioSource
        audioSource = GetComponent<AudioSource>();
        if (audioSource == null)
        {
            audioSource = gameObject.AddComponent<AudioSource>();
        }

        // Configura o som do laser para ser reproduzido de forma cont�nua
        audioSource.loop = true;

        if (gameController == null)
        {
            gameController = FindObjectOfType<GameController>();
        }

        if (animacaoArma == null)
        {
            animacaoArma = GetComponent<AnimacaoArma>();
        }
    }

    private void Update()
    {
        if (isLaserActive && !isRecharging)
        {
            ShootLaser();
            DrainEnergy();
        }

        if (!isLaserActive && currentEnergy < maxEnergy && !isRecharging)
        {
            // Come�a a contagem regressiva para recarregar
            rechargeTimer += Time.deltaTime;
            if (rechargeTimer >= rechargeDelay)
            {
                isRecharging = true;
                rechargeTimer = 0f;
            }
        }

        if (isRecharging)
        {
            RechargeEnergy();
        }
    }

    public void ActivateLaser()
    {
        if (currentEnergy > 0 && !isRecharging)
        {
            isLaserActive = true;
            lineRenderer.enabled = true;

            // Ativa a anima��o de disparo
            if (animacaoArma != null)
            {
                animacaoArma.ActivateFireAnimation();
            }

            // Tocar o som do laser ao ativar
            if (laserSound != null)
            {
                audioSource.clip = laserSound;
                audioSource.Play();
            }
        }
    }

    public void DeactivateLaser()
    {
        isLaserActive = false;
        lineRenderer.enabled = false;

        // Desativa a anima��o de disparo
        if (animacaoArma != null)
        {
            animacaoArma.DeactivateFireAnimation();
        }

        // Parar o som do laser ao desativar
        if (audioSource.isPlaying)
        {
            audioSource.Stop();
        }
    }

    private void ShootLaser()
    {
        lineRenderer.SetPosition(0, firePoint.position);

        if (Physics.Raycast(firePoint.position, firePoint.forward, out RaycastHit hit, maxLaserDistance))
        {
            lineRenderer.SetPosition(1, hit.point);

            if (hit.collider.CompareTag("Asteroid"))
            {
                AsteroidHit asteroidHit = hit.collider.GetComponent<AsteroidHit>();
                if (asteroidHit != null)
                {
                    asteroidHit.HandleAsteroidDestruction();
                }
            }
        }
        else
        {
            lineRenderer.SetPosition(1, firePoint.position + firePoint.forward * maxLaserDistance);
        }
    }

    private void DrainEnergy()
    {
        currentEnergy -= depletionRate * Time.deltaTime;
        energyBar.fillAmount = currentEnergy / maxEnergy;

        if (currentEnergy <= 0)
        {
            currentEnergy = 0;
            DeactivateLaser();
            rechargeTimer = 0f;
            Invoke(nameof(StartRecharge), rechargeDelay);
        }
    }

    private void StartRecharge()
    {
        isRecharging = true;
    }

    private void RechargeEnergy()
    {
        currentEnergy += (maxEnergy / rechargeDuration) * Time.deltaTime;
        energyBar.fillAmount = currentEnergy / maxEnergy;

        if (currentEnergy >= maxEnergy)
        {
            currentEnergy = maxEnergy;
            isRecharging = false;

            // Parar o som de recarga quando a energia estiver cheia
            if (audioSource.isPlaying)
            {
                audioSource.Stop();
            }
        }
    }
}
