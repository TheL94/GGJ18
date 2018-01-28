using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour {

    public float speed;

    private Pipe lastPipe;
    private Transform t;
    private bool braking = false;

    internal void SlowDown()
    {
        braking = true;
        Audio.Play(Audio.Sfx.Crash);
    }

    // Use this for initialization
    void Start () {
        t = transform;
	}
	
	void Update () {
        if (braking)
        {
            speed *= 0.95f;
        }
        t.Translate(0, Time.deltaTime * speed, 0, Space.Self);
	}

    void OnBecameInvisible()
    {
        Death death = GetComponent<Death>();
        if (!death)
        {            
            Debug.Log("Out of screen: destroyed");
            Destroy(gameObject);
        }
        
    }

    public bool IsLastPipe(Pipe pipe)
    {
        return lastPipe == pipe;
    }

    public void RememberLastPipe(Pipe pipe)
    {
        lastPipe = pipe;
    }
}
