using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using UnityEngine;

public class TileSpawner : MonoBehaviour, IInitializable
{
    // TODO : Refactor this?
    [SerializeField]
    private Tile _initTilePrefab, _pipeTilePrefab;

    private const float SpawnIntervalReduction = 0.02f;

    // TODO : refactor with pool
    private readonly List<Tile> _spawnedTiles = new();

    private GameParams _params;

    private ScoreManager _scoreManager;

    private DifficultyState _difficultyState;

    private float TileSpawnInterval =>
        _pipeTilePrefab.Width / _difficultyState.TileVelocity -
        SpawnIntervalReduction;

    public void Initialize()
    {
        _difficultyState = new DifficultyState();
        
        _difficultyState.Construct(_params);
        _difficultyState.Initialize();

        SpawnFirstTile();
        StartCoroutine(DoSpawnTiles());
    }

    public void Construct(GameParams gameParams, ScoreManager scoreManager)
    {
        _params = gameParams;
        _scoreManager = scoreManager;
    }

    private void SpawnFirstTile()
    {
        // TODO : refactor spawning position
        Tile initTile =
            Instantiate(_initTilePrefab, Vector3.zero, Quaternion.identity)
                .GetComponent<Tile>();
        
        InitializeTile(initTile);
    }

    private IEnumerator DoSpawnTiles()
    {
        while (true)
        {
            Tile tile =
                Instantiate(_pipeTilePrefab, transform.position,
                    Quaternion.identity).GetComponent<Tile>();

            InitializeTile(tile);

            yield return new WaitForSeconds(TileSpawnInterval);
        }   
    }

    private void InitializeTile(Tile tile)
    {
        tile.Construct(_params, this, _difficultyState);
        tile.Initialize();
    }

    public void AddTile(Tile tile) => _spawnedTiles.Add(tile);

    public void ReleaseTile(Tile tile) => _spawnedTiles.Remove(tile);

    public void SendPipePassed()
    {
        // TODO : refactor this to events?
        _difficultyState.OnPipePassed();
        _scoreManager.IncrementScore();
    }

    public void StopAllTiles()
    {
        StopAllCoroutines();
        
        foreach (Tile tile in _spawnedTiles)
            tile.transform.DOKill();
    }
}
