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
        DialogueLua.SetVariable("CheckRoll", roll);
    }
}