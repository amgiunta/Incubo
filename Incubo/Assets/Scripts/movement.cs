using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class movement : MonoBehaviour {

    [HideInInspector] public bool jump = false;
    public float moveForce = 365f;
    public float maxSpeed = 5f;
    public float jumpForce = 1000f;
    public Transform groundCheck;
    public Slider healthBar;
    public int currentHealth;
    private bool grounded = false;
    public int key;
    private Rigidbody2D rb2d;
    public Text keyCount;
    public Text winLose;

    // Use this for initialization
    void Awake()
    {
        key = 0;
        currentHealth = 2;
        healthBar.value = currentHealth;
        rb2d = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
        
        healthBar.value = currentHealth;
        textOnScreen();
        grounded = Physics2D.Linecast(transform.position, groundCheck.position, 1 << LayerMask.NameToLayer("Ground"));

        if (Input.GetButtonDown("Jump") && grounded)
        {
            jump = true;
        }
    }

    void FixedUpdate()
    {
        float h = Input.GetAxis("Horizontal");


        if (h * rb2d.velocity.x < maxSpeed)
            rb2d.AddForce(Vector2.right * h * moveForce);

        if (Mathf.Abs(rb2d.velocity.x) > maxSpeed)
            rb2d.velocity = new Vector2(Mathf.Sign(rb2d.velocity.x) * maxSpeed, rb2d.velocity.y);

        if (jump)
        { 
            rb2d.AddForce(new Vector2(0f, jumpForce));
            jump = false;
        }
    }
    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("Enemy"))
        {
            currentHealth -= 1;
            Destroy(other.gameObject);
        }
        if (other.gameObject.CompareTag("Collect"))
        {
            key += 1;
            Destroy(other.gameObject);
        }
    }
    void textOnScreen()
    {
        keyCount.text = "Key Count: " + key.ToString();
        if (currentHealth == 0)
        {
            winLose.text = "you lose";
        }
        if (key >= 1)
        {
            winLose.text = "you win";
        }
    }
}
