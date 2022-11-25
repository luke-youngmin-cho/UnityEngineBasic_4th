using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyStateHurt : StateBase<EnemyStates>
{
    public EnemyStateHurt(EnemyStates stateType, Func<bool> condition, List<KeyValuePair<Func<bool>, EnemyStates>> transitions, GameObject owner) : base(stateType, condition, transitions, owner)
    {
    }

    public override void Execute()
    {
        base.Execute();
        animationManager.SetBool("DoHurt", true);
    }

    public override void Stop()
    {
        base.Stop();
        animationManager.SetBool("DoHurt", false);
    }

    public override EnemyStates Tick()
    {
        EnemyStates next = stateType;

        switch (current)
        {
            case IState<EnemyStates>.Commands.Idle:
                break;
            case IState<EnemyStates>.Commands.Prepare:
                {
                    if (animationManager.GetBool("OnHurt"))
                        current = IState<EnemyStates>.Commands.OnAction;
                }
                break;
            case IState<EnemyStates>.Commands.Casting:
                break;
            case IState<EnemyStates>.Commands.OnAction:
                {
                    if (animationManager.GetNormalizedTime() >= 1.0f)
                        MoveNext();
                }
                break;
            case IState<EnemyStates>.Commands.Finish:
                {
                    MoveNext();
                }
                break;
            case IState<EnemyStates>.Commands.WaitUntilFinished:
                {
                    foreach (var transition in transitions)
                    {
                        if (transition.Key.Invoke())
                        {
                            next = transition.Value;
                            break;
                        }    
                    }
                }
                break;
            default:
                break;
        }

        return next;
    }
}
