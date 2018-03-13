using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DeathController : MonoBehaviour {

	[SerializeField] private GameObject Dead;
	[SerializeField] private GameObject Alive;

	[SerializeField] private bool isAlive = true;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		Alive.SetActive (isAlive);
		Dead.SetActive (!isAlive);
	}
}
