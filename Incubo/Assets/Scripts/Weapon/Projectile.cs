using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// [9-21-18] Adam Giunta <amgiunta.2016@mymail.becker.edu>
/// <summary>
/// A base class for a projectile that damages characters on contact. Derived from MelleeWeapon to gain access to InRange function.
/// </summary>
public class Projectile : MelleeWeapon {

    Character target;

    /// <summary>
    /// Damages the contacted character or inflicts damage on any characters within a range if that range is greater than 0.
    /// </summary>
    public override void Attack()
    {
        if (range > 0)
        {
            AOEAttack();
        }
        else {
            target.TakeDamage(user.GetDamage(damageMultiplier));
        }
    }

    /// <summary>
    /// Finds all valid Characters in range of the projectile's effect and inflicts damage on them.
    /// </summary>
    protected void AOEAttack() {
        List<Character> characters = InRange();

        foreach (Character character in characters) {
            character.TakeDamage(user.GetDamage(damageMultiplier));
        }
    }
    
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.GetComponent<Character>() && tagMask.Contains(collision.transform.tag))
        {
            target = collision.gameObject.GetComponent<Character>();
            Attack();
        }

        Destroy(gameObject);
    }
}
