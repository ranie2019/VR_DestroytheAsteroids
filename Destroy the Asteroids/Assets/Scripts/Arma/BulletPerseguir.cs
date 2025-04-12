using UnityEngine;
using System.Collections;

[RequireComponent(typeof(Rigidbody))]
public class BulletPerseguir : MonoBehaviour
{
    [Header("Configurações de Detecção")]
    [SerializeField] private string tagAlvo = "Asteroid";
    [SerializeField] private float tempoDeVida = 5f;
    [SerializeField] private float tempoDeChegada = 1f;
    [SerializeField] private float tempoDeDelay = 0.5f;
    [SerializeField] private float velocidadeInicial = 10f;

    private Transform alvo;
    private Rigidbody rb;
    private float velocidadeDinamica;
    private bool podePerseguir;

    private void Awake()
    {
        rb = GetComponent<Rigidbody>();
        rb.useGravity = false;

        tempoDeVida = Mathf.Max(tempoDeVida, 0.1f);
        tempoDeChegada = Mathf.Max(tempoDeChegada, 0.01f);
        tempoDeDelay = Mathf.Max(tempoDeDelay, 0f);

        alvo = EncontrarAlvoMaisProximo();
        CalcularVelocidade();

        Destroy(gameObject, tempoDeVida);
        StartCoroutine(DelayParaPerseguir());
    }

    private void FixedUpdate()
    {
        if (podePerseguir)
        {
            if (alvo != null)
            {
                Vector3 direcao = (alvo.position - transform.position).normalized;
                rb.velocity = direcao * velocidadeDinamica;
                rb.MoveRotation(Quaternion.Slerp(rb.rotation, Quaternion.LookRotation(direcao), Time.fixedDeltaTime * 5f));
            }
            else
            {
                alvo = EncontrarAlvoMaisProximo();
                CalcularVelocidade();
            }
        }
        else
        {
            rb.velocity = transform.forward * velocidadeInicial;
        }
    }

    private IEnumerator DelayParaPerseguir()
    {
        yield return new WaitForSeconds(tempoDeDelay);
        podePerseguir = true;
    }

    private Transform EncontrarAlvoMaisProximo()
    {
        GameObject[] alvos = GameObject.FindGameObjectsWithTag(tagAlvo);
        Transform melhor = null;
        float menorDistancia = float.MaxValue;
        Vector3 pos = transform.position;

        foreach (var obj in alvos)
        {
            if (!obj.activeInHierarchy) continue;

            float dist = (obj.transform.position - pos).sqrMagnitude;
            if (dist < menorDistancia)
            {
                menorDistancia = dist;
                melhor = obj.transform;
            }
        }
        return melhor;
    }

    private void CalcularVelocidade()
    {
        if (alvo != null)
            velocidadeDinamica = Vector3.Distance(transform.position, alvo.position) / tempoDeChegada;
        else
            velocidadeDinamica = 0f;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag(tagAlvo))
            Destroy(gameObject);
    }
}
