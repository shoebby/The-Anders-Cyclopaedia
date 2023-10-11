using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;
using UnityEngine.UIElements;

public class GridController : MonoBehaviour
{
    private Grid grid;
    [SerializeField] private Tilemap interactionMap = null;
    [SerializeField] private Tilemap highlightMap = null;
    [SerializeField] private Tile hoverTile = null;

    void Start()
    {
        grid = gameObject.GetComponent<Grid>();
    }

    void Update()
    {
        //mouse over -> highlight tile
        Vector3Int mousePos = GetMousePosition();
        if (interactionMap.HasTile(mousePos))
        {
            highlightMap.SetTile(mousePos, hoverTile);
        }
        else if (!interactionMap.HasTile(mousePos))
        {
            highlightMap.ClearAllTiles();
        }
    }

    Vector3Int GetMousePosition()
    {
        Vector3 mouseWorldPos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        return grid.WorldToCell(mouseWorldPos);
    }
}
