using UnityEngine;

public class AnimacaoArma : MonoBehaviour
{
    // Lista de referências para os componentes Animator
    [Header("Configurações de Animação")]
    [Tooltip("Referências aos Animators dos objetos.")]
    [SerializeField] private Animator[] animators;

    // Nome do parâmetro bool que controla a animação de "Fire"
    [Header("Controle de Parâmetro")]
    [SerializeField] private string parametroFire = "Fire";  // A variável agora é configurável no Inspector

    private void Awake()
    {
        // Garantir que todos os Animators estejam configurados
        for (int i = 0; i < animators.Length; i++)
        {
            if (animators[i] == null)
            {
                animators[i] = GetComponent<Animator>();
                if (animators[i] == null)
                {
                    Debug.LogError($"Animator não encontrado no objeto para índice {i}. Certifique-se de que há um Animator anexado.");
                }
            }
        }
    }

    // Método para ativar a animação
    public void AtivarAnimacao()
    {
        // Verifica se os Animators estão configurados
        foreach (Animator animator in animators)
        {
            if (animator != null)
            {
                // Ativa a animação de "Fire"
                animator.SetBool(parametroFire, true);
            }
        }
    }

    // Método para desativar a animação
    public void DesativarAnimacao()
    {
        // Verifica se os Animators estão configurados
        foreach (Animator animator in animators)
        {
            if (animator != null)
            {
                // Desativa a animação de "Fire"
                animator.SetBool(parametroFire, false);
            }
        }
    }

    // Método para alternar o estado da animação
    public void AlternarAnimacao()
    {
        foreach (Animator animator in animators)
        {
            if (animator != null)
            {
                // Alterna entre ativar ou desativar a animação de "Fire"
                bool estadoAtual = animator.GetBool(parametroFire);
                animator.SetBool(parametroFire, !estadoAtual);
            }
        }
    }
}
