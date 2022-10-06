using System;
using System.Security.Cryptography;
using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
[RequireComponent(typeof(BoxCollider2D))]
public class PipeSet : MonoBehaviour, IInitializable
{
    [SerializeField]
    private Transform _bottomPipe, _topPipe;

    private GameParams _params;

    private PipeSpawner _spawner;

    private Rigidbody2D _rigidbody;

    private Action _onPipePassed;

    private float _y, _space;

    private void Update() => CheckIfIsToDespawn();

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
        _rigidbody = GetComponent<Rigidbody2D>();
        
        _spawner.AddPipeSet(this);
        
        _onPipePassed = _spawner.SendPipePassed;
        
        SetPipesPositions();
    }

    public void Construct(GameParams gameParams, PipeSpawner spawner, float y,
        float space)
    {
        _params = gameParams;
        _spawner = spawner;

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
    
    private void CheckIfIsToDespawn()
    {
        if (transform.position.x <= _params.PipeDespawnX)
            Despawn();
    }

    private void Despawn()
    {
        _spawner.ReleasePipeSet(this);
        
        Destroy(gameObject);
    }

    public void Deactivate()
    {
        UpdateVelocity(0);
        
        _onPipePassed = null;
    }
    
    public void UpdateVelocity(float velocity) =>
        _rigidbody.velocity = velocity * Vector2.left;
    
}
