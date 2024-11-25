using UnityEngine;

public class MoveTowardsPlayer : MonoBehaviour
{
    [Header("Configura��es de Movimento")]
    [Tooltip("Velocidade de movimento do objeto.")]
    [SerializeField] private float velocidade = 5f;

    private Transform alvo; // Refer�ncia ao Transform do objeto Player

    private void Start()
    {
        // Encontra o objeto com a tag "Player"
        GameObject player = GameObject.FindGameObjectWithTag("Player");
        if (player != null)
        {
            alvo = player.transform;
        }
    }

    private void Update()
    {
        if (alvo != null)
        {
            // Move o objeto na dire��o do Player
            Vector3 direcao = (alvo.position - transform.position).normalized;
            transform.position += direcao * velocidade * Time.deltaTime;
        }
    }
}
