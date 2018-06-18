using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tower : MonoBehaviour {
    [Header("Target Options")]
    [SerializeField][Tooltip("Part of the tower that will follow target.")] Transform objectToPan;
    [SerializeField][Tooltip("Target of the tower gun.")] Transform targetEnemy;
    [SerializeField] [Tooltip("The shoot range of the tower. An enemy must be within range for the tower to shoot at it.")] float range = 50f;

    ParticleSystem particleSystem;
    ParticleSystem.EmissionModule emission;

    private void Start()
    {
        particleSystem = GetComponentInChildren<ParticleSystem>();
        emission = particleSystem.emission;
        emission.enabled = false;
    }

    // Update is called once per frame
    void Update() {
        // Look at enemy if it exists and is within range
        float distance = Vector3.Distance(targetEnemy.transform.position, gameObject.transform.position);

        if (targetEnemy != null && distance <= range)
        {
            LookAtTarget(targetEnemy);
        }
        else
        {
            emission.enabled = false;
        }
	}

    private void LookAtTarget(Transform targetEnemy)
    {
        emission.enabled = true;
        objectToPan.LookAt(targetEnemy);
    }
}
