using System;
using System.Collections.Generic;
using UnityEngine;

public enum PlayerState
{
    Idle,
    Move,
    Jump,
    Fall,
    Attack,
}

public class PlayerStateMachine : StateMachineBase<PlayerState>
{
    public PlayerStateMachine(GameObject owner) : base(owner)
    {
    }

    protected override void InitStates()
    {
        IState<PlayerState> temp;

        // Idle 
        temp = new PlayerStateIdle(PlayerState.Idle,
                                   () => true,
                                   new List<KeyValuePair<Func<bool>, PlayerState>>()
                                   {
                                       new KeyValuePair<Func<bool>, PlayerState>
                                       (
                                           () => true,
                                           PlayerState.Move
                                       )
                                   },
                                   owner);
        states.Add(PlayerState.Idle, temp);

        // Move
        temp = new PlayerStateMove(PlayerState.Move,
                                   () => true,
                                   new List<KeyValuePair<Func<bool>, PlayerState>>(),
                                   owner);
        states.Add(PlayerState.Move, temp);
    }
}
