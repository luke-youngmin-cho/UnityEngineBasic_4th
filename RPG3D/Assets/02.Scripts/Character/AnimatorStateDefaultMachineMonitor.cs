using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnimatorStateDefaultMachineMonitor : StateMachineBehaviour
{
    public event Action<int> OnEnter;
    public event Action<int> OnExit;
    public List<string> paramNames;

    public override void OnStateMachineEnter(Animator animator, int stateMachinePathHash)
    {
        foreach (var paramName in paramNames)
        {
            if (string.IsNullOrEmpty(paramName) == false)
                animator.SetBool(paramName, false);
        }

        OnEnter?.Invoke(stateMachinePathHash);
    }

    public override void OnStateMachineExit(Animator animator, int stateMachinePathHash)
    {
        OnExit?.Invoke(stateMachinePathHash);
    }
}
