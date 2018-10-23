using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;


public class Spider : MonoBehaviour {

    public Collider2D detection;
    public bool isHostile;
    public float speed;
    public float maxSpeed;
    private GameObject activePlayer;
    private Rigidbody2D rb;


	// Use this for initialization
	void Start () {
        rb = GetComponent<Rigidbody2D>();
        activePlayer = GameObject.FindGameObjectWithTag("Player");
	}
	
	// Update is called once per frame
	void Update () {
        Vector2 heading = activePlayer.transform.position - transform.position;
        float direction = heading.normalized.x;

        if (isHostile == true)
        {
            rb.AddForce(new Vector2(direction, 0) * speed);
        }
        if(rb.velocity.magnitude>maxSpeed)
        {
            Vector2 newVelocity = new Vector2(direction * maxSpeed, rb.velocity.y);
            rb.velocity = newVelocity;
        }
	}

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.gameObject.CompareTag("Player"))
        {
            isHostile = true;
        }
    }

}
