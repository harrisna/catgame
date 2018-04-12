using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DeathController : MonoBehaviour {

	[SerializeField] private GameObject enemy;

	[SerializeField] private Transform alivePos;
	[SerializeField] private Transform deadPos;

	private GameObject alive;
	private GameObject dead;

	[SerializeField] private LifeController aliveLC;
    [SerializeField] private LifeController deadLC;

	[SerializeField] private bool isAlive = true;

	// Use this for initialization
	void Start () {
		alive = Instantiate (enemy, alivePos.position, alivePos.rotation);
		dead = Instantiate (enemy, deadPos.position, deadPos.rotation);

		aliveLC = alive.GetComponent<LifeController>();
        deadLC = dead.GetComponent<LifeController>();

		aliveLC.SetDC(this);
        deadLC.SetDC(this);
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
