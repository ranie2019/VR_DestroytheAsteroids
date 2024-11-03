using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RotacaoObjeto : MonoBehaviour
{
    [Header("Configura��es de Rota��o")]
    [SerializeField] private float velocidadeRotacao = 5.0f; // Velocidade padr�o de rota��o

    // Update is called once per frame
    void Update()
    {
        // Rotaciona o objeto ao redor do eixo Y no sentido anti-hor�rio (como a rota��o da Terra)
        transform.Rotate(0, -velocidadeRotacao * Time.deltaTime, 0);
    }
}
