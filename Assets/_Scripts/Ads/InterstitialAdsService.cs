using UnityEngine;
using UnityEngine.Advertisements;

public class InterstitialAdsService : 
    IUnityAdsLoadListener,
    IUnityAdsShowListener
{
#if UNITY_ANDROID
    private const string PlacementId = "Interstitial_Android";
#elif UNITY_IOS
    private const string PlacementId = "Interstitial_iOS";
#endif

    private readonly AdsManager _adsManager;

    public InterstitialAdsService(AdsManager adsManager) =>
        _adsManager = adsManager;

    public void OnUnityAdsAdLoaded(string placementId)
    {
        
    }

    public void OnUnityAdsFailedToLoad(string placementId,
        UnityAdsLoadError error, string message)
    {
       
    }

    public void OnUnityAdsShowFailure(string placementId,
        UnityAdsShowError error, string message)
    {
      
    }

    public void OnUnityAdsShowStart(string placementId)
    {
     
    }

    public void OnUnityAdsShowClick(string placementId)
    {
    
    }

    public void OnUnityAdsShowComplete(string placementId,
        UnityAdsShowCompletionState showCompletionState)
    {
        _adsManager.EndInterstitial();
    }

    public void LoadAd()
    {
        Debug.Log($"Loading ad {PlacementId}");
        Advertisement.Load(PlacementId, this);
    }

    public void ShowAd()
    {
        Debug.Log($"Showing ad {PlacementId}");
        Advertisement.Show(PlacementId, this);
    }
}