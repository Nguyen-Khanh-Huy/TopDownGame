using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyNearMoving : PISMonoBehaviour
{
    [SerializeField] private EnemyNearCtrlAbstract _enemyNearAbstract;
    [SerializeField] private float _moveSpeed = 2.2f;

    private void Update()
    {
        LookAtTarget();
        EnemyMoving();
    }

    private void LookAtTarget()
    {
        _enemyNearAbstract.transform.LookAt(_enemyNearAbstract.Player.transform);
    }

    private void EnemyMoving()
    {
        if (!_enemyNearAbstract.EnemyTarget.IsStop)
        {
            Vector3 direction = (_enemyNearAbstract.Player.transform.position - _enemyNearAbstract.transform.position).normalized;
            _enemyNearAbstract.Rb.velocity = direction * _moveSpeed;
        }
        else
        {
            _enemyNearAbstract.Rb.velocity = Vector3.zero;
        }
    }

    protected override void LoadComponents()
    {
        if (_enemyNearAbstract != null) return;
        _enemyNearAbstract = GetComponentInParent<EnemyNearCtrlAbstract>();
    }
}
