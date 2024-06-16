using TMPro;

public class ClicksHandler : IClicksHandler
{
    private TMP_Text clickCounterText;
    private int clicksCounter = 0;

    public ClicksHandler(TMP_Text clickCounterText)
    {
        this.clickCounterText = clickCounterText;
    }

    public void OnClick()
    {
        clicksCounter++;
        UpdateClicksText();
    }

    public void ResetClicks()
    {
        clicksCounter = 0;
        UpdateClicksText();
    }

    private void UpdateClicksText()
    {
        clickCounterText.text = clicksCounter.ToString("D2") + " clicks";
#if UNITY_ANDROID
        clickCounterText.text = clicksCounter.ToString("D2") + " taps";
#endif
    }

    public int GetClicksCount()
    {
        return clicksCounter;
    }
}
