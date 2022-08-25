using System;
using DG.Tweening;
using UnityEngine;

[RequireComponent(typeof(BoxCollider2D))]
public class PipeSet : MonoBehaviour, IInitializable
{
    [SerializeField]
    private Transform _bottomPipe, _topPipe;
    
    private GameParams _params;
    
    private PipeSpawner _spawner;

    private Action _incrementScoreAction;

    private float _lifetime, _y, _space;

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

    public void Construct(GameParams gameParams, PipeSpawner spawner,
        float lifetime, float y, float space)
    {
        _params = gameParams;
        _spawner = spawner;

        _lifetime = lifetime;
        _y = y;
        _space = space;
    }

    private void SetPipesPositions()
    {
        transform.position = new Vector3(transform.position.x, _y, 0);

        float halfSpace = _space / 2;

        _bottomPipe.position = transform.position + halfSpace * Vector3.down;
        _topPipe.position = transform.position + halfSpace * Vector3.up;
    }

    private void SetPipesMovement()
    {
        float destinationX = transform.position.x - _params.PipeTravelDistance;

        transform.DOMoveX(destinationX, _lifetime)
            .SetEase(Ease.Linear).OnComplete(Despawn);
    }

    private void Despawn()
    {
        _spawner.ReleasePipeSet(this);
        Destroy(gameObject);
    }
}
