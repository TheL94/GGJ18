using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LethalObstacle : MonoBehaviour {

    public string[] victimTags;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    void OnTriggerEnter2D(Collider2D collider)
    {
        if (isActiveAndEnabled)
        {
            GameObject collidingObject = collider.gameObject;
            if (victimTags.Length == 0)
            {
                Kill(collidingObject);
            }
            else foreach (string victimTag in victimTags)
                {
                    if (collidingObject.tag == victimTag)
                    {
                        Kill(collidingObject);
                    }
                    return;
                }
        }
    }

    private void Kill(GameObject collidingObject)
    {
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
