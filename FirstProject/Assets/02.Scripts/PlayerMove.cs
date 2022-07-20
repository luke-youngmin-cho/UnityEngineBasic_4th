using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMove : MonoBehaviour
{
    public float moveSpeed = 2f;
    public float rotateSpeed;

    private void FixedUpdate()
    {
        float h = Input.GetAxis("Horizontal"); // x 축 입력
        float v = Input.GetAxis("Vertical"); // z 축 입력

        Vector3 dir = new Vector3(h, 0, v).normalized; // 크기가 1인 방향벡터
        Vector3 moveVec = dir * moveSpeed * Time.fixedDeltaTime;
        transform.Translate(moveVec);

        float r = Input.GetAxis("Mouse X");
        Vector3 rotateVec = new Vector3(0, r, 0) * rotateSpeed * Time.fixedDeltaTime;
        transform.Rotate(rotateVec);
    }
}
