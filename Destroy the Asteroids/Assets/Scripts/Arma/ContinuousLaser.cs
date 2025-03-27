using UnityEngine;
using UnityEngine.UI;

public class ContinuousLaser : MonoBehaviour
{
    [Header("Configurações do Laser")]
    [SerializeField] private Transform firePoint;
    [SerializeField] private float maxLaserDistance = 100f;
    [SerializeField] private LineRenderer lineRenderer;
    [SerializeField] private GameController gameController;
    [SerializeField] private Image energyBar;

    [Header("Configurações de Energia")]
    [SerializeField] private float maxEnergy = 100f;
    [SerializeField] private float depletionRate = 10f;
    [SerializeField] private float rechargeDelay = 5f;
    [SerializeField] private float rechargeDuration = 10f;

    [Header("Configurações de Áudio")]
    [SerializeField] private AudioClip laserSound;
    private AudioSource audioSource;

    private bool isLaserActive = false;
    private float currentEnergy;
    private bool isRecharging = false;
    private float rechargeTimer = 0f;

    private void Start()
    {
        currentEnergy = maxEnergy;
        lineRenderer.enabled = false;

        audioSource = GetComponent<AudioSource>() ?? gameObject.AddComponent<AudioSource>();
        audioSource.loop = true;

        if (gameController == null)
        {
            gameController = FindObjectOfType<GameController>();
        }
    }

    private void Update()
    {
        if (isLaserActive && !isRecharging)
        {
            ShootLaser();
            DrainEnergy();
        }

        if (!isLaserActive && currentEnergy < maxEnergy)
        {
            rechargeTimer += Time.deltaTime;

            if (rechargeTimer >= rechargeDelay && currentEnergy < maxEnergy)
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
        if (currentEnergy > 0)
        {
            isLaserActive = true;
            lineRenderer.enabled = true;

            if (laserSound != null && !audioSource.isPlaying)
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
                hit.collider.GetComponent<AsteroidHit>()?.HandleAsteroidDestruction();
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
        currentEnergy = Mathf.Max(0, currentEnergy);
        energyBar.fillAmount = currentEnergy / maxEnergy;

        if (currentEnergy <= 0)
        {
            currentEnergy = 0;
            isRecharging = true;
            rechargeTimer = 0f;
            DeactivateLaser();
        }
    }

    private void RechargeEnergy()
    {
        currentEnergy += (maxEnergy / rechargeDuration) * Time.deltaTime;
        currentEnergy = Mathf.Min(currentEnergy, maxEnergy);
        energyBar.fillAmount = currentEnergy / maxEnergy;

        if (currentEnergy >= maxEnergy)
        {
            isRecharging = false;
        }
    }
}
