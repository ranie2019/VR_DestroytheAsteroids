using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
public class AutoDestroy : MonoBehaviour
{
    [Header("Tempo de Vida do Objeto (em segundos)")]
    public float lifeTime = 5.0f;

    [Header("Velocidade da Bala")]
    public float velocidade = 2000f;

    private Rigidbody rb;

    private void Awake()
    {
        rb = GetComponent<Rigidbody>();
    }

    private void Start()
    {
        // Define a velocidade da bala na direção que ela está apontando
        rb.linearVelocity = transform.forward * velocidade;

        // Destroi o objeto após o tempo definido
        Destroy(gameObject, lifeTime);
    }

    private void OnCollisionEnter(Collision collision)
    {
        Destroy(gameObject);
    }
}