using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DoorController : MonoBehaviour, IPushable {

	[SerializeField] private Collider2D c;

	public void onActivate() {
		c.enabled = false;
	}

	public void onDeactivate() {
		c.enabled = true;
	}
}
