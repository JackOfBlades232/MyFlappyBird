using UnityEngine;

[CreateAssetMenu(
    fileName = "New GameParams",
    menuName = "GameParams",
    order = 101)]
public class GameParams : ScriptableObject
{
    [Header("Bird jumping")]
    [SerializeField]
    private float _birdJumpImpulse;

    [SerializeField]
    private float _birdRotationCeil;

    [SerializeField]
    private float _birdJumpCooldown;

    [SerializeField]
    private float _birdWingsDownFrameDuration;

    [Header("Pipe spawning")]
    [SerializeField]
    private float _pipeSpawnBaseCooldown;

    [SerializeField]
    private float _pipeTravelDistance;

    [SerializeField]
    private float _pipeBaseVelocity;
    
    public Vector2 BirdJumpImpulse => Vector2.up * _birdJumpImpulse;
    public float BirdRotationCeil => _birdRotationCeil;
    public float BirdJumpCooldown => _birdJumpCooldown;
    public float BirdWingsDownFrameDuration => _birdWingsDownFrameDuration;

    public float PipeSpawnBaseCooldown => _pipeSpawnBaseCooldown;
    public float PipeTravelDistance => _pipeTravelDistance;
    public float PipeBaseLifetime => _pipeTravelDistance / _pipeBaseVelocity;
}
