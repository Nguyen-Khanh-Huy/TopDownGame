using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyLongMoving : EnemyMoving
{
    protected override void Start()
    {
        _distance = 6f;
    }

    protected override void LookAtTarget()
    {
        if (_enemyCtrl.Hp <= 0) return;
        if (!UIGamePlayManager.Ins.CheckPlayTime
         || _isFreeze
         || _enemyCtrl.EnemyAttack.CurState == EnemyNearLongState.Walk
         || _enemyCtrl.EnemyAttack.CurState == EnemyNearLongState.Attack
         || _enemyCtrl.EnemyAttack.CurState == EnemyNearLongState.Dying) return;

        Vector3 targetPosition = PlayerCtrl.Ins.transform.position;
        targetPosition.y = _enemyCtrl.transform.position.y;
        //_enemyCtrl.transform.LookAt(targetPosition);
        Quaternion targetRotation = Quaternion.LookRotation(targetPosition - _enemyCtrl.transform.position);
        _enemyCtrl.transform.rotation = Quaternion.Slerp(_enemyCtrl.transform.rotation, targetRotation, Time.deltaTime * 10f);
    }
}
