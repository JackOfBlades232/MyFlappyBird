using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using UnityEngine;

public class PipeSpawner : MonoBehaviour, IInitializable
{
    // TODO : Refactor this?
    [SerializeField]
    private GameObject _pipeSetPrefab;

    // TODO : refactor with pool
    private readonly List<PipeSet> _spawnedPipes = new();

    private GameParams _params;

    private ScoreManager _scoreManager;

    private DifficultyState _difficultyState;

    public void Initialize()
    {
        StartCoroutine(DoSpawnPipes());
    }

    public void Construct(GameParams gameParams, ScoreManager scoreManager,
        DifficultyState difficultyState)
    {
        _params = gameParams;
        _scoreManager = scoreManager;
        _difficultyState = difficultyState;
    }

    private IEnumerator DoSpawnPipes()
    {
        while (true)
        {
            PipeSet pipeSet =
                Instantiate(_pipeSetPrefab, transform.position,
                    Quaternion.identity).GetComponent<PipeSet>();

            pipeSet.Construct(
                _params,
                this,
                _difficultyState.PipeLifetime,
                _difficultyState.GetNextPipeSetY(),
                _difficultyState.GetNextPipeSpace()
            );
            
            pipeSet.Initialize();
            
            _spawnedPipes.Add(pipeSet);

            yield return new WaitForSeconds(_difficultyState.PipeSpawnCooldown);
        }   
    }

    public void ReleasePipeSet(PipeSet pipeSet) =>
        _spawnedPipes.Remove(pipeSet);

    public void SendIncrementScore() => _scoreManager.IncrementScore();

    public void StopAllPipes()
    {
        foreach (PipeSet pipeSet in _spawnedPipes)
            pipeSet.transform.DOKill();
    }
}
