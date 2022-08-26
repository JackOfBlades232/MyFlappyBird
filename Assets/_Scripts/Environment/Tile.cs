using System.Collections.Generic;
using DG.Tweening;
using UnityEngine;

public class Tile : MonoBehaviour, IInitializable
{
    [SerializeField]
    private List<PipeSet> _pipeSets;
    
    [SerializeField]
    private SpriteRenderer _floorRenderer;

    private GameParams _params;
    
    private TileSpawner _spawner;

    private DifficultyState _difficultyState;

    public float Width => _floorRenderer.bounds.size.x;

    public void Initialize()
    {
        _spawner.AddTile(this);

        foreach (PipeSet pipeSet in _pipeSets)
        {
            pipeSet.Construct(this, _difficultyState.GetNextPipeSetY(),
                _difficultyState.GetNextPipeSpace());
            
            pipeSet.Initialize();
        }
        
        SetMovement();
    }

    public void Construct(GameParams gameParams, TileSpawner spawner,
        DifficultyState difficultyState)
    {
        _params = gameParams;
        _spawner = spawner;
        _difficultyState = difficultyState;
    }

    private void SetMovement()
    {
        float lifetime =
            Mathf.Abs(_params.TileDespawnX - transform.position.x) /
            _difficultyState.TileVelocity;

        transform
            .DOMoveX(_params.TileDespawnX, lifetime).SetEase(Ease.Linear)
            .OnComplete(Despawn);
    }

    private void Despawn()
    {
        _spawner.ReleaseTile(this);

        Destroy(gameObject);
    }

    public void SendPipePassed() => _spawner.SendPipePassed();
}