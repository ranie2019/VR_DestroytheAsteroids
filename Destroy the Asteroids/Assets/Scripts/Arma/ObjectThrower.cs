using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;

public class c : MonoBehaviour
{
    [Header("Configura��es de Lan�amento")]
    public float throwForce = 10f;  // For�a aplicada ao lan�ar o objeto

    private UnityEngine.XR.Interaction.Toolkit.Interactables.XRGrabInteractable grabInteractable;
    private Rigidbody objectRigidbody;

    private void Awake()
    {
        grabInteractable = GetComponent<UnityEngine.XR.Interaction.Toolkit.Interactables.XRGrabInteractable>();
        objectRigidbody = GetComponent<Rigidbody>();

        // Verifica se o Rigidbody est� marcado como kinematic
        if (objectRigidbody.isKinematic)
        {
            objectRigidbody.isKinematic = false;
        }

        // Atribuindo o evento de 'on detach' para o lan�amento
        grabInteractable.selectExited.AddListener(OnThrow);
    }

    private void OnThrow(SelectExitEventArgs args)
    {
        if (!args.isCanceled) // Garante que o lan�amento n�o foi cancelado
        {
            // Obt�m a dire��o de lan�amento do interactor
            Vector3 throwDirection = args.interactorObject.transform.forward;

            // Lan�a o objeto com a for�a definida
            LaunchObject(throwDirection);
        }
    }

    private void LaunchObject(Vector3 direction)
    {
        objectRigidbody.isKinematic = false; // Garante que o Rigidbody esteja ativo

        // Aplica uma for�a ao Rigidbody para lan�ar o objeto
        objectRigidbody.AddForce(direction * throwForce, ForceMode.Impulse);
    }

    private void OnEnable()
    {
        grabInteractable.selectEntered.AddListener(OnGrab);
    }

    private void OnDisable()
    {
        grabInteractable.selectEntered.RemoveListener(OnGrab);
        grabInteractable.selectExited.RemoveListener(OnThrow);
    }

    private void OnGrab(SelectEnterEventArgs args)
    {
        // L�gica opcional ao agarrar o objeto
    }
}
