using System.Collections;
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
        // Called from animation
        CharacterAction action = GetComponentInParent<Character>().currentAction;
        if (action != CharacterAction.Idle && action != CharacterAction.Jump)
        {
            Audio.Play(Audio.Sfx.Footstep);
        }
    }

    private void WingEvent()
    {
        // Called from animation
        Audio.Play(Audio.Sfx.Wing);
    }
}
