using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum EnemyStates
{
    Idle,
    Move,
    Jump,
    Attack,
    Hurt,
    Die,
}

public class EnemyStateMachine : StateMachineBase<EnemyStates>
{
    public EnemyStateMachine(GameObject owner) : base(owner)
    {
    }

    protected override void InitStates()
    {
        IState<EnemyStates> temp;
        GroundDetector groundDetector = owner.GetComponent<GroundDetector>();
        AnimationManager animationManager = owner.GetComponent<AnimationManager>();

        // Idle
        temp = new EnemyStateIdle(EnemyStates.Idle,
                                  () => true,
                                  new List<KeyValuePair<Func<bool>, EnemyStates>>(),
                                  owner);
        states.Add(EnemyStates.Idle, temp);

        // Move
        temp = new EnemyStateMove(EnemyStates.Move,
                                  () => true,
                                  new List<KeyValuePair<Func<bool>, EnemyStates>>(),
                                  owner);
        states.Add(EnemyStates.Move, temp);

        // Jump
        temp = new EnemyStateJump(stateType: EnemyStates.Jump,
                                  condition: () =>
                                             (currentType == EnemyStates.Idle || currentType == EnemyStates.Move) &&
                                             groundDetector.IsDetected,
                                  transitions: new List<KeyValuePair<Func<bool>, EnemyStates>>()
                                               {
                                                   new KeyValuePair<Func<bool>, EnemyStates>
                                                   (
                                                     () => groundDetector.IsDetected,
                                                     EnemyStates.Move
                                                   )
                                               },
                                  owner: owner);

        states.Add(EnemyStates.Jump, temp);

        // Attack
        temp = new EnemyStateAttack(stateType: EnemyStates.Attack,
                                    condition: () =>
                                               (currentType == EnemyStates.Idle || currentType == EnemyStates.Move),
                                    transitions: new List<KeyValuePair<Func<bool>, EnemyStates>>()
                                                 {
                                                     new KeyValuePair<Func<bool>, EnemyStates>
                                                     (
                                                       () => animationManager.GetNormalizedTime() >= 1.0f,
                                                       EnemyStates.Move
                                                     )
                                                 },
                                    owner: owner);

        states.Add(EnemyStates.Attack, temp);

        // Hurt
        temp = new EnemyStateHurt(stateType: EnemyStates.Hurt,
                                  condition: () => true,
                                  transitions: new List<KeyValuePair<Func<bool>, EnemyStates>>()
                                               {
                                                   new KeyValuePair<Func<bool>, EnemyStates>
                                                   (
                                                     () => animationManager.GetNormalizedTime() >= 1.0f,
                                                     EnemyStates.Move
                                                   )
                                               },
                                  owner: owner);

        states.Add(EnemyStates.Hurt, temp);

        // Die
        temp = new EnemyStateDie(stateType: EnemyStates.Die,
                                  condition: () => true,
                                  transitions: new List<KeyValuePair<Func<bool>, EnemyStates>>(),
                                  owner: owner);

        states.Add(EnemyStates.Die, temp);
    }
}
