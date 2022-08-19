using System.Collections;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour, IInitializable
{
    private PlayerFacade _player;
    private PipeSpawner _pipeSpawner;
    private ScoreManager _scoreManager;

    public void Initialize()
    {
        _player = FindObjectOfType<PlayerFacade>();
        _pipeSpawner = FindObjectOfType<PipeSpawner>();
        _scoreManager = FindObjectOfType<ScoreManager>();

        InitializeAll();
    }

    private void InitializeAll()
    {
        _player.Construct(_pipeSpawner);
        _pipeSpawner.Construct(_scoreManager);
        
        _player.Initialize();
        _pipeSpawner.Initialize();
        _scoreManager.Initialize();
        
        _player.OnFallen.AddListener(OnPlayerDeath);
    }

    private void OnPlayerDeath()
    {
        // TODO : implement death logic

        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }
}