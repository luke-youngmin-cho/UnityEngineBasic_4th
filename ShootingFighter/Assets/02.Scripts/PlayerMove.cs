using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMove : MonoBehaviour
{
    [SerializeField] private float moveSpeed;
    private Transform _tr;

    private void Awake()
    {
        _tr = transform;
    }

    private void FixedUpdate()
    {
        // Input.GetxxxRaw : 0 또는 1 출력
        float h = Input.GetAxisRaw("Horizontal");
        float v = Input.GetAxisRaw("Vertical");

        Vector3 dir = new Vector3(h, 0, v).normalized;
        Vector3 deltaMove = dir * moveSpeed * Time.fixedDeltaTime;

        _tr.Translate(deltaMove);
    }
}
