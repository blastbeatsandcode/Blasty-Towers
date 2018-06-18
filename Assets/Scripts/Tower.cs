using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tower : MonoBehaviour {
    [SerializeField] Transform objectToPan;
    [SerializeField] Transform targetEnemy;

	// Update is called once per frame
	void Update () {
        // Look at enemy
        LookAtTarget(targetEnemy);
		
	}

    private void LookAtTarget(Transform targetEnemy)
    {
        objectToPan.LookAt(targetEnemy);
    }
}
