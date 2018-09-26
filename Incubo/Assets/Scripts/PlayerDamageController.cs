using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerDamageController : Character {

    //Remove Comments from UI Stuff
    //public Slider healthBar;
    //public Text DeathText;

    public float fearTickTime = 1f;
    public int fearRestoreTick = 1;

    //float currentHealth;
    bool inFearZone;

	// Use this for initialization
	void Start () {
        currentFear = 0;
        inFearZone = false;
        InvokeRepeating("FearTicker", 0f, fearTickTime);
	}

    //Temp Code to test Enemy Death
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if(collision.gameObject.CompareTag("Enemy"))
        {
            Destroy(collision.gameObject);
        }
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
        else if(GameObject.FindGameObjectsWithTag("Enemy").Length == 0 && currentFear > 0)
        {
            if (currentFear - fearRestoreTick < 0)
            {
                int excessRestore = -(currentFear - fearRestoreTick);
                TakeDamage(-(fearRestoreTick - excessRestore));
            }
            else { TakeDamage(-fearRestoreTick); }
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
