using UnityEngine;

public class GameInitializer : MonoBehaviour
{
    [SerializeField]
    private GameParams _params;
    
    private GameManager _gameManager;
    private EndgameManager _endgameManager;

    private void Awake()
    {
        _gameManager = FindObjectOfType<GameManager>();
        _endgameManager = FindObjectOfType<EndgameManager>();

        _gameManager.Construct(_params);
        _gameManager.Initialize();
        
        _endgameManager.Construct(_params, _gameManager);
        _endgameManager.Initialize();

        Utils.Pause();
    }
}