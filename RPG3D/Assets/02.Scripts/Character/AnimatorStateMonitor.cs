﻿using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnimatorStateMonitor : StateMachineBehaviour
{
    public event Action<int> OnEnter;
    public event Action<int> OnExit;
    public string boolParameterName;

    public override void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        if (string.IsNullOrEmpty(boolParameterName))
            animator.SetBool(boolParameterName, true);
        OnEnter?.Invoke(stateInfo.fullPathHash);
    }

    public override void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        if (string.IsNullOrEmpty(boolParameterName))
            animator.SetBool(boolParameterName, false);
        OnExit?.Invoke(stateInfo.fullPathHash);
    }
}