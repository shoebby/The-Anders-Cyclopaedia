using UnityEngine;
using UnityEngine.SceneManagement;
using PixelCrushers.DialogueSystem;

[CreateAssetMenu(menuName = "Dialogue Utilities")]
public class DialogueUtils : ScriptableObject
{
    public void GenerateCheckRoll(string senseActorName)
    {
        int senseValue = DialogueManager.masterDatabase.GetActor(senseActorName).LookupInt("SenseValue");

        int roll = Random.Range(1, 6);
        Debug.Log("Rolled " + roll + " + " + senseValue);
        roll += senseValue;
        DialogueLua.SetVariable("CheckRoll", roll);
    }

    public void PlayAmbience(AudioClip clip)
    {
        AudioSystem.Instance.PlayAmbience(clip);
    }

    public void FadeAmbienceVolume()
    {
        AudioSystem.Instance.FadeAmbienceVolume();
    }

    public void PlayClip(AudioClip clip)
    {
        AudioSystem.Instance.PlayAClip(clip);
    }

    public void LoadScene(string sceneName)
    {
        SceneManager.LoadScene(sceneName);
    }

    public void QuitGame() => Application.Quit();
}