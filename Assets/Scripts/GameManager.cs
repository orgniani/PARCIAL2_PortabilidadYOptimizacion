using UnityEngine;
using System.Collections;

public class GameManager : MonoBehaviour
{
    [Header("Ads")]
    [SerializeField] private AdManager adManager;

    [Header("Parameters")]
    [SerializeField] private float initialCountdownTime = 10f;
    [SerializeField] private float waitToRestart = 2f;

    private bool gameStart = false;
    private bool gameOver = false;

    private ITimer timer;
    private IClicksHandler clicksHandler;
    private IHighScoreManager highScoreManager;

    private UIManager uiManager;

    private InterstitialAdManager interstitialAd;
    private RewardedAdManager rewardedAd;

    private void Awake()
    {
        uiManager = GetComponent<UIManager>();

        timer = new Timer(this, uiManager.TimerText, initialCountdownTime);
        clicksHandler = new ClicksHandler(uiManager.ClickCounterText);
        highScoreManager = new HighScoreManager(uiManager.HighScoreText);

        interstitialAd = adManager.GetComponent<InterstitialAdManager>();
        rewardedAd = adManager.GetComponent<RewardedAdManager>();
    }

    private void OnEnable()
    {
        rewardedAd.OnRewardedAdCompleted += HandleRewardExtraSeconds;
    }

    private void OnDisable()
    {
        rewardedAd.OnRewardedAdCompleted -= HandleRewardExtraSeconds;
    }

    public void OnClick()
    {
        if (gameOver) return;

        clicksHandler.OnClick();

        if (gameStart) return;

        uiManager.ShowInstructionText(false);
        timer.StartTimer(OnTimerComplete);

        gameStart = true;
    }

    private void OnTimerComplete()
    {
        uiManager.EnableButton(false);
        gameOver = true;
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
        if (!highScoreManager.IsHighScore(clicksHandler.GetClicksCount())) ShowAd();
    }

    private void ShowAd()
    {
        interstitialAd.ShowInterstitial();
    }

    private void ResetGame()
    {
        gameStart = false;
        gameOver = false;

        uiManager.ShowInstructionText(true);
        uiManager.EnableButton(true);

        clicksHandler.ResetClicks();
        timer.ResetTimer(initialCountdownTime);

        rewardedAd.ResetButton();
    }

    private void HandleRewardExtraSeconds(float extraSeconds)
    {
        timer.AddExtraSeconds(extraSeconds);
    }
}
