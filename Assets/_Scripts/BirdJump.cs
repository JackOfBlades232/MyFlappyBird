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

    public void Initialize()
    {
        _spriteSwapper = GetComponent<BirdSpriteSwapper>();
        _rigidbody = GetComponent<Rigidbody2D>();

        _lastJumpTime = Time.time - _params.BirdJumpCooldown;
        
        _spriteSwapper.Initialize();
        _spriteSwapper.Construct(_params);
    }

    public void PerformJump()
    {
        float timeSinceJump = Time.time - _lastJumpTime;

        if (timeSinceJump >= _params.BirdJumpCooldown)
        {
            OnJumpStarted?.Invoke();
            
            _rigidbody.velocity = Vector2.zero;
            
            _rigidbody.AddForce(_params.BirdJumpImpulse, ForceMode2D.Impulse);
            _rigidbody.AddTorque(_params.BidJumpTorque, ForceMode2D.Impulse);

            _lastJumpTime = Time.time;
        }
    }
}


