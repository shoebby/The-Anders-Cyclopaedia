using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class InteractionPromptUI : Singleton<InteractionPromptUI>
{
    [SerializeField] private GameObject promptPanel;
    [SerializeField] private TextMeshProUGUI promptTextField;
    public bool isDisplayed = false;

    private void Start() => Close();

    public void SetUp(string promptText)
    {
        promptTextField.text = promptText;
        promptPanel.SetActive(true);
        isDisplayed = true;
    }

    public void UpdatePrompt(string promptText)
    {
        promptTextField.text = promptText;
    }

    public void Close()
    {
        promptPanel.SetActive(false);
        isDisplayed = false;
    }
}
