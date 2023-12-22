using UnityEngine;

public class LookAtFocusPoint : MonoBehaviour
{
    [SerializeField] private Transform focusPoint;
    [SerializeField] private Vector3 orientationCorrection;
    
    void Update()
    {
        transform.LookAt(focusPoint);
        transform.Rotate(orientationCorrection);
    }
}
