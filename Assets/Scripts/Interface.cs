using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Interface : MonoBehaviour {

    [SerializeField] [Tooltip("Reference to the text component to update the ")] Text baseHealthText;
	
	// Update is called once per frame
	void Update () {
        int baseHealth = GameObject.FindGameObjectWithTag("FriendlyBase").GetComponent<Base>().GetBaseHealth();

        if (baseHealth <= 0)
        {
            baseHealthText.text = "BASE HEALTH: 0";
        }
        else
        {
            baseHealthText.text = "BASE HEALTH: " + baseHealth;
        }
	}
}
