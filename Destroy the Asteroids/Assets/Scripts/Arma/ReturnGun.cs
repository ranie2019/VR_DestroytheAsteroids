using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;

public class ReturnGun : MonoBehaviour
{
    [Header("Configura��es de Posi��o Original")]
    [Tooltip("Posi��o e rota��o originais da arma.")]
    [SerializeField] private Pose originPose;

    private XRGrabInteractable grabInteractable;

    private void Awake()
    {
        InitializeGrabInteractable();
        SaveOriginalPose();
    }

    private void OnEnable()
    {
        RegisterEventListeners();
    }

    private void OnDisable()
    {
        UnregisterEventListeners();
    }

    /// <summary>
    /// Inicializa o XRGrabInteractable.
    /// </summary>
    private void InitializeGrabInteractable()
    {
        grabInteractable = GetComponent<XRGrabInteractable>();
    }

    /// <summary>
    /// Salva a posi��o e rota��o originais da arma.
    /// </summary>
    private void SaveOriginalPose()
    {
        originPose.position = transform.position;
        originPose.rotation = transform.rotation;
    }

    /// <summary>
    /// Registra os listeners para os eventos necess�rios.
    /// </summary>
    private void RegisterEventListeners()
    {
        if (grabInteractable != null)
        {
            grabInteractable.selectExited.AddListener(OnGunReleased);
        }
    }

    /// <summary>
    /// Remove os listeners dos eventos.
    /// </summary>
    private void UnregisterEventListeners()
    {
        if (grabInteractable != null)
        {
            grabInteractable.selectExited.RemoveListener(OnGunReleased);
        }
    }

    /// <summary>
    /// Chamado quando a arma � solta pelo jogador.
    /// </summary>
    /// <param name="arg0">Argumentos do evento de soltura.</param>
    private void OnGunReleased(SelectExitEventArgs arg0)
    {
        ResetToOriginalPose();
    }

    /// <summary>
    /// Retorna a arma � posi��o e rota��o originais.
    /// </summary>
    private void ResetToOriginalPose()
    {
        transform.SetPositionAndRotation(originPose.position, originPose.rotation);
    }
}
