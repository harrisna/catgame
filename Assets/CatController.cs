using System.Collections;
using System.Collections.Generic;
using UnityEngine;

enum State { Grounded, Jumping, Falling };

public class CatController : MonoBehaviour {
	[SerializeField] private float moveAccel = 10.0f;
	[SerializeField] private float jumpHeight = 10.0f;
	[SerializeField] private float maxSpeed = 10.0f;
	[SerializeField] private float friction = 0.8f;
	[SerializeField] private float gravity = 25.0f;

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

		rb.velocity += new Vector2(0.0f, -gravity * Time.fixedDeltaTime);

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
				Vector2 moveVec = new Vector2(moveAccel * moveDir, 0.0f);

				if (moveVec.x == 0.0f && Mathf.Abs(rb.velocity.x) > (friction * Time.deltaTime))
					moveVec.x -= Mathf.Sign (rb.velocity.x) * (friction * Time.deltaTime);
				else if (moveVec.x == 0.0f)
					moveVec.x -= rb.velocity.x;

				if (Input.GetButton ("Jump") && !hasJumped) {
					st = State.Jumping;
					jumpTimer = jumpTime;
					moveVec += new Vector2(0.0f, jumpHeight * Time.deltaTime);
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
	}
}
