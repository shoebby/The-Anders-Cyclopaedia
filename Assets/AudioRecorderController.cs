using UnityEngine;

public class AudioRecorderController : MonoBehaviour
{
    private AudioSource source;
    [SerializeField] AudioClip pingClip;

    private void Awake()
    {
        source = GetComponent<AudioSource>();
    }

    public void PlaySound()
    {
        source.PlayOneShot(pingClip);
    }
}
