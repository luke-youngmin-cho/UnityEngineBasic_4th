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
        Fall,
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
    private enum JumpState
    {
        Idle,
        Prepare,
        Casting,
        OnAction,
        Finish
    }
    private enum FallState
    {
        Idle,
        Prepare,
        Casting,
        OnAction,
        Finish
    }
    private enum SlideState
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
    [SerializeField] private JumpState _jumpState;
    [SerializeField] private FallState _fallState;
    [SerializeField] private SlideState _slideState;
    private Vector2 _move;
    [SerializeField] private float _moveSpeed = 1.0f;
    [SerializeField] private float _jumpForce = 2.0f;
    [SerializeField] private float _slideSpeed = 3.0f;
    
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
    private Rigidbody2D _rb;
    private CapsuleCollider2D _col;
    private GroundDetector _groundDetector;

    private bool isMovable = true;
    private float _slideAnimationTime;
    private float _animationTimer;


    private float h { get => Input.GetAxis("Horizontal"); }
    private float v { get => Input.GetAxis("Vertical"); }


    private void Awake()
    {
        direction = _directionInit;    
        _rb = GetComponent<Rigidbody2D>();
        _col = GetComponent<CapsuleCollider2D>();
        _animator = GetComponent<Animator>();
        _groundDetector = GetComponent<GroundDetector>();
        _slideAnimationTime = GetAnimationTime("Slide");
    }

    private void Update()
    {
        if (h < 0.0f)
            direction = -1;
        else if (h > 0.0f)
            direction = 1;
        _move.x = h;

        if (isMovable)
        {
            if (Mathf.Abs(_move.x) > 0.0f)
                ChangeState(State.Move);
            else
                ChangeState(State.Idle);
        }
        

        if (Input.GetKeyDown(KeyCode.LeftAlt) &&
            state != State.Jump &&
            state != State.Fall)
        {
            ChangeState(State.Jump);
        }


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
                UpdateJumpState();
                break;
            case State.Fall:
                UpdateFallState();
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
                _jumpState = JumpState.Idle;
                break;
            case State.Fall:
                _fallState = FallState.Idle;
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
                _jumpState = JumpState.Prepare;
                break;
            case State.Fall:
                _fallState = FallState.Prepare;
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
                isMovable = true;
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
                isMovable = true;
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

    private void UpdateJumpState()
    {
        switch (_jumpState)
        {
            case JumpState.Idle:
                break;
            case JumpState.Prepare:
                isMovable = false;
                _animator.Play("Jump");
                _rb.AddForce(Vector2.up * _jumpForce, ForceMode2D.Impulse);
                _jumpState++;
                break;
            case JumpState.Casting:
                if (_groundDetector.isDetected == false)
                    _jumpState++;
                break;
            case JumpState.OnAction:
                if (_rb.velocity.y < 0)
                {
                    ChangeState(State.Fall);
                }
                break;
            case JumpState.Finish:
                break;
            default:
                break;
        }
    }

    private void UpdateFallState()
    {
        switch (_fallState)
        {
            case FallState.Idle:
                break;
            case FallState.Prepare:
                isMovable = false;
                _animator.Play("Fall");
                _fallState = FallState.OnAction;
                break;
            case FallState.Casting:
                break;
            case FallState.OnAction:
                if (_groundDetector.isDetected)
                {
                    ChangeState(State.Idle);
                }
                break;
            case FallState.Finish:
                break;
            default:
                break;
        }
    }

    private void UpdateSlideState()
    {
        switch (_slideState)
        {
            case SlideState.Idle:
                break;
            case SlideState.Prepare:
                _animator.Play("Slide");
                _animationTimer = _slideAnimationTime;
                _slideState++;
                break;
            case SlideState.Casting:
                if (_animationTimer < _slideAnimationTime * 3 / 4)
                    _slideState++;
                else
                {
                    _rb.velocity = Vector2.right * _direction * _moveSpeed;
                }
                _animationTimer -= Time.deltaTime;
                break;
            case SlideState.OnAction:
                if (_animationTimer < _slideAnimationTime * 1 / 4)
                    _slideState++;
                {
                    _rb.velocity = Vector2.right * _direction * _slideSpeed;
                }
                _animationTimer -= Time.deltaTime;
                break;
            case SlideState.Finish:
                if (_animationTimer < 0)
                {
                    ChangeState(State.Idle);
                }
                else
                {
                    _rb.velocity = Vector2.right * _direction * _moveSpeed;
                }
                _animationTimer -= Time.deltaTime;
                break;
            default:
                break;
        }
    }

    private float GetAnimationTime(string clipName)
    {
        RuntimeAnimatorController rac = _animator.runtimeAnimatorController;
        for (int i = 0; i < rac.animationClips.Length; i++)
        {
            if (rac.animationClips[i].name == clipName)
            {
                return rac.animationClips[i].length;
            }
        }

        Debug.LogWarning($"GetAnimationTime : {clipName} 을 찾을 수 없습니다.");
        return -1.0f;
    }
}
