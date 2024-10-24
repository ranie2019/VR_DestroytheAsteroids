using UnityEngine;
using TMPro; // Certifique-se de incluir isso para usar TextMeshPro
using System.Collections;

public class Ammunition : MonoBehaviour
{
    [Header("Configura��es de Muni��o")]
    [Tooltip("N�mero m�ximo de muni��es que a arma pode disparar antes de precisar recarregar.")]
    [SerializeField] private int maxAmmo = 10;

    [Tooltip("Tempo de recarregamento da arma (em segundos).")]
    [SerializeField] private float reloadTime = 3f;

    [Tooltip("Texto na UI que mostrar� a quantidade de muni��o restante.")]
    [SerializeField] private TMP_Text ammoDisplay; // Usando TMP_Text para TextMeshPro

    private int currentAmmo;  // Muni��o atual
    private bool isReloading = false; // Flag para verificar se a arma est� recarregando

    private void Start()
    {
        // Inicializa a muni��o com o valor m�ximo
        currentAmmo = maxAmmo;
        UpdateAmmoDisplay(); // Atualiza o display da muni��o ao iniciar o jogo
    }

    private void Update()
    {
        // Verifica se o gatilho foi pressionado e h� muni��o dispon�vel
        if (OVRInput.GetDown(OVRInput.Button.PrimaryIndexTrigger) && currentAmmo > 0 && !isReloading)
        {
            Fire();
        }

        // Inicia o recarregamento automaticamente se a muni��o acabar
        if (currentAmmo <= 0 && !isReloading)
        {
            StartCoroutine(Reload());
        }
    }

    private void Fire()
    {
        // Verifica se ainda h� muni��o
        if (currentAmmo > 0)
        {
            // Diminui a muni��o e atualiza o display
            currentAmmo--;
            UpdateAmmoDisplay();

            Debug.Log("Arma disparada! Muni��o restante: " + currentAmmo);

            // Aqui voc� pode chamar a fun��o de disparo da arma, como instanciar proj�teis
        }
        else
        {
            Debug.Log("Sem muni��o!");
        }
    }

    private IEnumerator Reload()
    {
        isReloading = true;
        Debug.Log("Recarregando...");

        // Espera o tempo de recarregamento
        yield return new WaitForSeconds(reloadTime);

        // Ap�s o recarregamento, restaura a muni��o ao valor m�ximo e atualiza o display
        currentAmmo = maxAmmo;
        isReloading = false;
        UpdateAmmoDisplay();
        Debug.Log("Recarregamento completo! Muni��o restaurada.");
    }

    private void UpdateAmmoDisplay()
    {
        // Atualiza o texto na UI com a quantidade de muni��o restante
        if (ammoDisplay != null)
        {
            ammoDisplay.text = currentAmmo + "/" + maxAmmo;
        }
    }
}
