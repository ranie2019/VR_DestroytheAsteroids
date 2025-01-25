using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class TorrentControl : MonoBehaviour
{
    [Header("Configurações da Torreta")]
    public float distance = 50f; // Alcance máximo da torreta
    public Transform mount; // Base da torreta que rotaciona no eixo Y
    public Transform head; // Cabeça da torreta que rotaciona no eixo X
    public Transform[] attachPoints; // Pontos de disparo dos projéteis
    public GameObject projectile; // Prefab do projétil
    public float taxaDisparo = 1f; // Taxa de disparo (projéteis por segundo)
    public float projectileSpeed = 800f; // Velocidade do projétil

    [Header("Efeitos Sonoros")]
    public AudioClip laserSFX; // Som do disparo

    private AudioSource laserAudioSource; // Componente de áudio
    private TurrentAnimator turrentAnimator; // Controlador de animações
    private Transform closestAsteroid; // Referência ao asteroide mais próximo
    private int currentAttachIndex = 0; // Índice do ponto de disparo atual
    private float nextFireTime = 0f; // Tempo para o próximo disparo

    private void Awake()
    {
        // Configura o componente de áudio
        laserAudioSource = GetComponent<AudioSource>() ?? gameObject.AddComponent<AudioSource>();
        if (laserSFX != null)
        {
            laserAudioSource.clip = laserSFX;
            laserAudioSource.playOnAwake = false;
        }

        // Obtém o controlador de animações
        turrentAnimator = GetComponentInChildren<TurrentAnimator>();
    }

    private void Update()
    {
        // Encontra o asteroide mais próximo
        FindClosestAsteroid();

        // Se houver um asteroide dentro do alcance, mira e dispara
        if (closestAsteroid != null)
        {
            RotateTowardsAsteroid();

            // Verifica se a torreta pode disparar
            if (CanFire())
            {
                ShootProjectile();
            }
        }
    }

    /// <summary>
    /// Encontra o asteroide mais próximo dentro do alcance da torreta.
    /// </summary>
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

    /// <summary>
    /// Gira a torreta na direção do asteroide mais próximo.
    /// </summary>
    private void RotateTowardsAsteroid()
    {
        if (closestAsteroid != null)
        {
            Vector3 directionToAsteroid = closestAsteroid.position - mount.position;

            // Rotação da base (eixo Y)
            Quaternion targetRotationY = Quaternion.LookRotation(new Vector3(directionToAsteroid.x, 0, directionToAsteroid.z));
            mount.rotation = Quaternion.RotateTowards(mount.rotation, targetRotationY, 5f * Time.deltaTime);

            // Rotação da cabeça (eixo X)
            Quaternion targetRotationX = Quaternion.LookRotation(directionToAsteroid);
            head.localRotation = Quaternion.RotateTowards(head.localRotation, Quaternion.Euler(targetRotationX.eulerAngles.x, 0, 0), 5f * Time.deltaTime);
        }
    }

    /// <summary>
    /// Verifica se a torreta está pronta para disparar.
    /// </summary>
    private bool CanFire()
    {
        if (Time.time >= nextFireTime)
        {
            nextFireTime = Time.time + 1f / taxaDisparo;
            return true;
        }
        return false;
    }

    /// <summary>
    /// Dispara um projétil no ponto de disparo atual.
    /// </summary>
    private void ShootProjectile()
    {
        Transform attach = attachPoints[currentAttachIndex];
        GameObject clone = Instantiate(projectile, attach.position, head.rotation);
        Rigidbody rb = clone.GetComponent<Rigidbody>();

        if (rb != null)
        {
            rb.AddForce(head.forward * projectileSpeed);
        }

        // Reproduz o som do disparo
        PlaySound(laserSFX);

        // Ativa a animação de disparo
        StartCoroutine(FireAnimationRoutine());

        // Atualiza o índice para o próximo ponto de anexo
        currentAttachIndex = (currentAttachIndex + 1) % attachPoints.Length;
    }

    /// <summary>
    /// Reproduz um som no componente de áudio.
    /// </summary>
    private void PlaySound(AudioClip clip)
    {
        if (laserAudioSource != null && clip != null)
        {
            laserAudioSource.PlayOneShot(clip);
        }
    }

    /// <summary>
    /// Corrotina para gerenciar a animação de disparo.
    /// </summary>
    private IEnumerator FireAnimationRoutine()
    {
        if (turrentAnimator != null)
        {
            turrentAnimator.ActivateFireAnimation();
            yield return new WaitForSeconds(0.5f); // Tempo ajustável
            turrentAnimator.DeactivateFireAnimation();
        }
    }

    /// <summary>
    /// Desenha o alcance da torreta e a linha de disparo no Editor.
    /// </summary>
    private void OnDrawGizmos()
    {
        Gizmos.color = Color.green;
        Gizmos.DrawWireSphere(transform.position, distance); // Desenha o alcance da torreta

        if (closestAsteroid != null)
        {
            Gizmos.color = Color.red;
            Gizmos.DrawLine(attachPoints[currentAttachIndex].position, closestAsteroid.position); // Linha de disparo
        }
    }
}
