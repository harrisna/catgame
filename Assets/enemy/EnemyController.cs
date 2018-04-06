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
	[SerializeField] private float wallCheckerRaySize = 0.01f;
	// Use this for initialization
	void Start () {
		origin = transform.position.x;
		lastSpeed = speed;
	}
	
	void OnCollisionEnter2D(Collision2D col){
        if(col.gameObject.name == "Player")
        {
            Destroy(col.gameObject);
        }
		
	}
	
	void FixedUpdate()
    {
	//Ledge Checking
      RaycastHit2D ledgeRight = Physics2D.Raycast(new Vector2(transform.position.x+ledgeCheckerOffset,transform.position.y), Vector2.down, ledgeCheckerRaySize);
	  RaycastHit2D ledgeLeft = Physics2D.Raycast(new Vector2(transform.position.x-ledgeCheckerOffset,transform.position.y), Vector2.down, ledgeCheckerRaySize);
	  if (ledgeRight.collider == null) {
		  
            speed = -directionSpeed; //flip direction to left
      }else if (ledgeLeft.collider == null) {
		  
            speed = directionSpeed; //flip direction to  right
        }
		//Wall checking
		RaycastHit2D wallRight = Physics2D.Raycast(new Vector2(transform.position.x+wallCheckerOffset,transform.position.y), Vector2.right, wallCheckerRaySize);
		RaycastHit2D wallLeft = Physics2D.Raycast(new Vector2(transform.position.x-wallCheckerOffset,transform.position.y), Vector2.left, wallCheckerRaySize);
		
		if (wallRight.collider != null) {
		  
            speed = -directionSpeed; //flip direction to left
      }else if (wallLeft.collider != null) {
		  
            speed = directionSpeed; //flip direction to  right
        }
		
    }
	
	// Update is called once per frame
	void Update () {
			
        
			 
             transform.Translate(speed*Time.deltaTime,0,0);
         
	}
}
