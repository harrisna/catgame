using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class JumperController : MonoBehaviour {

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
	private Rigidbody2D rb;
	private bool jumping;
	private bool grounded;
	private float yPos;
	public AudioClip squishSound;
	private AudioSource source;
	
	// Use this for initialization
	void Start () {
		origin = transform.position.x;
		lastSpeed = speed;
		sp = GetComponent<SpriteRenderer>();
		rb = GetComponent<Rigidbody2D>();
		source = GetComponent<AudioSource>();
		jumping = false;
		grounded = true;
		yPos = transform.position.y;
	}
	
	void OnCollisionEnter2D(Collision2D col){
        
		
		if(col.gameObject.name == "Tilemap"){
			rb.velocity = new Vector2 (0.0f, -rb.velocity.y + 500.0f * Time.fixedDeltaTime);
			source.PlayOneShot(squishSound);
			if(yPos < 0f){
			Debug.Log("In Here");
			}
		}
		
	}
	
	void FixedUpdate()
    {
	//Ledge Checking
      RaycastHit2D ledgeRight = Physics2D.Raycast(new Vector2(transform.position.x+ledgeCheckerOffset,transform.position.y), Vector2.down, ledgeCheckerRaySize, groundMask);
	  RaycastHit2D ledgeLeft = Physics2D.Raycast(new Vector2(transform.position.x-ledgeCheckerOffset,transform.position.y), Vector2.down, ledgeCheckerRaySize, groundMask);
	  if (ledgeRight.collider == null) {
		  
            speed = -directionSpeed; //flip direction to left
			
      }else if (ledgeLeft.collider == null) {
		  
            speed = directionSpeed; //flip direction to  right
			
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
			//Check if it is facing left or right and make sure the image is facing that direction
			
			if(speed == -directionSpeed){
				sp.flipX=false;
			}else{
				sp.flipX = true;
			}
		//	if( yPos <0f){
			//Debug.Log("This is y " + transform.position.y + "This is ypos " + yPos);
	//}
        //if(transform.position.y > yPos){
		//	grounded = false;
		//}else{
		//	grounded = true;
		//}
			 
             //transform.Translate(speed*Time.deltaTime,0,0);
			 if(rb.velocity.y == 0f){
			 rb.velocity = new Vector2 (0.0f, -rb.velocity.y + 500.0f * Time.fixedDeltaTime);
			 }
			 
	}
}
