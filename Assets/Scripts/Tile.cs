using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tile : MonoBehaviour {
    // Opting to make this not available in the editor will prevent any kind of location errors
    const int gridSize = 10;

    // We want to keep track of the grid position on the tile itself.
    Vector2Int gridPos;

    [Tooltip("Tile from which this tile was discovered in the path. This gives the algorithm the \"breadcrumb\" functionality of breadth first search")]
    public Tile exploredFrom;

    [Tooltip("Marks a tile as explored so our algorithm does not calculate the same tile twice.")]
    public bool isExplored = false;

    [SerializeField] [Tooltip("Colorize explored tiles?")] bool colorize;
    [SerializeField] [Tooltip("Color to colorize explored tiles. Defaults to blue")] Color color = Color.blue;

    void Update()
    {
        // If this tile is explored, color it
        if (colorize && isExplored)
        {
            SetTopColor(color);
        }
    }


    public int GetGridSize()
    {
        return gridSize;
    }

    public Vector2Int GetGridPos()
    {
        return new Vector2Int(
            Mathf.RoundToInt(transform.position.x / gridSize),
            Mathf.RoundToInt(transform.position.z / gridSize)
        );
    }

    public void SetTopColor(Color color)
    {
        MeshRenderer top = transform.Find("Top").GetComponent<MeshRenderer>();
        top.material.color = color;
    }
}
