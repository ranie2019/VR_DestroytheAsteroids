using UnityEngine;

public class AutoDestroy : MonoBehaviour
{
    [Header("Tempo de Vida do Objeto (em segundos)")]
    [Tooltip("Tempo após o qual o objeto será destruído automaticamente.")]
    public float lifeTime = 5.0f; // Tempo de vida padrão de 5 segundos

    private void Start()
    {
        // Destroi o objeto após o tempo definido
        Destroy(gameObject, lifeTime);
    }

    private void OnCollisionEnter(Collision collision)
    {
        // Destroi o objeto ao colidir com qualquer coisa
        Destroy(gameObject);
    }
}
