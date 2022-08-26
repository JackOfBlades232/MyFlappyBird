using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour, IInitializable
{
    private GameParams _params;

    private PlayerFacade _player;
    private TileSpawner _tileSpawner;
    private ScoreManager _scoreManager;

    public void Initialize()
    {
        _player = FindObjectOfType<PlayerFacade>();
        _tileSpawner = FindObjectOfType<TileSpawner>();
        _scoreManager = FindObjectOfType<ScoreManager>();

        InitializeAll();
    }

    public void Construct(GameParams gameParams) => _params = gameParams;

    private void InitializeAll()
    {
        _player.Construct(_params, _tileSpawner);
        _tileSpawner.Construct(_params, _scoreManager);
        
        _player.Initialize();
        _tileSpawner.Initialize();
        _scoreManager.Initialize();
        
        _player.OnFallen.AddListener(OnPlayerDeath);
    }

    private void OnPlayerDeath()
    {
        // TODO : implement death logic

        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }
}