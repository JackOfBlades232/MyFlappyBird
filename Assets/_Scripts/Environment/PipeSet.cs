using System;
using UnityEngine;

[RequireComponent(typeof(BoxCollider2D))]
public class PipeSet : MonoBehaviour, IInitializable
{
    [SerializeField]
    private Transform _bottomPipe, _topPipe;
    
    private Tile _tile;

    private Action _onPipePassed;

    private float _y, _space;

    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.TryGetComponent(out PlayerFacade player))
        {
            _onPipePassed?.Invoke();

            _onPipePassed = null;
        }
    }

    public void Initialize()
    {
        _onPipePassed = () => _tile.SendPipePassed();
        
        SetPipesPositions();
    }

    public void Construct(Tile tile, float y, float space)
    {
        _tile = tile;

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
}
