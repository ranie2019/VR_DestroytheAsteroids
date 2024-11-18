using UnityEngine;
using System;
using System.Collections;

public class AudioPlayer : MonoBehaviour
{
    [Header("Audio Source")]
    [Tooltip("Referência ao AudioSource que tocará os áudios.")]
    [SerializeField] private AudioSource audioSource;

    [Header("Audio Clips")]
    [Tooltip("Áudios de introdução que serão tocados no início do jogo.")]
    [SerializeField] private AudioClip[] introClips;
    [Tooltip("Áudios principais do jogo que serão tocados após a introdução.")]
    [SerializeField] private AudioClip[] mainGameClips;
    [Tooltip("Áudio de Game Over que será tocado quando o jogo terminar.")]
    [SerializeField] private AudioClip gameOverClip;

    [Header("Transições")]
    [Tooltip("Duração do fade in/out em segundos.")]
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
            StartCoroutine(PlayWithFade(randomClip, false)); // Sem loop para áudios de introdução
            Debug.Log($"Tocando áudio de introdução: {randomClip.name}");
        }
    }

    public void PlayRandomMainGameAudio()
    {
        if (audioSource != null && mainGameClips.Length > 0)
        {
            AudioClip randomClip = mainGameClips[UnityEngine.Random.Range(0, mainGameClips.Length)];
            StartCoroutine(PlayWithFade(randomClip, true)); // Com loop para áudios principais
            Debug.Log($"Tocando áudio principal: {randomClip.name}");
        }
    }

    public void PlayGameOverAudio()
    {
        if (audioSource != null && gameOverClip != null)
        {
            StartCoroutine(PlayWithFade(gameOverClip, false)); // Sem loop para áudio de Game Over
            Debug.Log($"Tocando áudio de Game Over: {gameOverClip.name}");
        }
    }

    public void StopMainGameAudio()
    {
        if (audioSource != null && audioSource.isPlaying && audioSource.clip != null &&
            Array.Exists(mainGameClips, clip => clip == audioSource.clip))
        {
            StartCoroutine(FadeOutCoroutine(fadeDuration));
            Debug.Log($"Parando áudio principal: {audioSource.clip.name}");
        }
    }

    private IEnumerator PlayWithFade(AudioClip clip, bool loop)
    {
        // Faz o fade out do áudio atual
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
        audioSource.volume = startVolume; // Restaura o volume para o próximo áudio
    }

    // Métodos para acionar eventos
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
