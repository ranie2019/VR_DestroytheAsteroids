using UnityEngine;

public class MoveTowardsPlayer : MonoBehaviour
{
    [Header("Configurações de Movimento")]
    [Tooltip("Velocidade de movimento do objeto.")]
    [SerializeField] private float velocidade = 5f;

    private Transform alvo; // Referência ao Transform do objeto Player

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
            // Move o objeto na direção do Player
            Vector3 direcao = (alvo.position - transform.position).normalized;
            transform.position += direcao * velocidade * Time.deltaTime;
        }
    }
}
