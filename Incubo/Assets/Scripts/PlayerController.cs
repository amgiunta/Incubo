using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class PlayerController : MonoBehaviour
{
    GameObject Characters;
    public float movespeed;
    public float jumpheight;
    //public float dashwait;
    //public float dashspeed;
    private bool isgrounded = true;
    Rigidbody2D rb;

    // Ben Shackman [2018-10-03] <bshackman@protonmail.com>
    //Temp Variable to access character.cs
    public Character characterScript;

    // Adam Giunta [9-20-18] <amgiunta.2016@mymail.becker.edu>
    public Weapon weapon;
    GameObject hand;

    // Use this for initialization
    void Start()
    {
        characterScript = GetComponent<Character>();
        rb = GetComponent<Rigidbody2D>();
        if (!transform.Find("Hand")) { hand = Instantiate<GameObject>(new GameObject("Hand"), transform); }
        else
            hand = transform.Find("Hand").gameObject;
        weapon = hand.GetComponentInChildren<Weapon>();
    }

    // Adam Giunta [9-20-18] <amgiunta.2016@mymail.becker.edu>
    private void Update()
    {
        if (Input.GetButtonDown("Cancel")) { MenuMaster.menuMaster.ToggleMenu(); }
        if (Input.GetButtonDown("Fire1")) { weapon.Attack(); }
    }

    // Update is called once per frame
    private void FixedUpdate()
    {
        if (Input.GetKey("a"))
        {
            transform.position = new Vector2(transform.position.x - movespeed * characterScript.fearMultiplier, transform.position.y);
            /*could also do trasnform.position instead, design decision tho, that is more of a teleport*/
        }
        /*if (Input.GetKeyDown("space") && Input.GetKey("a") && isgrounded == false)
        {
            rb.AddForce(new Vector2(-dashspeed, 0));
            StartCoroutine(dashtime());
        }*/
        if (Input.GetKey("d"))
        {
            transform.position = new Vector2(transform.position.x + movespeed * characterScript.fearMultiplier, transform.position.y);
        }
        /*if (Input.GetKeyDown("space") && Input.GetKey("d") && isgrounded == false)
        {
            rb.AddForce(new Vector2(dashspeed, 0));
            StartCoroutine(dashtime());
        }*/
        if (Input.GetKeyDown("w") && isgrounded == true)
            rb.AddForce(transform.up * jumpheight);

        // [10-5-18] Adam Giunta <amgiunta.2016@mymail.becker.edu> creating a pause button
        if (Input.GetButtonDown("Cancel")) {
            MenuMaster.menuMaster.ToggleMenu();
            MenuMaster.menuMaster.OpenMenu("Pause Screen");
        }
    }
    /*IEnumerator dashtime()
    {
        rb.constraints = RigidbodyConstraints2D.FreezePositionY;
        yield return new WaitForSeconds(dashwait);
        rb.constraints = RigidbodyConstraints2D.FreezePositionX;
        rb.constraints = RigidbodyConstraints2D.None;
    }*/
    private void OnCollisionStay2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Environment"))
            isgrounded = true;
    }
    private void OnCollisionExit2D(Collision2D collision)
    {
        isgrounded = false;
    }

}
