using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ring : MonoBehaviour {

	public GameObject ringSegmentPrefab;
	public NearRail rail;

	int totalRingPoints = 360;
	public float ringRadius = .2f;
	float currentAngle = 0f;

	float startingAngle = 0;
	Vector3 center = Vector3.zero;

	// Use this for initialization
	void Start () {
		//StartCoroutine (BuildRing());
		BuildRing();
		transform.Rotate (0, -90, 0);
		transform.Translate (0, 1.8f, 2);

		center.y = rail.transform.position.y;
	}

	public void Init(NearRail rail, float startingAngle) {
		this.rail = rail;
		currentAngle = startingAngle;
	}
		

	void Update() {

		if (Input.GetKey (KeyCode.RightArrow) && currentAngle < 90f) {
			currentAngle += 1.0f;
		} else if (Input.GetKey (KeyCode.LeftArrow) && currentAngle > -90f) {
			currentAngle -= 1.0f;
		}
		Vector3 targetPosition = GetPointFromAngle (new Vector3(0, rail.transform.position.y, 0), rail.railRadius, currentAngle - 90);
		Quaternion rotation = Quaternion.Euler (0, currentAngle, 0);
		transform.position = targetPosition;
		transform.rotation = rotation;
	}

	void BuildRing() {

		for (int point = 0; point < totalRingPoints; point++) {
			Vector3 position = GetPointOnArc (Vector3.zero, 360, ringRadius, point, totalRingPoints, false);
			GameObject ringSegment = Instantiate (ringSegmentPrefab, position, Quaternion.identity);
			ringSegment.transform.parent = transform;
		}
	}

	private Vector3 GetPointOnArc ( Vector3 center, float arcLength, float radius, int nPoint, int totalPoints, bool horizontal = true){

		if (totalPoints < 2) {
			Vector3 position = new Vector3 (center.x, center.y, center.z + radius); // HO-R must test
			return position;
		}

		float step = arcLength / (totalPoints /* - 1*/);
		float ang = step * nPoint - (arcLength / 2);// - 90;//(90 - arcLength / 2);// /*90*/50;

		Vector3 pos = GetPointFromAngle (center, radius, ang, horizontal);
//		pos.x = center.x + radius * Mathf.Sin(ang * Mathf.Deg2Rad);
//		pos.y = center.y;// + radius * Mathf.Cos(ang * Mathf.Deg2Rad);
//		pos.z = center.z + radius * Mathf.Cos(ang * Mathf.Deg2Rad);
		return pos;
	}

	private Vector3 GetPointFromAngle (Vector3 center, float radius, float ang, bool horizontal = true) {
		Vector3 pos;
		pos.x = center.x + radius * Mathf.Sin (ang * Mathf.Deg2Rad);
		pos.y = center.y + (horizontal ? 0 : radius * Mathf.Cos(ang * Mathf.Deg2Rad));
		pos.z = center.z + (horizontal ? radius * Mathf.Cos (ang * Mathf.Deg2Rad) : 0);

		return pos;
	}

}
