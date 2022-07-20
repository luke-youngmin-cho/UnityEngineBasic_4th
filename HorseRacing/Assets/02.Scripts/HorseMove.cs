using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HorseMove : MonoBehaviour
{
    private Transform _tr;
    [SerializeField] private float minSpeed = 2.0f;
    [SerializeField] private float maxSpeed = 5.0f;
    private float _moveDistance;
    private float _targetDistance;
    private bool _doMove;
    
    public bool isFinished
    {
        get
        {
            return _moveDistance >= _targetDistance;
        }
    }

    public void StartMove(float targetDistance)
    {
        _doMove = true;
        _targetDistance = targetDistance;
    }

    private void Awake()
    {
        _tr = transform;
    }

    private void FixedUpdate()
    {
        if (_doMove &&
            isFinished == false)
            Move();
    }

    private void Move()
    {
        float speed = Random.Range(minSpeed, maxSpeed);
        Vector3 moveVec = Vector3.forward * speed * Time.fixedDeltaTime;
        _tr.Translate(moveVec);
        _moveDistance += moveVec.z;
    }
}
