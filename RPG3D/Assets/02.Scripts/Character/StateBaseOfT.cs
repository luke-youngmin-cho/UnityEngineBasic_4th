using System;
using System.Collections.Generic;
using UnityEngine;

public abstract class StateBase<T> : IState<T> where T : Enum
{
    public IState<T>.Commands current { get; protected set; }

    public T stateType { get; protected set; }

    public bool canExecute => (condition != null ? condition.Invoke() : true) &&
                              animationManager.isPreviousStateMachineHasFinished &&
                              animationManager.isPreviousStateHasFinished;


    protected Func<bool> condition;
    protected List<KeyValuePair<Func<bool>, T>> transitions;
    protected AnimationManager animationManager;

    public StateBase(T stateType, Func<bool> condition, List<KeyValuePair<Func<bool>, T>> transitions, GameObject owner)
    {
        this.stateType = stateType;
        this.condition = condition;
        this.transitions = transitions;
        this.animationManager = owner.GetComponent<AnimationManager>();
    }

    public virtual void Execute()
    {
        current = IState<T>.Commands.Prepare;
    }
    public virtual void Stop()
    {
        current = IState<T>.Commands.Idle;
    }
    public virtual T Tick()
    {
        T next = stateType;

        switch (current)
        {
            case IState<T>.Commands.Idle:
                break;
            case IState<T>.Commands.Prepare:
                MoveNext();
                break;
            case IState<T>.Commands.Casting:
                MoveNext();
                break;
            case IState<T>.Commands.OnAction:
                {
                    if (animationManager.GetNormalizedTime() >= 0.95f)
                        MoveNext();
                }
                break;
            case IState<T>.Commands.Finish:
                MoveNext();
                break;
            case IState<T>.Commands.WaitUntilFinished:
                foreach (var transition in transitions)
                {
                    if (transition.Key.Invoke())
                    {
                        next = transition.Value;
                    }
                }
                break;
            default:
                break;
        }

        return next;
    }

    public void MoveNext()
    {
        if (current < IState<T>.Commands.WaitUntilFinished)
            current++;
    }
}