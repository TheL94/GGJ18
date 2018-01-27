using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Teleport : MonoBehaviour {

    public Transform destination;

    private float insideRange;

	// Use this for initialization
	void Start () {
        Vector2 objectCenter = transform.position;
        Vector2 colliderCenter = GetComponent<Collider2D>().bounds.center;
        insideRange = (colliderCenter - objectCenter).magnitude;
    }
	
	// Update is called once per frame
	void Update () {
		
	}

    void OnTriggerEnter2D(Collider2D collider)
    {
        if ((collider.transform.position - transform.position).magnitude > insideRange)
        {
            Debug.Log("Colliding from outside: teleport!");
            GameObject collidingObject = collider.gameObject;
            collidingObject.transform.position = destination.position;
            collidingObject.transform.rotation = destination.rotation;            
        } 
        else
        {
            Debug.Log("Colliding from inside: ignore");
        }
    }

}
