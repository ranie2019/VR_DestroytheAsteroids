using UnityEngine;

public class TurrentAnimator : MonoBehaviour
{
    [Header("Animators")]
    [Tooltip("Refer�ncias aos Animators que controlam as anima��es da torre.")]
    [SerializeField] private Animator turretAnimator1;
    [SerializeField] private Animator turretAnimator2;

    // M�todo p�blico para ativar a anima��o de disparo
    public void ActivateFireAnimation()
    {
        if (turretAnimator1 != null)
        {
            turretAnimator1.SetBool("Laser", true);
        }

        if (turretAnimator2 != null)
        {
            turretAnimator2.SetBool("Laser", true);
        }
    }

    // M�todo p�blico para desativar a anima��o de disparo
    public void DeactivateFireAnimation()
    {
        if (turretAnimator1 != null)
        {
            turretAnimator1.SetBool("Laser", false);
        }

        if (turretAnimator2 != null)
        {
            turretAnimator2.SetBool("Laser", false);
        }
    }
}
