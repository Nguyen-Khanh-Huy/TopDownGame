using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyMoving : PISMonoBehaviour
{
    [SerializeField] private EnemyCtrlAbstract _enemyCtrlAbstract;
    [SerializeField] private float _distance;
    [SerializeField] private bool _isStopMoving;

    public bool IsStopMoving { get => _isStopMoving; }

    private void Start()
    {
        CheckEnemy();
    }

    private void Update()
    {
        LookAtTarget();
        EnemyMove();
    }

    private void LookAtTarget()
    {
        Vector3 targetPosition = _enemyCtrlAbstract.Player.transform.position;
        targetPosition.y = _enemyCtrlAbstract.transform.position.y;
        _enemyCtrlAbstract.transform.LookAt(targetPosition);
    }

    private void EnemyMove()
    {
        _enemyCtrlAbstract.Agent.SetDestination(_enemyCtrlAbstract.Player.transform.position);
        float checkDistance = Vector3.Distance(_enemyCtrlAbstract.Player.transform.position, _enemyCtrlAbstract.transform.position);
        if (checkDistance >= _distance)
        {
            _isStopMoving = false;
            //_enemyCtrlAbstract.Agent.isStopped = false;
        }
        else
        {
            _isStopMoving = true;
            //_enemyCtrlAbstract.Agent.isStopped = true;
        }
    }

    private void CheckEnemy()
    {
        if(GetComponentInParent<EnemyNearCtrlAbstract>() != null)
        { _distance = 0.8f; }

        else { _distance = 6f; }
    }
    protected override void LoadComponents()
    {
        if (_enemyCtrlAbstract != null) return;
        _enemyCtrlAbstract = GetComponentInParent<EnemyCtrlAbstract>();
    }
}
