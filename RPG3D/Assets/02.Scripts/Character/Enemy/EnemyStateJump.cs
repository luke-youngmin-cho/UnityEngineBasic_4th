using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyStateJump : StateBase<EnemyStates>
{
    public EnemyStateJump(EnemyStates stateType, Func<bool> condition, List<KeyValuePair<Func<bool>, EnemyStates>> transitions, GameObject owner) : base(stateType, condition, transitions, owner)
    {
    }
}
