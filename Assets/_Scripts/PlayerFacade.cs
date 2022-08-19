using UnityEngine;
using UnityEngine.Events;

[RequireComponent(typeof(BirdJump))]
public class PlayerFacade : MonoBehaviour, IInitializable
{
    private PipeSpawner _pipeSpawner;
    
    private BirdJump _jumper;

    private GroundStatic _groundStatic;

    public UnityEvent OnKilled, OnFallen;

    public void Initialize()
    {
        _jumper = GetComponent<BirdJump>();
        _groundStatic = FindObjectOfType<GroundStatic>();
        
        _groundStatic.Initialize();
        
        _jumper.Construct(_groundStatic);
        _jumper.Initialize();
        
        OnKilled.AddListener(_pipeSpawner.StopAllPipes);
        
        _jumper.OnReachedGround.AddListener(() => OnFallen?.Invoke());
    }

    public void Construct(PipeSpawner pipeSpawner) =>
        _pipeSpawner = pipeSpawner;

    public void Kill()
    {
        OnKilled?.Invoke();
        
        _jumper.Kill();
    }
}