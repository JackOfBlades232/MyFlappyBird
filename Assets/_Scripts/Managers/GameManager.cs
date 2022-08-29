using UnityEngine;
using UnityEngine.Events;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour, IInitializable
{
    private GameParams _params;

    private EndgameUI _endgameUI;

    private PlayerFacade _player;
    private PipeSpawner _pipeSpawner;
    private ScoreManager _scoreManager;

    public UnityEvent OnGameEnded;

    public void Initialize()
    {
        _player = FindObjectOfType<PlayerFacade>();
        _pipeSpawner = FindObjectOfType<PipeSpawner>();
        _scoreManager = FindObjectOfType<ScoreManager>();

        InitializeAll();
    }

    public void Construct(GameParams gameParams) => _params = gameParams;

    public void StartGame()
    {
        Utils.Unpause();
        
        _player.Activate();  
    } 
    
    private void InitializeAll()
    {
        _player.Construct(_params, _pipeSpawner);
        _pipeSpawner.Construct(_params, _scoreManager);
        
        _player.Initialize();
        _pipeSpawner.Initialize();
        _scoreManager.Initialize();
        
        _player.OnFallen.AddListener(() => OnGameEnded?.Invoke());
    }
}