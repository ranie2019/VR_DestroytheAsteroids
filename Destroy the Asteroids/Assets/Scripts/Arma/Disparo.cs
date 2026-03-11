using UnityEngine;
using System.Collections;

public class Disparo : MonoBehaviour
{
    [Header("Configurações do Audio")]
    [SerializeField] private AudioClip audio;

    [Header("Configurações da Bala")]
    [SerializeField] private GameObject bulletPrefab;

    [Header("Ponto de Spawn da Bala")]
    [SerializeField] private Transform bulletSpawnPoint;

    [Header("Configurações de Disparo")]
    [SerializeField] private float fireRate = 0.2f;

    [Header("Ajustes de Segurança")]
    [SerializeField] private float distanciaExtraDoSpawn = 0.05f;
    [SerializeField] private float tempoIgnorarColisaoInicial = 0.05f;
    [SerializeField] private Collider[] collidersParaIgnorar;

    private AudioSource audioSource;
    private float nextFireTime = 0f;

    private void Awake()
    {
        audioSource = GetComponent<AudioSource>();

        if (audioSource == null)
            audioSource = gameObject.AddComponent<AudioSource>();

        audioSource.playOnAwake = false;
    }

    public void LaserGunFire()
    {
        if (Time.time < nextFireTime)
            return;

        if (bulletPrefab == null || bulletSpawnPoint == null)
            return;

        nextFireTime = Time.time + fireRate;

        PlayLaserSound();
        FireBulletSeguro();
    }

    private void PlayLaserSound()
    {
        if (audio != null)
            audioSource.PlayOneShot(audio);
    }

    private void FireBulletSeguro()
    {
        Physics.SyncTransforms();

        Vector3 spawnPosition = bulletSpawnPoint.position + bulletSpawnPoint.forward * distanciaExtraDoSpawn;
        Quaternion spawnRotation = bulletSpawnPoint.rotation;

        GameObject bullet = Instantiate(bulletPrefab, spawnPosition, spawnRotation);

        Collider bulletCol = bullet.GetComponent<Collider>();

        if (bulletCol != null && collidersParaIgnorar != null)
        {
            for (int i = 0; i < collidersParaIgnorar.Length; i++)
            {
                Collider col = collidersParaIgnorar[i];
                if (col != null)
                {
                    Physics.IgnoreCollision(bulletCol, col, true);
                }
            }

            if (tempoIgnorarColisaoInicial > 0f)
                StartCoroutine(ReativarColisoesDepois(bulletCol));
        }
    }

    private IEnumerator ReativarColisoesDepois(Collider bulletCol)
    {
        yield return new WaitForSeconds(tempoIgnorarColisaoInicial);

        if (bulletCol == null)
            yield break;

        if (collidersParaIgnorar == null)
            yield break;

        for (int i = 0; i < collidersParaIgnorar.Length; i++)
        {
            Collider col = collidersParaIgnorar[i];
            if (col != null)
            {
                Physics.IgnoreCollision(bulletCol, col, false);
            }
        }
    }
}