using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyMoving : PISMonoBehaviour
{
    [SerializeField] private EnemyCtrlAbstract _enemyCtrlAbstract;
    [SerializeField] private float _distance;
    [SerializeField] private bool _isMoving;

    public bool IsMoving { get => _isMoving; }

    private void Start()
    {
        CheckEnemy();
    }

    private void Update()
    {
        if (_enemyCtrlAbstract.Hp <= 0 || !UIGamePlayManager.Ins.CheckPlayTime) return;
        LookAtTarget();
        EnemyMove();
    }

    private void LookAtTarget()
    {
        Vector3 targetPosition = _enemyCtrlAbstract.PlayerCtrl.transform.position;
        targetPosition.y = _enemyCtrlAbstract.transform.position.y;
        _enemyCtrlAbstract.transform.LookAt(targetPosition);
    }

    private void EnemyMove()
    {
        _enemyCtrlAbstract.Agent.SetDestination(_enemyCtrlAbstract.PlayerCtrl.transform.position);
        float checkDistance = Vector3.Distance(_enemyCtrlAbstract.PlayerCtrl.transform.position, _enemyCtrlAbstract.transform.position);
        if (checkDistance > _distance)
        {
            _isMoving = true;
        }
        else
        {
            _isMoving = false;
        }
    }

    private void CheckEnemy()
    {
        if(GetComponentInParent<EnemyNearCtrlAbstract>() != null)
        { _distance = 1f; }

        else { _distance = 6f; }
    }
    protected override void LoadComponents()
    {
        if (_enemyCtrlAbstract != null) return;
        _enemyCtrlAbstract = GetComponentInParent<EnemyCtrlAbstract>();
    }
}
