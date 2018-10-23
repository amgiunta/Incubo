using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DamagerController : MonoBehaviour {

    //public PlayerDamageController damaged;
    public int damageDealt = 10;
    public float invulnTime = 1;
    bool invulnWindow;

    //For Solid Objects
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if(collision.gameObject.CompareTag("Player"))
        {
            //damaged.TakeDamage(damageDealt);
            if(invulnWindow == false)
            {
                collision.gameObject.GetComponent<PlayerDamageController>().TakeDamage(damageDealt);
                invulnWindow = true;
                StartCoroutine(playerInvulnWindow());
            }
        }
    }
    
    //For Triggers (Death pits, etc...)
    private void OnTriggerEnter2D(Collider2D collision)
    {
        Debug.Log("Yep");
        if (collision.gameObject.CompareTag("Player"))
        {
            //damaged.TakeDamage(damageDealt);
            if (invulnWindow == false)
            {
                collision.gameObject.GetComponent<PlayerDamageController>().TakeDamage(damageDealt);
                invulnWindow = true;
                StartCoroutine(playerInvulnWindow());
            }
        }
    }

    IEnumerator playerInvulnWindow()
    {
        yield return new WaitForSeconds(invulnTime);
        invulnWindow = false;
    }
}
