using UnityEngine;
using UnityEngine.Advertisements;

public class AdsManager : SingletonMono<AdsManager>,
    IUnityAdsInitializationListener
{
    [SerializeField]
    private string _iosGameId, _androidGameId;

    private GameParams _params;

    private string _gameId;

    public void Construct(GameParams gameParams) => _params = gameParams;

    protected override void InitSingleInstance()
    {
        base.InitSingleInstance();

        InitializeAds();
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
}