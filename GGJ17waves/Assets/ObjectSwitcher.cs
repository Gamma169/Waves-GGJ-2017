using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ObjectSwitcher : MonoBehaviour {

	public int state = 0;
	public GameObject[] Stuff;
	
	// Update is called once per frame
	void Update () {
		if (Input.anyKeyDown) {
			state += 1;
		}

		if (state == 2) {
			Stuff [0].SetActive (true);
		}
		if (state == 4) {
			Stuff [1].SetActive (true);
			Stuff [0].SetActive (false);
		}
		if (state == 6) {
			Stuff [2].SetActive (true);
			Stuff [0].SetActive (false);
			Stuff [1].SetActive (false);
		}
		if (state == 8) {
			SceneManager.LoadScene ("Game");
		}
	}
}
