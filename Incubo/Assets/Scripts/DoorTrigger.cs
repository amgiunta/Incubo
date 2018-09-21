using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DoorTrigger : MonoBehaviour
{
    public float slidedistance;

    public PlayerController player;
    private bool closed = true;
    // Use this for initialization
    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerController>();
    }

    // Update is called once per frame
    void Update()
    {


    }
    public void open()
    {
        if (closed == true)
        {
            transform.position = new Vector2(transform.position.x, transform.position.y + slidedistance);
            closed = false;
        }
    }
    public void close()
    {
        if (closed == false)
        {
            transform.position = new Vector2(transform.position.x, transform.position.y - slidedistance);
            closed = true;
        }
    }
}