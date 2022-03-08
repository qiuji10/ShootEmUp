using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ScoringSystem : MonoBehaviour
{
    public int score;
    public int highscore;
    public Text scoreText;
    public Text highscoreText;

    private void Awake()
    {
        highscore = PlayerPrefs.GetInt("Highscore");
        highscoreText.text = "Highscore: " + highscore.ToString();
    }

    private void Update()
    {
        if (score > highscore)
        {
            highscore = score;
            PlayerPrefs.SetInt("Highscore", highscore);
            highscoreText.text = "Highscore: " + highscore.ToString();
        }
    }
}
