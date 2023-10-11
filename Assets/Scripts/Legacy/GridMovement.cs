using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GridMovement : MonoBehaviour
{
    private bool isMoving;
    private Vector2 startPos, targetPos;
    [SerializeField] private float timeToMove = 0.2f;

    void Update()
    {
        if (!isMoving)
        {
            if (Input.GetKeyDown(KeyCode.A))
                StartCoroutine(MovePlayer(Vector2.left));
            if (Input.GetKeyDown(KeyCode.D))
                StartCoroutine(MovePlayer(Vector2.right));

            if (Input.GetKeyDown(KeyCode.S))
                StartCoroutine(MovePlayer(Vector2.down));
            if (Input.GetKeyDown(KeyCode.W))
                StartCoroutine(MovePlayer(Vector2.up));
        }
    }

    private IEnumerator MovePlayer(Vector2 moveDir) 
    {
        isMoving = true;

        float elapsedTime = 0f;

        startPos = transform.position;
        targetPos = startPos + moveDir;
        while (elapsedTime < timeToMove)
        {
            transform.position = Vector2.Lerp(startPos, targetPos, (elapsedTime / timeToMove));
            elapsedTime += Time.deltaTime;
            yield return null;
        }

        transform.position = targetPos;

        isMoving = false;
    }
}
