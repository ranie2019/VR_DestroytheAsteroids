using UnityEngine;
using System; // Adiciona a diretiva para usar a classe Array

public class AudioPlayer : MonoBehaviour
{
    [Header("Audio Source")]
    [Tooltip("Refer�ncia ao AudioSource que tocar� os �udios.")]
    [SerializeField] private AudioSource audioSource;

    [Header("Audio Clips")]
    [Tooltip("�udios de introdu��o que ser�o tocados no in�cio do jogo.")]
    [SerializeField] private AudioClip[] introClips;
    [Tooltip("�udios principais do jogo que ser�o tocados ap�s a introdu��o.")]
    [SerializeField] private AudioClip[] mainGameClips;
    [Tooltip("�udio de Game Over que ser� tocado quando o jogo terminar.")]
    [SerializeField] private AudioClip gameOverClip;

    private void Start()
    {
        PlayRandomIntroAudio();
    }

    private void PlayRandomIntroAudio()
    {
        if (audioSource != null && introClips.Length > 0)
        {
            AudioClip randomClip = introClips[UnityEngine.Random.Range(0, introClips.Length)];
            audioSource.clip = randomClip;
            audioSource.loop = false; // O �udio de introdu��o n�o deve repetir
            audioSource.Play();
        }
    }

    public void PlayRandomMainGameAudio()
    {
        if (audioSource != null && mainGameClips.Length > 0)
        {
            AudioClip randomClip = mainGameClips[UnityEngine.Random.Range(0, mainGameClips.Length)];
            audioSource.clip = randomClip;
            audioSource.loop = true; // O �udio principal pode repetir
            audioSource.Play();
        }
    }

    public void PlayGameOverAudio()
    {
        if (audioSource != null && gameOverClip != null)
        {
            audioSource.clip = gameOverClip;
            audioSource.loop = false; // O �udio de Game Over n�o deve repetir
            audioSource.Play();
        }
    }

    public void StopMainGameAudio()
    {
        if (audioSource != null && audioSource.isPlaying && audioSource.clip != null && Array.Exists(mainGameClips, clip => clip == audioSource.clip))
        {
            audioSource.Stop();
        }
    }
}
