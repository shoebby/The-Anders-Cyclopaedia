using UnityEngine;
using UnityEngine.Events;

public class InteractableEventBased : MonoBehaviour, IInteractable
{
    [SerializeField]
    private string prompt;

    [SerializeField]
    private UnityEvent onInteractEvent = default;

    public string InteractionPrompt => prompt;

    public UnityEvent OnInteract => onInteractEvent;

    public bool Interact(Interactor interactor)
    {
        OnInteract.Invoke();
        return true;
    }
}
