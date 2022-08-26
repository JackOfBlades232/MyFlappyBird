using UnityEngine;

public class GameInitializer : MonoBehaviour
{
    [SerializeField]
    private GameParams _params;
    
    private GameManager _gameManager;

    private StartUI _startUI;
    private MainUI _mainUI;
    private EndgameUI _endgameUI;

    private void Awake()
    {
        _gameManager = FindObjectOfType<GameManager>();
        _startUI = FindObjectOfType<StartUI>();
        _mainUI = FindObjectOfType<MainUI>();
        _endgameUI = FindObjectOfType<EndgameUI>();

        _gameManager.Construct(_params);
        _gameManager.Initialize();
        
        _startUI.Initialize();
        _mainUI.Initialize();
        _endgameUI.Initialize();
        
        _startUI.OnClicked.AddListener(_gameManager.StartGame);
        _startUI.OnClicked.AddListener(_mainUI.Activate);
        
        _gameManager.OnGameEnded.AddListener(_mainUI.Deactivate);
        _gameManager.OnGameEnded.AddListener(_endgameUI.Activate);
        
        Utils.Pause();
    }
}