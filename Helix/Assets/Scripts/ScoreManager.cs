using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ScoreManager : MonoBehaviour
{
    public int Score = 0;
    public Text scoreText;
    public int Gold = 500;
    void Start()
    {
        scoreText = GameObject.FindGameObjectWithTag("score").GetComponent<Text>();
        if (!PlayerPrefs.HasKey("GOLD"))
        {
            PlayerPrefs.SetInt("GOLD", Gold);
        }
    }
    private void Update()
    {
        UpdateHighScore();
    }
    public void UpdateScoreUI()
    {
        scoreText.text = Score.ToString();
    }
    void UpdateHighScore()
    {
        if (!PlayerPrefs.HasKey("HIGHSCORE"))
        {
            PlayerPrefs.SetInt("HIGHSCORE", Score);
        }
        else if (Score > PlayerPrefs.GetInt("HIGHSCORE"))
        {
            PlayerPrefs.SetInt("HIGHSCORE", Score);
        }
        else
        {
            UpdateHighScoreTextUI(PlayerPrefs.GetInt("HIGHSCORE"));
        }
    }
    void UpdateHighScoreTextUI(int score)
    {
        if (GameManager.GMState.Equals(GameManager.GameState.GameHome))
        {
            GameObject.Find("UIScoreText").GetComponent<Text>().text = score.ToString();
            GameObject.Find("HomeScreenGold").GetComponent<Text>().text = PlayerPrefs.GetInt("GOLD").ToString();
        }
        if (GameManager.GMState.Equals(GameManager.GameState.GameOver))
        {
            GameObject.Find("GameOverGold").GetComponent<Text>().text = PlayerPrefs.GetInt("GOLD").ToString();
            GameObject.Find("GameOverScore").GetComponent<Text>().text = Score.ToString();
            GameObject.Find("GameOverHighScore").GetComponent<Text>().text = score.ToString();
        }
        if (GameManager.GMState.Equals(GameManager.GameState.GameShop))
        {
            GameObject.Find("GameShopGold").GetComponent<Text>().text = PlayerPrefs.GetInt("GOLD").ToString();
        }
        if (GameManager.GMState.Equals(GameManager.GameState.GamePause))
        {
            GameObject.Find("GamePauseScoreText").GetComponent<Text>().text = Score.ToString();
            GameObject.Find("PauseHighScoreText").GetComponent<Text>().text = score.ToString();
        }
    }
    public void UpdateGold(int gold)
    {
        if (!PlayerPrefs.HasKey("GOLD"))
        {
            PlayerPrefs.SetInt("GOLD", Gold);
        }
        else
        {
            PlayerPrefs.SetInt("GOLD", gold + PlayerPrefs.GetInt("GOLD"));
        }
    }
}
