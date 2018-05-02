using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CheckpointController : MonoBehaviour {

	[SerializeField] private Transform respawnPt;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

	void OnCollisionEnter2D(Collision2D col){
		CatController c = col.gameObject.GetComponent<CatController> ();
		if (c != null) {
			c.SetSpawnPt(respawnPt.position);
		}
	}
}
