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
    [SerializeField] private GameObject medalButton;
    [SerializeField] private GameObject trophyButton;

    [Header("Parameters")]
    [SerializeField] private float countdownTime = 10f;
    [SerializeField] private float waitToRestart = 2f;

    private int clicksCounter = 0;
    private int highScore = 0;

    private bool gameOver = false;
    private bool gameStart = false;

    private const string HighScoreKey = "HighScore";

    private void Start()
    {
        highScore = PlayerPrefs.GetInt(HighScoreKey, 0);
        highScoreText.text = "High Score: " + highScore.ToString("D2");

#if UNITY_ANDROID
    medalButton.SetActive(true);
    trophyButton.SetActive(true);
#endif
    }

    public void OnClick()
    {
        if (gameOver) return;

        clicksCounter++;
        clickCounterText.text = clicksCounter.ToString("D2") + " clicks";

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

            int seconds = Mathf.FloorToInt(timer);
            int milliseconds = Mathf.FloorToInt((timer - seconds) * 100);
            timerText.text = $"Tiempo: {seconds:D2}:{milliseconds:D2}";

            yield return null;
        }

        timerText.text = "Tiempo: 00:00";

        clickerButton.interactable = false;

        gameOver = true;
        SaveHighScore();

        yield return new WaitForSeconds(waitToRestart);

        ResetGame();
    }

    private void SaveHighScore()
    {
        if (clicksCounter > highScore)
        {
            PlayerPrefs.SetInt(HighScoreKey, clicksCounter);
            PlayerPrefs.Save();
            highScoreText.text = "High Score: " + clicksCounter.ToString("D2");
        }
    }

    private void ResetGame()
    {
        gameStart = false;
        gameOver = false;

        instructionText.SetActive(true);
        clickerButton.interactable = true;

        clicksCounter = 0;


    }
}
