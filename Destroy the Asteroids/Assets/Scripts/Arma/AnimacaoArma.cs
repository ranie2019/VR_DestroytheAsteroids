using UnityEngine;

public class AnimacaoArma : MonoBehaviour
{
    [Header("Animators")]
    [Tooltip("Refer�ncias aos Animators que controlam as anima��es da torre.")]
    [SerializeField] private Animator cima;
    [SerializeField] private Animator baixo;
    [SerializeField] private Animator gatilho;

    // M�todo p�blico para ativar a anima��o de disparo
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

    // M�todo p�blico para desativar a anima��o de disparo
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
