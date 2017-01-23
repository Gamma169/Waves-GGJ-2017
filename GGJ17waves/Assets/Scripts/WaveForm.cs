using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WaveForm : MonoBehaviour {

	public const int numPoints = 40;
	public const float maxAmplitude = 4.5f;


	public GameObject wavePoint;

	public int type = 0;

	public const float singleFrequency = 2.5f;

	public GameObject destroyer;

	private GameObject[] pointPositions;
	private float[] radiusWavesTime;
	private float[] amplitude;
	private float[] frequency;


	private Material[] pointMats;
	private Color activeColor;

	private bool startingWave;

	private bool waving;
	private bool firstWave;


	private BoxCollider bc;

	// Use this for initialization
	void Start () {

		pointPositions = new GameObject[numPoints];
		radiusWavesTime = new float[numPoints];
		amplitude = new float[numPoints];
		frequency = new float[numPoints];
		pointMats = new Material[numPoints];
		for (int i = 0; i < numPoints; i++) {

			Vector3 pos = new Vector3(-i*0.5f, transform.position.y, 0);
			//Vector3 pos = new Vector3(transform.position.x, 0, i * 0.5f);

			GameObject point = (GameObject)Instantiate(wavePoint, pos, Quaternion.identity);
			point.transform.parent = this.transform;
			pointPositions[i] = point;
			pointMats[i] = point.GetComponent<Renderer>().material;
			activeColor = pointMats[i].color;
			radiusWavesTime[i] = (1 / (float)numPoints) * i;
			frequency[i] = singleFrequency;
			amplitude[i] = 0;
		}

		bc = destroyer.GetComponent<BoxCollider> ();

	}
	
	// Update is called once per frame
	void Update () {

		/*
		for (int i = 0; i < numPoints; i++) {
			radiusWavesTime[i] += Time.deltaTime * frequency[i];
			if (radiusWavesTime[i] > 1)
				radiusWavesTime[i] -= 1;

			//frequency[i] = 0.5f * i / numPoints;
			//amplitude[i] = 1f * i / numPoints;

			if (i > 0 && (amplitude[i] > amplitude[i-1]))
				amplitude[i] = (amplitude[i] + amplitude[i - 1]) / 2f;

			if (type == 0)
				pointPositions[i].transform.localPosition = new Vector3(0, amplitude[i] * Mathf.Sin(radiusWavesTime[i] * 2 * Mathf.PI), pointPositions[i].transform.localPosition.z);
			else if (type == 1) {  // Square Wave
				float ypos = 0;
				if (Mathf.Sin(radiusWavesTime[i] * 2 * Mathf.PI) > 0f)
					ypos = 1;
				else if (Mathf.Sin(radiusWavesTime[i] * 2 * Mathf.PI) <= 0f)
					ypos = -1;

				pointPositions[i].transform.localPosition = new Vector3(0, amplitude[i] * ypos, pointPositions[i].transform.localPosition.z);
			}
			else if (type == 2) {
				
				pointPositions[i].transform.localPosition = new Vector3(0, amplitude[i] * SawWave(radiusWavesTime[i]), pointPositions[i].transform.localPosition.z);
			}

		}
		*/


		for (int i = 0; i < numPoints; i++) {
			radiusWavesTime[i] += Time.deltaTime * frequency[i];
			if (radiusWavesTime[i] > 1)
				radiusWavesTime[i] -= 1;

			//frequency[i] = 0.5f * i / numPoints;
			//amplitude[i] = 1f * i / numPoints;

			if (i > 0 && (amplitude[i] > amplitude[i-1]))
				amplitude[i] = (amplitude[i] + amplitude[i - 1]) / 2f;

			pointMats[i].color = Color.Lerp(new Color(activeColor.r, activeColor.g, activeColor.b, 0.15f), activeColor, amplitude[i]/maxAmplitude);

			if (type == 0)
				pointPositions[i].transform.localPosition = new Vector3(pointPositions[i].transform.localPosition.x, amplitude[i] * Mathf.Sin(radiusWavesTime[i] * 2 * Mathf.PI), 0);
			else if (type == 1) {  // Square Wave
				float ypos = 0;
				if (Mathf.Sin(radiusWavesTime[i] * 2 * Mathf.PI) > 0f)
					ypos = 1;
				else if (Mathf.Sin(radiusWavesTime[i] * 2 * Mathf.PI) <= 0f)
					ypos = -1;

				pointPositions[i].transform.localPosition = new Vector3(pointPositions[i].transform.localPosition.x, amplitude[i] * ypos, 0);
			}
			else if (type == 2) {

				pointPositions[i].transform.localPosition = new Vector3(pointPositions[i].transform.localPosition.x, amplitude[i] * SawWave(radiusWavesTime[i]), 0);
			}

		}

		if (amplitude [numPoints - 1] / maxAmplitude > 0.5f)
			bc.enabled = true;
		else
			bc.enabled = false;

		if (waving && !firstWave && !startingWave) {
			//for (int i = 0; i < numPoints; i++)
			//	radiusWavesTime[i] = 0;//(1 / (float)numPoints) * i;
			StartCoroutine("StartWave");
			firstWave = true;
		}


		if (waving) {
			amplitude[0] = maxAmplitude;
		}
		else { 
			StopCoroutine("StartWave");
			startingWave = false;

			firstWave = false;

			if (amplitude[0] > 0)
				amplitude[0] -= 0.05f;
			if (amplitude[0] <= 0)
				amplitude[0] = 0;
		}
		/*
		if (Input.GetKey("s"))
			waving = true;
		else
			waving = false;
*/
	}

	private IEnumerator StartWave() {
		startingWave = true;
		for (int i = 0; i < numPoints; i++) {
			radiusWavesTime[i] = 0;
			amplitude[i] = 0;
		}
		for (int i = 1; i < numPoints; i++) {
			radiusWavesTime[i] = i / numPoints;
			amplitude[i] = maxAmplitude;
			yield return new WaitForSeconds(1f / numPoints);
			//yield return null;
		}
		startingWave = false;
	}


	private float SawWave(float t) {
		if (t > 1)
			t = t - Mathf.Floor(t);
		if (t < 0.25f)
			return t * 4f;
		else if (t<0.75f)
			return 2 - 4*t;
		else
			return -4 + 4*t;
	}

	public void Wave(bool turnOn) {
		waving = turnOn;
	}
}
