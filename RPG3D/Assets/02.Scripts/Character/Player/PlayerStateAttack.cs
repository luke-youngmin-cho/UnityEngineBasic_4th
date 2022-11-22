using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerStateAttack : StateBase<PlayerState>
{
    public PlayerStateAttack(PlayerState stateType, Func<bool> condition, List<KeyValuePair<Func<bool>, PlayerState>> transitions, GameObject owner) : base(stateType, condition, transitions, owner)
    {
    }
}
