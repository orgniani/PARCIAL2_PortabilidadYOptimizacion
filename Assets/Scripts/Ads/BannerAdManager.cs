using UnityEngine;
using UnityEngine.Advertisements;

public class BannerAdManager : AdManager
{
    [SerializeField] private string bannerAndroidAdUnitId = "Banner_Android";
    [SerializeField] private string bannerIOSAdUnitId = "Banner_iOS";

    private void OnEnable()
    {
        OnUnityAdsInitialized += InitializeBanner;
    }

    private void OnDisable()
    {
        OnUnityAdsInitialized -= InitializeBanner;
    }

    protected override void SetIDs()
    {
#if UNITY_IOS
        adUnitId = bannerIOSAdUnitId;
#elif UNITY_ANDROID
        adUnitId = bannerAndroidAdUnitId;
#endif
    }

    private void InitializeBanner()
    {
        BannerLoadOptions loadOptions = new BannerLoadOptions
        {
            loadCallback = OnBannerLoaded,
            errorCallback = OnBannerError
        };

        Advertisement.Banner.SetPosition(BannerPosition.BOTTOM_CENTER);
        Advertisement.Banner.Load(adUnitId, loadOptions);
    }

    private void OnBannerLoaded()
    {
        Debug.Log("Banner loaded successfully");
        Advertisement.Banner.Show(adUnitId);
    }

    private void OnBannerError(string message)
    {
        Debug.Log($"Banner Error: {message}");
    }
}