using System;
using DG.Tweening;
using UnityEngine;

[RequireComponent(typeof(BoxCollider2D))]
public class PipeSet : MonoBehaviour, IInitializable
{
    [SerializeField]
    private GameParams _params;
    
    private PipeSpawner _spawner;

    private Action _incrementScoreAction;

    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.TryGetComponent(out PlayerFacade player))
        {
            _incrementScoreAction?.Invoke();

            _incrementScoreAction = null;
        }
    }

    public void Initialize()
    {
        _incrementScoreAction = () => _spawner.SendIncrementScore();
        
        SetPipesPositions();
        SetPipesMovement();
    }

    public void Construct(PipeSpawner spawner) => _spawner = spawner;

    private void SetPipesPositions()
    {
        // TODO : implement 2 pipes setting (distance, offset)
    }

    private void SetPipesMovement()
    {
        float destinationX = transform.position.x - _params.PipeTravelDistance;

        transform.DOMoveX(destinationX, _params.PipeBaseLifetime)
            .SetEase(Ease.Linear).OnComplete(Despawn);
    }

    private void Despawn()
    {
        _spawner.ReleasePipeSet(this);
        Destroy(gameObject);
    }
}
