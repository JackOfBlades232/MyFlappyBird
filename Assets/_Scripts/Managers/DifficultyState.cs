using System.Collections.Generic;
using UnityEngine;

public class DifficultyState : IInitializable
{
    private GameParams _params;
    
    private float _pipeSetYDelta;
    private float _pipeAvgSpace;

    private IEnumerator<float> _pipeSetYGenerator;

    public float EnvVelocity { get; private set; }

    public float PipeSpawnCooldown =>
        _params.PipeSpawnDistanceInterval / EnvVelocity;

    public void Initialize()
    {
        EnvVelocity = _params.EnvBaseVelocity;
        _pipeSetYDelta = _params.PipeSetYBaseDelta;
        _pipeAvgSpace = _params.PipeBaseAvgSpace;

        _pipeSetYGenerator = GetPipeSetYGenerator();
    }

    public void Construct(GameParams gameParams) => _params = gameParams;

    #region DifficultyIncrease

    public void OnPipePassed()
    {
        IncreaseDifficultyByOnePoint();
        TruncateDifficulty();
    }

    private void IncreaseDifficultyByOnePoint()
    {
        EnvVelocity += _params.EnvVelocityIncPerPoint;
        _pipeSetYDelta += _params.PipeSetYDeltaIncPerPoint;
        _pipeAvgSpace -= _params.PipeAvgSpaceDecPerPoint;
    }

    private void TruncateDifficulty()
    {
        EnvVelocity = Mathf.Min(EnvVelocity, _params.EnvMaxVelocity);
        _pipeSetYDelta = Mathf.Min(_pipeSetYDelta, _params.PipeSetYMaxDelta);
        _pipeAvgSpace = Mathf.Max(_pipeAvgSpace, _params.PipeMinAvgSpace);
    }

    #endregion

    #region RandomGeneration

    public float GetNextPipeSetY()
    {
        _pipeSetYGenerator.MoveNext();

        return _pipeSetYGenerator.Current;
    }
    
    public float GetNextPipeSpace() => _pipeAvgSpace +
                                             Random.Range(
                                                 -_params.PipeSpaceSpread,
                                                 _params.PipeSpaceSpread) / 2;

    private IEnumerator<float> GetPipeSetYGenerator()
    {
        float pipeSetY =
            (_params.PipeSetMinY + _params.PipeSetMaxY) / 2;

        while (true)
        {
            pipeSetY += Random.Range(-_pipeSetYDelta, _pipeSetYDelta);
            pipeSetY = Mathf.Clamp(pipeSetY, _params.PipeSetMinY,
                _params.PipeSetMaxY);

            yield return pipeSetY;
        }
    }

    #endregion
}