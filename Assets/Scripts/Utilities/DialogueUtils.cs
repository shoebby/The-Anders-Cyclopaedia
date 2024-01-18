using UnityEngine;
using PixelCrushers.DialogueSystem;

[CreateAssetMenu(menuName = "Dialogue Utilities")]
public class DialogueUtils : ScriptableObject
{   
    public void GenerateCheckRoll(string senseActorName)
    {
        int senseValue = DialogueManager.masterDatabase.GetActor(senseActorName).LookupInt("SenseValue");

        int roll = Random.Range(1, 7);
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
        Helpers.LoadScene(sceneName);
    }

    public void StoreScene()
    {
        Helpers.StorePreviousScene();
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

    public void BackdropCrossfade(Sprite backdropSprite) => BackdropCanvasScript.Instance.CrossfadeBackdrop(backdropSprite);

    public void BackdropFadeLoadScene(string sceneName) => BackdropCanvasScript.Instance.FadeBackdrop(sceneName);
    #endregion

    public void ToggleOverworldAbilities()
    {
        Helpers.ToggleCursorLock();
        Helpers.ToggleMovements();
        Helpers.ToggleInteractor();

        Interactor.Instance.isEngaged = !Interactor.Instance.isEngaged;
    }

    public void ToggleMovement()
    {
        Helpers.ToggleMovements();
    }
    public void ToggleDialogueAnimStanding()
    {
        PlayerAnimationController.Instance.ToggleDialogueAnim(false);
    }

    public void ToggleDialogueAnimCrouching()
    {
        PlayerAnimationController.Instance.ToggleDialogueAnim(true);
    }

    public void PlayInterruption()
    {
        PlayInterruptionCanvasController.Instance.EnableInterruptionCanvas();
    }

    public void ResetAllVariables()
    {
        Debug.Log("Printing pre reset sense values");
        for (int i = 2; i < 10; i++)
        {
            Debug.Log(DialogueManager.masterDatabase.GetActor(i).Name + ": " + DialogueManager.masterDatabase.GetActor(i).LookupInt("SenseValue"));
        }

        DialogueManager.ResetDatabase(DatabaseResetOptions.RevertToDefault);

        Debug.Log("Printing post reset sense values");
        for (int i = 2; i < 10; i++)
        {
            Debug.Log(DialogueManager.masterDatabase.GetActor(i).Name + ": " + DialogueManager.masterDatabase.GetActor(i).LookupInt("SenseValue"));
        }
        
    }

    public void CheckSenseValues()
    {
        Debug.Log("------------------------------------------------");
        for (int i = 2; i < 10; i++)
        {
            Debug.Log(DialogueManager.masterDatabase.GetActor(i).Name + ": " + DialogueManager.masterDatabase.GetActor(i).LookupInt("SenseValue"));
        }
        Debug.Log("------------------------------------------------");
    }
}