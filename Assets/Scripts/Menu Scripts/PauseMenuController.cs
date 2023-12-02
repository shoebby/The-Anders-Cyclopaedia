using UnityEngine;
using PixelCrushers.DialogueSystem;

public class PauseMenuController : MonoBehaviour
{
    [SerializeField] GameObject pauseMenu;
    [SerializeField] GameObject mainPauseMenu;
    [SerializeField] GameObject optionsPauseMenu;
    [SerializeField] KeyCode pauseKey;

    private bool isPaused;

    private void Awake()
    {
        isPaused = false;

        mainPauseMenu.SetActive(true);
        optionsPauseMenu.SetActive(false);

        pauseMenu.SetActive(false);
    }

    private void Update()
    {
        if (Input.GetKeyDown(pauseKey))
        {
            TogglePauseMenu();
        }
    }

    private void TogglePauseMenu()
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
