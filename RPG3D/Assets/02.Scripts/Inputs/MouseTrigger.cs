using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MouseTrigger : MonoBehaviour
{
    private static bool _triggered;
    public static bool Triggered
    {
        get
        {
            if (_triggered)
            {
                _triggered = false;
                return true;
            }
            else
            {
                return false;
            }
        }
        set
        {
            _triggered = value;
            OnTriggerActive?.Invoke();
        }
    }
    public static event Action OnTriggerActive;

    private void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            Triggered = true;
        }
    }
}
