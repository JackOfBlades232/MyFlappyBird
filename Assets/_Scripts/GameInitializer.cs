using UnityEngine;

public class GameInitializer : MonoBehaviour
{
    [SerializeField]
    private BoxCollider2D _groundStatic;
    
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