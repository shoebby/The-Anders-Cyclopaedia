using System.Collections;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.Tilemaps;

public class PlayerMovement : Actor
{
    [Header("Tilemaps")]
    [SerializeField] private Tilemap bgTilemap;
    [SerializeField] private Tilemap collisionTilemap;

    [Header("Player Components")]
    [SerializeField] private SpriteRenderer _spriteRenderer;

    [Header("Audio Clips")]
    [SerializeField] private AudioClip[] hurtClips;
    [SerializeField] private AudioClip movePlayerClip;
    [SerializeField] private AudioClip moveHighlightClip;
    [SerializeField] private AudioClip toggleStateClip;

    //non-visible variables
    [HideInInspector] public bool canInteract;
    [HideInInspector] public Entity interactableTarget;
    private Controls controls;
    private Transform highlighterTransform;
    private InteractionSelector _interactionSelector;
    private float startingHealth = 100;
    private float standardMoveDelay = 0;

    protected void Awake()
    {      
        controls = new Controls();

        InitPlayerStats();

        highlighterTransform = GetComponentInChildren<Highlighter>().transform;
        _interactionSelector = GetComponentInChildren<InteractionSelector>();
    }

    protected void OnEnable()
    {
        controls.Enable();
    }

    protected void OnDisable()
    {
        controls.Disable();
    }

    protected void Start()
    {
        controls.world.move.performed += ctx => Move(ctx.ReadValue<Vector2>());

        controls.world.interactmodetoggle.performed += ToggleInteractMode;
        canInteract = false;
    }

    private void InitPlayerStats()
    {
        actorName = "Player";
        health = startingHealth;
        moveDelay = standardMoveDelay;
    }

    private void Move(Vector2 moveDir)
    {
        if (CanMove(moveDir) && !canInteract)
        {
            transform.position += (Vector3)moveDir;
            AudioSystem.Instance.PlayAClip(movePlayerClip);
        }
        else if (canInteract)
        {
            highlighterTransform.position += (Vector3)moveDir;
            AudioSystem.Instance.PlayAClip(moveHighlightClip);
        }

        if (moveDir.x > 0)
            _spriteRenderer.flipX = false;
        else if (moveDir.x < 0)
            _spriteRenderer.flipX = true;
    }

    private bool CanMove(Vector2 direction)
    {
        if (timeToMove > 0)
            return false;
        
        Vector3Int gridPosition =  bgTilemap.WorldToCell(transform.position + (Vector3)direction);
        if (!bgTilemap.HasTile(gridPosition) || collisionTilemap.HasTile(gridPosition))
            return false;

        timeToMove = moveDelay;
        return true;
    }

    protected override void HurtActor(int hurtAmount)
    {
        health -= hurtAmount;
        ActionLog.Log.LogAction(ActionLog.Log.actionColor_damage, "You just took " + hurtAmount + " Hurt.");
        AudioSystem.Instance.PlayAClip(hurtClips[Random.Range(0, hurtClips.Length)]);
    }

    private void ToggleInteractMode(InputAction.CallbackContext value)
    {
        canInteract = !canInteract;

        if (!canInteract)
            return;
    }

    public void SetTargetEntity(Entity entity)
    {
        interactableTarget = entity;
        _interactionSelector.TogglePanel();
    }

    public void ClearTargetEntity()
    {
        _interactionSelector.TogglePanel();
        interactableTarget = null;
    }
}
