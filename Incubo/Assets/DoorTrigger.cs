using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DoorTrigger : MonoBehaviour {
    public float slidespeed;
    private bool closed = true;
    public GameObject slidingDoor;
	// Use this for initialization
	void Start () {
	    	
	}
	
	// Update is called once per frame
	void Update ()
    {
        if (closed == false)
            slidingDoor.transform.position = new Vector2(transform.position.x, transform.position.y + slidespeed);
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
            closed = false;
    }
}
