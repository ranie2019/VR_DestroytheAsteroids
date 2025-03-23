using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;

public class c : MonoBehaviour
{
    [Header("Configurações de Lançamento")]
    public float throwForce = 10f;  // Força aplicada ao lançar o objeto

    private XRGrabInteractable grabInteractable;
    private Rigidbody objectRigidbody;

    private void Awake()
    {
        grabInteractable = GetComponent<XRGrabInteractable>();
        objectRigidbody = GetComponent<Rigidbody>();

        // Verifica se o Rigidbody está marcado como kinematic
        if (objectRigidbody.isKinematic)
        {
            objectRigidbody.isKinematic = false;
        }

        // Atribuindo o evento de 'on detach' para o lançamento
        grabInteractable.selectExited.AddListener(OnThrow);
    }

    private void OnThrow(SelectExitEventArgs args)
    {
        if (!args.isCanceled) // Garante que o lançamento não foi cancelado
        {
            // Obtém a direção de lançamento do interactor
            Vector3 throwDirection = args.interactorObject.transform.forward;

            // Lança o objeto com a força definida
            LaunchObject(throwDirection);
        }
    }

    private void LaunchObject(Vector3 direction)
    {
        objectRigidbody.isKinematic = false; // Garante que o Rigidbody esteja ativo

        // Aplica uma força ao Rigidbody para lançar o objeto
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
        // Lógica opcional ao agarrar o objeto
    }
}
