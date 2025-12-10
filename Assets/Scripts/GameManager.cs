using System;
using UnityEngine;
using Random = UnityEngine.Random;

public delegate void OnUpdateBoardDataHandler();

public delegate void OnSwitchToPlayingStateHandler();
public delegate void OnSwitchToFailedStateHandler();

public delegate void OnSwitchToVictoryStateHandler();

public enum GameState {
    Playing,
    Failed,
    Victory
}


public class GameManager : Singletone<GameManager>
{
    public GameState gameState = GameState.Playing;
    
    public bool cheatMode = false;
    public int[][] Board;
    public const int GridRow = 9;
    public const int GridCol = 9;
    public const int BombNum = 14;

    public int foundBomb = 0;
    public float timePast = 0;

    public Action OnSwitchToPlayingStateHandler = ()=>{ Debug.Log("Playing Mode"); };
    public Action OnSwitchToFailedStateHandler = ()=> { };
    public Action OnSwitchToVictoryStateHandler = ()=> { };

    private void Awake()
    {
        OnSwitchToPlayingStateHandler += () =>
        {
            gameState = GameState.Playing;
            
            timePast = 0;
            foundBomb = 0;
        };
    }

    private void Update()
    {
        timePast += Time.deltaTime;

        if (Input.GetKeyDown(KeyCode.Escape))
        {
            cheatMode = !cheatMode;
        }
    }

    void Start()
    {
        Play();
    }

    public void Play()
    {
        gameState = GameState.Playing;
        OnSwitchToPlayingStateHandler();
    }

    public void Victory()
    {
        gameState = GameState.Victory;
        OnSwitchToVictoryStateHandler();
    }

    public void Failed()
    {
        gameState = GameState.Failed;
        OnSwitchToFailedStateHandler();
    }

    public void Reset()
    {
        Play();
    }
}
