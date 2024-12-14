using UnityEngine;

public class AnimacaoArma : MonoBehaviour
{
    [Header("Animators")]
    [Tooltip("Referências aos Animators que controlam as animações da torre.")]
    [SerializeField] private Animator cima;
    [SerializeField] private Animator baixo;
    [SerializeField] private Animator gatilho;

    // Método público para ativar a animação de disparo
    public void ActivateFireAnimation()
    {
        if (cima != null)
        {
            cima.SetBool("Laser", true);
        }

        if (baixo != null)
        {
            baixo.SetBool("Laser", true);
        }

        if (gatilho != null)
        {
            gatilho.SetBool("Laser", true);
        }
    }

    // Método público para desativar a animação de disparo
    public void DeactivateFireAnimation()
    {
        if (cima != null)
        {
            cima.SetBool("Laser", false);
        }

        if (baixo != null)
        {
            baixo.SetBool("Laser", false);
        }

        if (gatilho != null)
        {
            gatilho.SetBool("Laser", false);
        }
    }
}
