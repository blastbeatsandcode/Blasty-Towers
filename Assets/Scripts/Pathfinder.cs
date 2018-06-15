using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pathfinder : MonoBehaviour {

    // Dictionary to store our locations and the tiles associated with it
    Dictionary<Vector2Int, Tile> worldGrid = new Dictionary<Vector2Int, Tile>();

    // Set Start and endpoints in inspector
    [Tooltip("Set start and end points for path")]
    [SerializeField] Tile startPoint, endPoint;

    // Queue up our path
    Queue<Tile> queue = new Queue<Tile>();
    bool isRunning;

    // Define directions
    Vector2Int[] directions = {
        Vector2Int.up,
        Vector2Int.down,
        Vector2Int.left,
        Vector2Int.right
    };

	// Use this for initialization
	void Start ()
    {
        LoadTiles();
        ColorStartAndEnd();
        Pathfind();
        //ExploreNeighbors();
    }

    private void Pathfind()
    {
        queue.Enqueue(startPoint);

        while (queue.Count > 0)
        {
            Tile searchCenter = queue.Dequeue();
            print("Searching from: " + searchCenter);

            HaltIfEndFound(searchCenter);
        }
    }

    private void HaltIfEndFound(Tile searchCenter)
    {
        // If the start and endpoint are the same, do nothing
        if (searchCenter == endPoint)
        {
            print("STOPPED");
            isRunning = false;
        }
    }

    private void ExploreNeighbors()
    {
        foreach (Vector2Int direction in directions)
        {
            Vector2Int explorationCoords = startPoint.GetGridPos() + direction;
            try
            {
                worldGrid[explorationCoords].SetTopColor(Color.blue);
            }
            catch
            {

            }
        }
    }

    private void ColorStartAndEnd()
    {
        startPoint.SetTopColor(Color.cyan);
        endPoint.SetTopColor(Color.red);
    }

    // Load all of the tiles in the map into an array
    private void LoadTiles()
    {
        // Get all of the tiles, put them into an array
        Tile[] tiles = FindObjectsOfType<Tile>();

        // Iterate over each tile in the array
        foreach (Tile tile in tiles)
        {
            // Check for if the tile is overlapping
            if (!worldGrid.ContainsKey(tile.GetGridPos()))
            {
                worldGrid.Add(tile.GetGridPos(), tile);
            }
        }
    }
}
