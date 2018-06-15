using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tile : MonoBehaviour {
    // Opting to make this not available in the editor will prevent any kind of location errors
    const int gridSize = 10;

    // We want to keep track of the grid position on the tile itself.
    Vector2Int gridPos;

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
