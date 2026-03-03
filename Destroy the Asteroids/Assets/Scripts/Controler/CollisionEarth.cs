using UnityEngine;

public class CollisionEarth : MonoBehaviour
{
    [Header("�rea de Atra��o")]
    [Tooltip("Raio da �rea de atra��o em que a Terra atrai os asteroides.")]
    [SerializeField] private float attractionRadius = 10f;

    [Header("For�a de Atra��o")]
    [Tooltip("A for�a com que a Terra atrai os asteroides.")]
    [SerializeField] private float attractionForce = 5f;

    [Header("Multiplicador de Gravidade")]
    [Tooltip("Multiplicador para ajustar a for�a de atra��o simulando gravidade.")]
    [SerializeField] private float gravityMultiplier = 1f;

    [Header("Suaviza��o da Atra��o")]
    [Tooltip("Valor para suavizar a velocidade de atra��o dos asteroides.")]
    [SerializeField] private float attractionSmoothing = 0.5f;

    [Header("Velocidade M�xima")]
    [Tooltip("Limite m�ximo de velocidade que os asteroides podem alcan�ar.")]
    [SerializeField] private float maxSpeed = 10f;

    private string asteroidTag = "Asteroid";

    private void Update()
    {
        AttractAsteroids();
    }

    private void AttractAsteroids()
    {
        // Encontra todos os objetos com a tag "Asteroid" dentro do raio de atra��o usando f�sica
        Collider[] asteroidsInRange = Physics.OverlapSphere(transform.position, attractionRadius);

        foreach (Collider collider in asteroidsInRange)
        {
            if (collider.CompareTag(asteroidTag))
            {
                Rigidbody asteroidRigidbody = collider.GetComponent<Rigidbody>();
                if (asteroidRigidbody != null)
                {
                    // Calcula a dire��o e aplica a for�a de atra��o
                    Vector3 directionToEarth = (transform.position - collider.transform.position).normalized;
                    float distanceToEarth = Vector3.Distance(transform.position, collider.transform.position);

                    // Escalona a for�a com a dist�ncia para simular gravidade
                    float scaledForce = attractionForce / Mathf.Pow(distanceToEarth, 2) * gravityMultiplier;
                    asteroidRigidbody.linearVelocity = Vector3.Lerp(asteroidRigidbody.linearVelocity, directionToEarth * scaledForce, attractionSmoothing * Time.deltaTime);

                    // Limita a velocidade do asteroide
                    if (asteroidRigidbody.linearVelocity.magnitude > maxSpeed)
                    {
                        asteroidRigidbody.linearVelocity = asteroidRigidbody.linearVelocity.normalized * maxSpeed;
                    }
                }
            }
        }
    }

    private void OnDrawGizmosSelected()
    {
        // Desenha uma esfera na �rea de atra��o para visualiza��o no editor
        Gizmos.color = Color.blue;
        Gizmos.DrawWireSphere(transform.position, attractionRadius);
    }
}
