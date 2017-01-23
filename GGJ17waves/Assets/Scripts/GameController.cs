using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameController : MonoBehaviour {

	public static GameController instance;

	public Slider lastTouchedSlider;

	void Awake()
	{
		Application.targetFrameRate = 90;
		Debug.Log ("Application.targetFrameRate=" + Application.targetFrameRate);
		if (instance == null) {
			instance = this;
		} else if (instance != this) {
			//Then destroy this. This enforces our singleton pattern, meaning there can only ever be one instance of a GameManager.
			Destroy (gameObject);
		}

		//Sets this to not be destroyed when reloading scene
		DontDestroyOnLoad (gameObject);
	}

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
