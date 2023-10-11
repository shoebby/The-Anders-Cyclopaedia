using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class Interactor : MonoBehaviour
{
    private bool canInteract;

    private IInteractable interactableInstance;

    private Controls controls;

    private void Start()
    {
        controls = new Controls();
        controls.world.interactmodetoggle.performed += ToggleInteractMode;

        canInteract = false;
    }

    private void ToggleInteractMode(InputAction.CallbackContext value)
    {
        canInteract = !canInteract;

        if (interactableInstance != null)
        {
            //behaviour
        }
    }

    public void SetIInstance(IInteractable interactable)
    {
        interactableInstance = interactable;
    }

    public void ClearIInstance()
    {
        interactableInstance = null;
    }
}
