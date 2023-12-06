using UnityEngine;

public class GateController : MonoBehaviour
{
    [SerializeField] private AudioClip inhaleClip;
    [SerializeField] private AudioClip exhaleClip;
    [SerializeField] private AudioSource source;

    private void Awake()
    {
        source = GetComponent<AudioSource>();
    }

    public void PlayInhale()
    {
        source.PlayOneShot(inhaleClip);
    }

    public void PlayExhale() 
    {
        source.PlayOneShot(inhaleClip);
    }
}
