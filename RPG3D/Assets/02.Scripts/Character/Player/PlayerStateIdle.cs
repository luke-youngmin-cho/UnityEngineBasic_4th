using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerStateIdle : StateBase<PlayerState>
{
    public PlayerStateIdle(PlayerState stateType, Func<bool> condition, List<KeyValuePair<Func<bool>, PlayerState>> transitions, GameObject owner) : base(stateType, condition, transitions, owner)
    {
    }

    public override void Execute()
    {
        base.Execute();
        animationManager.SetBool("DoIdle", true);
    }

    public override void Stop()
    {
        base.Stop();
        animationManager.SetBool("DoIdle", false);
    }

    public override PlayerState Tick()
    {
        foreach (var transition in transitions)
        {
            if (transition.Key.Invoke())
            {
                return transition.Value;
            }
        }
        return stateType;
    }
}
