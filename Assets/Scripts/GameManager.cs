using UnityEngine;
using TMPro;
using System.Collections;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    [Header("References")]
    [Header("Text")]
    [SerializeField] private TMP_Text clickCounterText;
    [SerializeField] private TMP_Text timerText;
    [SerializeField] private TMP_Text highScoreText;

    [Header("Objects")]
    [SerializeField] private GameObject instructionText;
    [SerializeField] private GameObject creditsCanvas;

    [Header("Buttons")]
    [SerializeField] private Button clickerButton;
    [SerializeField] private GameObject trophyButton;

    [Header("Ads")]
    [SerializeField] private InterstitialAdManager interstitialAd;
    [SerializeField] private RewardedAdManager rewardedAd;

    [Header("Parameters")]
    [SerializeField] private float initialCountdownTime = 10f;
    [SerializeField] private float waitToRestart = 2f;

    private float countdownTime = 0f;

    private int clicksCounter = 0;
    private int highScore = 0;

    private bool gameStart = false;
    private bool gameOver = false;

    private const string HighScoreKey = "HighScore";

    private void Awake()
    {
        if (!clickCounterText)
        {
            Debug.LogError($"{name}: {nameof(clickCounterText)} is null!" +
                           $"\nDisabling object to avoid errors.");
            enabled = false;
            return;
        }

        if (!timerText)
        {
            Debug.LogError($"{name}: {nameof(timerText)} is null!" +
                           $"\nDisabling object to avoid errors.");
            enabled = false;
            return;
        }


        if (!highScoreText)
        {
            Debug.LogError($"{name}: {nameof(highScoreText)} is null!" +
                           $"\nDisabling object to avoid errors.");
            enabled = false;
            return;
        }

        if (!instructionText)
        {
            Debug.LogError($"{name}: {nameof(instructionText)} is null!" +
                           $"\nDisabling object to avoid errors.");
            enabled = false;
            return;
        }

        if (!creditsCanvas)
        {
            Debug.LogError($"{name}: {nameof(creditsCanvas)} is null!" +
                           $"\nDisabling object to avoid errors.");
            enabled = false;
            return;
        }

        if (!clickerButton)
        {
            Debug.LogError($"{name}: {nameof(clickerButton)} is null!" +
                           $"\nDisabling object to avoid errors.");
            enabled = false;
            return;
        }

        if (!trophyButton)
        {
            Debug.LogError($"{name}: {nameof(trophyButton)} is null!" +
                           $"\nDisabling object to avoid errors.");
            enabled = false;
            return;
        }

        if (!interstitialAd)
        {
            Debug.LogError($"{name}: {nameof(interstitialAd)} is null!" +
                           $"\nDisabling object to avoid errors.");
            enabled = false;
            return;
        }

        if (!rewardedAd)
        {
            Debug.LogError($"{name}: {nameof(rewardedAd)} is null!" +
                           $"\nDisabling object to avoid errors.");
            enabled = false;
            return;
        }

        trophyButton.SetActive(false);
    }

    private void OnEnable()
    {
        rewardedAd.OnRewardedAdCompleted += HandleRewardedAdCompleted;
    }

    private void OnDisable()
    {
        rewardedAd.OnRewardedAdCompleted -= HandleRewardedAdCompleted;
    }

    private void Start()
    {
        highScore = PlayerPrefs.GetInt(HighScoreKey, 0);
        highScoreText.text = "High Score: " + highScore.ToString("D2");

        countdownTime = initialCountdownTime;
        UpdateTimeText(countdownTime);

#if UNITY_ANDROID
    trophyButton.SetActive(true);
#endif
    }

    public void OnClick()
    {
        if (gameOver) return;

        clicksCounter++;
        UpdateClicksText();

        if (gameStart) return;

        instructionText.SetActive(false);
        StartCoroutine(CountDownTimer());

        gameStart = true;
    }

    public void OnOpenAndCloseCredits()
    {
        if (creditsCanvas.activeSelf)
        {
            creditsCanvas.SetActive(false);
            Time.timeScale = 1;
        }

        else
        {
            creditsCanvas.SetActive(true);
            Time.timeScale = 0;
        }
    }

    private IEnumerator CountDownTimer()
    {
        float timer = countdownTime;

        while(timer > 0)
        {
            timer -= Time.deltaTime;

            UpdateTimeText(timer);

            yield return null;
        }

        UpdateTimeText(0f);

        clickerButton.interactable = false;

        gameOver = true;
        CheckIfHighScore();

        yield return new WaitForSeconds(waitToRestart);

        ResetGame();
    }

    private void UpdateTimeText(float timer)
    {
        int seconds = Mathf.FloorToInt(timer);
        int milliseconds = Mathf.FloorToInt((timer - seconds) * 100);
        timerText.text = $"Tiempo: {seconds:D2}:{milliseconds:D2}";
    }

    private void UpdateClicksText()
    {
        clickCounterText.text = clicksCounter.ToString("D2") + " clicks";
    }

    private void CheckIfHighScore()
    {
        if (clicksCounter > highScore)
        {
            PlayerPrefs.SetInt(HighScoreKey, clicksCounter);
            PlayerPrefs.Save();
            highScoreText.text = "High Score: " + clicksCounter.ToString("D2");
        }

        else ShowAd();
    }

    private void ShowAd()
    {
        interstitialAd.ShowInterstitial();
    }

    private void ResetGame()
    {
        gameStart = false;
        gameOver = false;

        instructionText.SetActive(true);
        clickerButton.interactable = true;

        clicksCounter = 0;
        UpdateClicksText();

        rewardedAd.ResetButton();
        ResetCountdownTime();
    }

    private void ResetCountdownTime()
    {
        countdownTime = initialCountdownTime;
        UpdateTimeText(countdownTime);
    }

    private void HandleRewardedAdCompleted(float extraTime)
    {
        countdownTime += extraTime;
        UpdateTimeText(countdownTime);
    }
}
