using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
public class MovingFloor : MonoBehaviour, IInitializable
{
    [SerializeField]
    private SpriteRenderer _tileRenderer;
    
    private Rigidbody2D _rigidbody;

    private Vector3 _initPosition;

    private float Width => _tileRenderer.bounds.size.x;

    private void Update() => CheckResetPosition();

    public void Initialize()
    {
        _rigidbody = GetComponent<Rigidbody2D>();

        _initPosition = transform.position;
    }

    private void CheckResetPosition()
    {
        if (_initPosition.x - transform.position.x >= Width)
            transform.Translate(Width, 0, 0);
    }

    public void Deactivate() => UpdateVelocity(0);
    
    public void UpdateVelocity(float value) =>
        _rigidbody.velocity = value * Vector2.left;
}
