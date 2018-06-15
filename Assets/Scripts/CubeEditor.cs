using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[ExecuteInEditMode]
[SelectionBase]
[RequireComponent(typeof(Tile))]
public class CubeEditor : MonoBehaviour {

    //[Tooltip("Set the size of each object to tile; this is how much the object must be moved each time for snapping effect.")][SerializeField] [Range(1f, 20f)] float gridSize = 10f;
    Tile tile;

    private void Awake()
    {
        tile = GetComponent<Tile>();
    }

    void Update ()
    {
        SnapToGrid();
        UpdateLabel();
    }

    private void SnapToGrid()
    {
        // Force the object to move only 10 units at a time in any direction
        // This will make it "snap" so we don't have any inconsistencies
        int gridSize = tile.GetGridSize();
        transform.position = new Vector3(
            tile.GetGridPos().x * gridSize * gridSize,
            0f,
            tile.GetGridPos().y * gridSize * gridSize
        );
    }

    private void UpdateLabel()
    {
        int gridSize = this.tile.GetGridSize();
        // Get a reference to the textMesh component so we can write out coordinates
        TextMesh textMesh = GetComponentInChildren<TextMesh>();
        // Create the label for the tile
        string labelText = tile.GetGridPos().x / gridSize +
            "," + 
            tile.GetGridPos().y / gridSize;
        textMesh.text = labelText;
        gameObject.name = "[Tile] " + labelText;

        transform.position = new Vector3(tile.GetGridPos().x, 0f, tile.GetGridPos().y);
    }
}
