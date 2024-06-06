using System;
using UnityEngine;
using UnityEngine.Advertisements;

public abstract class AdManager : MonoBehaviour, IUnityAdsInitializationListener
{
    private string gameId;
    protected string adUnitId;

    protected bool adLoaded = false;

    protected static event Action OnUnityAdsInitialized;

    private void Awake()
    {
#if UNITY_IOS
        gameId = "5629789";
#elif UNITY_ANDROID
        gameId = "5629788";
#elif UNITY_EDITOR
        gameId = "5629788";
#endif

        if (!Advertisement.isInitialized && Advertisement.isSupported)
        {
            Advertisement.Initialize(gameId, true, this);
        }
    }

    private void Start()
    {
        SetIDs();
    }

    public void OnInitializationComplete()
    {
        Debug.Log("Unity Ads initialization complete.");
        OnUnityAdsInitialized?.Invoke();
    }

    public void OnInitializationFailed(UnityAdsInitializationError error, string message)
    {
        Debug.Log($"Unity Ads Initialization Failed: {error.ToString()} - {message}");
    }


    protected abstract void SetIDs();
}