using UnityEngine;

public class BulletExplosion : MonoBehaviour
{
    [Header("Configurações da Explosão")]
    [Tooltip("Prefab dos fragmentos que serão instanciados após a colisão.")]
    [SerializeField] private GameObject fragmentPrefab;

    [Tooltip("Quantidade de fragmentos a serem instanciados.")]
    [SerializeField] private int fragmentCount = 10;

    [Tooltip("Força com que os fragmentos serão arremessados.")]
    [SerializeField] private float explosionForce = 5f;

    [Tooltip("Raio de distribuição dos fragmentos.")]
    [SerializeField] private float explosionRadius = 1f;

    [Header("Efeito de Explosão")]
    [Tooltip("Prefab do efeito de explosão que será ativado na colisão.")]
    [SerializeField] private GameObject explosionEffectPrefab;

    private void OnCollisionEnter(Collision collision)
    {
        // Chama o método de explosão ao detectar uma colisão
        Explode();
    }

    private void Explode()
    {
        // Ativa o efeito de explosão, se houver um prefab atribuído
        if (explosionEffectPrefab != null)
        {
            Instantiate(explosionEffectPrefab, transform.position, Quaternion.identity);
        }

        // Instancia fragmentos em direções diferentes
        if (fragmentPrefab != null)
        {
            for (int i = 0; i < fragmentCount; i++)
            {
                // Calcula uma direção aleatória para cada fragmento
                Vector3 randomDirection = Random.insideUnitSphere.normalized;

                // Instancia o fragmento na posição da explosão
                GameObject fragment = Instantiate(fragmentPrefab, transform.position, Quaternion.identity);

                // Aplica uma força para arremessar o fragmento em uma direção aleatória
                Rigidbody fragmentRb = fragment.GetComponent<Rigidbody>();
                if (fragmentRb != null)
                {
                    fragmentRb.AddForce(randomDirection * explosionForce, ForceMode.Impulse);
                }
            }
        }

        // Destroi o objeto original (a bala ou bomba) após a explosão
        Destroy(gameObject);
    }
}
