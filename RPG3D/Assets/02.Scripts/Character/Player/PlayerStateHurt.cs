using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerStateHurt : StateBase<PlayerState>
{
    public PlayerStateHurt(PlayerState stateType, Func<bool> condition, List<KeyValuePair<Func<bool>, PlayerState>> transitions, GameObject owner) : base(stateType, condition, transitions, owner)
    {
    }

    public override void Execute()
    {
        current = IState<PlayerState>.Commands.OnAction;
        animationManager.SetBool("DoHurt", true);
    }

    public override PlayerState Tick()
    {
        PlayerState next = stateType;

        if (animationManager.GetNormalizedTime() >= 1.0f)
            next = PlayerState.Idle;

        return next;
    }
}
