using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pipe : MonoBehaviour {

    public bool ccw;
    public SpriteRenderer rotatingArt;
    public float rotationDuration;

    private bool rotating;
    private float rotationDegrees;
    private float DiscreteRotation
    {
        get { return (ccw ? +1 : -1) * 90f; }
    }

	// Use this for initialization
	void Start () {
        rotationDegrees = 0;
    }
	
	// Update is called once per frame
	void Update () {
		if (rotating)
        {
            float deltaDegrees = Time.deltaTime * (DiscreteRotation / rotationDuration);            
            rotationDegrees = rotationDegrees + deltaDegrees;
            if (Mathf.Abs(rotationDegrees) >= Mathf.Abs(DiscreteRotation))
            {
                transform.Rotate(0, 0, DiscreteRotation);
                rotationDegrees = 0;
                rotating = false;
            }
            rotatingArt.transform.localRotation = Quaternion.Euler(0, 0, rotationDegrees);
        }
	}

    private void OnMouseDown()
    {
        rotating = true;        
    }
}
