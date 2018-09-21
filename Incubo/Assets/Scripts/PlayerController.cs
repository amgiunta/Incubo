using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour {
    GameObject Player;
    public float movespeed;
    public float jumpheight;
    public float dashwait;
    public float dashspeed;
    private bool isgrounded = true;
    public float slidespeed;
    public float doortime;
    private bool closed = true;
    private bool reclosed = true;
    public GameObject slidingDoor;
    Rigidbody2D rb;

    // Adam Giunta [9-20-18] <amgiunta.2016@mymail.becker.edu>
    public Weapon weapon;
    GameObject hand;

    // Use this for initialization
    void Start() {
        rb = GetComponent<Rigidbody2D>();
        hand = transform.Find("Hand").gameObject;
        weapon = hand.GetComponentInChildren<Weapon>();
    }

    // Adam Giunta [9-20-18] <amgiunta.2016@mymail.becker.edu>
    private void Update()
    {
        if (Input.GetButtonDown("Fire1")) { weapon.Attack(); }
    }

    // Update is called once per frame
    private void FixedUpdate()
    {
        if (Input.GetKey("a"))
        {
            transform.position = new Vector2(transform.position.x - movespeed, transform.position.y);
            /*could also do trasnform.position instead, design decision tho, that is more of a teleport*/
        }
        if (Input.GetKeyDown("space") && Input.GetKey("a") && isgrounded == false)
        {
            rb.AddForce(new Vector2(-dashspeed, 0));
            StartCoroutine(dashtime());
        }
        if (Input.GetKey("d"))
        {
            transform.position = new Vector2(transform.position.x + movespeed, transform.position.y);
        }
        if (Input.GetKeyDown("space") && Input.GetKey("d") && isgrounded == false)
        {
            rb.AddForce(new Vector2(dashspeed, 0));
            StartCoroutine(dashtime());
        }
        if (Input.GetKeyDown("w") && isgrounded == true)
            rb.AddForce(transform.up * jumpheight);

        if (closed == false)
        {
            StartCoroutine(DoorStop());

        }
        /*if (reclosed == false)
        {
            StartCoroutine(DoorReturn());
        }
    IEnumerator DoorReturn()
    {
        slidingDoor.transform.position = new Vector2(slidingDoor.transform.position.x, slidingDoor.transform.position.y - slidespeed);
        yield return new WaitForSeconds(doortime);
        slidingDoor.transform.position = new Vector2(slidingDoor.transform.position.x, slidingDoor.transform.position.y);
    }*/
    }
    IEnumerator DoorStop()
    {
        slidingDoor.transform.position = new Vector2(slidingDoor.transform.position.x, slidingDoor.transform.position.y + slidespeed);
        yield return new WaitForSeconds(doortime);
        slidingDoor.transform.position = new Vector2(slidingDoor.transform.position.x, slidingDoor.transform.position.y);
    }
    IEnumerator dashtime()
    {
        rb.constraints = RigidbodyConstraints2D.FreezePositionY;
        yield return new WaitForSeconds(dashwait);
        rb.constraints = RigidbodyConstraints2D.None;
    }
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.name == "Floor")
            isgrounded = true;
    }
    private void OnCollisionExit2D(Collision2D collision)
    {
            isgrounded = false;
    }
    private void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Switch" && Input.GetKey("s"))
            closed = false;
    }
    /*private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "ClosingSwitch")
            reclosed = false;
    }*/
}
