using UnityEngine;

public class BulletExplosion : MonoBehaviour
{
    [Header("Configura��es da Explos�o")]
    [Tooltip("Prefab dos fragmentos que ser�o instanciados ap�s a colis�o.")]
    [SerializeField] private GameObject fragmentPrefab;

    [Tooltip("Quantidade de fragmentos a serem instanciados.")]
    [SerializeField] private int fragmentCount = 10;

    [Tooltip("For�a com que os fragmentos ser�o arremessados.")]
    [SerializeField] private float explosionForce = 5f;

    [Tooltip("Raio de distribui��o dos fragmentos.")]
    [SerializeField] private float explosionRadius = 1f;

    [Header("Efeito de Explos�o")]
    [Tooltip("Prefab do efeito de explos�o que ser� ativado na colis�o.")]
    [SerializeField] private GameObject explosionEffectPrefab;

    private void OnCollisionEnter(Collision collision)
    {
        // Chama o m�todo de explos�o ao detectar uma colis�o
        Explode();
    }

    private void Explode()
    {
        // Ativa o efeito de explos�o, se houver um prefab atribu�do
        if (explosionEffectPrefab != null)
        {
            Instantiate(explosionEffectPrefab, transform.position, Quaternion.identity);
        }

        // Instancia fragmentos em dire��es diferentes
        if (fragmentPrefab != null)
        {
            for (int i = 0; i < fragmentCount; i++)
            {
                // Calcula uma dire��o aleat�ria para cada fragmento
                Vector3 randomDirection = Random.insideUnitSphere.normalized;

                // Instancia o fragmento na posi��o da explos�o
                GameObject fragment = Instantiate(fragmentPrefab, transform.position, Quaternion.identity);

                // Aplica uma for�a para arremessar o fragmento em uma dire��o aleat�ria
                Rigidbody fragmentRb = fragment.GetComponent<Rigidbody>();
                if (fragmentRb != null)
                {
                    fragmentRb.AddForce(randomDirection * explosionForce, ForceMode.Impulse);
                }
            }
        }

        // Destroi o objeto original (a bala ou bomba) ap�s a explos�o
        Destroy(gameObject);
    }
}
