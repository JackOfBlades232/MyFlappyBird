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
    private float _pipeTravelDistance;

    [SerializeField]
    private float _pipeSetMinY, _pipeSetMaxY;

    [Header("Difficulty")]
    [SerializeField]
    private float _maxScoreForIncDifficulty;
    
    [SerializeField]
    private float _pipeBaseVelocity, _pipeMaxVelocity;

    [SerializeField]
    private float _pipeSpawnBaseCooldown, _pipeSpawnMinCooldown;

    [SerializeField]
    private float _pipeSetYBaseDelta, _pipeSetYMaxDelta;

    [SerializeField]
    private float _pipeSpaceSpread;

    [SerializeField]
    private float _pipeBaseAvgSpace, _pipeMinAvgSpace;
    
    public Vector2 BirdJumpImpulse => Vector2.up * _birdJumpImpulse;
    public float BirdRotationCeil => _birdRotationCeil;
    public float BirdJumpCooldown => _birdJumpCooldown;
    public float BirdWingsDownFrameDuration => _birdWingsDownFrameDuration;
    
    public float PipeTravelDistance => _pipeTravelDistance;
    public float PipeSetMinY => _pipeSetMinY;
    public float PipeSetMaxY => _pipeSetMaxY;

    public float PipeBaseVelocity => _pipeBaseVelocity;
    public float PipeMaxVelocity => _pipeMaxVelocity;
    public float PipeVelocityIncPerPoint =>
        (_pipeMaxVelocity - _pipeBaseVelocity) / _maxScoreForIncDifficulty;
    public float PipeSpawnBaseCooldown => _pipeSpawnBaseCooldown;
    public float PipeSpawnMinCooldown => _pipeSpawnMinCooldown;
    public float PipeSpawnCooldownDecPerPoint =>
        (_pipeSpawnBaseCooldown - _pipeSpawnMinCooldown) /
        _maxScoreForIncDifficulty;
    public float PipeSetYBaseDelta => _pipeSetYBaseDelta;
    public float PipeSetYMaxDelta => _pipeSetYMaxDelta;
    public float PipeSetYDeltaIncPerPoint =>
        (_pipeSetYMaxDelta - _pipeSetYBaseDelta) / _maxScoreForIncDifficulty;
    public float PipeSpaceSpread => _pipeSpaceSpread;
    public float PipeBaseAvgSpace => _pipeBaseAvgSpace;
    public float PipeMinAvgSpace => _pipeMinAvgSpace;
    public float PipeAvgSpaceDecPerPoint =>
        (_pipeBaseAvgSpace - _pipeMinAvgSpace) / _maxScoreForIncDifficulty;
}
