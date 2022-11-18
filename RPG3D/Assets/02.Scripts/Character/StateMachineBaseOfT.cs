using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq.Expressions;
using UnityEngine;

public abstract class StateMachineBase<T> : IStateMachine<T> where T : Enum
{
    public T currentType { get; protected set; }
    public IState<T> current { get; protected set; }
    public Dictionary<T, IState<T>> states { get; protected set; }
    public GameObject owner;

    public StateMachineBase(GameObject owner)
    {
        this.owner = owner;
        states = new Dictionary<T, IState<T>>();

        InitStates();

        current = states[default(T)];
        currentType = default(T);
    }

    public void Tick()
    {
        ChangeState(current.Tick());
    }

    public void ChangeState(T newStateType)
    {
        if (EqualityComparer<T>.Default.Equals(currentType, newStateType))
            return;

        if (states[newStateType].canExecute)
        {
            current.Stop();
            current = states[newStateType];
            current.Execute();
            currentType = newStateType;
        }
    }
    public void ChangeState(object newStateType) => ChangeState((T)newStateType);

    protected abstract void InitStates();

}
