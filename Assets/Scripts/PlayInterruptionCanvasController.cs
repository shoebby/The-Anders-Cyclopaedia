using UnityEngine;

public class PlayInterruptionCanvasController : Singleton<PlayInterruptionCanvasController>
{
    [SerializeField] private GameObject container;

    private new void Awake()
    {
        base.Awake();
        container.SetActive(false);
    }

    public void EnableInterruptionCanvas()
    {
        PlayerAnimationController.Instance.ToggleDialogueAnim();

        Helpers.ToggleCursorLock();
        Helpers.ToggleMovements();
        Helpers.ToggleInteractor();

        container.SetActive(true);
    }

    public void DisableInterruptionCanvas()
    {
        PlayerAnimationController.Instance.ToggleDialogueAnim();

        Helpers.ToggleCursorLock();
        Helpers.ToggleMovements();
        Helpers.ToggleInteractor();

        container.SetActive(false);
    }
}
