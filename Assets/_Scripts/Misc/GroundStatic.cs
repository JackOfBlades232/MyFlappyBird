using UnityEngine;

[RequireComponent(typeof(BoxCollider2D))]
public class GroundStatic : MonoBehaviour, IInitializable
{
    private BoxCollider2D _collider;

    public float UpperBoundY => transform.position.y + _collider.offset.y +
                                _collider.size.y / 2;

    public void Initialize() => _collider = GetComponent<BoxCollider2D>();
}