using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyNearAttack : PISMonoBehaviour
{
    [SerializeField] private EnemyNearCtrlAbstract _enemyNearAbstract;
    private bool _isAttackComplete;
    private void Update()
    {
        EnemyAttack();
    }

    private void ChangeState(EnemyState State)
    {
        _enemyNearAbstract.Anim.SetInteger("State", (int)State);
    }

    private void EnemyAttack()
    {
        if (!_enemyNearAbstract.EnemyMoving.IsStopMoving)
        {
            _isAttackComplete = false;
            ChangeState(EnemyState.Walk);
        }
        else
        {
            if (!_isAttackComplete)
            {
                ChangeState(EnemyState.Attack);
                AnimatorStateInfo stateInfo = _enemyNearAbstract.Anim.GetCurrentAnimatorStateInfo(0);
                if (stateInfo.IsName(EnemyState.Attack.ToString()) && stateInfo.normalizedTime >= 1f)
                {
                    _isAttackComplete = true;
                    ChangeState(EnemyState.Idle);
                }
            }
        }
    }

    protected override void LoadComponents()
    {
        if (_enemyNearAbstract != null) return;
        _enemyNearAbstract = GetComponentInParent<EnemyNearCtrlAbstract>();
    }
}
