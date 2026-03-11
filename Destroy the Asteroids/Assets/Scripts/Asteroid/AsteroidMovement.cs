using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
public class AsteroidMovement : MonoBehaviour
{
    [Header("Control the speed of the Asteroid")]
    [Tooltip("Initial speed of the asteroid.")]
    public float initialSpeed = 1f;

    [Tooltip("Maximum speed of the asteroid.")]
    public float maxSpeed = 10f;

    [Tooltip("Rate at which the speed increases.")]
    public float speedIncreaseRate = 0.5f;

    [Header("Control the rotational speed")]
    [Tooltip("Minimum rotational speed.")]
    public float rotationalSpeedMin = 5f;

    [Tooltip("Maximum rotational speed.")]
    public float rotationalSpeedMax = 15f;

    [Header("Movement Direction")]
    [Tooltip("Direction in which the asteroid moves.")]
    public Vector3 movementDirection = Vector3.back;

    private float rotationalSpeed;
    private float asteroidSpeed;
    private Rigidbody rb;

    private void Awake()
    {
        rb = GetComponent<Rigidbody>();
    }

    private void Start()
    {
        asteroidSpeed = Mathf.Max(initialSpeed, 0f);

        transform.rotation = Random.rotation;

        rotationalSpeed = Random.Range(rotationalSpeedMin, rotationalSpeedMax);

        if (movementDirection.sqrMagnitude <= 0.0001f)
            movementDirection = Vector3.back;

        movementDirection = movementDirection.normalized;

        // velocidade inicial
        rb.linearVelocity = movementDirection * asteroidSpeed;

        // configuração recomendada para objeto espacial
        rb.useGravity = false;
        rb.linearDamping = 0f;
        rb.angularDamping = 0f;
    }

    private void FixedUpdate()
    {
        asteroidSpeed = Mathf.Clamp(
            asteroidSpeed + speedIncreaseRate * Time.fixedDeltaTime,
            0f,
            maxSpeed
        );

        Vector3 direcaoAtual = rb.linearVelocity.sqrMagnitude > 0.0001f
            ? rb.linearVelocity.normalized
            : movementDirection;

        rb.linearVelocity = direcaoAtual * asteroidSpeed;

        transform.Rotate(Vector3.one * rotationalSpeed * Time.fixedDeltaTime);
    }
}