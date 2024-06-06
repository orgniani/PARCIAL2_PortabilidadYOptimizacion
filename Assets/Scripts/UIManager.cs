using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{
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

    public TMP_Text ClickCounterText => clickCounterText;
    public TMP_Text TimerText => timerText;
    public TMP_Text HighScoreText => highScoreText;

    private void Start()
    {
        trophyButton.SetActive(false);

#if UNITY_ANDROID
        trophyButton.SetActive(true);
#endif
    }

    public void ShowInstructionText(bool show)
    {
        instructionText.SetActive(show);
    }

    public void ShowCreditsCanvas(bool show)
    {
        creditsCanvas.SetActive(show);
        Time.timeScale = show ? 0 : 1;
    }

    public void EnableButton(bool enable)
    {
        clickerButton.interactable = enable;
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
}
