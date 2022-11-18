using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyStateDie : StateBase<EnemyStates>
{
    public EnemyStateDie(EnemyStates stateType, Func<bool> condition, List<KeyValuePair<Func<bool>, EnemyStates>> transitions, GameObject owner) : base(stateType, condition, transitions, owner)
    {
    }

    public override void Execute()
    {
        base.Execute();
        animationManager.SetBool("DoDie", true);
    }

    public override void Stop()
    {
        base.Stop();
        animationManager.SetBool("DoDie", false);
    }

    public override EnemyStates Tick()
    {
        EnemyStates next = stateType;
        return next;
    }
}
