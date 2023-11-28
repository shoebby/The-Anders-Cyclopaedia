using UnityEngine;
using UnityEngine.SceneManagement;

//Static class for generally helpful methods
public static class Helpers
{
    public static string previousScene;

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

    public static void StorePreviousScene()
    {
        previousScene = SceneManager.GetActiveScene().name;
    }

    public static string GetPreviousScene()
    {
        return previousScene;
    }

    public static void LoadScene(string sceneName)
    {
        SceneManager.LoadScene(sceneName);
    }
}
