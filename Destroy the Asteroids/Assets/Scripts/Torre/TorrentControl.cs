using UnityEngine;

public class TorrentControl : MonoBehaviour
{
    public float distance = 50f;
    public Transform mount;
    public Transform head;
    public Transform[] attachPoints;
    public GameObject projectile;
    public float fireRate = 1f;
    public float projectileSpeed = 800f;
    public AudioClip laserSFX;

    private AudioSource laserAudioSource;
    private TurrentAnimator turrentAnimator;
    private Transform closestAsteroid;
    private int currentAttachIndex = 0;
    private float nextFireTime = 0f;

    private void Awake()
    {
        laserAudioSource = GetComponent<AudioSource>() ?? gameObject.AddComponent<AudioSource>();
        if (laserSFX != null)
        {
            laserAudioSource.clip = laserSFX;
            laserAudioSource.playOnAwake = false;
        }
        turrentAnimator = GetComponentInChildren<TurrentAnimator>();
    }

    private void Update()
    {
        FindClosestAsteroid();
        if (closestAsteroid != null)
        {
            RotateTowardsAsteroid();
            if (Time.time >= nextFireTime)
            {
                nextFireTime = Time.time + 1f / fireRate;
                ShootProjectile();
            }
        }
    }

    private void FindClosestAsteroid()
    {
        GameObject[] asteroids = GameObject.FindGameObjectsWithTag("Asteroid");
        closestAsteroid = null;
        float closestDistance = Mathf.Infinity;

        foreach (GameObject asteroid in asteroids)
        {
            float dist = Vector3.Distance(asteroid.transform.position, transform.position);
            if (dist < closestDistance && dist < distance)
            {
                closestDistance = dist;
                closestAsteroid = asteroid.transform;
            }
        }
    }

    private void RotateTowardsAsteroid()
    {
        if (closestAsteroid != null)
        {
            Vector3 directionToAsteroid = closestAsteroid.position - mount.position;
            Quaternion targetRotationY = Quaternion.LookRotation(new Vector3(directionToAsteroid.x, 0, directionToAsteroid.z));
            mount.rotation = Quaternion.Slerp(mount.rotation, targetRotationY, Time.deltaTime * 5f);

            Quaternion targetRotationX = Quaternion.LookRotation(directionToAsteroid);
            head.localRotation = Quaternion.Slerp(head.localRotation, Quaternion.Euler(targetRotationX.eulerAngles.x, 0, 0), Time.deltaTime * 5f);
        }
    }

    private void ShootProjectile()
    {
        Transform attach = attachPoints[currentAttachIndex];
        GameObject clone = Instantiate(projectile, attach.position, head.rotation);
        Rigidbody rb = clone.GetComponent<Rigidbody>();

        if (rb != null)
        {
            rb.AddForce(head.forward * projectileSpeed);
        }

        if (laserAudioSource != null && laserSFX != null)
        {
            laserAudioSource.Play();
        }

        // Ativa a anima��o de disparo
        if (turrentAnimator != null)
        {
            turrentAnimator.ActivateFireAnimation();
        }

        // Atualiza o �ndice para o pr�ximo ponto de anexo
        currentAttachIndex = (currentAttachIndex + 1) % attachPoints.Length;

        // Desativa a anima��o de disparo ap�s um curto per�odo (opcional)
        Invoke("DeactivateFireAnimation", 0.5f); // Tempo pode ser ajustado conforme necess�rio
    }

    private void DeactivateFireAnimation()
    {
        if (turrentAnimator != null)
        {
            turrentAnimator.DeactivateFireAnimation();
        }
    }

    private void OnDrawGizmos()
    {
        if (closestAsteroid != null)
        {
            Gizmos.color = Color.red;
            Gizmos.DrawLine(attachPoints[currentAttachIndex].position, closestAsteroid.position);
        }
    }
}
