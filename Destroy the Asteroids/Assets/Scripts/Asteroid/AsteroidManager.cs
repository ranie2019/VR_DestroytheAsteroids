using UnityEngine;
using System.Collections.Generic;

public class AsteroidManager : MonoBehaviour
{
    [Tooltip("Lista de posições de spawn para os asteroides.")]
    [SerializeField] private List<Transform> spawnPoints;

    public void DestroyAllAsteroids()
    {
        foreach (GameObject asteroid in GameObject.FindGameObjectsWithTag("Asteroid"))
        {
            Destroy(asteroid);
        }
    }

    public List<Vector3> GetSpawnPositions()
    {
        List<Vector3> positions = new List<Vector3>();
        foreach (Transform spawnPoint in spawnPoints)
        {
            positions.Add(spawnPoint.position);
        }
        return positions;
    }
}
