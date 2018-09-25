using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerDamageController : Character {

    //Remove Comments from UI Stuff
    //public Slider healthBar;
    //public Text DeathText;

    public float fearTickTime = 1f;

    //float currentHealth;
    bool inFearZone;

	// Use this for initialization
	void Start () {
        currentFear = 0;
        inFearZone = false;
        InvokeRepeating("FearTicker", 0f, fearTickTime);
	}

    private void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.CompareTag("FearZone"))
        {
            inFearZone = true;
        }
    }

    void FearTicker()
    {
        if(inFearZone)
        {
            //Note: Change to variable when avalable
            TakeDamage(1);
            inFearZone = false;
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
