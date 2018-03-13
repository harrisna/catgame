using System.Collections;
using System.Collections.Generic;
using UnityEngine;

enum State { Grounded, Jumping, Falling, Wall };

public class CatController : MonoBehaviour {
	[SerializeField] private float moveAccel = 50.0f;
	[SerializeField] private float jumpPower = 1000.0f;
	[SerializeField] private float maxSpeed = 15.0f;
	[SerializeField] private float friction = 40.0f;
	[SerializeField] private float gravity = 25.0f;
	[SerializeField] private float jumpMult = 0.8f;	// movement multiplier while jumping
	[SerializeField] private float airControlMult = 0.4f;	// movement multiplier while falling
	[SerializeField] private float wallFriction = 0.5f;

	[SerializeField] private int jumpTime = 10;	// number of fixed updates a jump lasts for

	[SerializeField] private Transform groundCheck;
    [SerializeField] private Transform wallCheckRight;
    [SerializeField] private Transform wallCheckLeft;
	[SerializeField] LayerMask groundMask;

	Rigidbody2D rb;

	bool grounded = false;
	bool wall = false;
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
		float moveDir = Input.GetAxis("Horizontal");
        wall = Physics2D.OverlapCircle(wallCheckRight.position, groundRadius, groundMask.value) ||
            Physics2D.OverlapCircle(wallCheckLeft.position, groundRadius, groundMask.value);

		switch (st) {
		case State.Grounded:
			if (!grounded)
				st = State.Falling;
			rb.velocity += new Vector2(0.0f, -gravity * Time.fixedDeltaTime);
			break;
		case State.Jumping:
			jumpTimer--;
			if (jumpTimer <= 0)
				st = State.Falling;
			if (wall)
				st = State.Wall;
			rb.velocity += new Vector2(0.0f, -gravity * Time.fixedDeltaTime);
			break;
		case State.Falling:
			if (grounded)
				st = State.Grounded;
			if (wall)
				st = State.Wall;
			rb.velocity += new Vector2(0.0f, -gravity * Time.fixedDeltaTime);
			break;
		case State.Wall:
			if (grounded)
				st = State.Grounded;
            if (!wall)
                st = State.Falling;
			rb.velocity += new Vector2(0.0f, -gravity * Time.fixedDeltaTime);
            rb.velocity *= wallFriction;
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
				Vector2 moveVec = new Vector2((moveAccel * Time.deltaTime) * moveDir, 0.0f);

				if (moveVec.x == 0.0f && Mathf.Abs(rb.velocity.x) > (friction * Time.deltaTime))
					moveVec.x -= Mathf.Sign (rb.velocity.x) * (friction * Time.deltaTime);
				else if (moveVec.x == 0.0f)
					moveVec.x -= rb.velocity.x;

				if (Input.GetButton ("Jump") && !hasJumped) {
					st = State.Jumping;
					jumpTimer = jumpTime;
					moveVec += new Vector2(0.0f, jumpPower * Time.deltaTime);
					hasJumped = true;
				}

				rb.velocity += moveVec;

				Vector2 clampedVel = rb.velocity;
				clampedVel.x = Mathf.Clamp (rb.velocity.x, -maxSpeed, maxSpeed);

				rb.velocity = clampedVel;
			}
			break;
		case State.Jumping:
			{
				float moveDir = Input.GetAxis("Horizontal");
				Vector2 moveVec = new Vector2((moveAccel * Time.deltaTime) * moveDir * jumpMult, 0.0f);

				if (moveVec.x == 0.0f && Mathf.Abs(rb.velocity.x) > (friction * Time.deltaTime * jumpMult))
					moveVec.x -= Mathf.Sign (rb.velocity.x) * (friction * Time.deltaTime * jumpMult);
				else if (moveVec.x == 0.0f)
					moveVec.x -= rb.velocity.x;

				if (Input.GetButton ("Jump")/* && !hasJumped*/) {
					moveVec += new Vector2(0.0f, -rb.velocity.y + jumpPower * Time.deltaTime);
					hasJumped = true;
				} else {
                    st = State.Falling; // jump released; skip to falling state
                }

				rb.velocity += moveVec;

				Vector2 clampedVel = rb.velocity;
				clampedVel.x = Mathf.Clamp (rb.velocity.x, -maxSpeed, maxSpeed);

                rb.velocity = clampedVel;
			}
			break;
		case State.Falling:
			{
				float moveDir = Input.GetAxis("Horizontal");
				Vector2 moveVec = new Vector2((moveAccel * Time.deltaTime) * moveDir * airControlMult, 0.0f);

				if (moveVec.x == 0.0f && Mathf.Abs(rb.velocity.x) > (friction * Time.deltaTime * airControlMult))
					moveVec.x -= Mathf.Sign (rb.velocity.x) * (friction * Time.deltaTime * airControlMult);
				else if (moveVec.x == 0.0f)
					moveVec.x -= rb.velocity.x;

				rb.velocity += moveVec;

				Vector2 clampedVel = rb.velocity;
				clampedVel.x = Mathf.Clamp (rb.velocity.x, -maxSpeed, maxSpeed);

                rb.velocity = clampedVel;
			}
			break;
		case State.Wall:
			{
				float moveDir = Input.GetAxis("Horizontal");
				Vector2 moveVec = new Vector2((moveAccel * Time.deltaTime) * moveDir * airControlMult, 0.0f);

				if (moveVec.x == 0.0f && Mathf.Abs(rb.velocity.x) > (friction * Time.deltaTime * airControlMult))
					moveVec.x -= Mathf.Sign (rb.velocity.x) * (friction * Time.deltaTime * airControlMult);
				else if (moveVec.x == 0.0f)
					moveVec.x -= rb.velocity.x;

				rb.velocity += moveVec;

				Vector2 clampedVel = rb.velocity;
				clampedVel.x = Mathf.Clamp (rb.velocity.x, -maxSpeed, maxSpeed);

                rb.velocity = clampedVel;
			}
			break;
		}
	}
}
