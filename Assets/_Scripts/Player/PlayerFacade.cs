using UnityEngine;
using UnityEngine.Events;
using UnityEngine.InputSystem;

[RequireComponent(typeof(BirdJump))]
[RequireComponent(typeof(PlayerInput))]
public class PlayerFacade : MonoBehaviour, IInitializable
{
    private GameParams _params;
    
    private TileSpawner _tileSpawner;
    
    private BirdJump _jumper;

    private PlayerInput _input;

    private GroundStatic _groundStatic;

    public UnityEvent OnKilled, OnFallen;

    public void Initialize()
    {
        _jumper = GetComponent<BirdJump>();
        _input = GetComponent<PlayerInput>();
        _groundStatic = FindObjectOfType<GroundStatic>();
        
        _groundStatic.Initialize();
        
        _jumper.Construct(_params, _groundStatic);
        _jumper.Initialize();
        
        OnKilled.AddListener(_tileSpawner.StopAllTiles);
        
        _jumper.OnReachedGround.AddListener(() => OnFallen?.Invoke());

        _input.DeactivateInput();
    }

    public void Activate()
    {
        _jumper.PerformJump();
        
        _input.ActivateInput();
    }

    public void Construct(GameParams gameParams, TileSpawner tileSpawner)
    {
        _params = gameParams;
        _tileSpawner = tileSpawner;
    }

    public void Kill()
    {
        OnKilled?.Invoke();
        
        _jumper.Kill();
        _input.DeactivateInput();
    }
}