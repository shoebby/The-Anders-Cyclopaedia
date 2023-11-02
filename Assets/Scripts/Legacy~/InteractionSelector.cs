using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using TMPro;

public class InteractionSelector : MonoBehaviour
{
    [Header("TMPro Components")]
    [SerializeField] private TMP_Text interactionName;
    [SerializeField] private TMP_Text interactableName;

    [Header("Interact Clips")]
    [SerializeField] private AudioClip listenClip;
    [SerializeField] private AudioClip lookClip;
    [SerializeField] private AudioClip tasteClip;
    [SerializeField] private AudioClip smellClip;
    [SerializeField] private AudioClip feelClip;
    [SerializeField] private AudioClip collectClip;


    private PlayerMovement player;
    private Controls controls;
    private Canvas _canvas;
    private bool canvasActive;

    private string[] interactions =
    {
        "Listen",
        "Look",
        "Taste",
        "Smell",
        "Feel",
        "Collect"
    };

    private int currentInteraction;

    private void Awake()
    {
        controls = new Controls();

        player = GetComponentInParent<PlayerMovement>();
        _canvas = GetComponent<Canvas>();
    }

    private void OnEnable()
    {
        controls.Enable();
    }

    private void OnDisable()
    {
        controls.Disable();
    }

    void Start()
    {
        controls.world.panelleft.performed += TabLeft;
        controls.world.panelright.performed += TabRight;
        controls.world.confirm.performed += FireInteraction;

        currentInteraction = 0;

        canvasActive = true;
        player.ClearTargetEntity();
    }

    private void Update()
    {
        if (player.interactableTarget != null)
        {
            interactableName.text = player.interactableTarget.actorName;
            interactionName.text = interactions[currentInteraction];
        }
    }

    private void FireInteraction(InputAction.CallbackContext value)
    {
        if (currentInteraction == 0)
        {
            player.interactableTarget.Listen();
            AudioSystem.Instance.PlayAClip(listenClip);
        }
        else if (currentInteraction == 1)
        {
            player.interactableTarget.Look();
            AudioSystem.Instance.PlayAClip(lookClip);
        }
        else if (currentInteraction == 2)
        {
            player.interactableTarget.Taste();
            AudioSystem.Instance.PlayAClip(tasteClip);
        }
        else if (currentInteraction == 3)
        {
            player.interactableTarget.Smell();
            AudioSystem.Instance.PlayAClip(smellClip);
        }
        else if (currentInteraction == 4)
        {
            player.interactableTarget.Feel();
            AudioSystem.Instance.PlayAClip(feelClip);
        }
        else if (currentInteraction == 5)
        {
            player.interactableTarget.Collect();
            AudioSystem.Instance.PlayAClip(collectClip);
        }
    }

    public void TogglePanel()
    {
        canvasActive = !canvasActive;

        _canvas.enabled = canvasActive;
        currentInteraction = 0;
    }

    private void TabLeft(InputAction.CallbackContext value)
    {
        if (currentInteraction > 0)
            currentInteraction -= 1;
        else if (currentInteraction == 0)
            currentInteraction = interactions.Length - 1;
    }

    private void TabRight(InputAction.CallbackContext value)
    {
        if (currentInteraction < interactions.Length - 1)
            currentInteraction += 1;
        else if (currentInteraction == interactions.Length - 1)
            currentInteraction = 0;
    }
}
