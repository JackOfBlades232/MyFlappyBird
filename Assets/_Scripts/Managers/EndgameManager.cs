using UnityEngine;

public class EndgameManager : MonoBehaviour, IInitializable
{
    private GameParams _params;
    
    private GameManager _gameManager;

    private SaveLoadManager _saveLoadManager;
    
    private EndgameUI _ui;

    public void Initialize()
    {
        _ui = FindObjectOfType<EndgameUI>();
        
        _ui.Construct(_params, _saveLoadManager.PlayerData);
        _ui.Initialize();

        _gameManager.OnGameEnded.AddListener(PlayAdAndActivateAfter);
    }

    public void Construct(GameParams gameParams, GameManager gameManager,
        SaveLoadManager saveLoadManager)
    {
        _params = gameParams;
        _gameManager = gameManager;
        _saveLoadManager = saveLoadManager;
    }

    private void PlayAdAndActivateAfter()
    {
        AdsManager.Instance.HideBanner();
        AdsManager.Instance.OnInterstitialEnded += Activate;
        AdsManager.Instance.ShowInterstitial();
    }

    private void Activate()
    {
        AdsManager.Instance.OnInterstitialEnded -= Activate;
        AudioManager.Instance.PlayMusic(MusicType.Menu);
        
        _ui.Activate();
    }
}