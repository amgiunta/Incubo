using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// [9-21-18] Adam Giunta <amgiunta.2016@mymail.becker.edu>
/// <summary>
/// A base class inheriting from Weapon to do more mellee specific things. 
/// </summary>
public class MelleeWeapon : Weapon {

    /// <summary>
    /// Attack any valid character within a radius of the player.
    /// </summary>
    public override void Attack() { base.Attack(); }

    /// <summary>
    /// Get a list of all characters within a certain radius of this character.
    /// </summary>
    /// <returns>A list of valid characters.</returns>
    protected List<Character> InRange() {
        Collider2D[] objects = Physics2D.OverlapCircleAll(transform.position, range, layerMask);

        return CharFromHit(objects);
    }

    /// <summary>
    /// Convert an array of 2D colliders into a list of Characters
    /// </summary>
    /// <param name="objs">An array of 2D colliders to convert.</param>
    /// <returns>A list of valid Characters</returns>
    private List<Character> CharFromHit(Collider2D[] objs) {
        List<Character> characters = new List<Character>();

        foreach (Collider2D hit in objs) {
            if (hit.transform.GetComponent<Character>() && tagMask.Contains(hit.transform.tag)) {
                characters.Add(hit.transform.GetComponent<Character>());
            }
        }

        return characters;
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.green;

        Transform parent;
        if (transform.parent && transform.parent.parent) { parent = transform.parent.parent; }
        else { parent = transform; }
        Gizmos.DrawWireSphere(parent.position, range);
    }
}
