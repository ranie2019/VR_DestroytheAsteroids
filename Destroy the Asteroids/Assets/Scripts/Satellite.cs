using UnityEngine;

/// <summary>
/// Rotaciona o sat�lite ao longo do eixo Y e faz uma �rbita ao redor da posi��o (0, 0, 0).
/// </summary>
public class Satellite : MonoBehaviour
{
    [Tooltip("Velocidade de rota��o ao longo do eixo Y")]
    public float rotationSpeed = 10f;

    [Tooltip("Velocidade de �rbita ao redor da posi��o (0, 0, 0)")]
    public float orbitSpeed = 5f;

    [Tooltip("Raio da �rbita ao redor da posi��o (0, 0, 0)")]
    public float orbitRadius = 10f;

    private Vector3 initialPosition;

    // Start is called before the first frame update
    void Start()
    {
        // Salva a posi��o inicial do sat�lite
        initialPosition = transform.position;
    }

    // Update is called once per frame
    void Update()
    {
        // Calcula a nova posi��o orbital
        float angle = Time.time * orbitSpeed;
        float x = Mathf.Cos(angle) * orbitRadius;
        float z = Mathf.Sin(angle) * orbitRadius;

        // Atualiza a posi��o do sat�lite em �rbita
        transform.position = new Vector3(x, transform.position.y, z);

        // Rotaciona o sat�lite ao longo do eixo Y
        transform.Rotate(Vector3.up, rotationSpeed * Time.deltaTime);
    }
}
