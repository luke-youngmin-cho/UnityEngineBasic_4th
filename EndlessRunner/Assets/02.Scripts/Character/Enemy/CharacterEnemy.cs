using BT;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public class CharacterEnemy : CharacterBase
{
    [Flags]
    public enum StateTypes
    {
        Idle    = 0 << 0,
        Move    = 1 << 0,
        Jump    = 1 << 1,
        Attack  = 1 << 2,
        Hurt    = 1 << 3,
        Die     = 1 << 4,
        All     = ~Idle,
    }
    private StateMachineBase<StateTypes> _machine;
    [SerializeField] private StateTypes _currentType => _machine.currentType;
    [SerializeField] private IState<StateTypes>.Commands _currentCommand => _machine.current.current;

    public GroundDetector groundDetector;
    
    public bool movable;    
    public Vector3 direction
    {
        get
        {
            return transform.eulerAngles;
        }
        set
        {
            transform.eulerAngles = value;
        }
    }

    public override int hp { get => throw new NotImplementedException(); set => throw new NotImplementedException(); }

    public class BehaviorTreeForEnemy : BehaviorTree
    {
        private CharacterEnemy _owner;

        public class Detect : Execution
        {
            private Collider[] _tmp;
            public Detect(Func<ReturnTypes> function, Vector3 center, float detectRange, LayerMask targetLayer) 
                : base(function)
            {
                function += () =>
                {
                    _tmp = Physics.OverlapSphere(center, detectRange, targetLayer);
                    if (_tmp != null &&
                        _tmp.Length > 0)
                    {
                        return ReturnTypes.Success;
                    }
                    else
                    {
                        return ReturnTypes.Failure;
                    }
                };
            }
        }
        public class Look : Execution
        {
            public Look(Func<ReturnTypes> function, Transform owner, Transform target) : base(function)
            {
                function += () =>
                {
                    if (target == null)
                        return ReturnTypes.Failure;
                    else
                    {
                        owner.LookAt(target);
                        return ReturnTypes.Success;
                    }
                };
            }
        }

        public override RootNode Root { get; set; }
        public Selector SelectorForTarget;
        public Sequence SequenceWhenTargetDetected;
        public ConditionNode ConditionPlayerNotDetected;
        public ConditionNode ConditionMovable;
        public RandomSelector RandomSelectorForMovement;
        public Execution ExecutionDetectPlayer;
        public Execution ExecutionLookPlayer;
        public Execution ExecutionDetectPlayerInAttackRange;
        public Execution ExecutionAttack;
        public Execution ExecutionJumpForward;
        public Execution ExecutionJumpBackward;


        // 
        public bool Attackable;

        public BehaviorTreeForEnemy(CharacterEnemy owner)
        {
            _owner = owner;
        }

        public override void Init()
        {
            Root = new RootNode();

            ExecutionDetectPlayer = new Execution(() =>
                                    {
                                        Collider[] cols = Physics.OverlapSphere(_owner.transform.position,
                                                                                _owner.detectRange,
                                                                                _owner.targetLayer);
                                        if (cols.Length > 0)
                                        {
                                            _owner.target = cols[0].gameObject;
                                            return ReturnTypes.Success;
                                        }
                                        else
                                        {
                                            _owner.target = null;
                                            return ReturnTypes.Failure;
                                        }
                                    });
            ExecutionDetectPlayerInAttackRange = new Execution(() =>
                                                 {
                                                     Collider[] cols = Physics.OverlapSphere(_owner.transform.position,
                                                                                             _owner.detectAttackRange,
                                                                                             _owner.targetLayer);
                                                     if (cols.Length > 0)
                                                     {
                                                         Attackable = true;
                                                         _owner.target = cols[0].gameObject;
                                                         return ReturnTypes.Success;
                                                     }
                                                     else
                                                     {
                                                         Attackable = false;
                                                         return ReturnTypes.Failure;
                                                     }
                                                 });
            ExecutionLookPlayer = new Execution(() =>
                                                {
                                                    if (_owner.target)
                                                    {
                                                        _owner.transform.LookAt(_owner.target.transform);
                                                        return ReturnTypes.Success;
                                                    }
                                                    else
                                                    {
                                                        return ReturnTypes.Failure;
                                                    }
                                                });
            ExecutionAttack = new Execution(() =>
                                            {
                                                if (Attackable)
                                                {
                                                    if (Root.RunningNode == null)
                                                    {
                                                        _owner.ChangeMachineState(StateTypes.Attack);
                                                        return ReturnTypes.OnRunning;
                                                    }
                                                    else if (_owner._machine.currentType == StateTypes.Attack)
                                                    {
                                                        return ReturnTypes.OnRunning;
                                                    }
                                                    else
                                                    {
                                                        return ReturnTypes.Success;
                                                    }
                                                }
                                                else
                                                {
                                                    return ReturnTypes.Failure;
                                                }
                                            });

            SequenceWhenTargetDetected = new Sequence();
            SequenceWhenTargetDetected.AddChild(ExecutionDetectPlayer)
                                      .AddChild(ExecutionLookPlayer)
                                      .AddChild(ExecutionDetectPlayerInAttackRange)
                                      .AddChild(ExecutionAttack);

            ExecutionJumpBackward = new Execution(() =>
                                                  {
                                                      if (_owner.groundDetector.isDetected)
                                                      {
                                                          _owner.direction = Vector3.up * 180.0f;
                                                          _owner.rb.velocity = Vector3.zero;
                                                          _owner.rb.AddRelativeForce(new Vector3(0.0f, 1.0f, 1.0f) * _owner.jumpForce, ForceMode.Impulse);
                                                          return ReturnTypes.Success;
                                                      }
                                                      else
                                                      {
                                                          return ReturnTypes.Failure;
                                                      }
                                                      
                                                  });
            ExecutionJumpForward = new Execution(() =>
                                                  {
                                                      if (_owner.groundDetector.isDetected)
                                                      {
                                                          _owner.direction = Vector3.up * 0.0f;
                                                          _owner.rb.velocity = Vector3.zero;
                                                          _owner.rb.AddRelativeForce(new Vector3(0.0f, 1.0f, 1.0f) * _owner.jumpForce, ForceMode.Impulse);
                                                          return ReturnTypes.Success;
                                                      }
                                                      else
                                                      {
                                                          return ReturnTypes.Failure;
                                                      }
                                                  });

            RandomSelectorForMovement = new RandomSelector();
            RandomSelectorForMovement.AddChild(ExecutionJumpBackward)
                                     .AddChild(ExecutionJumpForward);

            ConditionMovable = new ConditionNode(() => _owner.movable);
            ConditionMovable.SetChild(RandomSelectorForMovement);

            ConditionPlayerNotDetected = new ConditionNode(() => _owner.target == null);
            ConditionPlayerNotDetected.SetChild(ConditionMovable);

            SelectorForTarget = new Selector();
            SelectorForTarget.AddChild(SequenceWhenTargetDetected)
                             .AddChild(ConditionPlayerNotDetected);

            Root.SetChild(SelectorForTarget);
        }

        public override ReturnTypes Tick()
        {
            Node dummy = null;
            ReturnTypes result;
            if (Root.RunningNode != null)
            {
                result = Root.RunningNode.Invoke(out dummy);
                if (result != ReturnTypes.OnRunning)
                    Root.RunningNode = null;
            }
            else 
            {
                result = Root.Invoke(out dummy);
            }

            return result;
        }
    }
    private BehaviorTreeForEnemy _aiTree;
    public void ChangeMachineState(StateTypes newStateType) => _machine.ChangeState(newStateType);

    protected override void Awake()
    {
        base.Awake();
        _machine = new StateMachineBase<StateTypes>(gameObject,
                                                    GetStateExecuteConditionMask(),
                                                    GetStateTransitionParis());
        _aiTree = new BehaviorTreeForEnemy(this);
        GameStateManager.instance.OnStateChanged += OnGameStateChanged;
    }

    private void Start()
    {
        _aiTree.Init();
    }

    private void OnDestroy()
    {
        GameStateManager.instance.OnStateChanged -= OnGameStateChanged;
    }

    private void Update()
    {
        _aiTree.Tick();
        _machine.Update();
    }

    private Dictionary<StateTypes, StateTypes> GetStateExecuteConditionMask()
    {
        Dictionary<StateTypes, StateTypes> result = new Dictionary<StateTypes, StateTypes>();
        result.Add(StateTypes.Idle, StateTypes.All);
        result.Add(StateTypes.Move, StateTypes.All);
        result.Add(StateTypes.Jump, StateTypes.Idle | StateTypes.Move);
        result.Add(StateTypes.Attack, StateTypes.Idle | StateTypes.Move);
        result.Add(StateTypes.Hurt, StateTypes.All);
        result.Add(StateTypes.Die, StateTypes.All);
        return result;
    }

    private Dictionary<StateTypes, StateTypes> GetStateTransitionParis()
    {
        Dictionary<StateTypes, StateTypes> result = new Dictionary<StateTypes, StateTypes>();
        result.Add(StateTypes.Idle, StateTypes.Idle);
        result.Add(StateTypes.Move, StateTypes.Move);
        result.Add(StateTypes.Jump, StateTypes.Move);
        result.Add(StateTypes.Attack, StateTypes.Idle);
        result.Add(StateTypes.Hurt, StateTypes.Move);
        result.Add(StateTypes.Die, StateTypes.Move);
        return result;
    }

    private void OnGameStateChanged(GameStates newState)
    {
        enabled = newState == GameStates.Play;
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.yellow;
        Gizmos.DrawWireSphere(transform.position, detectRange);
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, detectAttackRange);
    }
}
