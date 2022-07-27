using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    public static Player instance;
    public void MoveTo(Transform target)
    {
        transform.position = target.position;
    }

    private void Awake()
    {
        instance = this;
    }
}
