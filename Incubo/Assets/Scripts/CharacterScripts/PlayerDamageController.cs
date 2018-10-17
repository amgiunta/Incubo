using System;
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
    [Tooltip("This is how much fear is restored per fear tick.")]
    public int fearRestoreTick = 1;
    [Tooltip("This controlls how far away from enemies the player must be to passively restore fear.")]
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
        //dialController = FindObjectOfType<DialScript>();
        dialController = DialScript.dialScript;
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
            //TESTCODE
            collision.gameObject.GetComponent<Enemy>().TakeDamage(GetDamage());
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
        //Creates a list of all enemies within fear range
        GameObject[] enemyList = GameObject.FindGameObjectsWithTag("Enemy");
        List<GameObject> enemyWithinRangeList = new List<GameObject>();
        if (enemyList.Length != 0)
        {
            foreach(GameObject enemy in enemyList)
            {
                if((Vector3.Distance(enemy.transform.position, transform.position) <= enemy.GetComponent<Enemy>().fearRange))
                {
                    enemyWithinRangeList.Add(enemy);
                }
            }
        }

        //Fear Tick Code
        if(inFearZone)
        {
            //Note: Change to variable when avalable
            if(currentFear < maxFear/2) { TakeDamage(1); }
            inFearZone = false;
        }

        
        //Debug.Log(dist);
        if (enemyWithinRangeList.Count == 0)
        {
            if(currentFear > maxFear*.3f)
            {
                float excessRestore = 0;
                if (currentFear - fearRestoreTick < maxFear * .3f) { excessRestore = -((currentFear - fearRestoreTick) - maxFear * .3f); }
                ReduceFear(fearRestoreTick - (int)excessRestore);
            }
        }
        else
        {
            foreach(GameObject enemy in enemyWithinRangeList)
            {
                if (currentFear < maxFear / 2) { TakeDamage(enemy.GetComponent<Enemy>().fearTicks); }
            }
        }
        //Debug.Log(fearStage);

        //Safezone Code
        if(inSafeZone)
        {
            ReduceFear(fearRestoreTick);
            inSafeZone = false;
        }
    }

    // Note, to override something in playerCharcter.cs when we make that class
    /// <summary>
    /// To be called when ever an enemy is killed.
    /// </summary>
    /// <param name="restoreValue"></param>
    public void OnEnemyKill(int restoreValue)
    {
        ReduceFear(restoreValue);
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
                fearMultiplier = 1;
                break;
            case FearStage.Scared:
                fearMultiplier = scaredMoveMod;
                break;
            case FearStage.Terrified:
                fearMultiplier = terrifiedMoveMod;
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
