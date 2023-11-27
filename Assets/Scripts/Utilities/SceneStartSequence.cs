using UnityEngine;
using UnityEngine.Events;

public class SceneStartSequence : MonoBehaviour
{
    [SerializeField]
    private UnityEvent onStartScene;

    void Start()
    {
        onStartScene.Invoke();
    }
}
