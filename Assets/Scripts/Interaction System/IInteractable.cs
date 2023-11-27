using UnityEngine.Events;

public interface IInteractable
{
    string InteractionPrompt { get; }

    UnityEvent OnInteract { get; }

    bool Interact(Interactor interactor);
}