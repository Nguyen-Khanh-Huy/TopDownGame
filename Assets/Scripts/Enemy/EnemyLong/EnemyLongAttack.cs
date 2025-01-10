using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyLongAttack : PISMonoBehaviour
{
    [SerializeField] private EnemyLongCtrlAbstract _enemyLongAbstract;
    [SerializeField] private EnemyState currentState;
    [SerializeField] private float _timeAttack = 3f;
    private float _timeCount;

    private void Update()
    {
        EnemyAttack();
    }

    private void ChangeState(EnemyState newState)
    {
        if (currentState != newState)
        {
            currentState = newState;
            _enemyLongAbstract.Anim.SetInteger("State", (int)newState);
        }
    }

    private void EnemyAttack()
    {
        AnimatorStateInfo stateInfo = _enemyLongAbstract.Anim.GetCurrentAnimatorStateInfo(0);
        _enemyLongAbstract.Agent.isStopped = (currentState == EnemyState.Idle || currentState == EnemyState.Attack);

        if (currentState == EnemyState.Attack && stateInfo.IsName(EnemyState.Attack.ToString()) && stateInfo.normalizedTime >= 1f)
        {
            ChangeState(_enemyLongAbstract.EnemyMoving.IsStopMoving ? EnemyState.Idle : EnemyState.Walk);
            return;
        }

        if (!_enemyLongAbstract.EnemyMoving.IsStopMoving)
        {
            if (currentState != EnemyState.Attack)
            {
                ChangeState(EnemyState.Walk);
            }
        }
        else
        {
            if (currentState != EnemyState.Attack)
            {
                _timeCount += Time.deltaTime;
                if (_timeCount >= _timeAttack)
                {
                    _timeCount = 0;
                    ChangeState(EnemyState.Attack);
                }
                else
                {
                    ChangeState(EnemyState.Idle);
                }
            }
        }
    }

    protected override void LoadComponents()
    {
        if (_enemyLongAbstract != null) return;
        _enemyLongAbstract = GetComponentInParent<EnemyLongCtrlAbstract>();
    }
}
