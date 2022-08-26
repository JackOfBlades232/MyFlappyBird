using System;
using UnityEngine;
using UnityEngine.Events;
using DG.Tweening;
using TMPro;

[RequireComponent(typeof(BirdSpriteSwapper))]
[RequireComponent(typeof(Rigidbody2D))]
public class BirdJump : MonoBehaviour, IInitializable
{
    private GameParams _params;
    
    private BirdSpriteSwapper _spriteSwapper;

    private GroundStatic _groundStatic;

    private Rigidbody2D _rigidbody;

    private float _lastJumpTime;

    private bool _jumpBlocked;

    public UnityEvent OnJumpStarted;

    public UnityEvent OnReachedGround;

    public void Initialize()
    {
        _spriteSwapper = GetComponent<BirdSpriteSwapper>();
        _rigidbody = GetComponent<Rigidbody2D>();
        
        _spriteSwapper.Construct(_params);
        _spriteSwapper.Initialize();
        
        OnReachedGround.AddListener(BlockJump);
        
        _jumpBlocked = false;

        _lastJumpTime = Time.time - _params.BirdJumpCooldown;
        SetAfterJumpRotate();
    }

    public void Construct(GameParams gameParams, GroundStatic groundStatic)
    {
        _params = gameParams;
        _groundStatic = groundStatic;
    }
    
    private void SetOnJumpRotate(float appliedImpulse)
    {
        transform.DOKill();

        float riseTime = -appliedImpulse * _rigidbody.mass / Physics.gravity.y;
        
        Vector3 finalRotation =
            new Vector3(0, 0, _params.BirdRotationCeil);

        transform.DORotate(finalRotation, riseTime)
            .OnComplete(SetAfterJumpRotate);
    }

    private void SetAfterJumpRotate()
    {
        transform.DOKill();

        float acceleration = -Physics.gravity.y / _rigidbody.mass;
        float fallDistance =
            Mathf.Max(transform.position.y - _groundStatic.UpperBoundY, 0);
        float fallTime = Mathf.Sqrt(2 * fallDistance / acceleration);

        Vector3 finalRotation = new Vector3(0, 0, -Utils.StraightAngle);

        transform.DORotate(finalRotation, fallTime)
            .OnComplete(() => OnReachedGround?.Invoke());
    }

    private void BlockJump()
    {
        if (_jumpBlocked)
            return;
        
        _jumpBlocked = true;
        
        _rigidbody.velocity = Vector2.zero;
    }

    public void PerformJump()
    {
        if (_jumpBlocked)
            return;
        
        float timeSinceJump = Time.time - _lastJumpTime;

        if (timeSinceJump >= _params.BirdJumpCooldown)
        {
            OnJumpStarted?.Invoke();
            
            _rigidbody.velocity = Vector2.zero;

            Vector3 impulse = _params.BirdJumpImpulse;
            _rigidbody.AddForce(impulse, ForceMode2D.Impulse);

            SetOnJumpRotate(impulse.y);

            _lastJumpTime = Time.time;
        }
    }

    public void Kill()
    {
        if (_jumpBlocked)
            return;
        
        BlockJump();

        SetAfterJumpRotate();
    }
}
