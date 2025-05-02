using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyMoving : PISMonoBehaviour
{
    [SerializeField] protected EnemyCtrlAbstract _enemyCtrl;
    [SerializeField] protected float _distance;
    [SerializeField] protected bool _isMoving;
    [SerializeField] protected bool _isFreeze;
    private float _coolDownFrozen;

    public bool IsMoving { get => _isMoving; }
    public bool IsFreeze { get => _isFreeze; set => _isFreeze = value; }

    protected virtual void Start()
    {
        CheckEnemyType();
    }

    private void Update()
    {
        EnemyFrozen();
        EnemyMove();
        LookAtTarget();
    }

    protected virtual void LookAtTarget(){}

    protected virtual void EnemyMove()
    {
        if (_enemyCtrl.Hp <= 0)
        {
            _enemyCtrl.Agent.isStopped = true;
            return; 
        }
        if(!_enemyCtrl.Agent.isStopped)
            _enemyCtrl.Agent.SetDestination(_enemyCtrl.PlayerCtrl.transform.position);

        float sqrDistance = (_enemyCtrl.PlayerCtrl.transform.position - _enemyCtrl.transform.position).sqrMagnitude;
        _isMoving = sqrDistance > (_distance * _distance);

        bool shouldStop = (!UIGamePlayManager.Ins.CheckPlayTime
                        || _isFreeze
                        || _enemyCtrl.EnemyAttack.CurState == EnemyNearLongState.Idle
                        || _enemyCtrl.EnemyAttack.CurState == EnemyNearLongState.Attack
                        || _enemyCtrl.EnemyAttack.CurState == EnemyNearLongState.Dying);

        _enemyCtrl.Agent.isStopped = shouldStop || !_isMoving;
    }

    private void EnemyFrozen()
    {
        if (_enemyCtrl.Hp <= 0)
        {
            _coolDownFrozen = 0;
            _isFreeze = false;
            return;
        }
        if (!_isFreeze) return;
        _coolDownFrozen += Time.deltaTime;
        if (_coolDownFrozen >= _enemyCtrl.PlayerCtrl.PlayerSkillsCtrl.PlayerSkillFreeze.TimeFreeze)
        {
            _coolDownFrozen = 0;
            _isFreeze = false;
        }
    }

    private void CheckEnemyType()
    {
        _distance = GetComponentInParent<EnemyNearCtrl>() != null ? 1.2f : _distance;
    }

    protected override void LoadComponents()
    {
        if (_enemyCtrl != null) return;
        _enemyCtrl = GetComponentInParent<EnemyCtrlAbstract>();
    }
}
