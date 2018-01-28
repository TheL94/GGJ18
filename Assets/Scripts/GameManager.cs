using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour {

    public static GameManager I;

    private void Awake()
    {
        // Singleton
        if (I == null)
            I = this;
        else
            DestroyImmediate(gameObject);

        DontDestroyOnLoad(gameObject);
    }

    public void GameOver()
    {
        SceneManager.LoadScene("Start", LoadSceneMode.Single);
    }
}
