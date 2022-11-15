using System;
using System.Collections.Generic;
using UnityEngine;

public abstract class StateBase<T> : IState<T> where T : Enum
{
    public IState<T>.Commands current { get; protected set; }

    public T stateType { get; protected set; }

    public bool canExecute => (_condition != null ? _condition.Invoke() : true) &&
                              _animationManager.isPreviousStateMachineHasFinished &&
                              _animationManager.isPreviousStateHasFinished;


    private Func<bool> _condition;
    private List<KeyValuePair<Func<bool>, T>> _transitions;
    private AnimationManager _animationManager;

    public StateBase(T stateType, Func<bool> condition, List<KeyValuePair<Func<bool>, T>> transitions, GameObject owner)
    {
        this.stateType = stateType;
        this._condition = condition;
        this._transitions = transitions;
        this._animationManager = owner.GetComponent<AnimationManager>();
    }

    public virtual void Execute()
    {
        current = IState<T>.Commands.Prepare;
    }
    public virtual void Stop()
    {
        
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
                MoveNext();
                break;
            case IState<T>.Commands.Finish:
                MoveNext();
                break;
            case IState<T>.Commands.WaitUntilFinished:
                foreach (var transition in _transitions)
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