using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// [9-21-18] Adam Giunta <amgiunta.2016@mymail.becker.edu>
/// <summary>
/// This class stores all necessary stat information, and gets stats from a character. Use as a base for different character types.
/// </summary>
[Serializable]
public class Character : MonoBehaviour {

    // [2018-09-25] Ben Shackman <bshackman@protonmail.com>
    // Altered Variable names to match fear Mechanics
    [Tooltip("The maximum health value for the character.")]
    /// <summary>
    /// The maximum health value of the character.
    /// </summary>
    public int maxFear;
    [Tooltip("The health value of the character at the current state.")]
    /// <summary>
    /// The health value of the character at the current state.
    /// </summary>
    // [2018-09-25] Ben Shackman <bshackman@protonmail.com>
    public int currentFear;
    [Tooltip("The base damage amount that the character can deal.")]
    /// <summary>
    /// The base damage amount that the character can deal.
    /// </summary>
    public int baseDamage;
    [Tooltip("A modifier variable for base damage when calculating damage amounts.")]
    /// <summary>
    /// A modifier variable for base damage when calculating damage amounts.
    /// </summary>
    public float damageMultiplier;

    /// <summary>
    /// A callback function used by the character script to run code after taking damage.
    /// </summary>
    public delegate void OnTakeDamage();
    /// <summary>
    /// A callback function used by the character script to run code after taking damage.
    /// </summary>
    public OnTakeDamage onTakeDamage;
    public FearStage fearStage;
    public float fearMultiplier = 1;

    /// <summary>
    /// Gets the damage that this character should deal based off the base damage, and in-class damage mutiplier.
    /// </summary>
    /// <returns>An integer value of the damage to deal.</returns>
    public virtual int GetDamage() {
        return (int) (baseDamage * damageMultiplier);
    }

    /// <summary>
    /// Gets the damage that this character should deal based off the base damage and a provided modifier.
    /// </summary>
    /// <param name="multiplier">Effects the amount of damage inflicted by this character by multiplying into the base damage.</param>
    /// <returns>An integer value of the damage to inflict by this character</returns>
    public virtual int GetDamage(float multiplier) {
        return (int) (baseDamage * multiplier);
    }

    /// <summary>
    /// Adds a value to the character's fear and kills them if their fear gets too high.
    /// </summary>
    /// <param name="damage">The amount of damage to inflict.</param>
    public virtual void TakeDamage(int damage) {
        // [2018-09-25] Ben Shackman <bshackman@protonmail.com>
        // Also changed death to require current fear to be >= maxFear
        currentFear += damage;
        if (currentFear >= maxFear) { Kill(); }
        // Added temp debug.log function to show health
        // Debug.Log(currentFear + "/" + maxFear);
    }

    /// <summary>
    /// Kills this character.
    /// </summary>
    public virtual void Kill() {
        Destroy(gameObject);
    }
}

[Serializable]
public class SerializableCharacter {
    public int maxFear;
    public int currentFear;
    public int baseDamage;
    public float damageMultiplier;
    public float fearMultiplier;

    public SerializableCharacter() { }

    public SerializableCharacter(Character character) {
        maxFear = character.maxFear;
        currentFear = character.currentFear;
        baseDamage = character.baseDamage;
        damageMultiplier = character.damageMultiplier;
        fearMultiplier = character.fearMultiplier;
    }

    public static implicit operator Character(SerializableCharacter s_char) {
        Character character = new Character();
        character.maxFear = s_char.maxFear;
        character.currentFear = s_char.currentFear;
        character.baseDamage = s_char.baseDamage;
        character.damageMultiplier = s_char.damageMultiplier;
        character.fearMultiplier = s_char.fearMultiplier;

        return character;
    }

    public static implicit operator SerializableCharacter(Character character) {
        SerializableCharacter s_char = new SerializableCharacter(character);
        return s_char;
    }
}
