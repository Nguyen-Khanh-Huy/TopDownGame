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

    private void Start()
    {
        CheckEnemyType();
    }

    private void Update()
    {
        EnemyFrozen();
        EnemyMove();
        LookAtTarget();
    }

    protected virtual void LookAtTarget()
    {
        if (!UIGamePlayManager.Ins.CheckPlayTime
         || _isFreeze
         || _enemyCtrl.EnemyAttack.CurState == EnemyState.Attack
         || _enemyCtrl.EnemyAttack.CurState == EnemyState.Dying) return;

        Vector3 targetPosition = _enemyCtrl.PlayerCtrl.transform.position;
        targetPosition.y = _enemyCtrl.transform.position.y;
        _enemyCtrl.transform.LookAt(targetPosition);
    }

    protected virtual void EnemyMove()
    {
        _enemyCtrl.Agent.SetDestination(_enemyCtrl.PlayerCtrl.transform.position);
        //float checkDistance = Vector3.Distance(_enemyCtrl.PlayerCtrl.transform.position, _enemyCtrl.transform.position);
        //_isMoving = checkDistance > _distance;

        float sqrDistance = (_enemyCtrl.PlayerCtrl.transform.position - _enemyCtrl.transform.position).sqrMagnitude;
        _isMoving = sqrDistance > (_distance * _distance);

        bool shouldStop = (!UIGamePlayManager.Ins.CheckPlayTime
                        || _isFreeze
                        || _enemyCtrl.EnemyAttack.CurState == EnemyState.Idle
                        || _enemyCtrl.EnemyAttack.CurState == EnemyState.Attack
                        || _enemyCtrl.EnemyAttack.CurState == EnemyState.Dying);

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
        _distance = GetComponentInParent<EnemyNearCtrlAbstract>() != null ? 1f : 6f;
    }

    protected override void LoadComponents()
    {
        if (_enemyCtrl != null) return;
        _enemyCtrl = GetComponentInParent<EnemyCtrlAbstract>();
    }
}
