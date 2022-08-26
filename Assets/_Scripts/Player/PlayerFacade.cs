using UnityEngine;
using UnityEngine.Events;

[RequireComponent(typeof(BirdJump))]
public class PlayerFacade : MonoBehaviour, IInitializable
{
    private GameParams _params;
    
    private TileSpawner _tileSpawner;
    
    private BirdJump _jumper;

    private GroundStatic _groundStatic;

    public UnityEvent OnKilled, OnFallen;

    public void Initialize()
    {
        _jumper = GetComponent<BirdJump>();
        _groundStatic = FindObjectOfType<GroundStatic>();
        
        _groundStatic.Initialize();
        
        _jumper.Construct(_params, _groundStatic);
        _jumper.Initialize();
        
        OnKilled.AddListener(_tileSpawner.StopAllTiles);
        
        _jumper.OnReachedGround.AddListener(() => OnFallen?.Invoke());
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
    }
}