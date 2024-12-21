using UnityEngine;

public class AudioExplosion : MonoBehaviour
{
    [Header("Áudios de Explosão")]
    [Tooltip("Áudio que será tocado primeiro quando o objeto colidir.")]
    [SerializeField] private AudioClip firstExplosionClip;

    [Tooltip("Áudio que será tocado após o primeiro quando o objeto colidir.")]
    [SerializeField] private AudioClip secondExplosionClip;

    [Header("Configurações do Áudio")]
    [Tooltip("Referência ao AudioSource que tocará os áudios.")]
    [SerializeField] private AudioSource audioSource;

    [Tooltip("Tempo de atraso para tocar o segundo áudio.")]
    [SerializeField] private float delayBetweenAudios = 0.5f;

    private void OnCollisionEnter(Collision collision)
    {
        // Verifica se a colisão foi com um objeto com a tag "Asteroid"
        if (collision.gameObject.CompareTag("Asteroid"))
        {
            // Verifica se os áudios e o AudioSource estão configurados
            if (firstExplosionClip != null && secondExplosionClip != null && audioSource != null)
            {
                // Toca o primeiro áudio
                audioSource.PlayOneShot(firstExplosionClip);

                // Inicia uma rotina para tocar o segundo áudio após o atraso
                StartCoroutine(PlaySecondAudioWithDelay());
            }
        }
    }

    private System.Collections.IEnumerator PlaySecondAudioWithDelay()
    {
        // Aguarda o tempo configurado antes de tocar o segundo áudio
        yield return new WaitForSeconds(delayBetweenAudios);

        // Toca o segundo áudio
        audioSource.PlayOneShot(secondExplosionClip);
    }
}
