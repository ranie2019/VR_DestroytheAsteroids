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
            // Se o Rigidbody for cinemático, desmarque para poder aplicar a física
            objectRigidbody.isKinematic = false;
        }

        // Atribuindo o evento de 'on detach' para o lançamento
        grabInteractable.onSelectExited.AddListener(OnThrow);
    }

    private void OnThrow(XRBaseInteractor interactor)
    {
        // Obtém a direção de lançamento
        Vector3 throwDirection = interactor.transform.forward;

        // Lança o objeto com a força definida
        LaunchObject(throwDirection);
    }

    private void LaunchObject(Vector3 direction)
    {
        // Garantir que o Rigidbody não seja cinemático durante o lançamento
        objectRigidbody.isKinematic = false;

        // Aplica uma força ao Rigidbody para lançar o objeto
        objectRigidbody.AddForce(direction * throwForce, ForceMode.Impulse);

        // Se você precisar de algo mais, como adicionar rotação ou alterar outros parâmetros, pode fazer aqui

        // Opcional: Se você quiser voltar a ser cinemático após o lançamento, faça isso
        // objectRigidbody.isKinematic = true;
    }

    // Se você precisar de lógica extra para o comportamento de agarrar, pode adicionar aqui
    private void OnEnable()
    {
        grabInteractable.onSelectEntered.AddListener(OnGrab);
    }

    private void OnDisable()
    {
        grabInteractable.onSelectEntered.RemoveListener(OnGrab);
        grabInteractable.onSelectExited.RemoveListener(OnThrow);
    }

    // Método chamado quando o objeto é agarrado
    private void OnGrab(XRBaseInteractor interactor)
    {
        // Se você quiser fazer algo quando o objeto é pego, pode adicionar lógica aqui.
    }
}
