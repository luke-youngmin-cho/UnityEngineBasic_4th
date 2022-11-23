using BT;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class Enemy : CharacterBase, IDamage
{
    // BT
    private BehaviorTree _bt;
    private Transform _target;
    [SerializeField] private float _detectRange = 3.0f;
    [SerializeField] private float _attackRange = 0.5f;
    [SerializeField] private float _thinkMinTime = 0.2f;
    [SerializeField] private float _thinkMaxTime = 1.5f;
    [SerializeField] private LayerMask _targetLayer;

    public GameObject Hitter { get; set; }
    public GameObject Hittee { get; set; }

    private void Start()
    {
        BuildBehaviorTree();    
    }

    protected override void Update()
    {
        _bt.Tick();
        base.Update();
    }

    protected override IStateMachine CreateStateMachine()
    {
        return new EnemyStateMachine(gameObject);
    }

    private void BuildBehaviorTree()
    {
        EnemyStateMachine enemyStateMachine = machine as EnemyStateMachine;


        _bt = new BehaviorTree().
            StartBuild()
            .Selector()
                .Sequence()
                    // 타겟 탐색
                    .Execution(() =>
                    {
                        Collider[] cols = Physics.OverlapSphere(transform.position, 3, _targetLayer);
                        if (cols.Length > 0)
                        {
                            _target = cols[0].transform;
                            return Status.Success;
                        }
                        else
                        {
                            return Status.Failure;
                        }
                    })
                    .Selector()
                        // 타겟이 공격 범위 내에 있는지
                        .Condition(() =>
                        {
                            return Vector3.Distance(transform.position, _target.position) <= _attackRange;
                        })
                            // 공격
                            .Execution(() =>
                            {
                                enemyStateMachine.ChangeState(EnemyStates.Attack);
                                return enemyStateMachine.currentType == EnemyStates.Attack ? Status.Success : Status.Failure;
                            })
                        // 타깃 따라가기
                        .Execution(() =>
                        {
                            transform.LookAt(new Vector3(_target.transform.position.x,
                                                         transform.position.y,
                                                         _target.transform.position.z));
                            enemyStateMachine.ChangeState(EnemyStates.Move);
                            return enemyStateMachine.currentType == EnemyStates.Move ? Status.Success : Status.Failure;
                        })
                        .ExitCurrentComposite()
                    .ExitCurrentComposite()
                .RandomSelector()
                    .RunAndSleepRandom(_thinkMinTime, _thinkMaxTime)
                        .Execution(() =>
                        {
                            enemyStateMachine.ChangeState(EnemyStates.Idle);
                            return enemyStateMachine.currentType == EnemyStates.Idle ? Status.Success : Status.Failure;
                        })
                    .RunAndSleepRandom(_thinkMinTime, _thinkMaxTime)
                        .Execution(() =>
                        {
                            transform.eulerAngles = Vector3.up * Random.Range(0.0f, 360.0f);
                            enemyStateMachine.ChangeState(EnemyStates.Move);
                            return enemyStateMachine.currentType == EnemyStates.Move ? Status.Success : Status.Failure;
                        });
    }

    public void GetDamage(GameObject hitter, GameObject hittee, int damage, bool isCritical)
    {
        Hitter = hitter;
        Hittee = hittee;

        if (hitter.TryGetComponent(out IStats istatsHitter) &&
            hittee.TryGetComponent(out IStats istatsHittee))
        {
            istatsHittee.stats.StatDictionary[Stat.ID_HP].
                Modify(-istatsHitter.stats.StatDictionary[Stat.ID_ATK].Value, StatModType.Flat);
        }

        _target = hitter.transform;
    }
}
