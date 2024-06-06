using TMPro;
using UnityEngine;

public class HighScoreManager : IHighScoreManager
{
    private const string HighScoreKey = "HighScore";
    private TMP_Text highScoreText;
    private int highScore;

    public HighScoreManager(TMP_Text highScoreText)
    {
        highScore = PlayerPrefs.GetInt(HighScoreKey, 0);
        this.highScoreText = highScoreText;

        UpdateHighScoreText(highScore);
    }

    public int GetHighScore()
    {
        return highScore;
    }

    private void SetHighScore()
    {
        PlayerPrefs.SetInt(HighScoreKey, highScore);
        PlayerPrefs.Save();

        UpdateHighScoreText(highScore);
    }

    public bool IsHighScore(int score)
    {
        if (score > highScore)
        {
            highScore = score;
            SetHighScore();
            return true;
        }

        else return false;
    }

    private void UpdateHighScoreText(int newHighScore)
    {
        highScoreText.text = "High Score: " + newHighScore.ToString("D2");
    }
}
