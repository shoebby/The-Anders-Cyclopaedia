using Sirenix.OdinInspector;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class MovingEntity : Entity
{
    [TitleGroup("Moving Entity")]
    [Title("Tilemap References", horizontalLine: false)]
    [SerializeField] private Tilemap bgTilemap;
    [SerializeField] private Tilemap collisionTilemap;
    [SerializeField] private Tilemap habitatTilemap;

    [Title("Variables", horizontalLine: false)]
    [SerializeField] private bool isTerritorial;
    [SerializeField] private Entity mainFoodSource;
    [Range(0, 10)]
    [SerializeField] private int hunger = 10;
    private enum State
    {
        Idle,
        Hunting,
        Alert,
        Hostile,
        Dying
    }
    [SerializeField] private State currentState = State.Idle;

    protected void Start()
    {
        NextState();
    }

    protected override void Update()
    {
        base.Update();

        if (health <= 0)
            currentState = State.Dying;
    }

    #region State Machine
    IEnumerator IdleState()
    {
        Debug.Log(actorName + ": Idle: Enter");

        float pickPosTimer = 5f;
        //int randX;
        //int randY;
        //Vector3 pickedPos;

        while (currentState == State.Idle)
        {
            pickPosTimer -= Time.deltaTime;
            
            //start hunting if hungry enough
            if (hunger <= 6)
                currentState = State.Hunting;

            

            yield return 0;
        }
        Debug.Log(actorName + ": Idle: Exit");
        NextState();
    }

    IEnumerator HuntingState()
    {
        Debug.Log(actorName + ": Hunting: Enter");
        while (currentState == State.Hunting)
        {
            yield return 0;
        }
        Debug.Log(actorName + ": Hunting: Exit");
        NextState();
    }

    IEnumerator AlertState()
    {
        Debug.Log(actorName + ": Alert: Enter");
        while (currentState == State.Alert)
        {
            yield return 0;
        }
        Debug.Log(actorName + ": Alert: Exit");
        NextState();
    }

    IEnumerator HostileState()
    {
        Debug.Log(actorName + ": Hostile: Enter");
        while (currentState == State.Hostile)
        {
            yield return 0;
        }
        Debug.Log(actorName + ": Hostile: Exit");
        NextState();
    }

    IEnumerator DyingState()
    {
        Debug.Log(actorName + ": Dying: Enter");
        while (currentState == State.Dying)
        {
            yield return 0;
        }
        Debug.Log(actorName + ": Dying: Exit");
        NextState();
    }

    private void NextState()
    {
        string methodName = currentState.ToString() + "State";
        StartCoroutine(methodName);
    }
    #endregion

    private bool CanMove(Vector2 direction, Tilemap walkableTilemap)
    {
        if (timeToMove > 0)
            return false;

        Vector3Int gridPosition = walkableTilemap.WorldToCell(transform.position + (Vector3)direction);
        if (!walkableTilemap.HasTile(gridPosition) || collisionTilemap.HasTile(gridPosition))
            return false;

        timeToMove = moveDelay;
        return true;
    }

    private void Move(Vector2 moveDir, Tilemap walkableTilemap)
    {
        if (CanMove(moveDir, walkableTilemap))
        {
            transform.position += (Vector3)moveDir;
        }

        if (moveDir.x > 0)
            entitySprite.flipX = false;
        else if (moveDir.x < 0)
            entitySprite.flipX = true;
    }

    #region Interact Functions
    public override void Collect()
    {
        throw new System.NotImplementedException();
    }

    public override void Feel()
    {
        throw new System.NotImplementedException();
    }

    public override void Listen()
    {
        throw new System.NotImplementedException();
    }

    public override void Look()
    {
        throw new System.NotImplementedException();
    }

    public override void Smell()
    {
        throw new System.NotImplementedException();
    }

    public override void Taste()
    {
        throw new System.NotImplementedException();
    }
    #endregion
}
