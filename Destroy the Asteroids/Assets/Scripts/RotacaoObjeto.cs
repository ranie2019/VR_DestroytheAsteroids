using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RotacaoObjeto : MonoBehaviour
{
    [Header("Configura��es de Rota��o")]
    public float velocidadeRotacao = 5.0f; // Velocidade padr�o de rota��o
    public Vector3 eixoRotacao = Vector3.up; // Eixo de rota��o padr�o (Y)

    // Update is called once per frame
    void Update()
    {
        // Aplica a rota��o no eixo escolhido
        transform.Rotate(eixoRotacao * velocidadeRotacao * Time.deltaTime);
    }
}
