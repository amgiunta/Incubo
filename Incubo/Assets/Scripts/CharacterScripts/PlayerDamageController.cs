using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

/**
 * Ben Shackman
 * Player Damage Controller Class
 * Handles the player's fear and fear state
 */

/// <summary>
/// The four stages of fear, Panicking is effectively dead.
/// </summary>
public enum FearStage { Fine, Scared, Terrified, Panicking };

//[RequireComponent(typeof(PlayerController))]
public class PlayerDamageController : Character {

    //Remove Comments from UI Stuff
    //public Slider healthBar;
    //public Text DeathText;
    
    public DialScript dialController;

    [Tooltip("This is the polling rate for fear. Note that all fear over time mechanics are tied to this.")]
    public float fearTickTime = 1f;
    public int fearRestoreTick = 1;
    public float enemyDistanceCheck = 10;

    //float currentHealth;
    bool inFearZone;
    bool inSafeZone;

    public float scaredMoveMod = 1.2f;
    public float terrifiedMoveMod = 1.4f;

    //PlayerController playerController;

	// Use this for initialization
	void Start () {
        currentFear = 0;
        inFearZone = false;
        inSafeZone = false;
        InvokeRepeating("FearTicker", 0f, fearTickTime);
        //playerController = GetComponent<PlayerController>();
	}

    public override void TakeDamage(int damage)
    {
        base.TakeDamage(damage);
        float dialAngle = (((float)currentFear / (float)maxFear) * 180);
        dialController.SetDialAngle(dialAngle);
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

        if(collision.CompareTag("SafeZone"))
        {
            inSafeZone = true;
        }
    }

    void FearTicker()
    {
        //Fear Tick Code
        if(inFearZone)
        {
            //Note: Change to variable when avalable
            if(currentFear < maxFear/2) { TakeDamage(1); }
            inFearZone = false;
        }

        //Reduces fear if outside range of all enemies
        float dist = enemyDistanceCheck + 1;
        GameObject[] enemyList = GameObject.FindGameObjectsWithTag("Enemy");
        if (enemyList.Length != 0)
        {
            foreach(GameObject enemy in enemyList)
            {
                float tempDist = Vector3.Distance(enemy.transform.position, transform.position);
                if(tempDist < dist) { dist = tempDist; }
            }
        }
        //Debug.Log(dist);
        if (dist > enemyDistanceCheck)
        {
            ReduceFear(fearRestoreTick);
        }
        Debug.Log(fearStage);

        //Safezone Code
        if(inSafeZone)
        {
            ReduceFear(fearRestoreTick);
            inSafeZone = false;
        }
    }

    /// <summary>
    /// Reduces fear by 'Rate' ammount. Use this function to avoid underflowing the value.
    /// </summary>
    /// <param name="rate"></param>
    void ReduceFear(int rate)
    {
        if (currentFear - rate < 0)
        {
            int excessRestore = -(currentFear - rate);
            TakeDamage(-(rate - excessRestore));
        }
        else { TakeDamage(-rate); }
    }


    // Update is called once per frame
    void Update ()
    {
        if(currentFear == maxFear) { fearStage = FearStage.Panicking; }
        else { fearStage = (FearStage)(currentFear / (maxFear / 3)); }

        switch(fearStage)
        {
            case FearStage.Fine:
                FearMultiplier = 1;
                break;
            case FearStage.Scared:
                FearMultiplier = scaredMoveMod;
                break;
            case FearStage.Terrified:
                FearMultiplier = terrifiedMoveMod;
                break;
        }

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
