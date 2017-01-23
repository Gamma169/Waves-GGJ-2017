using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NearRail : MonoBehaviour {

	public GameObject ringSegmentPrefab;
	//public Ring ringPrefab;
	public Slider sliderPrefab;

	public GameObject destroyer;

	int totalRingPoints = 360;

	public float yPos = 1.5f;

	public float railRadius = 1.15f;


	// Use this for initialization
	void Start () {
		BuildRail();
	}
	
	void BuildRail() {

		for (int point = 0; point < totalRingPoints; point++) {
			//Vector3 position = GetPointOnArc (Vector3.zero, 180, railRadius, point, totalRingPoints);
			//GameObject ringSegment = Instantiate (ringSegmentPrefab, position, Quaternion.identity);
			//ringSegment.transform.parent = transform;
		}

		transform.Rotate(new Vector3(0, -90, 0));
		transform.position = new Vector3(0.5f, yPos, 0f);

		Slider slider = Instantiate (sliderPrefab);

		slider.destroyer = destroyer;

		slider.Init (this, 0);//-45f);

		/*
		Ring ring = Instantiate (ringPrefab);
		ring.Init (this, -45f);

		ring = Instantiate (ringPrefab);
		ring.Init (this, 45f);
	*/
	}

	private Vector3 GetPointOnArc ( Vector3 center, float arcLength, float radius, int nPoint, int totalPoints){

		if (totalPoints < 2) {
			Vector3 position = new Vector3 (center.x, center.y, center.z + radius); // HO-R must test
			return position;
		}

		float step = arcLength / (totalPoints /* - 1*/);
		float ang = step * nPoint - (arcLength / 2);// - 90;//(90 - arcLength / 2);// /*90*/50;

		Vector3 pos;
		pos.x = center.x + radius * Mathf.Sin(ang * Mathf.Deg2Rad);
		pos.y = center.y;// + radius * Mathf.Cos(ang * Mathf.Deg2Rad);
		pos.z = center.z + radius * Mathf.Cos(ang * Mathf.Deg2Rad);
		return pos;
	}

}
