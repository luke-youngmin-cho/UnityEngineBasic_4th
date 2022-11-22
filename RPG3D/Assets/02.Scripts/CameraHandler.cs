using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraHandler : MonoBehaviour
{
    [SerializeField] private Transform _target;
    [SerializeField] private float _minDistance = 3.0f;
    [SerializeField] private float _maxDistance = 30.0f;
    [SerializeField] private float _wheelSpeed = 500.0f;
    [SerializeField] private float _xMoveSpeed = 500.0f;
    [SerializeField] private float _yMoveSpeed = 250.0f;
    [SerializeField] private float _yLimitMin = 5.0f;
    [SerializeField] private float _yLimitMax = 80.0f;
    private float y, x, distance;

    private void Awake()
    {
        distance = Vector3.Distance(transform.position, _target.position);
        Vector3 angles = transform.eulerAngles;
        x = angles.y;
        y = angles.x;
    }

    private void Start()
    {
        CursorHandler.DeactiveCursor();
    }

    private void Update()
    {
        x += Input.GetAxis("Mouse X") * _xMoveSpeed * Time.deltaTime;
        y -= Input.GetAxis("Mouse Y") * _yMoveSpeed * Time.deltaTime;
        x = ClampAngle(x, -360.0f, 360.0f);
        y = ClampAngle(y, _yLimitMin, _yLimitMax);

        distance -= Input.GetAxis("Mouse ScrollWheel") * _wheelSpeed * Time.deltaTime;
        distance = Mathf.Clamp(distance, _minDistance, _maxDistance);

        _target.rotation = Quaternion.Euler(0.0f, transform.eulerAngles.y, 0.0f);
    }

    private void LateUpdate()
    {
        transform.rotation = Quaternion.Euler(y, x ,0);
        transform.position = transform.rotation * new Vector3(0, 0, -distance) + _target.position;
    }

    private float ClampAngle(float angle, float limitMin, float limitMax)
    {
        angle %= 360.0f;
        return Mathf.Clamp(angle, limitMin, limitMax);
    }
}
