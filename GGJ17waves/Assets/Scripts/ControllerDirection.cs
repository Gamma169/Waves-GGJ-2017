using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ControllerDirection : MonoBehaviour {

	public int dir = 1; // 1 is right -1 is left 0 is o motion;
	// Use this for initialization
	float prevZ;

	void Start () {
		prevZ = transform.position.z;
	}
	
	// Update is called once per frame
	void Update () {
		float z = transform.position.z;
		if (z < prevZ) {
			dir = -1;
		} else if (z > prevZ) {
			dir = 1;
		} else {
			dir = 0;
		}
		prevZ = z;
	}
}
