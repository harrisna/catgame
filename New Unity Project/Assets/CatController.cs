using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CatController : MonoBehaviour {
	[SerializeField] private float moveSpeed = 10.0f;
	[SerializeField] private float jumpHeight = 10.0f;
	[SerializeField] private float maxSpeed = 10.0f;
	[SerializeField] private float friction = 0.8f;

	Rigidbody2D rb;
	bool isJumping = false;

	// Use this for initialization
	void Start () {
		rb = GetComponent<Rigidbody2D>();
	}
	
	// Update is called once per frame
	void Update () {
		float moveDir = Input.GetAxis("Horizontal");
		Vector2 moveVec = new Vector2(moveSpeed * moveDir, 0.0f);

		if (rb.velocity.magnitude > maxSpeed || moveVec.x == 0.0f)
			moveVec += new Vector2(rb.velocity.x * -friction, 0.0f);

		if (Input.GetButton ("Jump") && !isJumping) {
			isJumping = true;
			moveVec += new Vector2(0.0f, jumpHeight);
		}

		rb.velocity += moveVec;
	}
}
