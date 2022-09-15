using UnityEngine;

public class GameInitializer : MonoBehaviour
{
    [SerializeField]
    private GameParams _params;

    [SerializeField]
    private AudioManager _audioManagerPrefab;
    
    private GameManager _gameManager;
    private EndgameManager _endgameManager;

    private SaveLoadManager _saveLoadManager;

    private void Awake()
    {
        _gameManager = FindObjectOfType<GameManager>();
        _endgameManager = FindObjectOfType<EndgameManager>();

        _saveLoadManager = SaveLoadManager.Instance;
        _saveLoadManager.Initialize();

        AudioManager audioManager = Instantiate(_audioManagerPrefab);
        audioManager.Construct(_saveLoadManager);
        audioManager.Initialize();

        _gameManager.Construct(_params, _saveLoadManager);
        _gameManager.Initialize();
        
        _endgameManager.Construct(_params, _gameManager, _saveLoadManager);
        _endgameManager.Initialize();
    }
}