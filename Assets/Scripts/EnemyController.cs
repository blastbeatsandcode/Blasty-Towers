using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyController : MonoBehaviour {

    [Tooltip("List of blocks for the enemy to follow")][SerializeField] List<Tile> path;

    
    

	// Use this for initialization
	void Start ()
    {
        StartCoroutine(FollowPath());
    }

    // Update is called once per frame
    void Update () {
		
	}

    // Coroutine to show path
    // Print and then wait one second to continue
    IEnumerator FollowPath()
    {
        print("Starting Patrol...");
        foreach (Tile tile in path)
        {
            print("Visiting Tile " + tile.name);
            transform.position = tile.transform.position;
            yield return new WaitForSeconds(1);
        }

        print("Ending Patrol...");
    }
}
