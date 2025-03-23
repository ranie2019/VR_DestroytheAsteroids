using UnityEngine;

public class AutoDestroy : MonoBehaviour
{
    [Header("Tempo de Vida do Objeto (em segundos)")]
    public float lifeTime = 5.0f; // Tempo de vida padr�o de 5 segundos

    private void Start()
    {
        // Destroi o objeto ap�s o tempo definido
        Destroy(gameObject, lifeTime);
    }

    private void OnCollisionEnter(Collision collision)
    {
        // Destr�i o objeto ao colidir com qualquer coisa
        Destroy(gameObject);
    }
}
