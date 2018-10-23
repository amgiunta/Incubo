using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TempPlayerControls : MonoBehaviour {
    /// <summary>
    /// The acceleration/deceleration of the object.
    /// </summary>
    public float acceleration;
    /// <summary>
    /// The maximum velocity that the player is allowed to travel at
    /// </summary>
    public float maxVelocity;
    /// <summary>
    /// The max force to be applied in the horizontal direction.
    /// </summary>
    public float speed;

    /// <summary>
    /// The rigidbody component on the GameObject
    /// </summary>
    private Rigidbody2D RB;

	// Use this for initialization
	void Start () {
        RB = GetComponent<Rigidbody2D>();
	}
	
	// Update is called once per frame
	void Update () {
        MoveHorizontal(Input.GetAxis("Horizontal"));
	}

    private void MoveHorizontal(float direction) {
        if (direction == 0) Decelerate();

        Vector2 newHorizontalVelocity = new Vector2(direction * speed, 0);
        RB.AddForce(newHorizontalVelocity);

        if (RB.velocity.magnitude > maxVelocity) {
            RB.velocity = RB.velocity.normalized;
            RB.velocity *= maxVelocity;
        }
    }

    private void Decelerate() {
        if (Mathf.Abs(RB.velocity.magnitude) <= acceleration)
        {
            RB.velocity = new Vector2(0, RB.velocity.y);
            return;
        }

        Vector2 direction = RB.velocity.normalized;

        RB.velocity = new Vector2(Mathf.Abs(RB.velocity.x) - acceleration, RB.velocity.y);
        RB.velocity *= direction;
    }
}
