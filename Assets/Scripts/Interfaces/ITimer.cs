using System;

public interface ITimer
{
    public void StartTimer(Action onComplete);
    public void ResetTimer(float initialCountdownTime);
    public void AddExtraSeconds(float extraSeconds);
}