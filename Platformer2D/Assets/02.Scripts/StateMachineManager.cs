using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StateMachineManager : MonoBehaviour
{
    public enum State
    {
        Idle,
        Move,
        Jump,
        Fall,
        Attack,
        Dash,
        Slide,
        Crouch,
        DownJump,
        Hurt,
        Die
    }
    public State state;

    private Dictionary<State, StateMachineBase> _machines = new Dictionary<State, StateMachineBase>();

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
    public bool isMovable { get; set; }
    public bool isDirectionChangable { get; set; }

    private AnimationManager _animationManager;
    private StateMachineBase _current;
    private void Awake()
    {
        _animationManager = GetComponent<AnimationManager>();
        _machines.Add(State.Idle, new StateMachineIdle(State.Idle, this, _animationManager));
        _machines.Add(State.Move, new StateMachineMove(State.Move, this, _animationManager));
        _machines.Add(State.Jump, new StateMachineJump(State.Jump, this, _animationManager));
        _machines.Add(State.Fall, new StateMachineFall(State.Fall, this, _animationManager));
    }

    private void Update()
    {
        ChangeState(_current.UpdateState());
    }

    private void FixedUpdate()
    {
        _current.FixedUpdateState();
    }

    private void ChangeState(State newState)
    {
        if (state == newState ||
            _machines[newState].IsExecuteOK() == false)
            return;

        _machines[state].ForceStop();
        _machines[newState].Execute();
        _current = _machines[newState];
        state = newState;
    }
}
