using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerDamageController : MonoBehaviour {
    
    public float maxHealth = 100f;
    //Remove Comments from UI Stuff
    //public Slider healthBar;
    //public Text DeathText;

    float currentHealth;
    bool dead = false;

	// Use this for initialization
	void Start () {
        currentHealth = maxHealth;
	}

    //Called by Damagers
    public void TakeDamage(float damageDealt)
    {
        currentHealth -= damageDealt;
        Debug.Log("Health: " + currentHealth + "/" + maxHealth);
        //healthBar.value = currentHealth;

        if (currentHealth <= 0 && !dead)
        {
            //DeathText.gameObject.SetActive(true);
            //DeathText.text = "You Died";
            //Other Stuff Triggered by Death

            Debug.Log("You Died");
            dead = true;
            Time.timeScale = 0;
        }
    }
	
	// Update is called once per frame
	void Update () {

        //Testing Code
		if(Input.GetKeyDown("j"))
        {
            TakeDamage(10);
        }
        if(Input.GetKeyDown("k"))
        {
            TakeDamage(100);
        }
	}
}
