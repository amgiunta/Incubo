using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    GameObject Player;
    public float movespeed;
    public float jumpheight;
    public float dashwait;
    public float dashspeed;
    private bool isgrounded = true;
    Rigidbody2D rb;
    // Use this for initialization
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
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
}
