#define ADS

using System;
using UnityEngine;
using UnityEngine.Advertisements;

public class AdsManager : SingletonMono<AdsManager>
#if ADS
    ,IUnityAdsInitializationListener
#endif
{
    [SerializeField]
    private string _iosGameId, _androidGameId;

    [SerializeField]
    private BannerPosition _bannerPosition;

    private GameParams _params;

    private InterstitialAdsService _interstitialAdsService;
    private BannerService _bannerService;

    private string _gameId;

    private float _nextInterstitialShowTime;

    public event Action OnInterstitialEnded;

    public void Construct(GameParams gameParams) => _params = gameParams;

    protected override void InitSingleInstance()
    {
        base.InitSingleInstance();
        
        InitializeAds();
        CreateServices();

        _nextInterstitialShowTime = Time.realtimeSinceStartup +
                                    _params.MinInterstitialInterval;
    }

#if ADS
    public void OnInitializationComplete() { }

    public void OnInitializationFailed(
        UnityAdsInitializationError error, string message)
    {
        Debug.Log(
            $"Unity Ads Initialization Failed: {error.ToString()} - {message}");
    }
#endif

    private void InitializeAds()
    {
#if ADS
  #if UNITY_IOS
        _gameId = _iosGameId;
  #else
        _gameId = _androidGameId;
  #endif

        Advertisement.Initialize(_gameId, _params.IsTestAdsMode, Instance);
#endif
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

    public void ShowInterstitial()
    {
        if (Time.realtimeSinceStartup >= _nextInterstitialShowTime)
        {
            _interstitialAdsService.ShowAd();
            
            _nextInterstitialShowTime = Time.realtimeSinceStartup +
                                        _params.MinInterstitialInterval;
        }
        else
            EndInterstitial();
    }

    public void EndInterstitial() => OnInterstitialEnded?.Invoke();

    public void ShowBanner() => _bannerService.ShowBanner();

    public void HideBanner() => _bannerService.HideBanner();
}