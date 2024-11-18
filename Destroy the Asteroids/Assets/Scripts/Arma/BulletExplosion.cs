using UnityEngine;
using System.Collections;

public class BulletExplosion : MonoBehaviour
{
    [Header("Configurações da Explosão")]
    [Tooltip("Prefab dos fragmentos que serão instanciados após a colisão.")]
    [SerializeField] private GameObject fragmentPrefab;

    [Tooltip("Quantidade de fragmentos a serem instanciados.")]
    [SerializeField] private int fragmentCount = 5;

    [Tooltip("Tempo antes de destruir os fragmentos (em segundos).")]
    [SerializeField] private float fragmentLifetime = 3f;

    [Tooltip("Força com que os fragmentos serão arremessados em direções diferentes.")]
    [SerializeField] private float explosionForce = 5f;

    private void OnCollisionEnter(Collision collision)
    {
        // Chama o método de explosão ao detectar uma colisão
        Explode();
    }

    private void Explode()
    {
        // Destroi o objeto original (a bala ou bomba) após a explosão
        Destroy(gameObject);

        // Libera fragmentos após a destruição
        ReleaseFragments();
    }

    private void ReleaseFragments()
    {
        // Instancia e libera fragmentos
        if (fragmentPrefab != null)
        {
            for (int i = 0; i < fragmentCount; i++)
            {
                // Instancia o fragmento na posição da explosão
                GameObject fragment = Instantiate(fragmentPrefab, transform.position, Quaternion.identity);

                // Adiciona um Rigidbody para aplicar a força
                Rigidbody fragmentRb = fragment.GetComponent<Rigidbody>();
                if (fragmentRb == null)
                {
                    fragmentRb = fragment.AddComponent<Rigidbody>(); // Adiciona Rigidbody caso não exista
                }

                // Aplica uma força aleatória em direções diferentes
                Vector3 randomDirection = Random.insideUnitSphere.normalized;
                fragmentRb.AddForce(randomDirection * explosionForce, ForceMode.Impulse);

                // Destrói o fragmento após o tempo de vida definido
                Destroy(fragment, fragmentLifetime);
            }
        }
    }
}
