using System;
using UnityEngine;
using UnityEngine.Advertisements;

public class AdsManager : SingletonMono<AdsManager>,
    IUnityAdsInitializationListener
{
    [SerializeField]
    private string _iosGameId, _androidGameId;

    [SerializeField]
    private BannerPosition _bannerPosition;

    private GameParams _params;

    private InterstitialAdsService _interstitialAdsService;
    private BannerService _bannerService;

    private string _gameId;

    public event Action OnInterstitialEnded;

    public void Construct(GameParams gameParams) => _params = gameParams;

    protected override void InitSingleInstance()
    {
        base.InitSingleInstance();
        
        InitializeAds();
        CreateServices();
    }

    public void OnInitializationComplete()
    {
        // TODO : remove placeholder
        Debug.Log("Unity Ads initialization complete.");
    }

    public void OnInitializationFailed(
        UnityAdsInitializationError error, string message)
    {
        Debug.Log(
            $"Unity Ads Initialization Failed: {error.ToString()} - {message}");
    }

    private void InitializeAds()
    {
#if UNITY_IOS
        _gameId = _iosGameId;
#else
        _gameId = _androidGameId;
#endif

        Advertisement.Initialize(_gameId, _params.IsTestAdsMode, Instance);
    }

    private void CreateServices()
    {
        _interstitialAdsService = new InterstitialAdsService(this);
        _bannerService = new BannerService(this, _bannerPosition);
    }

    public void LoadAds()
    {
        _interstitialAdsService.LoadAd();
        _bannerService.LoadBanner();
    }

    public void ShowInterstitial() => _interstitialAdsService.ShowAd();

    public void EndInterstitial() => OnInterstitialEnded?.Invoke();

    public void ShowBanner() => _bannerService.ShowBanner();

    public void HideBanner() => _bannerService.HideBanner();
}