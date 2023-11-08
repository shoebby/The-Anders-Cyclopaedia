using UnityEngine;

//Static class for generally helpful methods
public static class Helpers
{
    public static void DestroyChildren(this Transform t)
    {
        foreach (Transform child in t) Object.Destroy(child.gameObject);
    }

    public static void ToggleCursorLock()
    {
        if (Cursor.lockState == CursorLockMode.Locked)
            Cursor.lockState = CursorLockMode.None;
        else if (Cursor.lockState == CursorLockMode.None)
            Cursor.lockState = CursorLockMode.Locked;
        
        Cursor.visible = !Cursor.visible;
    }

    public static void ToggleMovements()
    {
        PlayerMovement.Instance.disabledPlayerMovement = !PlayerMovement.Instance.disabledPlayerMovement;
        OrbitCamera.Instance.disabledCameraMovement = !OrbitCamera.Instance.disabledCameraMovement;
    }

    public static void ToggleInteractor()
    {
        Interactor.Instance.canInteract = !Interactor.Instance.canInteract;
    }
}
