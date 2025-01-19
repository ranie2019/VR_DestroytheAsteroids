using UnityEngine;

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

    private void Start()
    {
        // Inicializa a velocidade do asteroide
        asteroidSpeed = Mathf.Max(initialSpeed, 0f);

        // Define uma rota��o inicial aleat�ria
        transform.rotation = Random.rotation;

        // Define a velocidade de rota��o aleat�ria dentro do intervalo
        rotationalSpeed = Random.Range(rotationalSpeedMin, rotationalSpeedMax);
    }

    private void Update()
    {
        // Aumenta a velocidade do asteroide at� o m�ximo permitido
        asteroidSpeed = Mathf.Clamp(asteroidSpeed + speedIncreaseRate * Time.deltaTime, 0f, maxSpeed);

        // Move o asteroide na dire��o especificada
        transform.Translate(movementDirection.normalized * asteroidSpeed * Time.deltaTime, Space.World);

        // Gira o asteroide nos tr�s eixos
        transform.Rotate(new Vector3(1, 1, 1) * rotationalSpeed * Time.deltaTime);
    }
}