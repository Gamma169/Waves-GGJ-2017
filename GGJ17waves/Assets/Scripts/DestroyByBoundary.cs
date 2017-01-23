using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DestroyByBoundary : MonoBehaviour {

    public GameObject explosion;

    void OnTriggerEnter(Collider col)
    {
		if (col.transform.tag == "Castle") {
			Instantiate (explosion, this.gameObject.transform.position, Quaternion.identity);
			col.gameObject.GetComponent<CastleHealth> ().Health -= this.gameObject.GetComponent<HpShrink> ().healthPoints;
			Destroy (gameObject);
		}
    }
}
