using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RotacaoObjeto : MonoBehaviour
{
    [Header("Configurações de Rotação")]
    public float velocidadeRotacao = 5.0f; // Velocidade padrão de rotação
    public Vector3 eixoRotacao = Vector3.up; // Eixo de rotação padrão (Y)

    // Update is called once per frame
    void Update()
    {
        // Aplica a rotação no eixo escolhido
        transform.Rotate(eixoRotacao * velocidadeRotacao * Time.deltaTime);
    }
}
