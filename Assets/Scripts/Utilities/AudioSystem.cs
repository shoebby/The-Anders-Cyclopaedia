using System.Collections;
using UnityEngine;

public class AudioSystem : Singleton<AudioSystem>
{
    [SerializeField] private AudioSource ambientSource;
    [SerializeField] private AudioSource clipSource;

    public void PlayAmbience(AudioClip clip)
    {
        ambientSource.clip = clip;
        ambientSource.Play();
    }

    public void SwitchAmbience(AudioClip clip)
    {
        StartCoroutine(Fade(clip));
    }

    public void PlayAClip(AudioClip clip)
    {
        if (!clipSource.isPlaying)
        {
            clipSource.PlayOneShot(clip);
        }
    }

    public void FadeAmbienceVolume()
    {
        StartCoroutine(Fade(null));
    }

    public float GetAmbienceVolume()
    {
        return ambientSource.volume;
    }

    private IEnumerator Fade(AudioClip crossfadeClip)
    {
        float targetVolume = 0f;
        float initialVolume = ambientSource.volume;
        float elapsedTime = 0f;
        float fadeDuration = 3f;

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

        if (crossfadeClip != null)
        {
            ambientSource.clip = crossfadeClip;
            ambientSource.Play();

            StartCoroutine(Fade(null));
        }
    }
}
