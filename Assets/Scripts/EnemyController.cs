using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyController : MonoBehaviour {
	// Use this for initialization
	void Start ()
    {
        // Get instance of pathfinder script and store the path
        Pathfinder pathfinder = FindObjectOfType<Pathfinder>();
        List<Tile> path = pathfinder.GetPath();
        StartCoroutine(FollowPath(path));
    }

    // Update is called once per frame
    void Update () {
		
	}

    // Coroutine to show path
    // Print and then wait one second to continue
    IEnumerator FollowPath(List<Tile> path)
    {
        foreach (Tile tile in path)
        {
            transform.position = tile.transform.position;
            yield return new WaitForSeconds(1);
        }
    }
}
