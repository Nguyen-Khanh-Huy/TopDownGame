using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyMoving : PISMonoBehaviour
{
    [SerializeField] private EnemyCtrlAbstract _enemyCtrl;
    [SerializeField] private float _distance;
    [SerializeField] private bool _isMoving;

    public bool IsMoving { get => _isMoving; }

    private void Start()
    {
        CheckEnemy();
    }

    private void Update()
    {
        EnemyMove();
        LookAtTarget();
    }

    private void LookAtTarget()
    {
        if (_enemyCtrl.EnemyAttack.CurState == EnemyState.Dying) return;
        Vector3 targetPosition = _enemyCtrl.PlayerCtrl.transform.position;
        targetPosition.y = _enemyCtrl.transform.position.y;
        _enemyCtrl.transform.LookAt(targetPosition);
    }

    private void EnemyMove()
    {
        _enemyCtrl.Agent.SetDestination(_enemyCtrl.PlayerCtrl.transform.position);
        float checkDistance = Vector3.Distance(_enemyCtrl.PlayerCtrl.transform.position, _enemyCtrl.transform.position);
        _isMoving = checkDistance > _distance;

        bool shouldStop = (_enemyCtrl.EnemyAttack.CurState == EnemyState.Idle
                        || _enemyCtrl.EnemyAttack.CurState == EnemyState.Attack
                        || _enemyCtrl.EnemyAttack.CurState == EnemyState.Dying);

        _enemyCtrl.Agent.isStopped = shouldStop || !_isMoving;
    }

    private void CheckEnemy()
    {
        if (GetComponentInParent<EnemyNearCtrlAbstract>() != null)
            _distance = 1f;
        else
            _distance = 6f;
    }
    protected override void LoadComponents()
    {
        if (_enemyCtrl != null) return;
        _enemyCtrl = GetComponentInParent<EnemyCtrlAbstract>();
    }
}
