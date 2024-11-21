using System.Collections;
using UnityEngine;

public class Inativo : MonoBehaviour
{
    public float tempoDeInatividade;  // Tempo de inatividade, será definido publicamente
    private bool podeContar = false;  // Flag para garantir que a contagem só comece uma vez

    private MeshRenderer meshRenderer;
    private BoxCollider boxCollider;  // Referência para o BoxCollider
    private Canvas canvasFilho;
    private LaserPointer laserPointerFilho;

    private float spawnTimer;  // Timer para controlar o tempo de inatividade
    private bool jogoIniciado = false;  // Flag para verificar se o jogo foi iniciado

    private Transform[] filhos;  // Array para armazenar os objetos filhos

    private void Awake()
    {
        // Referências para os componentes do objeto
        meshRenderer = GetComponent<MeshRenderer>();
        boxCollider = GetComponent<BoxCollider>();  // Obtém o BoxCollider
        canvasFilho = GetComponentInChildren<Canvas>();  // Obtém o Canvas filho
        laserPointerFilho = GetComponentInChildren<LaserPointer>();  // Obtém o LaserPointer filho

        // Desativa inicialmente o MeshRenderer, o BoxCollider e os filhos
        if (meshRenderer != null)
        {
            meshRenderer.enabled = false;
        }

        if (boxCollider != null)
        {
            boxCollider.enabled = false;
        }

        // Armazena os filhos do objeto em um array
        filhos = GetComponentsInChildren<Transform>();

        // Desativa todos os objetos filhos (exceto o próprio objeto pai)
        foreach (var filho in filhos)
        {
            if (filho != transform)  // Ignora o próprio objeto pai
            {
                filho.gameObject.SetActive(false);
            }
        }
    }

    // Método para configurar o tempo de inatividade
    public void Configurar(float tempo)
    {
        tempoDeInatividade = tempo;
    }

    // Método chamado pelo StartGame para iniciar a contagem
    public void IniciarContagem()
    {
        if (!podeContar && gameObject.activeInHierarchy && jogoIniciado)  // Verifica se o objeto está ativo e se o jogo foi iniciado
        {
            podeContar = true;
            spawnTimer = tempoDeInatividade;  // Reseta o timer para o tempo de inatividade
        }
    }

    // Método chamado a cada frame para controlar o tempo de inatividade
    private void Update()
    {
        if (jogoIniciado && podeContar)
        {
            spawnTimer -= Time.deltaTime;  // Reduz o tempo de inatividade

            if (spawnTimer <= 0f)
            {
                // Ativa o BoxCollider e o MeshRenderer após o tempo de inatividade
                if (boxCollider != null)
                {
                    boxCollider.enabled = true;
                }

                if (meshRenderer != null)
                {
                    meshRenderer.enabled = true;
                }

                // Ativa todos os filhos
                foreach (var filho in filhos)
                {
                    if (filho != transform)  // Ignora o próprio objeto pai
                    {
                        filho.gameObject.SetActive(true);
                    }
                }

                podeContar = false;  // Reseta a flag para evitar nova contagem
            }
        }
    }

    // Método para notificar o início do jogo
    public void NotificarInicioDoJogo()
    {
        jogoIniciado = true;  // Marca o jogo como iniciado
        IniciarContagem();  // Inicia a contagem
    }
}
