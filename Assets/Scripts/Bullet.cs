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
	
	void Update () {
        t.Translate(0, Time.deltaTime * speed, 0, Space.Self);
	}

    void OnBecameInvisible()
    {
        Death death = GetComponent<Death>();
        if (death)
        {
            GetComponent<SpriteRenderer>().sortingOrder += 100;
            speed /= 5;
        }
        else
        {
            Debug.Log("Out of screen: destroyed");
            Destroy(gameObject);
        }
        
    }
}
