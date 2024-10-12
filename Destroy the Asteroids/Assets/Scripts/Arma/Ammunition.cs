using UnityEngine;
using TMPro; // Certifique-se de incluir isso para usar TextMeshPro
using System.Collections;

public class Ammunition : MonoBehaviour
{
    [Header("Configurações de Munição")]
    [Tooltip("Número máximo de munições que a arma pode disparar antes de precisar recarregar.")]
    [SerializeField] private int maxAmmo = 10;

    [Tooltip("Tempo de recarregamento da arma (em segundos).")]
    [SerializeField] private float reloadTime = 3f;

    [Tooltip("Texto na UI que mostrará a quantidade de munição restante.")]
    [SerializeField] private TMP_Text ammoDisplay; // Usando TMP_Text para TextMeshPro

    private int currentAmmo;  // Munição atual
    private bool isReloading = false; // Flag para verificar se a arma está recarregando

    private void Start()
    {
        // Inicializa a munição com o valor máximo
        currentAmmo = maxAmmo;
        UpdateAmmoDisplay(); // Atualiza o display da munição ao iniciar o jogo
    }

    private void Update()
    {
        // Verifica se o gatilho foi pressionado e há munição disponível
        if (OVRInput.GetDown(OVRInput.Button.PrimaryIndexTrigger) && currentAmmo > 0 && !isReloading)
        {
            Fire();
        }

        // Inicia o recarregamento automaticamente se a munição acabar
        if (currentAmmo <= 0 && !isReloading)
        {
            StartCoroutine(Reload());
        }
    }

    private void Fire()
    {
        // Verifica se ainda há munição
        if (currentAmmo > 0)
        {
            // Diminui a munição e atualiza o display
            currentAmmo--;
            UpdateAmmoDisplay();

            Debug.Log("Arma disparada! Munição restante: " + currentAmmo);

            // Aqui você pode chamar a função de disparo da arma, como instanciar projéteis
        }
        else
        {
            Debug.Log("Sem munição!");
        }
    }

    private IEnumerator Reload()
    {
        isReloading = true;
        Debug.Log("Recarregando...");

        // Espera o tempo de recarregamento
        yield return new WaitForSeconds(reloadTime);

        // Após o recarregamento, restaura a munição ao valor máximo e atualiza o display
        currentAmmo = maxAmmo;
        isReloading = false;
        UpdateAmmoDisplay();
        Debug.Log("Recarregamento completo! Munição restaurada.");
    }

    private void UpdateAmmoDisplay()
    {
        // Atualiza o texto na UI com a quantidade de munição restante
        if (ammoDisplay != null)
        {
            ammoDisplay.text = currentAmmo + "/" + maxAmmo;
        }
    }
}
