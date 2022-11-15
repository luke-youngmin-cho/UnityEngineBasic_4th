using System;
using System.Collections.Generic;
using UnityEngine;

public interface IStateMachine<T> : IStateMachine where T : Enum
{
    public T currentType { get; }
    public IState<T> current { get; }
    public Dictionary<T, IState<T>> states { get; }
    public void ChangeState(T newStateType);
}
