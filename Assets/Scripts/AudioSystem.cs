using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioSystem : Singleton<AudioSystem>
{
    [SerializeField] private AudioSource ambientSource;
    [SerializeField] private AudioSource soundsSource;

    public void PlayAmbience(AudioClip clip)
    {
        ambientSource.clip = clip;
        ambientSource.Play();
    }

    public void PlayAClip(AudioClip clip) => soundsSource.PlayOneShot(clip);

    public void FadeAmbienceVolume()
    {
        StartCoroutine(Fade());
    }

    private IEnumerator Fade()
    {
        float targetVolume = 0f;
        float initialVolume = ambientSource.volume;
        float elapsedTime = 0f;
        float fadeDuration = 2f;

        if (initialVolume > 0f)
            targetVolume = 0f;
        else if (initialVolume == 0f)
            targetVolume = 1f;

        while (elapsedTime < fadeDuration)
        {
            elapsedTime += Time.deltaTime;
            ambientSource.volume = Mathf.Lerp(initialVolume, targetVolume, elapsedTime / fadeDuration);
            yield return null;
        }
    }
}
