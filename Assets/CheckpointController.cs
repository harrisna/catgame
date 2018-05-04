using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CheckpointController : MonoBehaviour {

	[SerializeField] private Transform respawnPt;
	[SerializeField] private Sprite FlagUp;

	private bool used = false;
	private SpriteRenderer sp;

	// Use this for initialization
	void Start () {
		sp = GetComponent<SpriteRenderer>();
	}

	void OnTriggerEnter2D(Collider2D other){
		CatController c = other.GetComponent<CatController> ();
		if (c != null && !used) {
			c.SetSpawnPt(respawnPt.position);
			sp.sprite = FlagUp;
			used = true;
		}
	}
}
