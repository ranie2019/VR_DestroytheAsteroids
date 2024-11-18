using UnityEngine;
using System;
using System.Collections;

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

    [Header("Transi��es")]
    [Tooltip("Dura��o do fade in/out em segundos.")]
    [SerializeField] private float fadeDuration = 1f;

    // Eventos para troca de estados do jogo
    public event Action OnGameStart;
    public event Action OnGameOver;

    private void Start()
    {
        PlayRandomIntroAudio();
    }

    private void PlayRandomIntroAudio()
    {
        if (audioSource != null && introClips.Length > 0)
        {
            AudioClip randomClip = introClips[UnityEngine.Random.Range(0, introClips.Length)];
            StartCoroutine(PlayWithFade(randomClip, false)); // Sem loop para �udios de introdu��o
            Debug.Log($"Tocando �udio de introdu��o: {randomClip.name}");
        }
    }

    public void PlayRandomMainGameAudio()
    {
        if (audioSource != null && mainGameClips.Length > 0)
        {
            AudioClip randomClip = mainGameClips[UnityEngine.Random.Range(0, mainGameClips.Length)];
            StartCoroutine(PlayWithFade(randomClip, true)); // Com loop para �udios principais
            Debug.Log($"Tocando �udio principal: {randomClip.name}");
        }
    }

    public void PlayGameOverAudio()
    {
        if (audioSource != null && gameOverClip != null)
        {
            StartCoroutine(PlayWithFade(gameOverClip, false)); // Sem loop para �udio de Game Over
            Debug.Log($"Tocando �udio de Game Over: {gameOverClip.name}");
        }
    }

    public void StopMainGameAudio()
    {
        if (audioSource != null && audioSource.isPlaying && audioSource.clip != null &&
            Array.Exists(mainGameClips, clip => clip == audioSource.clip))
        {
            StartCoroutine(FadeOutCoroutine(fadeDuration));
            Debug.Log($"Parando �udio principal: {audioSource.clip.name}");
        }
    }

    private IEnumerator PlayWithFade(AudioClip clip, bool loop)
    {
        // Faz o fade out do �udio atual
        if (audioSource.isPlaying)
        {
            yield return StartCoroutine(FadeOutCoroutine(fadeDuration));
        }

        // Troca o clipe e toca com fade in
        audioSource.clip = clip;
        audioSource.loop = loop;
        audioSource.Play();
        yield return StartCoroutine(FadeInCoroutine(fadeDuration));
    }

    private IEnumerator FadeInCoroutine(float duration)
    {
        float targetVolume = 1f;
        audioSource.volume = 0f;

        while (audioSource.volume < targetVolume)
        {
            audioSource.volume += Time.deltaTime / duration;
            yield return null;
        }

        audioSource.volume = targetVolume;
    }

    private IEnumerator FadeOutCoroutine(float duration)
    {
        float startVolume = audioSource.volume;

        while (audioSource.volume > 0)
        {
            audioSource.volume -= startVolume * Time.deltaTime / duration;
            yield return null;
        }

        audioSource.Stop();
        audioSource.volume = startVolume; // Restaura o volume para o pr�ximo �udio
    }

    // M�todos para acionar eventos
    public void StartGame()
    {
        OnGameStart?.Invoke();
        PlayRandomMainGameAudio();
    }

    public void GameOver()
    {
        OnGameOver?.Invoke();
        PlayGameOverAudio();
    }
}
