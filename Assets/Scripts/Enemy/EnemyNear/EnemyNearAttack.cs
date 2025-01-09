using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyNearAttack : PISMonoBehaviour
{
    [SerializeField] private EnemyNearCtrlAbstract _enemyNearAbstract;
    [SerializeField] private EnemyState currentState;
    [SerializeField] private float _timeAttack = 0.5f;
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
            _enemyNearAbstract.Anim.SetInteger("State", (int)newState);
        }
    }
    private void EnemyAttack()
    {
        AnimatorStateInfo stateInfo = _enemyNearAbstract.Anim.GetCurrentAnimatorStateInfo(0);
        _enemyNearAbstract.Agent.isStopped = (currentState == EnemyState.Idle || currentState == EnemyState.Attack);

        if (currentState == EnemyState.Attack && stateInfo.IsName(EnemyState.Attack.ToString()) && stateInfo.normalizedTime >= 1f)
        {
            ChangeState(_enemyNearAbstract.EnemyMoving.IsStopMoving ? EnemyState.Idle : EnemyState.Walk);
            return;
        }

        if (!_enemyNearAbstract.EnemyMoving.IsStopMoving)
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
