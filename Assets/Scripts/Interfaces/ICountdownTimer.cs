using System;

public interface ICountdownTimer
{
    event Action<float> OnTimeUpdated;
    event Action OnTimerCompleted;
    void StartTimer(float initialTime);
    void AddExtraTime(float extraTime);
    void ResetTimer(float initialTime);
}
