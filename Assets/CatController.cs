using System.Collections;
using System.Collections.Generic;
using UnityEngine;

enum State { Grounded, Jumping, Falling, Wall };

public class CatController : MonoBehaviour {
	[SerializeField] private float moveAccel = 50.0f;
	[SerializeField] private float jumpPower = 1000.0f;
	[SerializeField] private float maxSpeed = 15.0f;
	[SerializeField] private float friction = 60.0f;
	[SerializeField] private float gravity = 25.0f;
	[SerializeField] private float jumpMult = 0.8f;	// movement multiplier while jumping
	[SerializeField] private float airControlMult = 0.4f;	// movement multiplier while falling
	[SerializeField] private float wallFriction = 0.5f;

	[SerializeField] private int jumpTime = 10;	// number of fixed updates a jump lasts for
    [SerializeField] private int attackTime = 10;

	[SerializeField] private Transform groundCheck;
    [SerializeField] private Transform wallCheckRight;
    [SerializeField] private Transform wallCheckLeft;
	[SerializeField] LayerMask groundMask;

    [SerializeField] private Collider2D attackHitboxRight;
	[SerializeField] private Collider2D attackHitboxLeft;

	Rigidbody2D rb;

	private Vector3 offset; // what is this? -nah

	bool grounded = false;
	bool wall = false;
    float groundRadius = 0.05f;
	float groundLen = 1.0f;
	float wallLen = 1.5f;

	bool hasJumped = false;	// prevent holding jump
	int jumpTimer = 0;

    bool isAttacking = false;
    bool hasAttacked = false;
    int attackTimer = 0;

	bool facingRight = false;

	State st = State.Falling;

	// Use this for initialization
	void Start () {
		rb = GetComponent<Rigidbody2D>();
		//Calculating camera offset
		offset = transform.position - rb.transform.position;
		
	}

	void FixedUpdate() {
		//Camera movement
		Vector3  temp = rb.transform.position +offset;
		temp.z = Camera.main.transform.position.z;
		temp.y = temp.y + 2;
		Camera.main.transform.position =   temp;
		//

		float moveDir = Input.GetAxis("Horizontal");
		facingRight = (moveDir > 0.0f);

		grounded = Physics2D.OverlapCapsule(groundCheck.position, new Vector2(groundRadius, groundLen), CapsuleDirection2D.Horizontal, 0.0f, groundMask.value);

		if (facingRight)
			wall = Physics2D.OverlapCapsule(wallCheckRight.position, new Vector2(groundRadius, wallLen), CapsuleDirection2D.Vertical, 0.0f, groundMask.value);
		else if (Mathf.Abs(moveDir) > 0.0f)
			wall = Physics2D.OverlapCapsule(wallCheckLeft.position, new Vector2(groundRadius, wallLen), CapsuleDirection2D.Vertical, 0.0f, groundMask.value);
		else
			wall = false;


		switch (st) {
		case State.Grounded:
			{
				if (!grounded)
					st = State.Falling;

				Vector2 moveVec = new Vector2 (0.0f, 0.0f);

				if (Input.GetButton ("Jump") && !hasJumped) {
					st = State.Jumping;
					jumpTimer = jumpTime;
					moveVec += new Vector2 (0.0f, jumpPower * Time.fixedDeltaTime);
					hasJumped = true;
				}

				rb.velocity += new Vector2 (0.0f, -gravity * Time.fixedDeltaTime);
				rb.velocity += moveVec;
			}
			break;
		case State.Jumping:
			{
				jumpTimer--;
				if (jumpTimer <= 0)
					st = State.Falling;
				if (wall)
					st = State.Wall;

				Vector2 moveVec = new Vector2 (0.0f, 0.0f);

				if (Input.GetButton ("Jump")/* && !hasJumped*/) {
					moveVec += new Vector2 (0.0f, -rb.velocity.y + jumpPower * Time.fixedDeltaTime);
					hasJumped = true;
				} else {
					st = State.Falling; // jump released; skip to falling state
				}

				rb.velocity += new Vector2 (0.0f, -gravity * Time.fixedDeltaTime);
				rb.velocity += moveVec;
			}
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

			if (rb.velocity.y < 0.0f)
            	rb.velocity *= wallFriction;
			break;
		}

        if (!isAttacking && hasAttacked && !Input.GetButton("Fire1"))
            hasAttacked = false;

        if (Input.GetButton("Fire1") && !hasAttacked && !isAttacking) {
			if (facingRight)
				attackHitboxRight.enabled = true;
			else
				attackHitboxLeft.enabled = true;
            attackTimer = attackTime;
            hasAttacked = true;
            isAttacking = true;
        } else if (isAttacking && attackTimer > 0) {
            attackTimer--;
        } else if (isAttacking) {
            attackHitboxRight.enabled = false;
			attackHitboxLeft.enabled = false;
            isAttacking = false;
        }
	}
	
	// Update is called once per frame
	void Update () {

		switch (st) {
		case State.Grounded:
			{
                if (hasJumped && !Input.GetButton ("Jump"))
			        hasJumped = false;

				float moveDir = Input.GetAxis("Horizontal");
				Vector2 moveVec = new Vector2((moveAccel * Time.deltaTime) * moveDir, 0.0f);

				if (moveVec.x == 0.0f && Mathf.Abs(rb.velocity.x) > (friction * Time.deltaTime))
					moveVec.x -= Mathf.Sign (rb.velocity.x) * (friction * Time.deltaTime);
				else if (moveVec.x == 0.0f)
					moveVec.x -= rb.velocity.x;

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

    void OnTriggerEnter2D(Collider2D other) {
        LifeController lc = other.GetComponent<LifeController>();
        if (lc) {
            lc.Kill();
        }
    }
}
