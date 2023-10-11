using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using System.Linq;

public class ActionLog : MonoBehaviour
{
    private static ActionLog _log;
    public static ActionLog Log
    {
        get
        {
            if (_log == null)
                Debug.LogError("GAME MANAGER IS NULL");
            return _log;
        }
    }
    private void Awake() => _log = this;

    [Header("Action Item Prefab")]
    [SerializeField] private List<GameObject> loggedItems;
    [SerializeField] private GameObject actionItem;
    [SerializeField] private int maxItems;

    [Header("Action Colors")]
    public Color actionColor_interact;
    public Color actionColor_statusEffect;
    public Color actionColor_damage;

    public void LogAction(Color itemColor, string itemText)
    {
        GameObject clone;
        clone = Instantiate(actionItem, this.transform);
        loggedItems.Add(clone);

        if (loggedItems.Count > maxItems)
        {
            Destroy(loggedItems.ElementAt(0));
            loggedItems.RemoveAt(0);
        }

        TextMeshProUGUI cloneText;
        cloneText = clone.GetComponentInChildren<TextMeshProUGUI>();

        cloneText.color = itemColor;
        cloneText.text = itemText;
    }
}
