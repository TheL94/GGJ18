﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Death : MonoBehaviour {

    private int[] blinks = new int[] { 10, 10, 10, 5, 5, 5, 5, 5, 5 };
    private int progressIndex = -1;
    private int progressFrames = 0;
    private bool on = true;
    private SpriteRenderer sr;

    // Use this for initialization
    void Start () {
        sr = GetComponent<SpriteRenderer>();
    }
	
	// Update is called once per frame
	void Update () {
		if (progressFrames <= 0)
        {
            on = !on;
            if (!on)
            {
                progressIndex++;
                if (progressIndex >= blinks.Length)
                {
                    Destroy(this.gameObject);
                    return;
                }                
            }
            progressFrames = blinks[progressIndex];
        }
        progressFrames--;
        sr.enabled = on;
	}
}