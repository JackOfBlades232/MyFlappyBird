using UnityEngine;

public class GameInitializer : MonoBehaviour
{
    [SerializeField]
    private GameParams _params;
    
    private GameManager _gameManager;
    private EndgameManager _endgameManager;

    private SaveLoadManager _saveLoadManager;

    private void Awake()
    {
        _gameManager = FindObjectOfType<GameManager>();
        _endgameManager = FindObjectOfType<EndgameManager>();

        _saveLoadManager = new SaveLoadManager();
        
        _saveLoadManager.Initialize();

        _gameManager.Construct(_params, _saveLoadManager);
        _gameManager.Initialize();
        
        _endgameManager.Construct(_params, _gameManager, _saveLoadManager);
        _endgameManager.Initialize();
    }
}