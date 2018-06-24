using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Base : MonoBehaviour {
    [SerializeField][Tooltip("Health for the base. Defaults to 100")] int baseHealth = 100;


    private void OnTriggerEnter(Collider other)
    {
        // Deal damage to base
        int damage = other.GetComponentInParent<EnemyController>().GetDamage();
        baseHealth -= damage;

        print("damage: " + damage);

        if (baseHealth <= 0)
        {
            print("YOU DIED");
        }

        Destroy(other.transform.parent.gameObject, 0.75f);
    }
}
