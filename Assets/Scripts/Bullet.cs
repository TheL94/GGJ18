using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour {

    public float speed;

    private Pipe lastPipe;
    private Transform t;

    internal void SlowDown()
    {
        speed /= 5;
        Audio.Play(Audio.Sfx.Crash);
    }

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
