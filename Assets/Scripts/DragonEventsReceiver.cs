﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DragonEventsReceiver : MonoBehaviour {

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    private void StepEvent()
    {       
        // Non trovavo la chiamata a questa funzione, da correggere se hai voglia
        if(GetComponentInParent<Character>().currentAction != CharacterAction.Idle)
            Audio.Play(Audio.Sfx.Footstep);
    }
}
