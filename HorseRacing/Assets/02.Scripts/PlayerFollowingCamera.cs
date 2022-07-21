using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerFollowingCamera : MonoBehaviour
{
    private List<Transform> targets = new List<Transform>();
    private Vector3 offset = new Vector3(0, 1.51f, -4.09f);
    private int targetIndex;

    private void Start()
    {
        targets = RacingPlay.instance.GetHorseTransforms();
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Alpha1))
            targetIndex = 0;
        else if (Input.GetKeyDown(KeyCode.Alpha2))
            targetIndex = 1;
        else if (Input.GetKeyDown(KeyCode.Alpha3))
            targetIndex = 2;
        else if (Input.GetKeyDown(KeyCode.Alpha4))
            targetIndex = 3;
        else if (Input.GetKeyDown(KeyCode.Alpha5))
            targetIndex = 4;
    }

    private void LateUpdate()
    {
        FollowTarget();
    }

    private void FollowTarget()
    {
        transform.position = targets[targetIndex].position + offset;
    }
}
