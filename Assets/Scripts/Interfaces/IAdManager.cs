public interface IAdManager
{
    void ShowInterstitial();
    void ShowRewardedAd(System.Action<float> onRewardedAdCompleted);
}
