using UnityEngine;
using PixelCrushers.DialogueSystem;

public class SetTypewriterForActor : MonoBehaviour
{
    private AbstractTypewriterEffect typewriter;
    [SerializeField] private float pitchUpperLimit = 1.5f;
    [SerializeField] private float pitchLowerLimit = 0.5f;

    private void Awake()
    {
        typewriter = GetComponent<AbstractTypewriterEffect>();
    }

    public void OnBeginTypewriter() // Assign to typewriter's OnBegin() event.
    {
        // Look up character's audio clip:
        var actorName = DialogueManager.currentConversationState.subtitle.speakerInfo.nameInDatabase;
        var clipName = DialogueManager.masterDatabase.GetActor(actorName).LookupValue("VoiceClip");
        var clip = Resources.Load<AudioClip>("Voices/" + clipName);

        // Assign to typewriter:
        typewriter.audioClip = clip;
        typewriter.audioSource.clip = clip;
        typewriter.audioSource.pitch = 1f;
    }

    public void PitchModulateCharacter()
    {
        float pitchStep = 0f;

        if (typewriter.audioSource.pitch <= pitchLowerLimit)
            pitchStep = Random.Range(0f, .1f);
        else if (typewriter.audioSource.pitch >= pitchUpperLimit)
            pitchStep = Random.Range(-.1f, 0f);
        else
            pitchStep = Random.Range(-.1f, .1f);

        typewriter.audioSource.pitch += pitchStep;
    }
}
