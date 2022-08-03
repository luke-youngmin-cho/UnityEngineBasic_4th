using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// 유한 상태 머신 (FSM : Finite State Machine)

public class GameManager : MonoBehaviour
{
    public enum State
    {
        Idle,
        WaitForSongSelected,
        StartGame,
        WaitForGameFinished,
        ShowScore
    }
    private State _state;

    private void Update()
    {
        WorkFlow();
    }

    private void WorkFlow()
    {
        switch (_state)
        {
            case State.Idle:
                break;
            case State.WaitForSongSelected:
                break;
            case State.StartGame:
                break;
            case State.WaitForGameFinished:
                break;
            case State.ShowScore:
                break;
            default:
                break;
        }
    }
}
