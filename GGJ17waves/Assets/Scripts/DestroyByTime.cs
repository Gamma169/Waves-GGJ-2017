using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DestroyByTime : MonoBehaviour {

    public float lifetime;

    private float life;


	// Update is called once per frame
	void Update () {
        life += Time.deltaTime;

        if (lifetime < life)
        {
            DestroyObject(this.gameObject);
        }
	}
}
