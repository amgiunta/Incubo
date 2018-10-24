using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//Ben Shackman [bshackman@protonmail.com]

/// <summary>
/// This class inhertits from character and stores all nessassary information for enemies. Use as a base for all enemy types.
/// </summary>
public class Enemy : Character {

    [Tooltip("The range at which an ememy becomes agressive to the player")]
    /// <summary>
    /// The range at which an ememy becomes agressive to the player
    /// </summary>
    public int agroRange;

    [Tooltip("The range at which an ememy starts to passively raise the player's fear")]
    /// <summary>
    /// The range at which an ememy starts to passively raise the player's fear
    /// </summary>
    public int fearRange;

    [Tooltip("How much damage the player takes per fear tick from passive fear incrase range.")]
    /// <summary>
    /// How much damage the player takes per fear tick from passive fear incrase range.
    /// </summary>
    public int fearTicks;

    [Tooltip("The fear value which is restored to the player upon this enemies death.")]
    /// <summary>
    /// The fear value which is restored to the player upon this enemies death.
    /// </summary>
    public int restoreOnDeath;

    //Note, probobly needs a refactor
    public override void Kill()
    {
        GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerCharacter>().OnEnemyKill(restoreOnDeath);
        base.Kill();
    }
}
