using System.Collections;
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

        _gameManager.OnGameEnded.AddListener(StartEndgameSequence);
    }

    public void Construct(GameParams gameParams, GameManager gameManager,
        SaveLoadManager saveLoadManager)
    {
        _params = gameParams;
        _gameManager = gameManager;
        _saveLoadManager = saveLoadManager;
    }

    private void StartEndgameSequence() =>
        StartCoroutine(PlayAdAndActivateDelayed());

    private IEnumerator PlayAdAndActivateDelayed()
    {
        yield return new WaitForSeconds(_params.DelayBeforeAd);
        
        DecidePlayAdAndActivateAfter();
    }

    private void DecidePlayAdAndActivateAfter()
    {
        AdsManager.Instance.ShowBanner();

        if (Random.Range(0f, 1f) < _params.InterstitialProbability)
        {
            AdsManager.Instance.OnInterstitialEnded += Activate;
            AdsManager.Instance.ShowInterstitial();
        }
        else
            Activate();
    }

    private void Activate()
    {
        Debug.Log("Activate");
        AdsManager.Instance.OnInterstitialEnded -= Activate;
        AudioManager.Instance.PlayMusic(MusicType.Menu);
        
        _ui.Activate();
    }
}