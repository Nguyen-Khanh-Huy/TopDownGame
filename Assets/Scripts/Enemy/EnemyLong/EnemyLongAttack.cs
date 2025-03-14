using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyLongAttack : PISMonoBehaviour
{
    [SerializeField] private EnemyLongCtrlAbstract _enemyLongAbstract;
    [SerializeField] private EnemyState _curState;
    [SerializeField] private float _timeAttack = 2f;
    private float _timeCount;

    private void Update()
    {
        EnemyAttack();
    }

    private void ChangeState(EnemyState newState)
    {
        if (_curState != newState)
        {
            _curState = newState;
            _enemyLongAbstract.Anim.SetInteger("State", (int)newState);
        }
    }

    private void EnemyAttack()
    {
        AnimatorStateInfo stateInfo = _enemyLongAbstract.Anim.GetCurrentAnimatorStateInfo(0);
        _enemyLongAbstract.Agent.isStopped = (_curState == EnemyState.Dying || _curState == EnemyState.Idle || _curState == EnemyState.Attack);

        if (_enemyLongAbstract.Hp <= 0)
        {
            ChangeState(EnemyState.Dying);
            return;
        }

        if (_curState == EnemyState.Attack && stateInfo.IsName(EnemyState.Attack.ToString()) && stateInfo.normalizedTime >= 1f)
        {
            ChangeState(_enemyLongAbstract.EnemyMoving.IsMoving ? EnemyState.Walk : EnemyState.Idle);
            return;
        }

        if (_enemyLongAbstract.EnemyMoving.IsMoving)
        {
            if (_curState != EnemyState.Attack)
            {
                ChangeState(EnemyState.Walk);
            }
        }
        else
        {
            if (_curState != EnemyState.Attack)
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
