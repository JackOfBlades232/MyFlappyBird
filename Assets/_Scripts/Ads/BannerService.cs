#define ADS

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

#if ADS
        Advertisement.Banner.SetPosition(bannerPosition);
#endif
    }

#if ADS
    private void OnBannerLoaded() { }
    private void OnBannerError(string message) { }
    private void OnBannerShown() { }
    private void OnBannerClicked() { }
    private void OnBannerHidden() { }
#endif

    public void LoadBanner()
    {
#if ADS
        BannerLoadOptions options = new BannerLoadOptions
        {
            loadCallback = OnBannerLoaded,
            errorCallback = OnBannerError
        };
        
        Advertisement.Banner.Load(PlacementId, options);
#endif
    }

    public void ShowBanner()
    {
#if ADS
        BannerOptions options = new BannerOptions
        {
            clickCallback = OnBannerClicked,
            hideCallback = OnBannerHidden,
            showCallback = OnBannerShown
        };
        
        Advertisement.Banner.Show(PlacementId, options);
#endif
    }

    public void HideBanner()
    {
#if ADS
        Advertisement.Banner.Hide();
#endif
    }
}
