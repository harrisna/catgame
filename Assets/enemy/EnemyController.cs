using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyController : MonoBehaviour {
     float speed = 1.0f;
     float origin;
	 float distance =3.0f;
	 float directionSpeed = 1.0f;
	// Use this for initialization
	void Start () {
		origin = transform.position.x;
	}
	
	void OnCollisionEnter2D(Collision2D col){
        if(col.gameObject.name == "Player")
        {
            Destroy(col.gameObject);
        }
		
	}
	
	// Update is called once per frame
	void Update () {
             if(origin - transform.position.x > distance)
             {
                 speed = directionSpeed; //flip direction
             }
             else if(origin - transform.position.x < -distance)
             {
                 speed = -directionSpeed; //flip direction
             }
             //Debug.Log (origX + " - " + transform.position.x + " = " + (origX - transform.position.x));
             transform.Translate(speed*Time.deltaTime,0,0);
         
	}
}
