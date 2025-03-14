using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyNearAttack : PISMonoBehaviour
{
    [SerializeField] private EnemyNearCtrlAbstract _enemyNearAbstract;
    [SerializeField] private EnemyState _curState;
    [SerializeField] private float _timeAttack = 0.3f;
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
            //_enemyNearAbstract.Anim.SetInteger("State", (int)newState);
            _enemyNearAbstract.Anim.SetInteger("State", (int)_curState);
        }
    }
    private void EnemyAttack()
    {
        AnimatorStateInfo stateInfo = _enemyNearAbstract.Anim.GetCurrentAnimatorStateInfo(0);
        _enemyNearAbstract.Agent.isStopped = (_curState == EnemyState.Dying || _curState == EnemyState.Idle || _curState == EnemyState.Attack);

        if(_enemyNearAbstract.Hp <= 0)
        {
            ChangeState(EnemyState.Dying);
            return;
        }

        if (_curState == EnemyState.Attack && stateInfo.IsName(EnemyState.Attack.ToString()) && stateInfo.normalizedTime >= 1f)
        {
            ChangeState(_enemyNearAbstract.EnemyMoving.IsMoving ? EnemyState.Walk : EnemyState.Idle);
            return;
        }

        if (_enemyNearAbstract.EnemyMoving.IsMoving)
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

    private void OnTriggerEnter(Collider other)
    {
        PlayerController player = other.GetComponent<PlayerController>();
        if(player != null)
        {
            if (player.Hp <= 0) return;
            player.Hp--;
        }
    }

    protected override void LoadComponents()
    {
        if (_enemyNearAbstract != null) return;
        _enemyNearAbstract = GetComponentInParent<EnemyNearCtrlAbstract>();
    }
}
