using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PipeSpawner : MonoBehaviour, IInitializable {
    // TODO : Refactor this?
    [SerializeField]
    private GameObject _pipeSetPrefab, _movingFloorPrefab;
    
    // TODO : refactor with pool
    private readonly List<PipeSet> _spawnedPipes = new();

    private GameParams _params;

    private ScoreManager _scoreManager;

    private DifficultyState _difficultyState;

    private MovingFloor _movingFloor;

    public void Initialize()
    {
        _difficultyState = new DifficultyState();
        
        _difficultyState.Construct(_params);
        _difficultyState.Initialize();
        
        SpawnFloor();
        StartCoroutine(DoSpawnPipes());
    }

    public void Construct(GameParams gameParams, ScoreManager scoreManager)
    {
        _params = gameParams;
        _scoreManager = scoreManager;
    }

    private void SpawnFloor()
    {
        _movingFloor =
            Instantiate(_movingFloorPrefab, Vector3.zero, Quaternion.identity)
                .GetComponent<MovingFloor>();
        
        _movingFloor.Initialize();
        _movingFloor.UpdateVelocity(_difficultyState.EnvVelocity);
    }

    private IEnumerator DoSpawnPipes()
    {
        while (true)
        {
            PipeSet pipeSet =
                Instantiate(_pipeSetPrefab, transform.position,
                    Quaternion.identity).GetComponent<PipeSet>();

            InitializePipeSet(pipeSet);

            yield return new WaitForSeconds(_difficultyState.PipeSpawnCooldown);
        }   
    }

    private void InitializePipeSet(PipeSet pipeSet)
    {
        pipeSet.Construct(
            _params,
            this,
            _difficultyState.GetNextPipeSetY(),
            _difficultyState.GetNextPipeSpace()
        );
        
        pipeSet.Initialize();
        pipeSet.UpdateVelocity(_difficultyState.EnvVelocity);
    }

    public void AddPipeSet(PipeSet pipeSet) => _spawnedPipes.Add(pipeSet);

    public void ReleasePipeSet(PipeSet pipeSet) =>
        _spawnedPipes.Remove(pipeSet);

    public void SendPipePassed()
    {
        // TODO : refactor this to events?
        _difficultyState.OnPipePassed();
        _scoreManager.IncrementScore();

        _movingFloor.UpdateVelocity(_difficultyState.EnvVelocity);
        
        foreach (PipeSet pipeSet in _spawnedPipes)
            pipeSet.UpdateVelocity(_difficultyState.EnvVelocity);
    }

    public void StopAllTiles()
    {
        StopAllCoroutines();
        
        _movingFloor.Deactivate();

        foreach (PipeSet pipeSet in _spawnedPipes)
            pipeSet.Deactivate();
    }
}
