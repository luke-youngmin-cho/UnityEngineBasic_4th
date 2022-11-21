using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerStateMove : StateBase<PlayerState>
{
    private CharacterBase _character;
    public PlayerStateMove(PlayerState stateType, Func<bool> condition, List<KeyValuePair<Func<bool>, PlayerState>> transitions, GameObject owner) : base(stateType, condition, transitions, owner)
    {
        owner.GetComponent<CharacterBase>();
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
        return next;
    }
}
