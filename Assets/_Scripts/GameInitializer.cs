using UnityEngine;

public class GameInitializer : MonoBehaviour
{
    private PlayerFacade _player;

    private void Awake()
    {
        _player = FindObjectOfType<PlayerFacade>();
        
        InitializeAll();
    }

    private void InitializeAll()
    {
        _player.Initialize();
    }
}