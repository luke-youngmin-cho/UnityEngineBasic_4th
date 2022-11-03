using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum GameStates
{
    Idle,
    Play,
    Paused,
}

public class GameStateManager
{
    private static GameStateManager _instance;
    public static GameStateManager instance
    {
        get
        {
            if (_instance == null)
                _instance = new GameStateManager();
            return _instance;
        }
    }

    public GameStates Current { get; private set; }
    public event Action<GameStates> OnStateChanged;

    public void SetState(GameStates newState)
    {
        if (Current == newState) 
            return;

        Current = newState;
        OnStateChanged?.Invoke(newState);
    }
}
