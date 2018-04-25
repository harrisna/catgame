using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DeathController : MonoBehaviour {

	[SerializeField] private GameObject enemy;
	[SerializeField] private GameObject aliveBodyObject;
	[SerializeField] private GameObject deadBodyObject;
	[SerializeField] private Transform alivePos;
	[SerializeField] private Transform deadPos;

	private GameObject alive;
	private GameObject dead;
	private GameObject aliveBody;
	private GameObject deadBody;

	[SerializeField] private LifeController aliveLC;
    [SerializeField] private LifeController deadLC;

	[SerializeField] private bool isAlive = true;

	// Use this for initialization
	void Start () {
		alive = Instantiate (enemy, alivePos.position, alivePos.rotation);
		dead = Instantiate (enemy, deadPos.position, deadPos.rotation);
		aliveBody = Instantiate (aliveBodyObject, alivePos.position, alivePos.rotation);
		deadBody = Instantiate (deadBodyObject, deadPos.position, deadPos.rotation);
		aliveLC = alive.GetComponent<LifeController>();
        deadLC = dead.GetComponent<LifeController>();

		aliveLC.SetDC(this);
        deadLC.SetDC(this);
    }
	
	// Update is called once per frame
	void Update () {
		alive.SetActive (isAlive);
		dead.SetActive (!isAlive);
		aliveBody.SetActive (!isAlive);
		deadBody.SetActive (isAlive);
		
		if(isAlive){
			
		}
	}

    public void Kill() {
        isAlive = !isAlive;
		
    }

	void OnDrawGizmos() {
		Gizmos.color = Color.cyan;
		Gizmos.DrawSphere (alivePos.position, 1.0f);
		Gizmos.color = Color.red;
		Gizmos.DrawSphere (deadPos.position, 1.0f);
	}
}
