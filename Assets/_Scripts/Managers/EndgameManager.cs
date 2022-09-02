using TMPro;
using UnityEngine;

public class EndgameManager : MonoBehaviour, IInitializable
{
    private GameParams _params;
    
    private GameManager _gameManager;
    
    private EndgameUI _ui;

    public void Initialize()
    {
        _ui = FindObjectOfType<EndgameUI>();
        _ui.Initialize();
        
        _gameManager.OnGameEnded.AddListener(_ui.Activate);
    }

    public void Construct(GameParams gameParams, GameManager gameManager)
    {
        _params = gameParams;
        _gameManager = gameManager;
    }
}