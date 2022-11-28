using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerStateAttack : StateBase<PlayerState>
{
    private Player _player;
    public PlayerStateAttack(PlayerState stateType, Func<bool> condition, List<KeyValuePair<Func<bool>, PlayerState>> transitions, GameObject owner) : base(stateType, condition, transitions, owner)
    {
        _player = owner.GetComponent<Player>();
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

    public override PlayerState Tick()
    {
        PlayerState next = stateType;

        switch (current)
        {
            case IState<PlayerState>.Commands.Idle:
                break;
            case IState<PlayerState>.Commands.Prepare:
                {
                    if (animationManager.GetBool("OnAttack") && 
                        animationManager.isPreviousStateHasFinished)
                    {
                        MouseTrigger.Triggered = false;
                        animationManager.SetBool("DoCombo", false);

                        MoveNext();
                    }
                }
                break;
            case IState<PlayerState>.Commands.Casting:
                {
                    if (animationManager.isCastingFinished)
                    {
                        foreach (var target in _player.GetTargetsCasted())
                        {
                            if (target.TryGetComponent(out IDamage idamage))
                            {
                                idamage.GetDamage(owner, target, _player.stats[Stat.ID_ATK].Value, false);
                            }
                        }


                        MoveNext();
                    }
                }
                break;
            case IState<PlayerState>.Commands.OnAction:
                {
                    if (animationManager.GetNormalizedTime() >= 1.0f)
                    {
                        MoveNext();
                    }
                    else if (animationManager.GetNormalizedTime() <= 0.8f && 
                             animationManager.GetBool("FinishCombo") == false &&
                             MouseTrigger.Triggered)
                    {
                        animationManager.SetBool("DoCombo", true);
                        current = IState<PlayerState>.Commands.Prepare;
                    }
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
