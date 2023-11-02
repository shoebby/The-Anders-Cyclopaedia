using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class Interactor : MonoBehaviour
{
    [SerializeField] private Transform interactionPoint;
    [SerializeField] private float interactionPointRadius = 0.5f;
    [SerializeField] private LayerMask interactibleMask = 1 << 6;
    [SerializeField] private KeyCode interactKey = KeyCode.Mouse0;
    private readonly Collider[] colliders = new Collider[3]; //can be raised if there are more interactables in a single scene
    [SerializeField] private int numFound;

    private IInteractable interactable;

    private void Update()
    {
        numFound = Physics.OverlapSphereNonAlloc(interactionPoint.position, interactionPointRadius, colliders, interactibleMask);

        if (numFound > 0)
        {
            interactable = colliders[0].GetComponent<IInteractable>();

            if (interactable != null)
            {
                InteractionPromptUI.Instance.UpdatePrompt(interactable.InteractionPrompt);

                if (!InteractionPromptUI.Instance.isDisplayed)
                    InteractionPromptUI.Instance.SetUp(interactable.InteractionPrompt);

                if (Input.GetKeyDown(interactKey))
                    interactable.Interact(this);
            }
        }
        else
        {
            if (interactable != null)
                interactable = null;

            if (InteractionPromptUI.Instance.isDisplayed)
                InteractionPromptUI.Instance.Close();
        }
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(interactionPoint.position, interactionPointRadius);
    }
}
