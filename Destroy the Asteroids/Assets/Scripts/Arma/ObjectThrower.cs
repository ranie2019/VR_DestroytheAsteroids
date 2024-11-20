using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;

public class c : MonoBehaviour
{
    [Header("Configura��es de Lan�amento")]
    public float throwForce = 10f;  // For�a aplicada ao lan�ar o objeto

    private XRGrabInteractable grabInteractable;
    private Rigidbody objectRigidbody;

    private void Awake()
    {
        grabInteractable = GetComponent<XRGrabInteractable>();
        objectRigidbody = GetComponent<Rigidbody>();

        // Verifica se o Rigidbody est� marcado como kinematic
        if (objectRigidbody.isKinematic)
        {
            // Se o Rigidbody for cinem�tico, desmarque para poder aplicar a f�sica
            objectRigidbody.isKinematic = false;
        }

        // Atribuindo o evento de 'on detach' para o lan�amento
        grabInteractable.onSelectExited.AddListener(OnThrow);
    }

    private void OnThrow(XRBaseInteractor interactor)
    {
        // Obt�m a dire��o de lan�amento
        Vector3 throwDirection = interactor.transform.forward;

        // Lan�a o objeto com a for�a definida
        LaunchObject(throwDirection);
    }

    private void LaunchObject(Vector3 direction)
    {
        // Garantir que o Rigidbody n�o seja cinem�tico durante o lan�amento
        objectRigidbody.isKinematic = false;

        // Aplica uma for�a ao Rigidbody para lan�ar o objeto
        objectRigidbody.AddForce(direction * throwForce, ForceMode.Impulse);

        // Se voc� precisar de algo mais, como adicionar rota��o ou alterar outros par�metros, pode fazer aqui

        // Opcional: Se voc� quiser voltar a ser cinem�tico ap�s o lan�amento, fa�a isso
        // objectRigidbody.isKinematic = true;
    }

    // Se voc� precisar de l�gica extra para o comportamento de agarrar, pode adicionar aqui
    private void OnEnable()
    {
        grabInteractable.onSelectEntered.AddListener(OnGrab);
    }

    private void OnDisable()
    {
        grabInteractable.onSelectEntered.RemoveListener(OnGrab);
        grabInteractable.onSelectExited.RemoveListener(OnThrow);
    }

    // M�todo chamado quando o objeto � agarrado
    private void OnGrab(XRBaseInteractor interactor)
    {
        // Se voc� quiser fazer algo quando o objeto � pego, pode adicionar l�gica aqui.
    }
}
