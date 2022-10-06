using UnityEngine;
using UnityEngine.Serialization;

[CreateAssetMenu(
    fileName = "New GameParams",
    menuName = "GameParams",
    order = 101)]
public class GameParams : ScriptableObject
{
    [Header("Ads")]
    [SerializeField]
    private bool _isTestAdsMode;

    [SerializeField]
    private float _interstitialProbability;

    [SerializeField]
    private float _delayBeforeAd;

    [SerializeField]
    private float _minInterstitialInterval;

    [Header("Bird jumping")]
    [SerializeField]
    private float _birdJumpImpulse;

    [SerializeField]
    private float _birdRotationCeil;

    [SerializeField]
    private float _birdJumpCooldown;

    [Header("Pipe spawning")]
    [SerializeField]
    private float _pipeDespawnX;

    [SerializeField]
    private float _pipeSetMinY, _pipeSetMaxY;

    [Header("Difficulty")]
    [SerializeField]
    private float _maxScoreForIncDifficulty;

    [SerializeField]
    private float _pipeSpawnDistanceInterval;

    [SerializeField]
    private float _envBaseVelocity, _envMaxVelocity; // 1 4

    [SerializeField]
    private float _pipeSetYBaseDelta, _pipeSetYMaxDelta;

    [SerializeField]
    private float _pipeSpaceSpread;

    [SerializeField]
    private float _pipeBaseAvgSpace, _pipeMinAvgSpace;

    [SerializeField]
    private int _goldMedalScoreThreshold;


    public bool IsTestAdsMode => _isTestAdsMode;
    public float InterstitialProbability => _interstitialProbability;
    public float DelayBeforeAd => _delayBeforeAd;
    public float MinInterstitialInterval => _minInterstitialInterval;

    public Vector2 BirdJumpImpulse => Vector2.up * _birdJumpImpulse;
    public float BirdRotationCeil => _birdRotationCeil;
    public float BirdJumpCooldown => _birdJumpCooldown;
    
    public float PipeDespawnX => _pipeDespawnX;
    public float PipeSetMinY => _pipeSetMinY;
    public float PipeSetMaxY => _pipeSetMaxY;

    public float PipeSpawnDistanceInterval => _pipeSpawnDistanceInterval;
    public float EnvBaseVelocity => _envBaseVelocity;
    public float EnvMaxVelocity => _envMaxVelocity;
    public float EnvVelocityIncPerPoint =>
        (_envMaxVelocity - _envBaseVelocity) / _maxScoreForIncDifficulty;
    public float PipeSetYBaseDelta => _pipeSetYBaseDelta;
    public float PipeSetYMaxDelta => _pipeSetYMaxDelta;
    public float PipeSetYDeltaIncPerPoint =>
        (_pipeSetYMaxDelta - _pipeSetYBaseDelta) / _maxScoreForIncDifficulty;
    public float PipeSpaceSpread => _pipeSpaceSpread;
    public float PipeBaseAvgSpace => _pipeBaseAvgSpace;
    public float PipeMinAvgSpace => _pipeMinAvgSpace;
    public float PipeAvgSpaceDecPerPoint =>
        (_pipeBaseAvgSpace - _pipeMinAvgSpace) / _maxScoreForIncDifficulty;

    public int GoldMedalScoreThreshold => _goldMedalScoreThreshold;
}
