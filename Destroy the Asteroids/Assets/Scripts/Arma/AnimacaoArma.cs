using UnityEngine;

public class AnimacaoArma : MonoBehaviour
{
    // Lista de refer�ncias para os componentes Animator
    [Header("Configura��es de Anima��o")]
    [Tooltip("Refer�ncias aos Animators dos objetos.")]
    [SerializeField] private Animator[] animators;

    // Nome do par�metro bool que controla a anima��o de "Fire"
    [Header("Controle de Par�metro")]
    [SerializeField] private string parametroFire = "Fire";  // A vari�vel agora � configur�vel no Inspector

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
                    Debug.LogError($"Animator n�o encontrado no objeto para �ndice {i}. Certifique-se de que h� um Animator anexado.");
                }
            }
        }
    }

    // M�todo para ativar a anima��o
    public void AtivarAnimacao()
    {
        // Verifica se os Animators est�o configurados
        foreach (Animator animator in animators)
        {
            if (animator != null)
            {
                // Ativa a anima��o de "Fire"
                animator.SetBool(parametroFire, true);
            }
        }
    }

    // M�todo para desativar a anima��o
    public void DesativarAnimacao()
    {
        // Verifica se os Animators est�o configurados
        foreach (Animator animator in animators)
        {
            if (animator != null)
            {
                // Desativa a anima��o de "Fire"
                animator.SetBool(parametroFire, false);
            }
        }
    }

    // M�todo para alternar o estado da anima��o
    public void AlternarAnimacao()
    {
        foreach (Animator animator in animators)
        {
            if (animator != null)
            {
                // Alterna entre ativar ou desativar a anima��o de "Fire"
                bool estadoAtual = animator.GetBool(parametroFire);
                animator.SetBool(parametroFire, !estadoAtual);
            }
        }
    }
}
