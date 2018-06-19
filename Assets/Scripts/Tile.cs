using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tile : MonoBehaviour {
    // Opting to make this not available in the editor will prevent any kind of location errors
    const int gridSize = 10;

    // We want to keep track of the grid position on the tile itself.
    Vector2Int gridPos;

    [Header("Changes at runtime")] public bool isPlaceable = true;
    public Tile exploredFrom;
    public bool isExplored = false;

    //[SerializeField] [Tooltip("Colorize explored tiles?")] bool colorize;
    //[SerializeField] [Tooltip("Color to colorize explored tiles. Defaults to blue")] Color color = Color.blue;
    [Header("Placeable Settings")]
    [SerializeField] [Tooltip("Tower prefab to place in world")] GameObject tower;
    [SerializeField] [Tooltip("Tower prefab to show tower can't be placed here")] GameObject towerCantPlace;
    [SerializeField] [Tooltip("Tower prefab to show tower can be placed here")] GameObject towerPlace;

    bool hasTower = false;
    bool entered = false;

    void Update()
    {
        //// If this tile is explored, color it
        //if (colorize && isExplored)
        //{
        //    SetTopColor(color);
        //}
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

    //public void SetTopColor(Color color)
    //{
    //    MeshRenderer top = transform.Find("Top").GetComponent<MeshRenderer>();
    //    top.material.color = color;
    //}

    private void OnMouseOver()
    {
        if (!entered && !hasTower)
        {
            DisplayPlacer();
        }

        if (isPlaceable && Input.GetKeyDown(KeyCode.Mouse0))
        {
            Destroy(gameObject.GetComponentInChildren<Tower>().gameObject);
            Instantiate(tower, gameObject.transform.position, Quaternion.identity, gameObject.transform);
            isPlaceable = false;
            hasTower = true;
        }
    }

    private void OnMouseExit()
    {
        // Destroy instance
        if (!hasTower)
        {
            Destroy(gameObject.GetComponentInChildren<Tower>().gameObject);
        }
        entered = false;
    }

    private void DisplayPlacer()
    {
            if (isPlaceable)
            {
                Instantiate(towerPlace, gameObject.transform.position, Quaternion.identity, gameObject.transform);
            }
            else
            {
                Instantiate(towerCantPlace, gameObject.transform.position, Quaternion.identity, gameObject.transform);
            }
        entered = true;
    }
}
