using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LifeCounter : MonoBehaviour {

	private int numLives;

	[SerializeField] private GameObject[] sprites;

	public void setLives(int n) {
		numLives = n;

		for (int i = 0; i < sprites.Length; i++) {
			sprites[i].SetActive((i < n));
		}
	}
}
