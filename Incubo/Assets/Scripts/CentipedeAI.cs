using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//Ben Shackman <2018-10-23> - Modified to work
public class CentipedeAI : Enemy {

    public Transform target;
    //public float pursueRange;
    public float distanceToTarget;

    public float lungeRange;
    float lungeHeight;

    private float lastLungeTime;
    private float lastBiteTime;
    public float lungeDelay;
    public float biteDelay;
    public float biteRange;
    public GameObject baby;
    Rigidbody2D rb;
    public float speed;
    public Transform spawnTwo;

    //public int Health = 1;
    // Use this for initialization
    void Start()
    {
        target = GameObject.FindWithTag("Player").transform;
        rb = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
        lungeHeight = Random.Range(1.0f, 2.0f);
        distanceToTarget = Vector3.Distance(transform.position, target.position);
        flip();
        if (distanceToTarget < agroRange && distanceToTarget > lungeRange && distanceToTarget > biteRange  )
        {
            Pursue();
        }
        if (distanceToTarget < lungeRange && distanceToTarget > biteRange)
        {
            Lunge();
        }
        if (distanceToTarget < biteRange)
        {
            Bite();
        }
        /*
        if (Health <= 0)
        {
            Death();
        }
        */
        
    }

    void Pursue()
    {
        if (distanceToTarget < agroRange)
        {
            transform.position = Vector2.MoveTowards(transform.position, target.position, speed * Time.deltaTime);
        }
    }
    void Lunge()
    {
        if (Time.time > lastLungeTime + lungeDelay)
        {
            float xdistance;
            xdistance = target.position.x - transform.position.x;
            float ydistance;
            ydistance = target.position.y - transform.position.y;

            float arcAngle = Mathf.Atan((ydistance + 4.905f * (lungeHeight * lungeHeight)) / xdistance);

            float totalVelo = xdistance / (Mathf.Cos(arcAngle) * lungeHeight);
            float xVelo, yVelo;
            xVelo = totalVelo * Mathf.Cos(arcAngle);
            yVelo = totalVelo * Mathf.Sin(arcAngle);


            rb.velocity = new Vector2(xVelo, yVelo);

            Debug.Log("hit");
            lastLungeTime = Time.time;
        }
    }
    void Bite()
    {
        if (Time.time > lastBiteTime + biteDelay)
        {
            Debug.Log("Hit");
            lastBiteTime = Time.time;
        }
    }
    void flip()
    {
        if (target.position.x > transform.position.x)
        {
            transform.localScale = new Vector3(3, 1, 1);
        }
        else if (target.position.x < transform.position.x)
        {
            transform.localScale = new Vector3(-3, 1, 1);
        }
    }
    /*
    void Death()
    {
        Instantiate(baby, transform.position, transform.rotation);
        Instantiate(baby, spawnTwo.position, transform.rotation);
        Destroy(this.gameObject);
    }
    */

    public override void Kill()
    {
        Instantiate(baby, transform.position, transform.rotation);
        Instantiate(baby, spawnTwo.position, transform.rotation);
        base.Kill();
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            this.TakeDamage(1);
        }
    }
}
