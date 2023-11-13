using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class DemoSceneStartSequence : MonoBehaviour
{
    [SerializeField]
    private UnityEvent onStartScene;

    void Start()
    {
        onStartScene.Invoke();
    }
}
