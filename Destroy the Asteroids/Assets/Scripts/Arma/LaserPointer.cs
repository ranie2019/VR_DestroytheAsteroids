using UnityEngine;

public class LaserPointer : MonoBehaviour
{
    public Transform laserOrigin; // O ponto de origem do laser (na arma)
    public LineRenderer lineRenderer;
    public float laserLength = 50.0f; // Comprimento do laser

    void Update()
    {
        // Definir o ponto de origem do laser
        lineRenderer.SetPosition(0, laserOrigin.position);

        // Realizar um raycast para detectar o que o laser atinge
        RaycastHit hit;
        if (Physics.Raycast(laserOrigin.position, laserOrigin.forward, out hit, laserLength))
        {
            // Se atingir algo, o laser termina nesse ponto
            lineRenderer.SetPosition(1, hit.point);
        }
        else
        {
            // Se não atingir nada, o laser termina no comprimento máximo
            lineRenderer.SetPosition(1, laserOrigin.position + laserOrigin.forward * laserLength);
        }
    }
}