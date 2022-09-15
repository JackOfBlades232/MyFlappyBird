using UnityEngine;
using UnityEngine.Events;
using DG.Tweening;

[RequireComponent(typeof(Rigidbody2D))]
public class BirdJump : MonoBehaviour, IInitializable
{
    private GameParams _params;
    private PlayerFacade _player;
    private GroundStatic _groundStatic;

    private Rigidbody2D _rigidbody;

    private float _lastJumpTime;
    private bool _isDead;

    public UnityEvent OnJumpStarted;
    public UnityEvent OnReachedGround;

    public void Initialize()
    {
        _rigidbody = GetComponent<Rigidbody2D>();

        OnJumpStarted.AddListener(() =>
            AudioManager.Instance.PlaySound(SoundType.Flap));
        OnReachedGround.AddListener(() =>
            AudioManager.Instance.PlaySound(SoundType.Die));
        
        _isDead = false;
        _lastJumpTime = Time.time - _params.BirdJumpCooldown - Utils.Precision;
        
        SetAfterJumpRotate();
    }

    public void Construct(GameParams gameParams, PlayerFacade player,
        GroundStatic groundStatic)
    {
        _params = gameParams;
        _player = player;
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

        transform.DORotate(finalRotation, fallTime).OnComplete(HitGround);
    }

    private void HitGround()
    {
        OnReachedGround?.Invoke();

        if (_isDead)
            return;
        
        SetIsDead();
        
        _player.Kill();
    }

    private void SetIsDead()
    {
        _isDead = true;
        
        _rigidbody.velocity = Vector2.zero;
    }

    public void PerformJump()
    {
        if (_isDead)
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

    public void KillJumping()
    {
        if (_isDead)
            return;
        
        SetIsDead();

        SetAfterJumpRotate();
    }
}
