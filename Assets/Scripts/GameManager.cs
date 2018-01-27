using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour {

    public static GameManager I;

    public Character character;

    private void Awake()
    {
        // Singleton
        if (I == null)
            I = this;
        else
            DestroyImmediate(gameObject);
    }

    private void Start()
    {
        character = FindObjectOfType<Character>();
    }

    public void GameOver()
    {
        SceneManager.LoadScene("Start", LoadSceneMode.Single);
    }
}
