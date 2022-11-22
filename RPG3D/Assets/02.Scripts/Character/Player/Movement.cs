using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class Movement : MonoBehaviour
{
    public float Speed
    {
        get
        {
            if (Input.GetKey(KeyCode.LeftShift))
                return RunSpeed;
            else
                return WalkSpeed;
        }
    }
    public readonly float WalkSpeed = 1.0f;
    public readonly float RunSpeed = 2.0f;
    
    public float h => Input.GetAxis("Horizontal") * Speed / RunSpeed;
    public float v => Input.GetAxis("Vertical") * Speed / RunSpeed;

}
