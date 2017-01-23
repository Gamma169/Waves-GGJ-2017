using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class CastleHealth : MonoBehaviour {

    public float Health;
    public GameObject damage1;
    public GameObject damage2;
    public GameObject damage3;

    private float startHealth;


    void Start ()
    {
        startHealth = Health;
    }
    // Update is called once per frame
    void Update () {
        if (Health < (startHealth * 0.75f))
        {
            damage1.SetActive(true);
        }
        if (Health < (startHealth * 0.5f))
        {
            damage2.SetActive(true);
        }
        if (Health < (startHealth * 0.25f))
        {
            damage3.SetActive(true);
        }
		if (Health <= 0f) {
			SceneManager.LoadScene ("Game Over");
		}
    }
}
