using UnityEngine;
using PixelCrushers.DialogueSystem;

public class PauseMenuController : MonoBehaviour
{
    [SerializeField] GameObject pauseMenu;
    [SerializeField] GameObject mainPauseMenu;
    [SerializeField] GameObject optionsPauseMenu;
    [SerializeField] GameObject confirmQuitMenu;
    [SerializeField] KeyCode pauseKey;

    private bool isPaused;

    private void Awake()
    {
        isPaused = false;

        mainPauseMenu.SetActive(true);
        optionsPauseMenu.SetActive(false);
        confirmQuitMenu.SetActive(false);

        pauseMenu.SetActive(false);
    }

    private void Update()
    {
        if (Input.GetKeyDown(pauseKey))
        {
            TogglePauseMenu();
        }
    }

    public void TogglePauseMenu()
    {
        isPaused = !isPaused;
        if (!DialogueManager.IsConversationActive)
        {
            Helpers.ToggleMovements();
            Helpers.ToggleCursorLock();
            Helpers.ToggleInteractor();
        }

        if (isPaused)
        {
            pauseMenu.SetActive(true);
        } else if (!isPaused)
        {
            pauseMenu.SetActive(false);
            mainPauseMenu.SetActive(true);
            optionsPauseMenu.SetActive(false);
        }
    }
}
