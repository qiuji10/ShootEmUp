using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{
    public static bool isPaused = false;
    public int score;
    public int highscore;

    public GameObject pauseMenu, gameOver;
    public Text scoreText;
    public Text highscoreText;
    public Text healthText;

    private PlayerCore playerCore;
    private GameSceneManager gsm;

    private void Awake()
    {
        gsm = GetComponent<GameSceneManager>();
        playerCore = FindObjectOfType<PlayerCore>();
        highscore = PlayerPrefs.GetInt("Highscore");
        highscoreText.text = "Highscore: " + highscore.ToString();
        UpdateHealth();
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
        gsm.SwitchScene(0);
    }

    public void GameOver()
    {
        gameOver.SetActive(true);
    }

    public void UpdateHealth()
    {
        healthText.text = "Health: " + playerCore.Health.ToString();
    }
}
