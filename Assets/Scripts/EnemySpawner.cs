﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawner : MonoBehaviour {

    [SerializeField][Range(0.1f, 120f)] [Tooltip("The amount of time between enemy spawns.")] float secondsBetweenSpawns = 5f;
    [SerializeField] [Tooltip("Enemy to spawn.")] EnemyController enemy;
    [SerializeField] [Tooltip("Sound to play at enemy spawn.")] AudioClip spawnSound;

    // Use this for initialization
    void Start () {
        StartCoroutine(SpawnEnemy());
    }

    IEnumerator SpawnEnemy()
    {
        while (true)
        {
            Instantiate(enemy, transform.position, Quaternion.identity);
            GetComponent<AudioSource>().PlayOneShot(spawnSound);
            yield return new WaitForSeconds(secondsBetweenSpawns);
        }
    }

}
