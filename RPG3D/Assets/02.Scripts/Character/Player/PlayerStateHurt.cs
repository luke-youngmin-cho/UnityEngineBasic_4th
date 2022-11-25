using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerStateHurt : StateBase<PlayerState>
{
    public PlayerStateHurt(PlayerState stateType, Func<bool> condition, List<KeyValuePair<Func<bool>, PlayerState>> transitions, GameObject owner) : base(stateType, condition, transitions, owner)
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

    public override PlayerState Tick()
    {
        PlayerState next = stateType;

        switch (current)
        {
            case IState<PlayerState>.Commands.Idle:
                break;
            case IState<PlayerState>.Commands.Prepare:
                {
                    if (animationManager.GetBool("OnHurt"))
                    {
                        current = IState<PlayerState>.Commands.OnAction;
                    }
                }
                break;
            case IState<PlayerState>.Commands.Casting:
                break;
            case IState<PlayerState>.Commands.OnAction:
                {
                    if (animationManager.GetNormalizedTime() >= 1.0f)
                        MoveNext();
                }
                break;
            case IState<PlayerState>.Commands.Finish:
                {
                    MoveNext();
                }
                break;
            case IState<PlayerState>.Commands.WaitUntilFinished:
                {
                    foreach (var transition in transitions)
                    {
                        if (transition.Key.Invoke())
                        {
                            next = transition.Value;
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
