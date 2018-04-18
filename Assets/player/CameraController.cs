using System.Collections;
using System.Collections.Generic;
using UnityEngine;
//Camera size should be 13
public class CameraController : MonoBehaviour {
	[SerializeField] private float yModifyer = 7f;
	[SerializeField] private float aliveXStaticPosition = -8.5f;
	[SerializeField] private float deadXStaticPosition = 15.5f;
	//public Rigidbody2D rb;
	public Transform playerTransform;

	// Use this for initialization
	void Start () {
		//offset
		
	}
	
	// Update is called once per frame
	void Update () {
		//Camera Movement
		Vector3 temp = playerTransform.position;
		temp.z = transform.position.z;
		//Y Modifier
		temp.y = temp.y + yModifyer;
		//Static x camera value
		if(playerTransform.name == "AlivePlayer"){
		temp.x = aliveXStaticPosition;
		}else{
			temp.x = deadXStaticPosition;
		}
		//
		transform.position = temp;
	}
}
