using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// [9-21-18] Adam Giunta <amgiunta.2016@mymail.becker.edu>
/// <summary>
/// A base class that inherits from Weapon to do more projectile weapon specific actions.
/// </summary>
public class ProjectileWeapon : Weapon {

    /// <summary>
    /// An enumerator that has options specifying this type of projectile producer.
    /// </summary>
    public enum ProjectileType { Instant, Standard };
    [Tooltip("The type of projectile this weapon will produce.")]
    /// <summary>
    /// They type of projectile this weapon will produce.
    /// </summary>
    public ProjectileType projectileType;

    [Tooltip("The transform in which the projectile will be created and launched from.")]
    /// <summary>
    /// The transform in which the projectile will be create and launched from.
    /// </summary>
    public Transform muzzle;
    public GameObject projectilePrefab;

    /// <summary>
    /// Launches the projectile, or casts a ray to damage other characters.
    /// </summary>
    public override void Attack() {
        if (!canFire) { return; }
        if (projectileType == ProjectileType.Instant) { RaycastAttack(); }
        else if (projectileType == ProjectileType.Standard) { ProjectileAttack(); }
        base.Attack();
    }

    /// <summary>
    /// Looks for characters in the way of this weapon within range, and damages them if they are valid.
    /// </summary>
    protected void RaycastAttack() {
        RaycastHit2D hit = Physics2D.Raycast(muzzle.position, muzzle.forward, range, layerMask);
        Character target = null;

        if (hit)
        {
            if (hit.transform.gameObject.GetComponent<Character>() && tagMask.Contains(hit.transform.tag))
            {
                target = hit.transform.gameObject.GetComponent<Character>();
                target.TakeDamage(user.GetDamage(damageMultiplier));
            }
        }

        Debug.DrawRay(muzzle.position, muzzle.forward * range, Color.red, 1f);
    }
    
    /// <summary>
    /// Creates and launches a projectile to damage other valid characters in range. Uses range as a force scalar.
    /// </summary>
    private void ProjectileAttack() {
        GameObject projectileObj = Instantiate<GameObject>(projectilePrefab, muzzle.position, muzzle.rotation);
        Rigidbody2D projectileRB = projectileObj.GetComponent<Rigidbody2D>();
        Projectile projectile = projectileObj.GetComponent<Projectile>();
        projectile.InitializeProjectile(this);
        projectile.SetUser(user);

        projectileRB.AddForce(muzzle.forward * range);
    }
}
