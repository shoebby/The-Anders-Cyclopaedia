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
        GameManager.Instance.Pause();
        container.SetActive(true);
    }

    public void DisableInterruptionCanvas()
    {
        GameManager.Instance.Unpause();
        container.SetActive(false);
    }
}
