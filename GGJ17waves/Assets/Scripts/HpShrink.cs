using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HpShrink : MonoBehaviour {

	[Range(.4f, 1.5f)] public float healthPoints;
	public bool isTargeted;
	public int attackerWaveType;
	public int friendlyWaveType;
	float damageMultiplier;

	public bool hasFriendlyWave;

    public GameObject explosion;

    // Use this for initialization
    void Start () {
		healthPoints = 1f;
		//we'll adjust this to balance the game
		damageMultiplier = 1f;
	}
	
	// Update is called once per frame
	void Update () {
		//check current attack status
		if (isTargeted) {
				if (attackerWaveType == friendlyWaveType &&  hasFriendlyWave) {
				//increment health points
				healthPoints += Time.deltaTime * damageMultiplier;
			}
			else{
				//decrement health points
				healthPoints -= Time.deltaTime * damageMultiplier;
			}
            if (healthPoints > 1.5f)
            {
                healthPoints = 1.5f;
            }
			//before we go, fix scale to healthpoints
			transform.localScale = new Vector3 (healthPoints, healthPoints, healthPoints);

			if (healthPoints < .5f || Input.GetKeyDown(KeyCode.Alpha0)) {
				destroySelf ();
                //ADD A VICTORY POINT
			}
		}
	}

	public void destroySelf(){
        Instantiate(explosion, this.gameObject.transform.position, Quaternion.identity);
        Destroy(this.gameObject);
    }
}
