using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DeathController : MonoBehaviour {

	[SerializeField] private GameObject dead;
	[SerializeField] private GameObject alive;

    private LifeController deadLC;
    private LifeController aliveLC;

	[SerializeField] private bool isAlive = true;

	// Use this for initialization
	void Start () {
        deadLC = dead.GetComponent<LifeController>();
        aliveLC = alive.GetComponent<LifeController>();

        deadLC.SetDC(this);
        aliveLC.SetDC(this);
    }
	
	// Update is called once per frame
	void Update () {
		alive.SetActive (isAlive);
		dead.SetActive (!isAlive);
	}

    public void Kill() {
        isAlive = !isAlive;
    }
}
