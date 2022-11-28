using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class EnemyStateAttack : StateBase<EnemyStates>
{
    private CharacterBase _character;
    public EnemyStateAttack(EnemyStates stateType, Func<bool> condition, List<KeyValuePair<Func<bool>, EnemyStates>> transitions, GameObject owner) : base(stateType, condition, transitions, owner)
    {
        _character = owner.GetComponent<CharacterBase>();
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

    public override EnemyStates Tick()
    {
        EnemyStates next = stateType;

        if (current > IState<EnemyStates>.Commands.Prepare &&
            animationManager.GetBool("OnAttack") == false)
        {
            current = IState<EnemyStates>.Commands.Error;
        }

        switch (current)
        {
            case IState<EnemyStates>.Commands.Idle:
                break;
            case IState<EnemyStates>.Commands.Prepare:
                {
                    if (animationManager.GetBool("OnAttack"))
                        MoveNext();
                }
                break;
            case IState<EnemyStates>.Commands.Casting:
                {
                    if (animationManager.isCastingFinished)
                    {
                        Collider[] cols = Physics.OverlapBox(owner.transform.position + owner.transform.forward * 0.5f,
                                                        Vector3.one,
                                                        Quaternion.identity,
                                                        1<<10);

                        foreach (var col in cols)
                        {
                            if (col.gameObject.TryGetComponent(out IDamage idamage))
                            {
                                idamage.GetDamage(owner,
                                                  col.gameObject,
                                                  _character.stats[Stat.ID_ATK].Value,
                                                  false);
                            }
                        }
                                           


                        if (Physics.BoxCast(owner.transform.position + owner.transform.forward * 0.5f,
                                            Vector3.one,
                                            owner.transform.forward,
                                            out RaycastHit hitInfo,
                                            Quaternion.identity,
                                            0.0f))
                        {
                            if (hitInfo.collider.gameObject.TryGetComponent(out IDamage idamage))
                            {
                                idamage.GetDamage(owner,
                                                  hitInfo.collider.gameObject,
                                                  _character.stats[Stat.ID_ATK].Value,
                                                  false);
                            }
                        }

                        MoveNext();
                    }
                }
                break;
            case IState<EnemyStates>.Commands.OnAction:
                {
                    if (animationManager.GetNormalizedTime() >= 1.0f)
                        MoveNext();
                }
                break;
            case IState<EnemyStates>.Commands.Finish:
                {
                    next = EnemyStates.Idle;
                }
                break;
            case IState<EnemyStates>.Commands.WaitUntilFinished:
                break;
            case IState<EnemyStates>.Commands.Error:
                {
                    owner.GetComponent<StateMachineBase<EnemyStates>>().Reset();
                    next = default(EnemyStates);
                }
                break;
            default:
                break;
        }

        return next;
    }
}
