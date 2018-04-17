using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DoorController : MonoBehaviour, IPushable {

	//[SerializeField] private GameObject door;

	public void onActivate() {
		gameObject.active = false;
	}

	public void onDeactivate() {
		gameObject.active = true;
	}
}
