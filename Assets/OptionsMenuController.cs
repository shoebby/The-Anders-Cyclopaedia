using UnityEngine;
using UnityEngine.Audio;

public class OptionsMenuController : MonoBehaviour
{
    [SerializeField] private AudioMixer mixer;

    public void ChangeMasterVolume(float sliderValue)
    {
        mixer.SetFloat("masterVolume", Mathf.Log10(sliderValue) * 20);
    }

    public void ChangeAmbienceVolume(float sliderValue)
    {
        mixer.SetFloat("ambienceVolume", Mathf.Log10(sliderValue) * 20);
    }

    public void ChangeClipVolume(float sliderValue)
    {
        mixer.SetFloat("clipVolume", Mathf.Log10(sliderValue) * 20);
    }

    public void ChangeDialogueVolume(float sliderValue)
    {
        mixer.SetFloat("dialogueVolume", Mathf.Log10(sliderValue) * 20);
    }
}