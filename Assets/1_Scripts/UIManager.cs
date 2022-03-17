using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{
    public static bool isPaused = false;
    public int score;
    public int highscore;

    public GameObject pauseMenu;
    public Text scoreText;
    public Text highscoreText;

    private void Awake()
    {
        highscore = PlayerPrefs.GetInt("Highscore");
        highscoreText.text = "Highscore: " + highscore.ToString();
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            if (isPaused == true)
            {
                ResumeGame();
            }
            else
            {
                PauseGame();
            }
        }

        if (score > highscore)
        {
            highscore = score;
            PlayerPrefs.SetInt("Highscore", highscore);
            highscoreText.text = "Highscore: " + highscore.ToString();
        }
    }

    public void PauseGame()
    {
        pauseMenu.SetActive(true);
        isPaused = true;
        Time.timeScale = 0f;
    }

    public void ResumeGame()
    {
        pauseMenu.SetActive(false);
        isPaused = false;
        Time.timeScale = 1f;
    }

    public void GoToMainMenu()
    {
        isPaused = false;
        Time.timeScale = 1f;
        gameObject.GetComponent<GameSceneManager>().SwitchScene(0);
    }

}
