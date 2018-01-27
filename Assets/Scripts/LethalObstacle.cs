using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LethalObstacle : MonoBehaviour {

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    void OnTriggerEnter2D(Collider2D collider)
    {        
        GameObject collidingObject = collider.gameObject;        
        Death death = collidingObject.GetComponent<Death>();
        if (death)
        {
            //collidingObject.GetComponent<Collider2D>().enabled = false;
            collidingObject.GetComponent<Death>().enabled = true;
        }
        else
        {
            Debug.Log("Missing Death component: simply destroy " + collidingObject);
            Destroy(collidingObject);
        }
    }
}
