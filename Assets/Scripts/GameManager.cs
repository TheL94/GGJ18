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

        DontDestroyOnLoad(I.gameObject);
    }

    private void Update()
    {
        if (currentLevel == 0)
        {
            Audio.Play(Audio.Music.MenuTheme);
        }
        else
        {
            Audio.Play(Audio.Music.MainTheme);
        }
        if (Input.GetKeyDown(KeyCode.Escape))
            Quit();
    }

    public void GoToLevel(int _levelNumber)
    {
        currentLevel = _levelNumber;
        if (_levelNumber == 1)
        {
            SceneManager.LoadScene("TutorialScene", LoadSceneMode.Single);
        }
        else if (_levelNumber == 2)
        {
            SceneManager.LoadScene("Level1", LoadSceneMode.Single);
        }
        else
            GoToMenu();

    }

    public void GoToNextLevel()
    {
        GoToLevel(currentLevel + 1);
    }

    public void GoToMenu()
    {
        SceneManager.LoadScene("MainMenu", LoadSceneMode.Single);
    }

    public void Quit()
    {
        Application.Quit();
    }
}
