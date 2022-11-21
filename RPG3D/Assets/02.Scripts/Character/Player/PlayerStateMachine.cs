using System.Collections;
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
        throw new System.NotImplementedException();
    }
}
