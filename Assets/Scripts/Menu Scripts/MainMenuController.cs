using UnityEngine;

public class MainMenuController : MonoBehaviour
{
    [SerializeField] GameObject mainMenu;
    [SerializeField] GameObject optionsMenu;

    private void Awake()
    {
        mainMenu.SetActive(true);
        optionsMenu.SetActive(false);
    }
}
