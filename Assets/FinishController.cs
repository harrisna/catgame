using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class FinishController : MonoBehaviour {

	//public Scene nextScene = null;

    [SerializeField] bool player1 = false;
    [SerializeField] bool player2 = false;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		if (player1 && player2) {
            // go to next scene
			/*if (nextScene != null)
				SceneManager.LoadScene(nextScene.name);
			else */
			SceneManager.LoadScene(SceneManager.GetActiveScene().name);	// just reset the level for now FIXME: make other scenes load
			
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
