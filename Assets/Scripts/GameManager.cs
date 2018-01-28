using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public static GameManager I;
    public float TransitionTime;

    int currentLevel;
    bool isPlayingMusic = false;

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
        if (!isPlayingMusic)
        {
            isPlayingMusic = false;
            Audio.Play(Audio.Music.MenuTheme);
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
            Audio.Play(Audio.Music.MainTheme);
        }
        else if (_levelNumber == 2)
        {
            SceneManager.LoadScene("Level1", LoadSceneMode.Single);
            Audio.Play(Audio.Music.MainTheme);
        }
        else if (_levelNumber == 3)
        {
            SceneManager.LoadScene("GameOver", LoadSceneMode.Single);
            Audio.Play(Audio.Music.MainTheme);
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
        Audio.Play(Audio.Music.MenuTheme);
    }

    public void GoToGameOver()
    {
        SceneManager.LoadScene("GameOver", LoadSceneMode.Single);
        Audio.Play(Audio.Sfx.Lose);
        StartCoroutine(GoToMenuAfterGameOver());
    }

    public void Quit()
    {
        Application.Quit();
    }

    IEnumerator GoToMenuAfterGameOver()
    {
        yield return new WaitForSeconds(2.5f);
        GoToMenu();
    }
}
