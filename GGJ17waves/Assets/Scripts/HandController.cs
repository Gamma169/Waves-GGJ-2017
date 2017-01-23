using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HandController : MonoBehaviour {

	private SteamVR_TrackedObject trackedObj;
	SteamVR_Controller.Device device;

	public Slider heldSlider = null;

	float timeOfLastReverse;
	int reversalsCount;
	float yAtLastReverse = 0;
	float previousY;
	bool up = true;

	public int dir = 1; // 1 is right -1 is left 0 is o motion;
	// Use this for initialization
	float prevZ;
	float yWhenLocked = 0;
	bool touchpadDown = false;

	private float maxY;
	private float minY;
	private bool  aboveMax;
	private bool belowMin;

	void Start() {
		trackedObj = GetComponent<SteamVR_TrackedObject> ();
		yAtLastReverse = transform.position.y;
		timeOfLastReverse = Time.time;
		reversalsCount = 0;
	}

	void Update() {
		device = SteamVR_Controller.Input ((int)trackedObj.index);

		float y = transform.position.y;

		if (touchpadDown) {
			if (y > yWhenLocked + 0.15f) {
				aboveMax = true;
			}
			if (y < yWhenLocked - 0.15f) {
				belowMin = true;
			} 

			if (aboveMax && belowMin)
				heldSlider.Wave (true);
			else
				heldSlider.Wave (false);
		}

		/*
		if ((up && y < previousY) || (!up && y > previousY)) {
			yAtLastReverse = previousY;
			up = !up;
			if (Mathf.Abs (y - previousY) > .2f) {
				// large enough wave
				float now = Time.time;
				if (now - timeOfLastReverse < 5f) {
					reversalsCount++;
					timeOfLastReverse = now;
				} else {
					reversalsCount = 0;
				}
			} else {
				reversalsCount = 0;
			}
		}
		*/

		previousY = y;		

		if (reversalsCount > 3 || device.GetPressDown (SteamVR_Controller.ButtonMask.Touchpad)) {
			Slider lastToucedSlider = GameController.instance.lastTouchedSlider;
			if (lastToucedSlider != null) {
				lastToucedSlider.Wave (true);
			}
		}

		if (device.GetPressDown (SteamVR_Controller.ButtonMask.Trigger)) {
			if (heldSlider != null) {
				heldSlider.TriggerHeld (true);
			}
		}

		if (device.GetPress (SteamVR_Controller.ButtonMask.Trigger) && !device.GetPressDown (SteamVR_Controller.ButtonMask.Touchpad)) {
			if (heldSlider != null && !touchpadDown) {
				device.TriggerHapticPulse (1000);
				heldSlider.AddToAngle (.5f * dir);
			}
		}

		if (device.GetPressUp (SteamVR_Controller.ButtonMask.Trigger)) {
			if (heldSlider != null) {
				touchpadDown = false;
				StopCoroutine ("ReadWaves");
				heldSlider.TriggerHeld(false);
				heldSlider = null;
			}
		}

		if (device.GetPressDown (SteamVR_Controller.ButtonMask.Touchpad)) {
			if (heldSlider != null) {
				yWhenLocked = transform.position.y;
				maxY = yWhenLocked + .1f;
				minY = yWhenLocked - 0.1f;
				touchpadDown = true;
				StartCoroutine ("ReadWaves");

				// will really check for up/down
				//heldSlider.Wave(true);
			}
		}

		//if (device.GetPress (SteamVR_Controller.ButtonMask.Touchpad)) {
		//}

		if (device.GetPressUp (SteamVR_Controller.ButtonMask.Touchpad)) {
			if (heldSlider != null) {
				heldSlider.Wave(false);
				touchpadDown = false;
			}
		}


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

	public void Vibrate() {
		device = SteamVR_Controller.Input ((int)trackedObj.index);
		device.TriggerHapticPulse (3000);
	}

	private IEnumerator ReadWaves() {
		if (aboveMax && belowMin)
			heldSlider.Wave (true);
		else
			heldSlider.Wave (false);
		aboveMax = false;
		belowMin = false;
		yield return new WaitForSeconds (0.3f);


	}
}
