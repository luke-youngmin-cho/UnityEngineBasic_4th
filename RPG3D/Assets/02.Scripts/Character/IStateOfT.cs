using System.Collections.Generic;
using System;

public interface IState<T> where T : Enum
{
    public enum Commands
    {
        Idle,
        Prepare,
        Casting,
        OnAction,
        Finish,
        WaitUntilFinished
    }
    public Commands current { get; }
    public T stateType { get; }
    public bool canExecute { get; }
    public void Execute();

    public void Stop();
    public T Tick();
    public void MoveNext();
}