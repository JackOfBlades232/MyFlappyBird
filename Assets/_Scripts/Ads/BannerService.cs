using UnityEngine.Advertisements;

public class BannerService
{
#if UNITY_ANDROID
    private const string PlacementId = "Banner_Android";
#elif UNITY_IOS
    private const string PlacementId = "Banner_iOS";
#endif

    private readonly AdsManager _adsManager;

    public BannerService(AdsManager adsManager, BannerPosition bannerPosition)
    {
        _adsManager = adsManager;

        Advertisement.Banner.SetPosition(bannerPosition);
    }

    private void OnBannerLoaded() { }
    private void OnBannerError(string message) { }
    private void OnBannerShown() { }
    private void OnBannerClicked() { }
    private void OnBannerHidden() { }

    public void LoadBanner()
    {
        BannerLoadOptions options = new BannerLoadOptions
        {
            loadCallback = OnBannerLoaded,
            errorCallback = OnBannerError
        };
        
        Advertisement.Banner.Load(PlacementId, options);
    }

    public void ShowBanner()
    {
        BannerOptions options = new BannerOptions
        {
            clickCallback = OnBannerClicked,
            hideCallback = OnBannerHidden,
            showCallback = OnBannerShown
        };
        
        Advertisement.Banner.Show(PlacementId, options);
    }

    public void HideBanner() => Advertisement.Banner.Hide();
}
