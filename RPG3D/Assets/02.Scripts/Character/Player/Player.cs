using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Movement))]
public class Player : CharacterBase
{
    protected override IStateMachine CreateStateMachine()
    {
        return new PlayerStateMachine(gameObject);
    }
}
