using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyMoving : PISMonoBehaviour
{
    [SerializeField] protected EnemyCtrlAbstract _enemyCtrl;
    [SerializeField] protected float _distance;
    [SerializeField] protected bool _isMoving;

    public bool IsMoving { get => _isMoving; }

    private void Start()
    {
        CheckEnemyType();
    }

    private void Update()
    {
        EnemyMove();
        LookAtTarget();
    }

    protected virtual void LookAtTarget()
    {
        if (_enemyCtrl.Hp <= 0) return;
        Vector3 targetPosition = _enemyCtrl.PlayerCtrl.transform.position;
        targetPosition.y = _enemyCtrl.transform.position.y;
        _enemyCtrl.transform.LookAt(targetPosition);
    }

    protected virtual void EnemyMove()
    {
        _enemyCtrl.Agent.SetDestination(_enemyCtrl.PlayerCtrl.transform.position);
        float checkDistance = Vector3.Distance(_enemyCtrl.PlayerCtrl.transform.position, _enemyCtrl.transform.position);
        _isMoving = checkDistance > _distance;

        bool shouldStop = (_enemyCtrl.Anim.speed == 0
                        || _enemyCtrl.EnemyAttack.CurState == EnemyState.Idle
                        || _enemyCtrl.EnemyAttack.CurState == EnemyState.Attack
                        || _enemyCtrl.EnemyAttack.CurState == EnemyState.Dying);

        _enemyCtrl.Agent.isStopped = shouldStop || !_isMoving;
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
