using System;
using UnityEngine;

public class AnimatorStateMachineMonitor : StateMachineBehaviour
{
    public event Action<int> OnEnter;
    public event Action<int> OnExit;
    public string paramName;

    public override void OnStateMachineEnter(Animator animator, int stateMachinePathHash)
    {
        if (string.IsNullOrEmpty(paramName) == false)
            animator.SetBool("paramName", true);

        OnEnter?.Invoke(stateMachinePathHash);
    }

    public override void OnStateMachineExit(Animator animator, int stateMachinePathHash)
    {
        if (string.IsNullOrEmpty(paramName) == false)
            animator.SetBool("paramName", false);

        OnExit?.Invoke(stateMachinePathHash);
    }
}