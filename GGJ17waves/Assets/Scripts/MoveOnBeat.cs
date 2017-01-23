using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Reaktion;

public class MoveOnBeat : MonoBehaviour {

    public float timeToEnd = 10f;
    public float radius = 1f;

    public bool otherside;
    public float overshoot;

    private float m_BeatOver = 0.95f;

    private float position;
    private float timeLeftToEnd;
  
    Reaktor m_Reaktor;
    float _counter = 0;

    void Start() {
        timeToEnd = Random.Range(30f, 50f);
        position = 0;
        timeLeftToEnd = timeToEnd;
        StartCoroutine("MoveOnPath");
        m_Reaktor = FindObjectOfType<Reaktor>();
    }


    // Update is called once per frame
    void Update()
    {
        _counter -= Time.deltaTime;
        if (m_Reaktor && m_Reaktor.output >= m_BeatOver && _counter <= 0 || Input.GetKeyDown("l"))
        {
            //timeLeftToEnd -= .4f;
            // MoveOnPath();
            // _counter = 0.1f;
            StartCoroutine(Bump(0.35f, 0.25f));
            _counter = 0.1f;
        }
    }

   IEnumerator  MoveOnPath()
    {
        //gameObject.transform.Translate(Vector3.right);

        while (timeLeftToEnd > 0) {
            if (otherside)
                position = Mathf.Lerp(Mathf.PI, (Mathf.PI / 2) - overshoot, 1 - (timeLeftToEnd / timeToEnd));
            else
                position = Mathf.Lerp(0, (Mathf.PI / 2) + overshoot, 1- (timeLeftToEnd/timeToEnd));
            float y = Mathf.Lerp(0, 5, 1 - (timeLeftToEnd / timeToEnd));
            MoveToPosition(position, radius, y);
            timeLeftToEnd -= Time.deltaTime;
            yield return null;
       }
    }

    IEnumerator Bump(float amount, float overTime) {
        float addedTo = 0;
        while (addedTo < amount) {
            timeLeftToEnd -= 0.05f;
            addedTo += 0.05f;
            yield return new WaitForSeconds(0.02f);
        }
    }

    public void MoveToPosition(float theta, float radius, float y) {
        transform.localPosition = new Vector3(-radius * 2 * Mathf.Sin(theta), y, radius * Mathf.Cos(theta));
    }


}
