using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ButtonCtrl : MonoBehaviour {

    public Button Start;
    public Button Quit;

    private void OnEnable()
    {
        Start.onClick.AddListener(CallGoToLevel);
        Quit.onClick.AddListener(CallQuit);
    }

    private void OnDisable()
    {
        Start.onClick.RemoveListener(CallGoToLevel);
        Quit.onClick.RemoveListener(CallQuit);
    }

    void CallGoToLevel()
    {
        GameManager.I.GoToLevel(1);
    }

    void CallQuit()
    {
        GameManager.I.Quit();
    }
}
