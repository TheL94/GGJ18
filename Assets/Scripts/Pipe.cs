using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pipe : MonoBehaviour {

    public float averageRandomRotations;
    public bool ccw;
    public SpriteRenderer rotatingArt;
    public float rotationDuration;

    private static Pipe firstRandomizer = null;
    private int randomizing;
    private bool rotating;
    private float rotationDegrees;
    private float DiscreteRotation
    {
        get { return (ccw ? +1 : -1) * 90f; }
    }

	// Use this for initialization
	void Start () {
        Randomize();
        rotationDegrees = 0;
    }
	
	// Update is called once per frame
	void Update () {
        if (Input.GetKeyDown(KeyCode.Alpha3))
        {
            Randomize();
        }

        if (randomizing > 0 && !rotating)
        {                        
            rotating = true;
            randomizing--;
            if (firstRandomizer == this)
            {
                Audio.Play(Audio.Sfx.Cog);                
                if (randomizing == 0)
                {
                    firstRandomizer = null;
                }
            }
        }

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

    private void Randomize()
    {
        if (firstRandomizer == null)
        {
            firstRandomizer = this;            
        }
        randomizing = Mathf.RoundToInt(UnityEngine.Random.Range(0f, averageRandomRotations * 2));
    }

    private void OnMouseDown()
    {
        rotating = true;
        Audio.Play(Audio.Sfx.Cog);        
    }
}
