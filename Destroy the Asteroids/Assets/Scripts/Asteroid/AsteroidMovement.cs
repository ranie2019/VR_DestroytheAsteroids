using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AsteroidMovement : MonoBehaviour
{
    [Header("Control the speed of the Asteroid")]
    public float initialSpeed = 1f; // Velocidade inicial
    public float maxSpeed = 10f;    // Velocidade máxima
    public float speedIncreaseRate = 0.5f; // Taxa de aumento da velocidade

    [Header("Control the rotational speed")]
    public float rotationalSpeedMin;
    public float rotationalSpeedMax;

    private float rotationalSpeed;
    private float xAngle, yAngle, zAngle;

    public Vector3 movementDirection;

    private float asteroidSpeed;

    // Start is called before the first frame update
    void Start()
    {
        // Define a velocidade inicial
        asteroidSpeed = initialSpeed;

        // Gera uma rotação aleatória
        xAngle = Random.Range(0, 360);
        yAngle = Random.Range(0, 360);
        zAngle = Random.Range(0, 360);

        transform.Rotate(xAngle, yAngle, zAngle);

        rotationalSpeed = Random.Range(rotationalSpeedMin, rotationalSpeedMax);
    }

    // Update is called once per frame
    void Update()
    {
        // Aumenta gradualmente a velocidade até atingir o máximo
        if (asteroidSpeed < maxSpeed)
        {
            asteroidSpeed += speedIncreaseRate * Time.deltaTime;
        }

        transform.Translate(movementDirection * Time.deltaTime * asteroidSpeed, Space.World);
        transform.Rotate(Vector3.up * Time.deltaTime * rotationalSpeed);
    }
}
