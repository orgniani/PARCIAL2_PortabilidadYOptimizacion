using UnityEngine;
using UnityEngine.Advertisements;

public class RewardedAdManager : AdManager, IUnityAdsLoadListener, IUnityAdsShowListener
{
    [SerializeField] private string rewardedAndroidAdUnitId = "Rewarded_Android";
    [SerializeField] private string rewardedIOSAdUnitId = "Rewarded_iOS";

    [SerializeField] private GameManager gameManager;

    private void OnEnable()
    {
        OnUnityAdsInitialized += InitializeRewardedAd;
    }

    private void OnDisable()
    {
        OnUnityAdsInitialized -= InitializeRewardedAd;
    }

    protected override void SetIDs()
    {
#if UNITY_IOS
        adUnitId = rewardedIOSAdUnitId;
#elif UNITY_ANDROID
        adUnitId = rewardedAndroidAdUnitId;
#endif
    }

    private void InitializeRewardedAd()
    {
        Advertisement.Load(adUnitId, this);
    }

    public void OnUnityAdsAdLoaded(string placementId)
    {
        adLoaded = true;
        Debug.Log("Rewarded Ad loaded successfully");
    }

    public void OnUnityAdsFailedToLoad(string placementId, UnityAdsLoadError error, string message)
    {
        Debug.Log($"Rewarded Ad: Error loading Ad Unit: {adUnitId} - {error.ToString()} - {message}");
    }

    public void OnUnityAdsShowFailure(string placementId, UnityAdsShowError error, string message)
    {
        Debug.Log($"Error showing Ad Unit {adUnitId}: {error.ToString()} - {message}");
    }

    public void OnUnityAdsShowStart(string placementId)
    {
        Debug.Log("Rewarded Ad showing successfully");
    }

    public void OnUnityAdsShowClick(string placementId)
    {
        Debug.Log("Rewarded Ad clicked successfully");

        if (adLoaded)
            Advertisement.Show(adUnitId, this);
    }

    public void OnUnityAdsShowComplete(string placementId, UnityAdsShowCompletionState showCompletionState)
    {
        if (placementId.Equals(adUnitId) && showCompletionState.Equals(UnityAdsShowCompletionState.COMPLETED))
        {
            Debug.Log("EXTRA TIME!");
            //gameManager.AddTimeFromReward();
        }
    }
}
