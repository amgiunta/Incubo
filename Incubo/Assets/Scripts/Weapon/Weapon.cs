using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// [9-21-19] Adam Giunta <amgiunta.2016@mymail.becker.edu>
/// <summary>
/// A general Weapon class to inherit from. Provides all basic weapon functionality.
/// </summary>
public class Weapon : MonoBehaviour {

    [Tooltip("The layers that this weapon will affect.")]
    /// <summary>
    /// The layers that this weapon can affect.
    /// </summary>
    public LayerMask layerMask;
    [Tooltip("Objects with any of these tags will be affeted by this weapon.")]
    /// <summary>
    /// Objects with any of these tags will be affected by this weapon.
    /// </summary>
    public List<string> tagMask;
    [Tooltip("A value used to modify the amount of damage this weapon yields based on on the character's base damage.")]
    /// <summary>
    /// A value used to modify the amount of damage this weapon yields based on the user character's base damage.
    /// </summary>
    public int damageMultiplier;
    [Tooltip("How far this weapon will project, or the force used to launch objects.")]
    /// <summary>
    /// The distance the weapon detects if melee or raycast projectile, and the force used to launch a physics based projectile.
    /// </summary>
    public float range;
    
    /// <summary>
    /// The character using this weapon.
    /// </summary>
    protected Character user;

    private void Awake()
    {
        user = GetComponentInParent<Character>();
    }

    /// <summary>
    /// Set the user of this weapon to a specific Character.
    /// </summary>
    /// <param name="user">The Character that should be using this weapon.</param>
    public void SetUser(Character user) { this.user = user; }

    /// <summary>
    /// Mutatable function to do weapon specific executions when attacking.
    /// </summary>
    public virtual void Attack() { }

    /// <summary>
    /// Called immediatly after an attack. Mutatable to do weapon specific actions.
    /// </summary>
    public virtual void OnAttack() { }
}
