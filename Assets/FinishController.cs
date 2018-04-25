using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FinishController : MonoBehaviour {

    bool player1 = false;
    bool player2 = false;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		if (player1 && player2) {
            // go to next scene
        }
	}

    void OnTriggerEnter2D(Collider2D other) {
        CatController c = other.GetComponent<CatController>();

        if (c != null) {
            if (c.IsPlayer1()) {
                player1 = true;
            } else {
                player2 = true;
            }
        }
    }

    void OnTriggerExit2D(Collider2D other) {
        CatController c = other.GetComponent<CatController>();

        if (c != null) {
            if (c.IsPlayer1()) {
                player1 = false;
            } else {
                player2 = false;
            }
        }
    }
}
