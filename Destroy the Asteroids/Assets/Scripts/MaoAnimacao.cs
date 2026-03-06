using UnityEngine;
using UnityEngine.XR;

public class MaoAnimacao : MonoBehaviour
{
    public enum LadoMao { Esquerda, Direita }

    [Header("Qual mão este Animator controla?")]
    [SerializeField] private LadoMao lado = LadoMao.Direita;

    [Header("Parâmetros do Animator")]
    [SerializeField] private string parametroPegar = "mao";      // grip (gatilho de baixo)
    [SerializeField] private string parametroAtirar = "atirar";  // trigger (gatilho da frente)

    [Header("Limiares (caso o controle retorne float ao invés de bool)")]
    [Range(0f, 1f)] [SerializeField] private float limiarGrip = 0.1f;
    [Range(0f, 1f)] [SerializeField] private float limiarTrigger = 0.1f;

    [Header("Regras")]
    [Tooltip("Se marcado, só permite 'atirar' quando estiver 'pegando'.")]
    [SerializeField] private bool exigirPegarParaAtirar = true;

    private Animator anim;
    private InputDevice device;

    private void Awake()
    {
        anim = GetComponent<Animator>();
        BuscarDevice();
    }

    private void OnEnable()
    {
        BuscarDevice();
    }

    private void Update()
    {
        if (!device.isValid)
            BuscarDevice();

        bool pegando = LerGrip(device);
        bool atirando = LerTrigger(device);

        if (exigirPegarParaAtirar)
            atirando = atirando && pegando;

        if (anim)
        {
            anim.SetBool(parametroPegar, pegando);
            anim.SetBool(parametroAtirar, atirando);
        }
    }

    private void BuscarDevice()
    {
        var characteristics =
            InputDeviceCharacteristics.HeldInHand |
            InputDeviceCharacteristics.Controller |
            (lado == LadoMao.Direita ? InputDeviceCharacteristics.Right : InputDeviceCharacteristics.Left);

        var devices = new System.Collections.Generic.List<InputDevice>();
        InputDevices.GetDevicesWithCharacteristics(characteristics, devices);

        if (devices.Count > 0)
            device = devices[0];
    }

    private bool LerGrip(InputDevice d)
    {
        // tenta bool
        if (d.TryGetFeatureValue(CommonUsages.gripButton, out bool gripBtn))
            return gripBtn;

        // fallback float
        if (d.TryGetFeatureValue(CommonUsages.grip, out float grip))
            return grip >= limiarGrip;

        return false;
    }

    private bool LerTrigger(InputDevice d)
    {
        // tenta bool
        if (d.TryGetFeatureValue(CommonUsages.triggerButton, out bool trigBtn))
            return trigBtn;

        // fallback float
        if (d.TryGetFeatureValue(CommonUsages.trigger, out float trig))
            return trig >= limiarTrigger;

        return false;
    }
}