using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyStateAttack : StateBase<EnemyStates>
{
    public EnemyStateAttack(EnemyStates stateType, Func<bool> condition, List<KeyValuePair<Func<bool>, EnemyStates>> transitions, GameObject owner) : base(stateType, condition, transitions, owner)
    {
    }
    public override void Execute()
    {
        base.Execute();
        animationManager.SetBool("DoAttack", true);
    }

    public override void Stop()
    {
        base.Stop();
        animationManager.SetBool("DoAttack", false);
    }
}
