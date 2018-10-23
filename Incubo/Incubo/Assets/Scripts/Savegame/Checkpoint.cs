using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
[RequireComponent(typeof(Collider2D))]
public class Checkpoint : MonoBehaviour {

    [Tooltip("How much fear is regenerated after every second?")]
    public float resurectionRate;

    Vector3 position;

    [NonSerialized]
    List<Character> charactersInRange;

	// Use this for initialization
	void Start () {
        GetComponent<Collider2D>().isTrigger = true;
        position = transform.position;
	}

    private void FixedUpdate()
    {
        foreach (Character character in charactersInRange) {
            character.currentFear -= (int) (resurectionRate * Time.fixedDeltaTime);
            if (character.currentFear < 0) { character.currentFear = 0; }
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player") && collision.GetComponent<Character>()) {
            charactersInRange.Add(collision.GetComponent<Character>());
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player") && collision.GetComponent<Character>()) {
            charactersInRange.Remove(collision.GetComponent<Character>());
        }
    }
}
