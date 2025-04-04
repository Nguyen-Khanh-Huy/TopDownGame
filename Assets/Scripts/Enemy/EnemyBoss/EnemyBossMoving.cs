using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyBossMoving : EnemyMoving
{
    protected override void LookAtTarget()
    {
        AnimatorStateInfo stateInfo = _enemyCtrl.Anim.GetCurrentAnimatorStateInfo(0);
        if (stateInfo.IsName(EnemyBossState.AttackDash.ToString())
         || stateInfo.IsName(EnemyBossState.AttackRain.ToString())
         || stateInfo.IsName(EnemyBossState.AttackLaser.ToString())
         || stateInfo.IsName(EnemyBossState.AttackFire.ToString())) return;
        base.LookAtTarget();
    }

    protected override void EnemyMove()
    {
        _enemyCtrl.Agent.SetDestination(_enemyCtrl.PlayerCtrl.transform.position);
        float checkDistance = Vector3.Distance(_enemyCtrl.PlayerCtrl.transform.position, _enemyCtrl.transform.position);
        _isMoving = checkDistance > _distance;

        AnimatorStateInfo stateInfo = _enemyCtrl.Anim.GetCurrentAnimatorStateInfo(0);
        bool shouldStop = (_enemyCtrl.Anim.speed == 0
                        || stateInfo.IsName(EnemyBossState.Idle.ToString())
                        || stateInfo.IsName(EnemyBossState.AttackDash.ToString())
                        || stateInfo.IsName(EnemyBossState.AttackRain.ToString())
                        || stateInfo.IsName(EnemyBossState.AttackLaser.ToString())
                        || stateInfo.IsName(EnemyBossState.AttackFire.ToString())
                        || stateInfo.IsName(EnemyBossState.Dying.ToString()));

        _enemyCtrl.Agent.isStopped = shouldStop || !_isMoving;
    }
}
