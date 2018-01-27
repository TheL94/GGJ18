using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour {

    public float speed;

    private Transform t;

	// Use this for initialization
	void Start () {
        t = transform;
	}
	
	// Update is called once per frame
	void FixedUpdate () {
        t.Translate(0, Time.fixedDeltaTime * speed, 0, Space.Self);
	}
}
