using UnityEngine;

public class AudioExplosion : MonoBehaviour
{
    [Header("�udios de Explos�o")]
    [Tooltip("�udio que ser� tocado primeiro quando o objeto colidir.")]
    [SerializeField] private AudioClip firstExplosionClip;

    [Tooltip("�udio que ser� tocado ap�s o primeiro quando o objeto colidir.")]
    [SerializeField] private AudioClip secondExplosionClip;

    [Header("Configura��es do �udio")]
    [Tooltip("Refer�ncia ao AudioSource que tocar� os �udios.")]
    [SerializeField] private AudioSource audioSource;

    [Tooltip("Tempo de atraso para tocar o segundo �udio.")]
    [SerializeField] private float delayBetweenAudios = 0.5f;

    private void OnCollisionEnter(Collision collision)
    {
        // Verifica se a colis�o foi com um objeto com a tag "Asteroid"
        if (collision.gameObject.CompareTag("Asteroid"))
        {
            // Verifica se os �udios e o AudioSource est�o configurados
            if (firstExplosionClip != null && secondExplosionClip != null && audioSource != null)
            {
                // Toca o primeiro �udio
                audioSource.PlayOneShot(firstExplosionClip);

                // Inicia uma rotina para tocar o segundo �udio ap�s o atraso
                StartCoroutine(PlaySecondAudioWithDelay());
            }
        }
    }

    private System.Collections.IEnumerator PlaySecondAudioWithDelay()
    {
        // Aguarda o tempo configurado antes de tocar o segundo �udio
        yield return new WaitForSeconds(delayBetweenAudios);

        // Toca o segundo �udio
        audioSource.PlayOneShot(secondExplosionClip);
    }
}
