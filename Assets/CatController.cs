using System.Collections;
using System.Collections.Generic;
using UnityEngine;

enum State { Grounded, Jumping, Falling };

public class CatController : MonoBehaviour {
	[SerializeField] private float moveSpeed = 10.0f;
	[SerializeField] private float jumpHeight = 10.0f;
	[SerializeField] private float maxSpeed = 10.0f;
	[SerializeField] private float friction = 0.8f;

	[SerializeField] private int jumpTime = 5;	// number of fixed updates a jump lasts for

	[SerializeField] private Transform groundCheck;
	[SerializeField] LayerMask groundMask = 8;

	Rigidbody2D rb;

	bool grounded = false;
	float groundRadius = 0.1f;

	bool hasJumped = false;	// prevent holding jump
	int jumpTimer = 0;

	State st = State.Falling;

	// Use this for initialization
	void Start () {
		rb = GetComponent<Rigidbody2D>();
	}

	void FixedUpdate() {
		grounded = Physics2D.OverlapCircle(groundCheck.position, groundRadius, groundMask.value);

		switch (st) {
		case State.Grounded:
			if (!grounded)
				st = State.Falling;
			break;
		case State.Jumping:
			jumpTimer--;
			if (jumpTimer <= 0)
				st = State.Falling;
			break;
		case State.Falling:
			if (grounded)
				st = State.Grounded;
			break;
		}
	}
	
	// Update is called once per frame
	void Update () {
		if (hasJumped && !Input.GetButton ("Jump"))
			hasJumped = false;
		
		switch (st) {
		case State.Grounded:
			{
				float moveDir = Input.GetAxis("Horizontal");
				Vector2 moveVec = new Vector2(moveSpeed * moveDir, 0.0f);

				if (moveVec.x == 0.0f)
					moveVec += new Vector2(rb.velocity.x * -friction, 0.0f);

				if (Input.GetButton ("Jump") && !hasJumped) {
					st = State.Jumping;
					jumpTimer = jumpTime;
					moveVec += new Vector2(0.0f, jumpHeight);
					hasJumped = true;
				}

				rb.velocity += moveVec;

				Vector3 clampedVel = rb.velocity;
				clampedVel.x = Mathf.Clamp (rb.velocity.x, -maxSpeed, maxSpeed);

				rb.velocity = clampedVel;
			}
			break;
		case State.Jumping:
			break;
		case State.Falling:
			break;
		}

		Debug.Log (st);
		Debug.Log (grounded);
	}
}
