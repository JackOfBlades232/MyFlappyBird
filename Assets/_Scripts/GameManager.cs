using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour, IInitializable
{
    private GameParams _params;

    private PlayerFacade _player;
    private PipeSpawner _pipeSpawner;
    private ScoreManager _scoreManager;
    private DifficultyState _difficultyState;

    public void Initialize()
    {
        _player = FindObjectOfType<PlayerFacade>();
        _pipeSpawner = FindObjectOfType<PipeSpawner>();
        _scoreManager = FindObjectOfType<ScoreManager>();

        _difficultyState = new DifficultyState();

        InitializeAll();
    }

    public void Construct(GameParams gameParams) => _params = gameParams;

    private void InitializeAll()
    {
        _player.Construct(_params, _pipeSpawner);
        _difficultyState.Construct(_params);
        _pipeSpawner.Construct(_params, _scoreManager, _difficultyState);
        
        _player.Initialize();
        _difficultyState.Initialize();
        _pipeSpawner.Initialize();
        _scoreManager.Initialize();
        
        _player.OnFallen.AddListener(OnPlayerDeath);

        _scoreManager.OnScoreChanged.AddListener(_ =>
            _difficultyState.OnScoreIncremented());
    }

    private void OnPlayerDeath()
    {
        // TODO : implement death logic

        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }
}