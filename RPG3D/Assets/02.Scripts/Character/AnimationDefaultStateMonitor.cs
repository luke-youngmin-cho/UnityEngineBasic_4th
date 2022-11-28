using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnimationDefaultStateMonitor : StateMachineBehaviour
{
    public event Action<int> OnEnter;
    public event Action<int> OnExit;
    public List<string> paramNames;

    public override void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {

        foreach (var paramName in paramNames)
        {
            if (string.IsNullOrEmpty(paramName) == false)
                animator.SetBool(paramName, false);
        }

        OnEnter?.Invoke(stateInfo.fullPathHash);
    }

    public override void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        OnExit?.Invoke(stateInfo.fullPathHash);
    }
}
