using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour {
	[SerializeField] private Vector3 offset;
	//public Rigidbody2D rb;
	public Transform playerTransform;

	// Use this for initialization
	void Start () {
		//offset
		offset = playerTransform.position - transform.position;
	}
	
	// Update is called once per frame
	void Update () {
		//Camera Movement
		Vector3 temp = playerTransform.position + offset;
		temp.z = transform.position.z;
		//Y Modifier
		temp.y = temp.y + 3;
		transform.position = temp;
	}
}
