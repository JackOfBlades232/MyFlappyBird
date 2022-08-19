using UnityEngine;

public class GameInitializer : MonoBehaviour
{
    private GameManager _gameManager;

    private void Awake()
    {
        _gameManager = FindObjectOfType<GameManager>();
        
        _gameManager.Initialize();
    }
}