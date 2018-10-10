using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Events;

// [10-6-18] Adam Giunta <amgiunta.2016@mymail.becker.edu>
/// <summary>
/// A custom wrapper for Unity's built in button object.
/// </summary>
public class CustomButton : Button {

    /// <summary>
    /// The code to be called when the button is clicked.
    /// </summary>
    public UnityAction clickEvent;

	// Use this for initialization
	protected override void Start () {
        onClick.AddListener(clickEvent);
	}
}
