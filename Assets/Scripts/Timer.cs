using System;
using System.Collections;
using TMPro;
using UnityEngine;

public class Timer : ITimer
{
    private MonoBehaviour coroutineOwner;
    private Coroutine countdownCoroutine;
    private TMP_Text timerText;
    private float countdownTime;

    public Timer(MonoBehaviour coroutineOwner, TMP_Text timerText, float initialCountdownTime)
    {
        this.coroutineOwner = coroutineOwner;
        this.timerText = timerText;
        countdownTime = initialCountdownTime;

        UpdateTimerText(countdownTime);
    }

    public void StartTimer(Action onComplete)
    {
        countdownCoroutine = coroutineOwner.StartCoroutine(CountdownTimer(onComplete));
    }

    public void ResetTimer(float initialCountdownTime)
    {
        if (countdownCoroutine != null)
        {
            coroutineOwner.StopCoroutine(countdownCoroutine);
        }

        countdownTime = initialCountdownTime;
        UpdateTimerText(countdownTime);
    }

    private IEnumerator CountdownTimer(Action onComplete)
    {
        float timer = countdownTime;

        while (timer > 0)
        {
            timer -= Time.deltaTime;
            UpdateTimerText(timer);
            yield return null;
        }

        UpdateTimerText(0f);
        onComplete?.Invoke();
    }

    private void UpdateTimerText(float timer)
    {
        int seconds = Mathf.FloorToInt(timer);
        int milliseconds = Mathf.FloorToInt((timer - seconds) * 100);
        timerText.text = $"{seconds:D2}:{milliseconds:D2}";
    }

    public void AddExtraSeconds(float extraSeconds)
    {
        countdownTime += extraSeconds;
        UpdateTimerText(countdownTime);
    }
}
