using UnityEngine;
using System.Collections;
using System;

public class GameManager : MonoBehaviour
{
    [Header("Parameters")]
    [SerializeField] private float initialCountdownTime = 10f;
    [SerializeField] private float waitToRestart = 2f;

    private ITimer timer;
    private IClicksHandler clicksHandler;
    private IHighScoreManager highScoreManager;

    private UIManager uiManager;

    public event Action OnShowAd;

    public bool GameStart { get; private set; }

    private void Awake()
    {
        uiManager = GetComponent<UIManager>();

        timer = new Timer(this, uiManager.TimerText, initialCountdownTime);
        clicksHandler = new ClicksHandler(uiManager.ClickCounterText);
        highScoreManager = new HighScoreManager(uiManager.HighScoreText);

        GameStart = false;
    }

    public void OnClick()
    {
        clicksHandler.OnClick();

        if (GameStart) return;

        uiManager.ShowInstructionText(false);
        timer.StartTimer(OnTimerComplete);

        GameStart = true;
    }

    private void OnTimerComplete()
    {
        uiManager.EnableButton(false);
        CheckIfHighScore();

        StartCoroutine(RestartAfterDelay());
    }

    private IEnumerator RestartAfterDelay()
    {
        yield return new WaitForSeconds(waitToRestart);
        ResetGame();
    }

    private void CheckIfHighScore()
    {
        if (!highScoreManager.IsHighScore(clicksHandler.GetClicksCount())) OnShowAd?.Invoke();
    }

    private void ResetGame()
    {
        GameStart = false;

        uiManager.ShowInstructionText(true);
        uiManager.EnableButton(true);

        clicksHandler.ResetClicks();
        timer.ResetTimer(initialCountdownTime);

        uiManager.ResetRewardCanvas();
    }

    public void RewardExtraSeconds(float extraSeconds)
    {
        timer.AddExtraSeconds(extraSeconds);
    }
}
