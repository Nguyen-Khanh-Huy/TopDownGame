using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyLongMoving : PISMonoBehaviour
{
    [SerializeField] private EnemyLongCtrlAbstract _enemyLongAbstract;
    [SerializeField] private float _moveSpeed = 1.8f;

    private void Update()
    {
        LookAtTarget();
        EnemyMoving();
    }

    private void LookAtTarget()
    {
        _enemyLongAbstract.transform.LookAt(_enemyLongAbstract.Player.transform);
    }

    private void EnemyMoving()
    {
        if (!_enemyLongAbstract.EnemyTarget.IsStop)
        {
            Vector3 direction = (_enemyLongAbstract.Player.transform.position - _enemyLongAbstract.transform.position).normalized;
            _enemyLongAbstract.Rb.velocity = direction * _moveSpeed;
        }
        else
        {
            _enemyLongAbstract.Rb.velocity = Vector3.zero;
        }
    }

    protected override void LoadComponents()
    {
        if(_enemyLongAbstract != null) return;
        _enemyLongAbstract = GetComponentInParent<EnemyLongCtrlAbstract>();
    }
}
