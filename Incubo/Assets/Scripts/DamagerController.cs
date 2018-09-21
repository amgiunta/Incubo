using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DamagerController : MonoBehaviour {

    //public PlayerDamageController damaged;
    public float damageDealt = 10;

    //For Solid Objects
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if(collision.gameObject.CompareTag("Player"))
        {
            //damaged.TakeDamage(damageDealt);
            collision.gameObject.GetComponent<PlayerDamageController>().TakeDamage(damageDealt);
        }
    }
    
    //For Triggers (Death pits, etc...)
    private void OnTriggerEnter2D(Collider2D collision)
    {
        Debug.Log("Yep");
        if (collision.gameObject.CompareTag("Player"))
        {
            //damaged.TakeDamage(damageDealt);
            collision.gameObject.GetComponent<PlayerDamageController>().TakeDamage(damageDealt);
        }
    }
}
