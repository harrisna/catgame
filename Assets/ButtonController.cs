using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ButtonController : MonoBehaviour {

	[SerializeField] private GameObject pushable;
	[SerializeField] private Sprite ButtonUp;
	[SerializeField] private Sprite ButtonDown;

	private SpriteRenderer sp;

	// Use this for initialization
	void Start () {
		sp = GetComponent<SpriteRenderer>();
	}
	
	// Update is called once per frame
	void Update () {
		
	}

	void OnTriggerEnter2D(Collider2D other) {
		if (other.GetComponent<CatController>()) {
			pushable.GetComponent<IPushable>().onActivate();
			sp.sprite = ButtonDown;
		}
	}

	void OnTriggerExit2D(Collider2D other) {
		if (other.GetComponent<CatController>()) {
			pushable.GetComponent<IPushable>().onDeactivate();
			sp.sprite = ButtonUp;
		}
	}
}
