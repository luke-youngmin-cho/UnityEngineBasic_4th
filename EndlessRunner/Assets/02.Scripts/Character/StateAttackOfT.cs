using System;
using System.Diagnostics;

public class StateAttack<T> : StateBase<T> where T : Enum
{
    private CharacterBase _character;
    public StateAttack(StateMachineBase<T> stateMachine, T machineState, T canExecuteConditionMask, T nextTarget) 
        : base(stateMachine, machineState, canExecuteConditionMask, nextTarget)
    {
        _character = stateMachine.owner.GetComponent<CharacterBase>();
    }

    public override bool canExecute => base.canExecute &&
                                       _character.target;

    public override void Execute()
    {
        base.Execute();
        animationManager.SetBool("DoAttack", true);
    }

    public override void Reset()
    {
        base.Reset();
        animationManager.SetBool("DoAttack", false);
    }

    public override T Update()
    {
        T next = machineState;

        switch (current)
        {
            case IState<T>.Commands.Idle:
                break;
            case IState<T>.Commands.Prepare:
                {
                    MoveNext();
                }
                break;
            case IState<T>.Commands.Casting:
                {
                    MoveNext();
                }
                break;
            case IState<T>.Commands.WaitForCastingFinished:
                {
                    if (animationManager.isCastingFinished)
                    {
                        animationManager.SetBool("DoAttack", false);
                        if (_character.target.TryGetComponent(out IHp hp))
                        {
                            hp.hp -= _character.atk;
                            UnityEngine.Debug.Log("Attack!");
                        }
                        MoveNext();
                    }
                }
                break;
            case IState<T>.Commands.Action:
                {
                    MoveNext();
                }
                break;
            case IState<T>.Commands.WaitForActionFinished:
                {
                    if (animationManager.GetNormalizedTime() > 1.0f)
                        MoveNext();
                }
                break;
            case IState<T>.Commands.Finish:
                {
                    next = nextTarget;
                }
                break;
            case IState<T>.Commands.WaitForFinished:
                break;
            default:
                break;
        }

        return next;
    }
}
