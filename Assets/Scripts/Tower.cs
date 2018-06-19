using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tower : MonoBehaviour {
    [Header("Target Options")]
    [SerializeField][Tooltip("Part of the tower that will follow target.")] Transform objectToPan;
    [SerializeField] [Tooltip("The shoot range of the tower. An enemy must be within range for the tower to shoot at it.")] float range = 50f;

    Transform targetEnemy;

    ParticleSystem particleSystem;
    ParticleSystem.EmissionModule emission;

    private void Start()
    {
        particleSystem = GetComponentInChildren<ParticleSystem>();
        this.emission = particleSystem.emission;
        this.emission.enabled = false;
    }

    // Update is called once per frame
    void Update() {
        SetTargetEnemy();

        // Look at enemy if it exists and is within range
        float distance = Vector3.Distance(targetEnemy.transform.position, gameObject.transform.position);

        if (targetEnemy != null && distance <= range)
        {
            LookAtTarget(targetEnemy);
        }
        else
        {
            this.emission.enabled = false;
        }
	}

    private void SetTargetEnemy()
    {
        EnemyController[] enemies = FindObjectsOfType<EnemyController>();
        if (enemies.Length == 0) return;

        Transform closestEnemy = enemies[0].transform;
        foreach (EnemyController enemyTransform in enemies)
        {
            closestEnemy = GetClosest(closestEnemy, enemyTransform.transform);
        }
        targetEnemy = closestEnemy;
    }

    private Transform GetClosest(Transform transformA, Transform transformB)
    {
        float distA = Vector3.Distance(transformA.position, gameObject.transform.position);
        float distB = Vector3.Distance(transformB.position, gameObject.transform.position);

        if (distA < distB)
        {
            return transformA;
        }

        return transformB;
    }

    private void LookAtTarget(Transform targetEnemy)
    {
        emission.enabled = true;
        objectToPan.LookAt(targetEnemy);
    }
}
