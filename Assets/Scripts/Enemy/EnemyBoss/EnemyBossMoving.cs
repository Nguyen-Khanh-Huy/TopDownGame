using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyBossMoving : EnemyMoving
{
    protected override void LookAtTarget()
    {
        AnimatorStateInfo stateInfo = _enemyCtrl.Anim.GetCurrentAnimatorStateInfo(0);
        if (!UIGamePlayManager.Ins.CheckPlayTime 
         || _isFreeze
         || stateInfo.IsName(EnemyBossState.AttackDash.ToString())
         || stateInfo.IsName(EnemyBossState.AttackRain.ToString())
         || stateInfo.IsName(EnemyBossState.AttackLaser.ToString())
         || stateInfo.IsName(EnemyBossState.AttackFire.ToString())
         || stateInfo.IsName(EnemyBossState.Dying.ToString())) return;

        Vector3 targetPosition = _enemyCtrl.PlayerCtrl.transform.position;
        targetPosition.y = _enemyCtrl.transform.position.y;
        _enemyCtrl.transform.LookAt(targetPosition);
    }

    protected override void EnemyMove()
    {
        _enemyCtrl.Agent.SetDestination(_enemyCtrl.PlayerCtrl.transform.position);
        //float checkDistance = Vector3.Distance(_enemyCtrl.PlayerCtrl.transform.position, _enemyCtrl.transform.position);
        //_isMoving = checkDistance > _distance;

        float sqrDistance = (_enemyCtrl.PlayerCtrl.transform.position - _enemyCtrl.transform.position).sqrMagnitude;
        _isMoving = sqrDistance > (_distance * _distance);

        AnimatorStateInfo stateInfo = _enemyCtrl.Anim.GetCurrentAnimatorStateInfo(0);
        bool shouldStop = (!UIGamePlayManager.Ins.CheckPlayTime
                        || _isFreeze
                        || stateInfo.IsName(EnemyBossState.Idle.ToString())
                        || stateInfo.IsName(EnemyBossState.AttackDash.ToString())
                        || stateInfo.IsName(EnemyBossState.AttackRain.ToString())
                        || stateInfo.IsName(EnemyBossState.AttackLaser.ToString())
                        || stateInfo.IsName(EnemyBossState.AttackFire.ToString())
                        || stateInfo.IsName(EnemyBossState.Dying.ToString()));

        _enemyCtrl.Agent.isStopped = shouldStop || !_isMoving;
    }
}
