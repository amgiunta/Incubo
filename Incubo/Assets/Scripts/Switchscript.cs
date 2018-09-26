using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Switchscript : MonoBehaviour {
    public DoorTrigger trigger;

    private bool inrange = false;
	// Use this for initialization
	void Start () {

    }
	
	// Update is called once per frame
	void Update () {
        if (inrange == true && Input.GetKey("s"))
            trigger.open();	
	}

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Player")
            inrange = true;

    }
    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            inrange = false;
            trigger.close();
        }
    }
}
