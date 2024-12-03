using UnityEngine;

public class Habilitado : MonoBehaviour
{
    private void OnValidate()
    {
        // Verifica se o objeto pai está habilitado no Inspector
        if (!gameObject.activeSelf)
        {
            // Habilita o objeto pai caso não esteja habilitado
            gameObject.SetActive(true);
        }

        // Itera sobre todos os filhos e garante que estejam habilitados
        foreach (Transform child in transform)
        {
            if (!child.gameObject.activeSelf)
            {
                // Habilita o filho se não estiver habilitado
                child.gameObject.SetActive(true);
            }
        }
    }
}
