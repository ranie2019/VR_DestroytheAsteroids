using UnityEngine;
using System.Collections;

public class BulletExplosion : MonoBehaviour
{
    [Header("Configura��es da Explos�o")]
    [Tooltip("Prefab dos fragmentos que ser�o instanciados ap�s a colis�o.")]
    [SerializeField] private GameObject fragmentPrefab;

    [Tooltip("Quantidade de fragmentos a serem instanciados.")]
    [SerializeField] private int fragmentCount = 5;

    [Tooltip("Tempo antes de destruir os fragmentos (em segundos).")]
    [SerializeField] private float fragmentLifetime = 3f;

    [Tooltip("For�a com que os fragmentos ser�o arremessados em dire��es diferentes.")]
    [SerializeField] private float explosionForce = 5f;

    private void OnCollisionEnter(Collision collision)
    {
        // Chama o m�todo de explos�o ao detectar uma colis�o
        Explode();
    }

    private void Explode()
    {
        // Destroi o objeto original (a bala ou bomba) ap�s a explos�o
        Destroy(gameObject);

        // Libera fragmentos ap�s a destrui��o
        ReleaseFragments();
    }

    private void ReleaseFragments()
    {
        // Instancia e libera fragmentos
        if (fragmentPrefab != null)
        {
            for (int i = 0; i < fragmentCount; i++)
            {
                // Instancia o fragmento na posi��o da explos�o
                GameObject fragment = Instantiate(fragmentPrefab, transform.position, Quaternion.identity);

                // Adiciona um Rigidbody para aplicar a for�a
                Rigidbody fragmentRb = fragment.GetComponent<Rigidbody>();
                if (fragmentRb == null)
                {
                    fragmentRb = fragment.AddComponent<Rigidbody>(); // Adiciona Rigidbody caso n�o exista
                }

                // Aplica uma for�a aleat�ria em dire��es diferentes
                Vector3 randomDirection = Random.insideUnitSphere.normalized;
                fragmentRb.AddForce(randomDirection * explosionForce, ForceMode.Impulse);

                // Destr�i o fragmento ap�s o tempo de vida definido
                Destroy(fragment, fragmentLifetime);
            }
        }
    }
}
