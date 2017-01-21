using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WaveForm : MonoBehaviour {

	public const int numPoints = 40;

	public float singleFrequency = 1f;
	public float singleAmplitude = 1f;


	public GameObject wavePoint;

	private GameObject[] pointPositions;
	private float[] radiusWavesTime;
	private float[] amplitude;
	private float[] frequency;

	private bool startingWave;

	// Use this for initialization
	void Start () {

		pointPositions = new GameObject[numPoints];
		radiusWavesTime = new float[numPoints];
		amplitude = new float[numPoints];
		frequency = new float[numPoints];
		for (int i = 0; i < numPoints; i++) {
			GameObject point = (GameObject)Instantiate(wavePoint, new Vector3(0, 1, i * 0.3f), Quaternion.identity);
			point.transform.parent = this.transform;
			pointPositions[i] = point;
			radiusWavesTime[i] = (1 / (float)numPoints) * i;
			frequency[i] = singleFrequency;
			amplitude[i] = 0;//singleAmplitude;
		}



	}
	
	// Update is called once per frame
	void Update () {
		for (int i = 0; i < numPoints; i++) {
			radiusWavesTime[i] += Time.deltaTime * frequency[i];
			if (radiusWavesTime[i] > 1)
				radiusWavesTime[i] -= 1;

			//frequency[i] = 0.5f * i / numPoints;
			//amplitude[i] = 1f * i / numPoints;

			if (i > 0 && (amplitude[i] > amplitude[i-1]))
				amplitude[i] = (amplitude[i] + amplitude[i - 1]) / 2f;


			pointPositions[i].transform.localPosition = new Vector3(0, 1 + amplitude[i] * Mathf.Sin(radiusWavesTime[i] * 2 * Mathf.PI), pointPositions[i].transform.localPosition.z);
		}



		if (Input.GetKeyDown(KeyCode.Space) && !startingWave) {
			//for (int i = 0; i < numPoints; i++)
			//	radiusWavesTime[i] = 0;//(1 / (float)numPoints) * i;
			StartCoroutine("StartWave");
		}


		if (Input.GetKey(KeyCode.Space)) {
			amplitude[0] = 1;
		}
		else { 
			StopCoroutine("StartWave");
			startingWave = false;

			if (amplitude[0] > 0)
				amplitude[0] -= 0.05f;
			if (amplitude[0] <= 0)
				amplitude[0] = 0;
		}

	}

	private IEnumerator StartWave() {
		startingWave = true;
		for (int i = 0; i < numPoints; i++) {
			radiusWavesTime[i] = 0;
			amplitude[i] = 0;
		}
		for (int i = 1; i < numPoints; i++) {
			radiusWavesTime[i] = i / numPoints;
			amplitude[i] = 1;
			yield return new WaitForSeconds(1f / numPoints);
			//yield return null;
		}
		startingWave = false;
	}
}
