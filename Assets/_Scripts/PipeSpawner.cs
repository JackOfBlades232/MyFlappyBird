using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using UnityEngine;

public class PipeSpawner : MonoBehaviour, IInitializable
{
    [SerializeField]
    private GameParams _params;
    
    // TODO : Refactor this?
    [SerializeField]
    private GameObject _pipeSetPrefab;

    // TODO : refactor with pool
    private readonly List<PipeSet> _spawnedPipes = new();

    private ScoreManager _scoreManager;

    public void Initialize()
    {
        StartCoroutine(DoSpawnPipes());
    }

    public void Construct(ScoreManager scoreManager) =>
        _scoreManager = scoreManager;

    private IEnumerator DoSpawnPipes()
    {
        while (true)
        {
            PipeSet pipeSet =
                Instantiate(_pipeSetPrefab, transform.position,
                    Quaternion.identity).GetComponent<PipeSet>();
            
            pipeSet.Construct(this);
            pipeSet.Initialize();
            
            _spawnedPipes.Add(pipeSet);

            yield return new WaitForSeconds(_params.PipeSpawnBaseCooldown);
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