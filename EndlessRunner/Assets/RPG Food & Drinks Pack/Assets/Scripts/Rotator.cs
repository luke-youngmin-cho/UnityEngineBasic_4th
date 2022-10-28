using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Rotator : MonoBehaviour {

    private void FixedUpdate()
    {
        transform.Rotate(Vector3.up * 180 * Time.fixedDeltaTime, Space.World);
    }
}
