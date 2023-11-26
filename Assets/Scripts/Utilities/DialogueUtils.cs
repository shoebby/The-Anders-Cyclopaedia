using UnityEngine;
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
        DialogueLua.SetVariable("General.CheckRoll", roll);
    }

    public void StopAllConversations()
    {
        for (int i = DialogueManager.instance.activeConversations.Count - 1; i >= 0; i--)
        {
            DialogueManager.instance.activeConversations[i].conversationController.Close();
        }
    }

    #region Scene Management
    public void LoadScene(string sceneName)
    {
        Helpers.StorePreviousScene();
        Helpers.LoadScene(sceneName);
    }

    public void QuitGame() => Application.Quit();
    #endregion

    #region Audio System
    public void PlayAmbience(AudioClip clip)
    {
        AudioSystem.Instance.PlayAmbience(clip);
    }

    public void SwitchAmbience(AudioClip clip)
    {
        AudioSystem.Instance.SwitchAmbience(clip);
    }

    public void FadeAmbienceVolume()
    {
        AudioSystem.Instance.FadeAmbienceVolume();
    }

    public void PlayClip(AudioClip clip)
    {
        AudioSystem.Instance.PlayAClip(clip);
    }
    #endregion

    #region Backdrop Canvas
    public void BackdropChange(Sprite backdropSprite) => BackdropCanvasScript.Instance.ChangeBackdrop(backdropSprite);

    public void BackdropFade() => BackdropCanvasScript.Instance.FadeBackdrop(null);

    public void BackdropFadeLoadScene(string sceneName) => BackdropCanvasScript.Instance.FadeBackdrop(sceneName);
    #endregion

    public void ToggleOverworldAbilities()
    {
        Helpers.ToggleCursorLock();
        Helpers.ToggleMovements();
        Helpers.ToggleInteractor();

        Interactor.Instance.isEngaged = !Interactor.Instance.isEngaged;
    }
}