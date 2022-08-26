using UnityEngine;
using UnityEngine.Serialization;

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
    private float _tileDespawnX;

    [SerializeField]
    private float _pipeSetMinY, _pipeSetMaxY;

    [Header("Difficulty")]
    [SerializeField]
    private float _maxScoreForIncDifficulty;
    
    [SerializeField]
    private float _tileBaseVelocity, _tileMaxVelocity; // 1 4

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
    
    public float TileDespawnX => _tileDespawnX;
    public float PipeSetMinY => _pipeSetMinY;
    public float PipeSetMaxY => _pipeSetMaxY;

    public float TileBaseVelocity => _tileBaseVelocity;
    public float TileMaxVelocity => _tileMaxVelocity;
    public float TileVelocityIncPerPoint =>
        (_tileMaxVelocity - _tileBaseVelocity) / _maxScoreForIncDifficulty;
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
