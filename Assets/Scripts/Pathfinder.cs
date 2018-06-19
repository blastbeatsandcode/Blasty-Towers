using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pathfinder : MonoBehaviour {
    // Set Start and endpoints in inspector
    [Tooltip("Set start and end points for path")]
    [SerializeField] Tile startPoint, endPoint;

    // Dictionary to store our locations and the tiles associated with it
    Dictionary<Vector2Int, Tile> worldGrid = new Dictionary<Vector2Int, Tile>();

    // Stores current search center.
    // Used for breadcrumbs and for current center of search
    Tile searchCenter;

    // Queue up our path
    Queue<Tile> queue = new Queue<Tile>();
    bool isRunning = true;

    // Define directions
    Vector2Int[] directions = {
        Vector2Int.up,
        Vector2Int.down,
        Vector2Int.left,
        Vector2Int.right
    };

    // Path to follow
    List<Tile> path = new List<Tile>();

    // Gets the path
    public List<Tile> GetPath()
    {
        if (path.Count == 0) CalculatePath();
        return path;
    }

    private void CalculatePath()
    {
        LoadTiles();
        BreadthFirstSearch();
        GeneratePath(endPoint);

        // Because the path is currently in reverse order from the breadcrumbs
        // We want to reverse it to return the proper path
        path.Add(startPoint);
        startPoint.isPlaceable = false;
        path.Reverse();
    }


    // TODO: Is recursion a problem?
    private void GeneratePath(Tile endpoint)
    {
        // If we have reached the end of the breadcrumbs, return
        if (endpoint.exploredFrom == null) return;

        //otherwise, add the tile explored from. Because it is part of the path, make not placeable
        path.Add(endpoint);
        endpoint.isPlaceable = false;
        GeneratePath(endpoint.exploredFrom);
    }

    private void BreadthFirstSearch()
    {
        queue.Enqueue(startPoint);

        while (queue.Count > 0 && isRunning)
        {
            searchCenter = queue.Dequeue();

            // Mark this tile as explored so we don't calculate on it again
            searchCenter.isExplored = true;

            // Halt if we reached the end of the path
            HaltIfEndFound(searchCenter);

            // Look for the next tile to move to
            ExploreNeighbors();
        }
    }

    private void HaltIfEndFound(Tile searchCenter)
    {
        // If the start and endpoint are the same, do nothing
        if (searchCenter == endPoint)
        {
            isRunning = false;
        }
    }

    private void ExploreNeighbors()
    {
        if (!isRunning) return; // We don't want to explore neighboring tiles if we are not moving
        foreach (Vector2Int direction in directions)
        {
            Vector2Int explorationCoords = searchCenter.GetGridPos() + direction;

            // If the grid contains the exploration coordinates, queue the new neighbor
            if (worldGrid.ContainsKey(explorationCoords)) QueueNewNeighbors(explorationCoords);
        }
    }

    // Add new neighbors to queue
    private void QueueNewNeighbors(Vector2Int explorationCoords)
    {
        // Get the coordinate of neighbor and set the top color
        Tile neighbor = worldGrid[explorationCoords];

        // If we have already explored the neighbor or if it is already queued, return
        if (neighbor.isExplored || queue.Contains(neighbor)) return;

        // Add this neighbor into the queue
        queue.Enqueue(neighbor);
        neighbor.exploredFrom = searchCenter; // Sets the breadcrumb
    }

    //private void ColorStartAndEnd()
    //{
    //    startPoint.SetTopColor(Color.cyan);
    //    endPoint.SetTopColor(Color.red);
    //}

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
