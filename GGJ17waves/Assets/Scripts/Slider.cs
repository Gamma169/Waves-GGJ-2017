using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Slider : MonoBehaviour {

	public Material originalMaterial;
	public Material collidedMaterial;
	public Material triggerHeldMaterial;

	public WaveForm waveForm;
	public NearRail rail;

	public GameObject destroyer;

	public float currentAngle = -80f;

	bool triggerHeld = false;
	bool inCollider = false;

	Renderer myRenderer;

	void Awake() {
		myRenderer = GetComponent<Renderer> ();
	}

	public void Init(NearRail rail, float startingAngle) {
		this.rail = rail;
		currentAngle = startingAngle;

		GetComponentInChildren<WaveForm> ().destroyer = this.destroyer;
	}

	// Update is called once per frame
	void Update () {
		//Debug.Log ("transform.parent.gameObject="+transform.parent.gameObject);

		SetDestroyerPosition (currentAngle);
		if (transform.parent == null) {
			if (Input.GetKey (KeyCode.RightArrow) && currentAngle < 90f) {
				currentAngle += 1.0f;
			} else if (Input.GetKey (KeyCode.LeftArrow) && currentAngle > -90f) {
				currentAngle -= 1.0f;
			}
			//Debug.Log ("center=" + rail.transform.position);
			Vector3 targetPosition = GetPointFromAngle (new Vector3 (rail.transform.position.x, rail.transform.position.y, rail.transform.position.z), rail.railRadius, currentAngle - 90);
			Quaternion rotation = Quaternion.Euler (0, currentAngle, 0);
			transform.position = targetPosition;
			transform.rotation = rotation;
		}

		Material material = originalMaterial;
		if (triggerHeld) {
			material = triggerHeldMaterial;
		} else if (inCollider) {
			material = collidedMaterial;
		}
		myRenderer.material = material;
	}

	private Vector3 GetPointFromAngle		 (Vector3 center, float radius, float ang, bool horizontal = true) {
		Vector3 pos;
		pos.x = center.x + radius * Mathf.Sin (ang * Mathf.Deg2Rad);
		pos.y = center.y + (horizontal ? 0 : radius * Mathf.Cos(ang * Mathf.Deg2Rad));
		pos.z = center.z + (horizontal ? radius * Mathf.Cos (ang * Mathf.Deg2Rad) : 0);

		return pos;
	}

	void OnTriggerStay (Collider other) {
		if (other.CompareTag ("HandController")) {

			inCollider = true;

			HandController handController = other.gameObject.GetComponent<HandController> ();
			handController.heldSlider = this;
		}
	}

	public void OnTriggerExit(Collider other) {
		inCollider = false;
	}

	public void AddToAngle(float angle) {
		if ((angle > 0 && currentAngle + angle < 88) || (angle < 0 && currentAngle + angle > -88)) {
			currentAngle += angle;
		}
	}

	public void Wave(bool on) {
		if (on) {
			//waveForm.gameObject.SetActive (true);
			waveForm.Wave (true);
		} else {
			waveForm.Wave (false);
			//waveForm.gameObject.SetActive (false);
		}
	}

	public void TriggerHeld(bool held) {
		triggerHeld = held;
		if (!triggerHeld) {
			waveForm.Wave (false);
		}
	}

	void SetDestroyerPosition(float angle) {
	
		//destroyer.transform.localPosition = Vector3.Lerp
		float ybla = 5f * (1f - Mathf.Abs(angle / 90f));
		MoveToPosition(angle * 2 * Mathf.PI / 360, 20, ybla);
		Quaternion rotation = Quaternion.Euler (0, currentAngle, 0);
		destroyer.transform.rotation = rotation;
	
	}

	public void MoveToPosition(float theta, float radius, float y) {
		destroyer.transform.localPosition = new Vector3(-radius * 2 * Mathf.Cos(theta), y, radius * Mathf.Sin(theta));
	}
}
