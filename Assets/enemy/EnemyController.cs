using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyController : MonoBehaviour {
	private float lastSpeed;
   [SerializeField] private  float speed = 1.0f;
    [SerializeField] private float origin;
	[SerializeField] private float distance =3.0f;
	[SerializeField] private float directionSpeed = 1.0f;
	// [SerializeField] private Transform wallCheckRight;
    //[SerializeField] private Transform wallCheckLeft;
	[SerializeField] private float ledgeCheckerOffset = 0.53125f;
	[SerializeField] private float ledgeCheckerRaySize = 2f;
	[SerializeField] private float wallCheckerOffset = 0.51f;
	[SerializeField] private float wallCheckerRaySize = 0.365f;
	[SerializeField] LayerMask groundMask;
	private SpriteRenderer sp;
	// Use this for initialization
	void Start () {
		origin = transform.position.x;
		lastSpeed = speed;
		sp = GetComponent<SpriteRenderer>();
	}
	
	void OnCollisionEnter2D(Collision2D col){
        if(col.gameObject.name == "AlivePlayer" || col.gameObject.name == "DeadPlayer")
        {
            Destroy(col.gameObject);
        }
		
	}
	
	void FixedUpdate()
    {
	//Ledge Checking
      RaycastHit2D ledgeRight = Physics2D.Raycast(new Vector2(transform.position.x+ledgeCheckerOffset,transform.position.y), Vector2.down, ledgeCheckerRaySize, groundMask);
	  RaycastHit2D ledgeLeft = Physics2D.Raycast(new Vector2(transform.position.x-ledgeCheckerOffset,transform.position.y), Vector2.down, ledgeCheckerRaySize, groundMask);
	  if (ledgeRight.collider == null) {
		  
            speed = -directionSpeed; //flip direction to left
			sp.flipX = false;
      }else if (ledgeLeft.collider == null) {
		  
            speed = directionSpeed; //flip direction to  right
			sp.flipX = true;
        }
		//Wall checking
		RaycastHit2D wallRight = Physics2D.Raycast(new Vector2(transform.position.x+wallCheckerOffset,transform.position.y), Vector2.right, wallCheckerRaySize, groundMask);
		//
		
		//
		RaycastHit2D wallLeft = Physics2D.Raycast(new Vector2(transform.position.x-wallCheckerOffset,transform.position.y), Vector2.left, wallCheckerRaySize, groundMask);
		//
		
       // Debug.DrawRay(new Vector2(transform.position.x-wallCheckerOffset,transform.position.y), Vector2.left, Color.green);
		//Debug.DrawRay(new Vector2(transform.position.x-ledgeCheckerOffset,transform.position.y), Vector2.down, Color.red);
		
		
		if (wallRight.collider != null) {
		  
            speed = -directionSpeed; //flip direction to left
			sp.flipX = false;
      }else if (wallLeft.collider != null) {
		  
            speed = directionSpeed; //flip direction to  right
			sp.flipX = true;
        }
		
    }
	
	// Update is called once per frame
	void Update () {
			
        
			 
             transform.Translate(speed*Time.deltaTime,0,0);
	}
}
