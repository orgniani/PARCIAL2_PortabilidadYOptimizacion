using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{
    [Header("Text")]
    [SerializeField] private TMP_Text clickCounterText;
    [SerializeField] private TMP_Text timerText;
    [SerializeField] private TMP_Text highScoreText;
    [SerializeField] private TMP_Text instructionText;

    [Header("Objects")]
    [SerializeField] private GameObject creditsCanvas;
    [SerializeField] private GameObject rewardCanvas;

    [Header("Buttons")]
    [SerializeField] private Button clickerButton;
    [SerializeField] private GameObject trophyButton;

    private bool rewardCanvasOpened = false;

    public TMP_Text ClickCounterText => clickCounterText;
    public TMP_Text TimerText => timerText;
    public TMP_Text HighScoreText => highScoreText;

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
    }

    private void Start()
    {
        trophyButton.SetActive(false);
        creditsCanvas.SetActive(false);
        rewardCanvas.SetActive(false);

#if UNITY_ANDROID
        trophyButton.SetActive(true);
        instructionText.text = "tap the button to start!";
        clickCounterText.text = "00 taps";
#endif
    }

    public void ShowInstructionText(bool show)
    {
        instructionText.gameObject.SetActive(show);
    }

    public void EnableButton(bool enable)
    {
        clickerButton.interactable = enable;
    }

    //Credits Button
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

    //Trophy Button
    public void OnOpenAndCloseReward(GameManager gameManager)
    {
        if (!gameManager.GameStart && !rewardCanvasOpened)
        {
            rewardCanvasOpened = true;
            rewardCanvas.SetActive(true);
        }

        else
        {
            rewardCanvas.SetActive(false);
        }
    }

    public void ResetRewardCanvas()
    {
        rewardCanvasOpened = false;
    }
}
