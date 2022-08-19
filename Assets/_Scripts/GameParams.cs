using UnityEngine;

[CreateAssetMenu(
    fileName = "New GameParams",
    menuName = "GameParams",
    order = 101)]
public class GameParams : ScriptableObject
{
    [SerializeField]
    private float _birdJumpImpulse, _birdJumpTorque;

    [SerializeField]
    private float _birdJumpCooldown;

    [SerializeField]
    private float _birdWingsDownFrameDuration;

    public Vector2 BirdJumpImpulse => Vector2.up * _birdJumpImpulse;
    public float BidJumpTorque => _birdJumpTorque;
    public float BirdJumpCooldown => _birdJumpCooldown;
    public float BirdWingsDownFrameDuration => _birdWingsDownFrameDuration;
}
