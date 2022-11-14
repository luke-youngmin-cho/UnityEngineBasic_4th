using System;
using System.Collections.Generic;
using UnityEngine;

public class StateBase<T> : IState<T> where T : Enum
{
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

    public void Execute()
    {
        Workflow().Reset();
    }
    public void Stop()
    {
        Workflow().Reset();
    }
    public T Tick()
    {
        return Workflow().Current;
    }

    public IEnumerator<T> Workflow()
    {
        // 다음 상태로 transition 가능한지 체크.. (가장 마지막 상태에 구현해야하는 내용)
        while (true)
        {
            foreach (var transition in _transitions)
            {
                if (transition.Key.Invoke())
                    yield return transition.Value;
            }
            yield return stateType;
        }
    }   
}