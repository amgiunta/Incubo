using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// [10-4-18] Adam Giunta <amgiunta.2016@mymail.becker.edu>
public class SpiderFangs : MelleeWeapon {
    //TODO: Deal more damage if that character is affraid of this type
    public override void Attack()
    {
        foreach (Character character in InRange()) {
            if (tagMask.Contains(character.gameObject.tag)) { character.TakeDamage(user.GetDamage()); }
        }
        base.Attack();
    }
}
