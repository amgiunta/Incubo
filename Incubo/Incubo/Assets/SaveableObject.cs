using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Character))]
public class SaveableObject : MonoBehaviour {

    Character character;

    private void Start()
    {
        character = GetComponent<Character>();
        GameManager.OnSave.AddListener(SaveObject);
    }

    public void SaveObject() {
        GameManager.gameManager.AddToSavedData(character);
        Debug.Log("This object (" + gameObject.name + ") was saved to the current save game.");
    }
}
