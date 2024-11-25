using UnityEngine;

public class AutoDestroy : MonoBehaviour
{
    [Header("Tempo de Vida do Objeto (em segundos)")]
    [Tooltip("Tempo ap�s o qual o objeto ser� destru�do automaticamente.")]
    public float lifeTime = 5.0f; // Tempo de vida padr�o de 5 segundos

    private void Start()
    {
        // Destroi o objeto ap�s o tempo definido
        Destroy(gameObject, lifeTime);
    }

    private void OnCollisionEnter(Collision collision)
    {
        // Destroi o objeto ao colidir com qualquer coisa
        Destroy(gameObject);
    }
}
