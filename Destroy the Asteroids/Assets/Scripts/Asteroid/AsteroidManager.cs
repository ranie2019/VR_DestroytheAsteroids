using UnityEngine;

public class AsteroidManager : MonoBehaviour
{
    [Header("Asteroides na Cena")]
    [SerializeField] private GameObject[] asteroids;

    public void DestroyAllAsteroids()
    {
        asteroids = GameObject.FindGameObjectsWithTag("Asteroid");
        foreach (var asteroid in asteroids)
        {
            Destroy(asteroid);
        }
    }
}
