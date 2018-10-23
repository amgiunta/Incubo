using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlagueDoctorAI : MonoBehaviour {

    public Transform target;
    public float pursueRange;
    public float distanceToTarget;

    public float meleeRange;
    float lungeHeight;

    private float lastMeleeTime;
    private float lastRangeTime;
    public float meleeDelay;
    float rangeDelay;

    public Transform projectSpawn;
    public GameObject projectile;
    public float rangedRange;
    float timeTillHit;

    Rigidbody2D rb;
    public float speed;

    // Use this for initialization
    void Start ()
    {
        target = GameObject.FindWithTag("Player").transform;
        rb = GetComponent<Rigidbody2D>();
	}
	
	// Update is called once per frame
	void Update ()
    {
        timeTillHit = Random.Range(1.0f, 2.0f);
        lungeHeight = Random.Range(1.0f, 2.0f);
        rangeDelay = Random.Range(2.0f, 3.0f);
        distanceToTarget = Vector3.Distance(transform.position,target.position);

        if (distanceToTarget < pursueRange  && distanceToTarget > meleeRange)
        {
            Pursue();
        }
        if (distanceToTarget < rangedRange && distanceToTarget > meleeRange)
        {
            Ranged();
        }
        if (distanceToTarget < meleeRange)
        {
            Melee();
        }
	}

    void Pursue()
    {
        if (distanceToTarget < pursueRange)
        {
            transform.position = Vector2.MoveTowards(transform.position, target.position, speed * Time.deltaTime);
        }
    }
    void Melee()
    {
        if (Time.time > lastMeleeTime + meleeDelay)
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
            lastMeleeTime = Time.time;
        }
    }
    void Ranged()
    {
        if (Time.time > lastRangeTime + rangeDelay)
        {
            //            Instantiate(projectile, projectSpawn.position, projectSpawn.rotation);
            //            lastAttackTime = Time.time;
            float xdistance;
            xdistance = target.position.x - projectSpawn.position.x;
            float ydistance;
            ydistance = target.position.y - projectSpawn.position.y;

            float arcAngle = Mathf.Atan((ydistance + 4.905f * (timeTillHit * timeTillHit)) / xdistance);

            float totalVelo = xdistance / (Mathf.Cos(arcAngle) * timeTillHit);
            float xVelo, yVelo;
            xVelo = totalVelo * Mathf.Cos(arcAngle);
            yVelo = totalVelo * Mathf.Sin(arcAngle);

            GameObject projectileInstance = Instantiate(projectile, projectSpawn.position, Quaternion.Euler(new Vector3(0, 0, 0))) as GameObject;
            Rigidbody2D rigid;
            rigid = projectileInstance.GetComponent<Rigidbody2D>();
            rigid.velocity = new Vector2(xVelo, yVelo);
            lastRangeTime = Time.time;

        }
    }
    void flip()
    {
        if (target.position.x > transform.position.x)
        {
            transform.localScale = new Vector3(3, 3, 1);
        }
        else if (target.position.x < transform.position.x)
        {
            transform.localScale = new Vector3(-3, 3, 1);
        }
    }
}
