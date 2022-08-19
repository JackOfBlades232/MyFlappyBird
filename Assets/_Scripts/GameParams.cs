using UnityEngine;

[CreateAssetMenu(
    fileName = "New GameParams",
    menuName = "GameParams",
    order = 101)]
public class GameParams : ScriptableObject
{
    [SerializeField]
    private float _birdJumpImpulse;

    [SerializeField]
    private float _birdAngularVelocityModifierTop,
        _birdAngularVelocityModifierBottom;

    [SerializeField]
    private float _birdRotationCeil;

    [SerializeField]
    private float _birdJumpCooldown;

    [SerializeField]
    private float _birdWingsDownFrameDuration;

    public Vector2 BirdJumpImpulse => Vector2.up * _birdJumpImpulse;
    public float BirdAngularVelocityModifierBottom =>
        _birdAngularVelocityModifierBottom;
    public float BirdAngularVelocityModifierTop =>
        _birdAngularVelocityModifierTop;
    public float BirdRotationCeil => _birdRotationCeil;
    public float BirdJumpCooldown => _birdJumpCooldown;
    public float BirdWingsDownFrameDuration => _birdWingsDownFrameDuration;
}
