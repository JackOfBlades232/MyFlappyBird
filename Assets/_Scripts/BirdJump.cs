using System;
using UnityEngine;
using UnityEngine.Events;

[RequireComponent(typeof(BirdSpriteSwapper))]
[RequireComponent(typeof(Rigidbody2D))]
public class BirdJump : MonoBehaviour, IInitializable
{
    // TODO : pass from initializer
    [SerializeField]
    private GameParams _params;

    private BirdSpriteSwapper _spriteSwapper;
    
    private Rigidbody2D _rigidbody;

    private float _lastJumpTime;

    public UnityEvent OnJumpStarted;

    private float AngularVelocityModifier =>
        _rigidbody.velocity.y > Utils.Precision
            ? _params.BirdAngularVelocityModifierTop
            : _params.BirdAngularVelocityModifierBottom;

    private void Update()
    {
        SetAngularVelocity();
    }

    public void Initialize()
    {
        _spriteSwapper = GetComponent<BirdSpriteSwapper>();
        _rigidbody = GetComponent<Rigidbody2D>();

        _lastJumpTime = Time.time - _params.BirdJumpCooldown;
        
        _spriteSwapper.Initialize();
        _spriteSwapper.Construct(_params);
    }

    private void SetAngularVelocity()
    {
        float angleToRight =
            Vector2.SignedAngle(Vector2.right, transform.right); 
        
        Debug.Log(angleToRight);

        if (angleToRight < -Utils.StraightAngle ||
            angleToRight > _params.BirdRotationCeil)
        {
            _rigidbody.angularVelocity = 0;

            float desiredAngle = angleToRight > 0
                ? _params.BirdRotationCeil - Utils.Precision
                : -Utils.StraightAngle + Utils.Precision;

            transform.right =
                Quaternion.Euler(0, 0, desiredAngle) * Vector3.right;
        }
        else
            _rigidbody.angularVelocity =
                _rigidbody.velocity.y * AngularVelocityModifier;
    }

    public void PerformJump()
    {
        float timeSinceJump = Time.time - _lastJumpTime;

        if (timeSinceJump >= _params.BirdJumpCooldown)
        {
            OnJumpStarted?.Invoke();
            
            _rigidbody.velocity = Vector2.zero;
            
            _rigidbody.AddForce(_params.BirdJumpImpulse, ForceMode2D.Impulse);

            _lastJumpTime = Time.time;
        }
    }
}
