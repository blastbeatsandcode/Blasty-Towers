using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TowerFactory : MonoBehaviour {
    [SerializeField] [Tooltip("Tower prefab to place in world")] GameObject tower;
    [SerializeField] [Tooltip("Maximum number of towers")] int towerLimit = 5;


    Queue<GameObject> towers = new Queue<GameObject>();

    public void AddTower(Tile baseTile)
    {
        towers.Enqueue(Instantiate(tower, baseTile.transform.position, Quaternion.identity, baseTile.transform));
        baseTile.isPlaceable = false;

        // Remove oldest tower from Queue and set its tile to placeable
        if (towers.Count > towerLimit)
        {
            towers.Peek().GetComponentInParent<Tile>().isPlaceable = true;
            Destroy(towers.Dequeue());
        }
    }
}
