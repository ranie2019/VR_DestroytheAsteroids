using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;
using System.Collections;

public class ReturnGun : MonoBehaviour
{
    [Header("Configurações de Posição Original")]
    [Tooltip("Posição e rotação originais da arma.")]
    [SerializeField] private Pose originPose;

    [Tooltip("Tempo de transição para retornar à posição original.")]
    [SerializeField] private float returnDuration = 0.5f;

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
    /// Salva a posição e rotação originais da arma.
    /// </summary>
    private void SaveOriginalPose()
    {
        originPose.position = transform.position;
        originPose.rotation = transform.rotation;
    }

    /// <summary>
    /// Registra os listeners para os eventos necessários.
    /// </summary>
    private void RegisterEventListeners()
    {
        if (grabInteractable != null)
        {
            grabInteractable.selectExited.AddListener(OnGunReleased);
            grabInteractable.selectEntered.AddListener(OnGunGrabbed);
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
            grabInteractable.selectEntered.RemoveListener(OnGunGrabbed);
        }
    }

    /// <summary>
    /// Chamado quando a arma é pega pelo jogador.
    /// </summary>
    /// <param name="arg0">Argumentos do evento de interação.</param>
    private void OnGunGrabbed(SelectEnterEventArgs arg0)
    {
        Rigidbody rb = GetComponent<Rigidbody>();
        if (rb != null)
        {
            rb.isKinematic = false; // Ativar física ao pegar
        }
    }

    /// <summary>
    /// Chamado quando a arma é solta pelo jogador.
    /// </summary>
    /// <param name="arg0">Argumentos do evento de soltura.</param>
    private void OnGunReleased(SelectExitEventArgs arg0)
    {
        StartCoroutine(SmoothResetToOriginalPose());
    }

    /// <summary>
    /// Retorna a arma suavemente à posição e rotação originais.
    /// </summary>
    private IEnumerator SmoothResetToOriginalPose()
    {
        Rigidbody rb = GetComponent<Rigidbody>();
        float elapsedTime = 0f;

        Vector3 startPosition = transform.position;
        Quaternion startRotation = transform.rotation;

        while (elapsedTime < returnDuration)
        {
            elapsedTime += Time.deltaTime;
            float t = elapsedTime / returnDuration;

            Vector3 newPosition = Vector3.Lerp(startPosition, originPose.position, t);
            Quaternion newRotation = Quaternion.Lerp(startRotation, originPose.rotation, t);

            if (rb != null)
            {
                rb.isKinematic = true; // Evitar interferências físicas durante a transição
                rb.MovePosition(newPosition);
                rb.MoveRotation(newRotation);
            }
            else
            {
                transform.SetPositionAndRotation(newPosition, newRotation);
            }

            yield return null;
        }

        // Certificar que o objeto termina na pose correta
        if (rb != null)
        {
            rb.MovePosition(originPose.position);
            rb.MoveRotation(originPose.rotation);
        }
        else
        {
            transform.SetPositionAndRotation(originPose.position, originPose.rotation);
        }
    }
}
