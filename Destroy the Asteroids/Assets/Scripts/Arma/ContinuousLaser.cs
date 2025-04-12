using UnityEngine;
using UnityEngine.UI;

public class ContinuousLaser : MonoBehaviour
{
    [Header("Configurações do Laser")]
    [SerializeField] private Transform firePoint;
    [SerializeField] private float maxLaserDistance = 100f;
    [SerializeField] private LineRenderer lineRenderer;
    [SerializeField] private PontoController gameController;
    [SerializeField] private Image energyBar;

    [Header("Configurações de Energia")]
    [SerializeField] private float maxEnergy = 100f;
    [SerializeField] private float depletionRate = 10f;
    [SerializeField] private float rechargeDelay = 5f;
    [SerializeField] private float rechargeDuration = 10f;

    [Header("Configurações de Áudio")]
    [SerializeField] private AudioClip laserSound;

    private AudioSource audioSource;
    private bool isLaserActive;
    private bool isRecharging;
    private float currentEnergy;
    private float rechargeTimer;

    private readonly int laserStart = 0;
    private readonly int laserEnd = 1;

    private void Awake()
    {
        currentEnergy = maxEnergy;
        lineRenderer.enabled = false;

        audioSource = GetComponent<AudioSource>() ?? gameObject.AddComponent<AudioSource>();
        audioSource.loop = true;

        if (gameController == null)
            gameController = FindObjectOfType<PontoController>();

        UpdateEnergyBar();
    }

    private void Update()
    {
        if (isLaserActive)
        {
            if (!isRecharging)
            {
                ShootLaser();
                DrainEnergy();
            }
        }
        else
        {
            if (currentEnergy < maxEnergy)
            {
                rechargeTimer += Time.deltaTime;

                if (rechargeTimer >= rechargeDelay)
                {
                    isRecharging = true;
                    rechargeTimer = 0f;
                }
            }
        }

        if (isRecharging)
            RechargeEnergy();
    }

    public void ActivateLaser()
    {
        if (currentEnergy <= 0f) return;

        isLaserActive = true;
        isRecharging = false;
        lineRenderer.enabled = true;

        if (laserSound != null && !audioSource.isPlaying)
        {
            audioSource.clip = laserSound;
            audioSource.Play();
        }
    }

    public void DeactivateLaser()
    {
        if (!isLaserActive) return;

        isLaserActive = false;
        lineRenderer.enabled = false;

        if (audioSource.isPlaying)
            audioSource.Stop();
    }

    private void ShootLaser()
    {
        Vector3 origem = firePoint.position;
        Vector3 direcao = firePoint.forward;

        lineRenderer.SetPosition(laserStart, origem);

        if (Physics.Raycast(origem, direcao, out RaycastHit hit, maxLaserDistance))
        {
            lineRenderer.SetPosition(laserEnd, hit.point);

            if (hit.collider.CompareTag("Asteroid"))
                hit.collider.GetComponent<AsteroidHit>()?.HandleAsteroidDestruction();
        }
        else
        {
            lineRenderer.SetPosition(laserEnd, origem + direcao * maxLaserDistance);
        }
    }

    private void DrainEnergy()
    {
        currentEnergy -= depletionRate * Time.deltaTime;
        if (currentEnergy <= 0f)
        {
            currentEnergy = 0f;
            isRecharging = true;
            rechargeTimer = 0f;
            DeactivateLaser();
        }
        UpdateEnergyBar();
    }

    private void RechargeEnergy()
    {
        currentEnergy += (maxEnergy / rechargeDuration) * Time.deltaTime;

        if (currentEnergy >= maxEnergy)
        {
            currentEnergy = maxEnergy;
            isRecharging = false;
            rechargeTimer = 0f;
        }

        UpdateEnergyBar();
    }

    private void UpdateEnergyBar()
    {
        if (energyBar != null)
            energyBar.fillAmount = currentEnergy / maxEnergy;
    }
}
