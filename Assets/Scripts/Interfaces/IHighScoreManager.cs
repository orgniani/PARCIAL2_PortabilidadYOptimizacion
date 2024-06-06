using UnityEngine;

public interface IHighScoreManager
{
    int GetHighScore();
    void CheckAndSetHighScore(int score);
}
