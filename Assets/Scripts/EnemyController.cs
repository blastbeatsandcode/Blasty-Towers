﻿using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyController : MonoBehaviour {

    [SerializeField][Tooltip("Sets enemy health. Defaults to 100.")] int health = 100;
    [SerializeField][Tooltip("Sets damage enemy deals to the base. Defaults to 10.")] int damage = 10;
    [SerializeField] [Tooltip("Enemy Hit particle effects")] ParticleSystem enemyHitFX;
    [SerializeField] [Tooltip("Enemy Die particle effects")] ParticleSystem enemyDieFX;
    [SerializeField] [Tooltip("Enemy reached base sound effects.")] AudioClip enemyHitBaseFX;
    [SerializeField] [Tooltip("Enemy takes damage sound effects.")] AudioClip takeDamageFX;
    [SerializeField] [Tooltip("Enemy dies sound effects.")] AudioClip enemyDeathSFX;

    bool hasDied = false;


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
		if (state == EnemyState.DEAD && !hasDied)
        {
            HandleDying();
            hasDied = true;
        }
	}

    private void HandleDying()
    {
        Instantiate(enemyDieFX, gameObject.transform.position, Quaternion.identity, gameObject.transform);
        GetComponentInChildren<MeshRenderer>().enabled = false;
        Destroy(gameObject, 0.25f);
        AudioSource.PlayClipAtPoint(enemyDeathSFX, Camera.main.transform.position);
    }

    private void ExplodeAtEnd()
    {
        GetComponent<AudioSource>().PlayOneShot(enemyHitBaseFX);
        Instantiate(enemyDieFX, gameObject.transform.position, Quaternion.identity, gameObject.transform);
        GetComponentInChildren<MeshRenderer>().enabled = false;
    }

    // Coroutine to show path
    // Print and then wait one second to continue
    IEnumerator FollowPath(List<Tile> path)
    {
        foreach (Tile tile in path)
        {
            yield return StartCoroutine(SmoothTransition(transform, tile.transform));
        }

        // Do damage to base

        ExplodeAtEnd();
    }

    IEnumerator SmoothTransition(Transform transform, Transform newTransform)
    {
        transform.position = Vector3.Lerp(transform.position, newTransform.position, 1f);
        yield return new WaitForSeconds(0.5f);
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
        GetComponent<AudioSource>().PlayOneShot(takeDamageFX);
    }


    public int GetDamage()
    {
        return this.damage;
    }
}

// State of the enemy
enum EnemyState { ALIVE, DEAD, ATTACKING }
