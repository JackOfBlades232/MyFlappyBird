﻿using UnityEngine;
using UnityEngine.Events;

[RequireComponent(typeof(BirdJump))]
public class PlayerFacade : MonoBehaviour, IInitializable
{
    private GameParams _params;
    
    private PipeSpawner _pipeSpawner;
    
    private BirdJump _jumper;

    private GroundStatic _groundStatic;

    public UnityEvent OnKilled, OnFallen;

    public void Initialize()
    {
        _jumper = GetComponent<BirdJump>();
        _groundStatic = FindObjectOfType<GroundStatic>();
        
        _groundStatic.Initialize();
        
        _jumper.Construct(_params, _groundStatic);
        _jumper.Initialize();
        
        OnKilled.AddListener(_pipeSpawner.StopAllPipes);
        
        _jumper.OnReachedGround.AddListener(() => OnFallen?.Invoke());
    }

    public void Construct(GameParams gameParams, PipeSpawner pipeSpawner)
    {
        _params = gameParams;
        _pipeSpawner = pipeSpawner;
    }

    public void Kill()
    {
        OnKilled?.Invoke();
        
        _jumper.Kill();
    }
}