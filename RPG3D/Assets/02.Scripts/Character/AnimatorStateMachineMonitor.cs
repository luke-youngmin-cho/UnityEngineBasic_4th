using System;
using UnityEngine;

public class AnimatorStateMachineMonitor : StateMachineBehaviour
{
    public event Action<int> OnEnter;
    public event Action<int> OnExit;

    public override void OnStateMachineEnter(Animator animator, int stateMachinePathHash)
    {
        OnEnter?.Invoke(stateMachinePathHash);
    }

    public override void OnStateMachineExit(Animator animator, int stateMachinePathHash)
    {
        OnExit?.Invoke(stateMachinePathHash);
    }
}