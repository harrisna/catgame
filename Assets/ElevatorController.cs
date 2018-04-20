using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ElevatorController : MonoBehaviour, IPushable {

	[SerializeField] Transform down;
	[SerializeField] Transform up;

	private float oldElapsed = 1.5f;
	private bool movingUp = false;

	private Coroutine c = null;

	public void onActivate() {
		if (c != null)
			StopCoroutine(c);

		movingUp = true;
		
		c = StartCoroutine(Move());
	}

	public void onDeactivate() {
		if (c != null)
			StopCoroutine(c);

		movingUp = false;

		c = StartCoroutine(Move());
	}

	private IEnumerator Move() {
		// Do animation step
		Vector3 startPosition = transform.position;
		Vector3 endPosition = movingUp ? up.position : down.position;

		float targetDuration = 1.5f - (1.5f - oldElapsed);
		float startTime = Time.time;
		//float elapsed = 0.0f + (1.5f - oldElapsed);
		float elapsed = 0.0f;

		while (elapsed <= targetDuration) {
			float proportion = elapsed / targetDuration;
			transform.position = Vector3.Lerp(startPosition, endPosition, proportion);
			yield return null;
			elapsed = Time.time - startTime;
			oldElapsed = elapsed;
		}

		transform.position = endPosition;
		oldElapsed = 1.5f;
	}
}
