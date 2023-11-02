using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Sirenix.OdinInspector;

[CreateAssetMenu(menuName = "Entity Stats")]
public class EntityStats : SerializedScriptableObject
{
    [PreviewField(60), HideLabel]
    [HorizontalGroup("Split", 60)]
    public Sprite _sprite;

    [VerticalGroup("Split/Right"), LabelWidth(120)]
    public string _name;

    public enum Stat
    {
        health,
        moveDelay,
        atkDamage
    }

    public enum ActionLog
    {
        collect,
        feel,
        listen,
        look,
        smell,
        taste
    }

    [TitleGroup("Stats")]
    public Dictionary<Stat, float> stats = new Dictionary<Stat, float>();

    [TitleGroup("Action Logs")]
    public Dictionary<ActionLog, string> actionLogs = new Dictionary<ActionLog, string>();

    public float GetStat(Stat stat)
    {
        if (stats.TryGetValue(stat, out float value))
        {
            return value;
        }
        else
        {
            Debug.LogError($"No stat value found for {stat} on {this._name}");
            return 0;
        }
    }

    public string GetActionLog(ActionLog log)
    {
        if (actionLogs.TryGetValue(log, out string value))
        {
            return value;
        }
        else
        {
            Debug.LogError($"No stat value found for {log} on {this._name}");
            return "null";
        }
    }

    [TitleGroup("SFX")]
    public AudioClip moveClip;
    public AudioClip idleClip;
    public AudioClip alertClip;
    public AudioClip hostileClip;
    public AudioClip attackClip;
    public AudioClip deathClip;
}
