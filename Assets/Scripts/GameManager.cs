using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public static GameManager I;
    public float TransitionTime;

    int currentLevel;

    private void Awake()
    {
        // Singleton
        if (I == null)
            I = this;
        else
            DestroyImmediate(gameObject);

        DontDestroyOnLoad(gameObject);
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
            Quit();
    }

    public void GoToLevel(int _levelNumber)
    {
        SceneManager.LoadScene(_levelNumber, LoadSceneMode.Single);
        currentLevel = _levelNumber;
    }

    public void GoToNextLevel()
    {
        GoToLevel(currentLevel + 1);
    }

    public void GoToMenu()
    {
        StartCoroutine(WaitToMenu());
    }

    public void Quit()
    {
        Application.Quit();
    }

    IEnumerator WaitToMenu()
    {
        yield return new WaitForSeconds(TransitionTime);
        SceneManager.LoadScene("MainMenu", LoadSceneMode.Single);
    }
}
