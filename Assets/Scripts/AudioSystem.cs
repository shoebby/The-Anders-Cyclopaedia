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
}
