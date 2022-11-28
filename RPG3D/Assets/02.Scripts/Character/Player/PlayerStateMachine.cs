using System;
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
    Hurt,
}

public class PlayerStateMachine : StateMachineBase<PlayerState>
{
    public PlayerStateMachine(GameObject owner) : base(owner)
    {
    }

    protected override void InitStates()
    {
        IState<PlayerState> temp;
        

        // Idle 
        temp = new PlayerStateIdle(PlayerState.Idle,
                                   () => true,
                                   new List<KeyValuePair<Func<bool>, PlayerState>>()
                                   {
                                       new KeyValuePair<Func<bool>, PlayerState>
                                       (
                                           () => true,
                                           PlayerState.Move
                                       )
                                   },
                                   owner);
        states.Add(PlayerState.Idle, temp);

        // Move
        temp = new PlayerStateMove(PlayerState.Move,
                                   () => true,
                                   new List<KeyValuePair<Func<bool>, PlayerState>>(),
                                   owner);
        states.Add(PlayerState.Move, temp);

        // Attack
        temp = new PlayerStateAttack(PlayerState.Attack,
                                     () => currentType == PlayerState.Idle || currentType == PlayerState.Move,
                                     new List<KeyValuePair<Func<bool>, PlayerState>>()
                                     {
                                         new KeyValuePair<Func<bool>, PlayerState>
                                         (
                                             () => true,
                                             PlayerState.Move
                                         )
                                     },
                                     owner);
        states.Add(PlayerState.Attack, temp);

        // Hurt
        temp = new PlayerStateHurt(PlayerState.Hurt,
                                   () => currentType == PlayerState.Idle || currentType == PlayerState.Move,
                                   new List<KeyValuePair<Func<bool>, PlayerState>>()
                                   {
                                       new KeyValuePair<Func<bool>, PlayerState>
                                       (
                                           () => true,
                                           PlayerState.Move
                                       )
                                   },
                                   owner);
        states.Add(PlayerState.Hurt, temp);

        owner.GetComponent<CharacterBase>().StartCoroutine(E_Init());
    }

    IEnumerator E_Init()
    {
        CharacterBase character = owner.GetComponent<CharacterBase>();

        MouseTrigger.OnTriggerActive += () => ChangeState(PlayerState.Attack);
        yield return new WaitUntil(() => character.stats != null);   
        character.stats[Stat.ID_HP].OnValueDecreased += (value) => ChangeState(PlayerState.Hurt);        
    }
}
