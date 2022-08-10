using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public enum State
    {
        Idle,
        Move,
        Jump,
        Attack,
        Dash,
        Slide
    }

    private enum IdleState
    {
        Idle,
        Prepare,
        Casting,
        OnAction,
        Finish
    }
    private enum MoveState
    {
        Idle,
        Prepare,
        Casting,
        OnAction,
        Finish
    }
    public State state;
    [SerializeField] private IdleState _idleState;
    [SerializeField] private MoveState _moveState;

    private Vector2 _move;
    [SerializeField] private float _moveSpeed = 1.0f;

    // -1 : left , +1 : right
    private int _direction;
    public int direction
    {
        get
        {
            return _direction;
        }
        set
        {
            if (value < 0)
            {
                _direction = -1;
                transform.eulerAngles = new Vector3(0.0f, 180.0f, 0.0f);
            }
            else
            {
                _direction = 1;
                transform.eulerAngles = Vector3.zero;
            }
        }
    }
    [SerializeField] private int _directionInit;
    private Animator _animator;



    private float h { get => Input.GetAxis("Horizontal"); }
    private float v { get => Input.GetAxis("Vertical"); }


    private void Awake()
    {
        direction = _directionInit;    
        _animator = GetComponent<Animator>();
    }

    private void Update()
    {
        if (h < 0.0f)
            direction = -1;
        else if (h > 0.0f)
            direction = 1;
        _move.x = h;

        if (Mathf.Abs(_move.x) > 0.0f)
            ChangeState(State.Move);
        else
            ChangeState(State.Idle);

        UpdateState();
    }

    private void FixedUpdate()
    {
        transform.position += new Vector3(_move.x * _moveSpeed, _move.y, 0) * Time.fixedDeltaTime;
    }

    private void UpdateState()
    {
        switch (state)
        {
            case State.Idle:
                UpdateIdleState();
                break;
            case State.Move:
                UpdateMoveState();
                break;
            case State.Jump:
                break;
            case State.Attack:
                break;
            case State.Dash:
                break;
            case State.Slide:
                break;
            default:
                break;
        }
    }

    private void ChangeState(State newState)
    {
        if (state == newState)
            return;

        // 이전 하위 상태 머신 초기화
        switch (state)
        {
            case State.Idle:
                _idleState = IdleState.Idle;
                break;
            case State.Move:
                _moveState = MoveState.Idle;
                break;
            case State.Jump:
                break;
            case State.Attack:
                break;
            case State.Dash:
                break;
            case State.Slide:
                break;
            default:
                break;
        }

        // 다음 하위 상태 머신 준비
        switch (newState)
        {
            case State.Idle:
                _idleState = IdleState.Prepare;
                break;
            case State.Move:
                _moveState = MoveState.Prepare;
                break;
            case State.Jump:
                break;
            case State.Attack:
                break;
            case State.Dash:
                break;
            case State.Slide:
                break;
            default:
                break;
        }

        state = newState;
    }

    private void UpdateIdleState()
    {
        switch (_idleState)
        {
            case IdleState.Idle:
                break;
            case IdleState.Prepare:
                _animator.Play("Idle");
                _idleState = IdleState.OnAction;
                break;
            case IdleState.Casting:
                break;
            case IdleState.OnAction:
                // nothing to do
                break;
            case IdleState.Finish:
                break;
            default:
                break;
        }
    }
    private void UpdateMoveState()
    {
        switch (_moveState)
        {
            case MoveState.Idle:
                break;
            case MoveState.Prepare:
                _animator.Play("Move");
                _moveState = MoveState.OnAction;
                break;
            case MoveState.Casting:
                break;
            case MoveState.OnAction:
                // nothing to do;
                break;
            case MoveState.Finish:
                break;
            default:
                break;
        }
    }
}
