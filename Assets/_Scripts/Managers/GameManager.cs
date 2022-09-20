using UnityEngine;
using UnityEngine.Events;

public class GameManager : MonoBehaviour, IInitializable
{
    private GameParams _params;

    private EndgameUI _endgameUI;

    private PlayerFacade _player;
    private PipeSpawner _pipeSpawner;
    
    private ScoreManager _scoreManager;
    private SaveLoadManager _saveLoadManager;

    private StartUI _startUI;
    private MainUI _mainUI;

    public UnityEvent OnGameEnded;

    public void Initialize()
    {
        _player = FindObjectOfType<PlayerFacade>();
        _pipeSpawner = FindObjectOfType<PipeSpawner>();
        _scoreManager = FindObjectOfType<ScoreManager>();

        _startUI = FindObjectOfType<StartUI>();
        _mainUI = FindObjectOfType<MainUI>();

        InitializeAll();
        
        Utils.Pause();
    }

    public void Construct(GameParams gameParams,
        SaveLoadManager saveLoadManager)
    {
        _params = gameParams;
        _saveLoadManager = saveLoadManager;
    }

    private void InitializeAll()
    {
        _player.Construct(_params, _pipeSpawner);
        _pipeSpawner.Construct(_params, _scoreManager);
        
        _player.Initialize();
        _pipeSpawner.Initialize();
        _scoreManager.Initialize();
        
        _startUI.Initialize();
        _mainUI.Initialize();
        
        _player.OnFallen.AddListener(() => OnGameEnded?.Invoke());
        
        _startUI.OnClicked.AddListener(StartGame);
        _startUI.OnClicked.AddListener(_mainUI.Activate);
        
        OnGameEnded.AddListener(_mainUI.Deactivate);
        OnGameEnded.AddListener(() =>
            _saveLoadManager.OnGameEnd(_scoreManager.Score));
    }

    private void StartGame()
    {
        Utils.Unpause();
        AudioManager.Instance.PlayMusic(MusicType.Game);
        
        AdsManager.Instance.ShowBanner();
        
        _player.Activate();  
    } 
}