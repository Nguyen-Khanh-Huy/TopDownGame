using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class EnemyAttack : PISMonoBehaviour
{
    [SerializeField] protected EnemyCtrlAbstract _enemyCtrl;
    [SerializeField] protected EnemyNearLongState _curEnemyState;
    [SerializeField] protected float _timeAttack;
    protected float _timeCount;

    public EnemyNearLongState CurState { get => _curEnemyState; }

    private void Update()
    {
        if (!UIGamePlayManager.Ins.CheckPlayTime || _enemyCtrl.EnemyMoving.IsFreeze)
        {
            if (_enemyCtrl.Anim.speed != 0) _enemyCtrl.Anim.speed = 0;
            return;
        }
        if (_enemyCtrl.Anim.speed != 1) _enemyCtrl.Anim.speed = 1;
        Attack();
    }

    private void ChangeState(EnemyNearLongState newState)
    {
        if (_curEnemyState == newState) return;
        _curEnemyState = newState;
        _enemyCtrl.Anim.SetInteger("State", (int)newState);
    }

    protected virtual void Attack()
    {
        AnimatorStateInfo stateInfo = _enemyCtrl.Anim.GetCurrentAnimatorStateInfo(0);
        if (_enemyCtrl.Hp <= 0)
        {
            ChangeState(EnemyNearLongState.Dying);
            return;
        }

        if (_curEnemyState == EnemyNearLongState.Attack)
        {
            if (stateInfo.IsName(_curEnemyState.ToString()) && stateInfo.normalizedTime >= 1f)
                ChangeState(_enemyCtrl.EnemyMoving.IsMoving ? EnemyNearLongState.Walk : EnemyNearLongState.Idle);
            return;
        }

        if (_enemyCtrl.EnemyMoving.IsMoving)
        {
            ChangeState(EnemyNearLongState.Walk);
        }
        else if (_timeCount >= _timeAttack)
        {
            _timeCount = 0;
            ChangeState(EnemyNearLongState.Attack);
        }
        else
        {
            _timeCount += Time.deltaTime;
            ChangeState(EnemyNearLongState.Idle);
        }
    }

    //private void Attack()
    //{
    //    AnimatorStateInfo stateInfo = _enemyCtrlAbstract.Anim.GetCurrentAnimatorStateInfo(0);
    //    if (_enemyCtrlAbstract.Hp <= 0)
    //    {
    //        ChangeState(EnemyState.Dying);
    //        return;
    //    }

    //    if (_curState == EnemyState.Hit)
    //    {
    //        if (stateInfo.IsName(EnemyState.Hit.ToString()) && stateInfo.normalizedTime >= 1f)
    //            ChangeState(_enemyCtrlAbstract.EnemyMoving.IsMoving ? EnemyState.Walk : EnemyState.Idle);
    //        return;
    //    }

    //    if (_curState == EnemyState.Attack)
    //    {
    //        if (stateInfo.IsName(EnemyState.Attack.ToString()) && stateInfo.normalizedTime >= 1f)
    //            ChangeState(_enemyCtrlAbstract.EnemyMoving.IsMoving ? EnemyState.Walk : EnemyState.Idle);
    //        return;
    //    }

    //    if (_curState != EnemyState.Attack && _enemyCtrlAbstract.EnemyMoving.IsMoving)
    //    {
    //        ChangeState(EnemyState.Walk);
    //    }

    //    else
    //    {
    //        if (_curState != EnemyState.Attack)
    //        {
    //            _timeCount += Time.deltaTime;
    //            if (_timeCount >= _timeAttack)
    //            {
    //                _timeCount = 0;
    //                ChangeState(EnemyState.Attack);
    //            }
    //            else
    //                ChangeState(EnemyState.Idle);
    //        }
    //    }
    //}

    protected override void LoadComponents()
    {
        if (_enemyCtrl != null) return;
        _enemyCtrl = GetComponentInParent<EnemyCtrlAbstract>();
    }
}
