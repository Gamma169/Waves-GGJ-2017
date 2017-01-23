using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Blaster : MonoBehaviour {

	public int WaveType = 0;


	void OnTriggerStay (Collider col) {
		if (col.transform.tag == "Enemy") {
			col.gameObject.GetComponent<HpShrink>().isTargeted = true;
			col.gameObject.GetComponent<HpShrink> ().attackerWaveType = WaveType;
		}
	}

	void OnTriggerExit (Collider col) {
		if (col.transform.tag == "Enemy") {
			col.gameObject.GetComponent<HpShrink>().isTargeted = false;
			col.gameObject.GetComponent<HpShrink> ().attackerWaveType = 0;
		}
	}
}
