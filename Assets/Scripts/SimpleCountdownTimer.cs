using System;
using System.Collections;
using UnityEngine;

public class SimpleCountdownTimer : MonoBehaviour, ICountdownTimer
{
    public event Action<float> OnTimeUpdated;
    public event Action OnTimerCompleted;

    private float timer;
    private bool running;

    public void StartTimer(float initialTime)
    {
        timer = initialTime;
        running = true;
        StartCoroutine(TimerCoroutine());
    }

    public void AddExtraTime(float extraTime)
    {
        timer += extraTime;
        OnTimeUpdated?.Invoke(timer);
    }

    public void ResetTimer(float initialTime)
    {
        StopAllCoroutines();
        timer = initialTime;
        OnTimeUpdated?.Invoke(timer);
    }

    private IEnumerator TimerCoroutine()
    {
        while (timer > 0 && running)
        {
            timer -= Time.deltaTime;
            OnTimeUpdated?.Invoke(timer);
            yield return null;
        }

        if (timer <= 0)
        {
            OnTimeUpdated?.Invoke(0);
            OnTimerCompleted?.Invoke();
        }
    }
}
