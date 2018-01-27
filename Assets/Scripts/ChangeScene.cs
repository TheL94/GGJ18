using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ChangeScene : MonoBehaviour {
    // Use this for initialization
    void Start () {
    }
    public void Change() {
        SceneManager.LoadScene("TutorialScene", LoadSceneMode.Single);
    }
    public void Quit() {
        Application.Quit();
    }
	// Update is called once per frame
	void Update () {
		
	}
}
