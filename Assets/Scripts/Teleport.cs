using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Teleport : MonoBehaviour {

    public Transform destination;

	// Use this for initialization
	void Start () {
    }
	
	// Update is called once per frame
	void Update () {
		
	}

    void OnTriggerEnter2D(Collider2D collider)
    {
        if (collider.tag != "Bullet")
        {
            return;
        }
        Pipe thisPipe = this.GetComponentInParent<Pipe>();
        GameObject collidingObject = collider.gameObject;
        if (collidingObject.GetComponent<Bullet>().IsLastPipe(thisPipe))
        {
            return;
        }
        collidingObject.GetComponent<Bullet>().RememberLastPipe(thisPipe);
        collidingObject.transform.position = destination.position;
        collidingObject.transform.rotation = destination.rotation;
    }

}
