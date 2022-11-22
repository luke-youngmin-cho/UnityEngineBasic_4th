using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerStateMove : StateBase<PlayerState>
{
    private Movement _movement;
    public PlayerStateMove(PlayerState stateType, Func<bool> condition, List<KeyValuePair<Func<bool>, PlayerState>> transitions, GameObject owner) : base(stateType, condition, transitions, owner)
    {
        _movement = owner.GetComponent<Movement>();
    }

    public override void Execute()
    {
        base.Execute();
        animationManager.SetBool("DoMove", true);
    }

    public override void Stop()
    {
        base.Stop();
        animationManager.SetBool("DoMove", false);
    }

    public override PlayerState Tick()
    {
        PlayerState next = stateType;
        animationManager.SetFloat("H", _movement.h);
        animationManager.SetFloat("V", _movement.v);
        return next;
    }
}
