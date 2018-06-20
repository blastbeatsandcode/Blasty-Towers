using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyController : MonoBehaviour {

    [SerializeField][Tooltip("Sets enemy health. Defaults to 100.")] int health = 100;
    [SerializeField] [Tooltip("Enemy Hit particle effects")] ParticleSystem enemyHitFX;
    [SerializeField] [Tooltip("Enemy Die particle effects")] ParticleSystem enemyDieFX;


    EnemyState state;

	// Use this for initialization
	void Start ()
    {
        // Get instance of pathfinder script and store the path
        Pathfinder pathfinder = FindObjectOfType<Pathfinder>();
        List<Tile> path = pathfinder.GetPath();
        StartCoroutine(FollowPath(path));

        // The enemy starts out alive
        state = EnemyState.ALIVE;
    }

    // Update is called once per frame
    void Update () {
        // If enemy is dead, handle dying
		if (state == EnemyState.DEAD)
        {
            HandleDying();
        }
	}

    private void HandleDying()
    {
        Instantiate(enemyDieFX, gameObject.transform.position, Quaternion.identity, gameObject.transform);
        GetComponentInChildren<MeshRenderer>().enabled = false;
        Destroy(gameObject, 0.25f);
    }

    // Coroutine to show path
    // Print and then wait one second to continue
    IEnumerator FollowPath(List<Tile> path)
    {
        foreach (Tile tile in path)
        {
            yield return StartCoroutine(SmoothTransition(transform, tile.transform));
        }
    }

    IEnumerator SmoothTransition(Transform transform, Transform newTransform)
    {
        transform.position = Vector3.Lerp(transform.position, newTransform.position, 1f);
        yield return new WaitForSeconds(1);
    }

    private void OnParticleCollision(GameObject other)
    {
        TakeDamage();
    }

    private void TakeDamage()
    {
        health -= 5; // Take 5 damage
        if (health <= 0)
        {
            state = EnemyState.DEAD;
        }
        else
        {
            enemyHitFX.Play();
        }
    }
}

// State of the enemy
enum EnemyState { ALIVE, DEAD, ATTACKING }
