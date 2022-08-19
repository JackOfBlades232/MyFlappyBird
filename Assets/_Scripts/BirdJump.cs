using UnityEngine;
using UnityEngine.Events;
using DG.Tweening;
using TMPro;

[RequireComponent(typeof(BirdSpriteSwapper))]
[RequireComponent(typeof(Rigidbody2D))]
public class BirdJump : MonoBehaviour, IInitializable
{
    // TODO : pass from initializer
    [SerializeField]
    private GameParams _params;

    private BirdSpriteSwapper _spriteSwapper;

    private GroundStatic _groundStatic;

    private Rigidbody2D _rigidbody;

    private float _lastJumpTime;

    public UnityEvent OnJumpStarted;

    public UnityEvent OnHitGround;

    public void Initialize()
    {
        _spriteSwapper = GetComponent<BirdSpriteSwapper>();
        _rigidbody = GetComponent<Rigidbody2D>();
        
        _spriteSwapper.Initialize();
        _spriteSwapper.Construct(_params);

        _lastJumpTime = Time.time - _params.BirdJumpCooldown;
        SetAfterJumpRotate();
    }

    public void Construct(GroundStatic groundStatic) =>
        _groundStatic = groundStatic;
    
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
            .OnComplete(() => OnHitGround?.Invoke());
    }

    public void PerformJump()
    {
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
}
