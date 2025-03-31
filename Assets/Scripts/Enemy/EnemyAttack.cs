using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class EnemyAttack : PISMonoBehaviour
{
    [SerializeField] protected EnemyCtrlAbstract _enemyCtrl;
    [SerializeField] protected EnemyState _curState;
    [SerializeField] protected float _timeAttack;
    protected float _timeCount;

    public EnemyState CurState { get => _curState; }

    private void Update()
    {
        _enemyCtrl.Anim.speed = UIGamePlayManager.Ins.CheckPlayTime ? 1 : 0;
        if (!UIGamePlayManager.Ins.CheckPlayTime) return;
        Attack();
    }

    private void ChangeState(EnemyState newState)
    {
        if (_curState == newState) return;
        _curState = newState;
        _enemyCtrl.Anim.SetInteger("State", (int)newState);
    }

    protected virtual void Attack()
    {
        AnimatorStateInfo stateInfo = _enemyCtrl.Anim.GetCurrentAnimatorStateInfo(0);
        if (_enemyCtrl.Hp <= 0)
        {
            ChangeState(EnemyState.Dying);
            return;
        }

        if (_curState == EnemyState.Attack)
        {
            if (stateInfo.IsName(_curState.ToString()) && stateInfo.normalizedTime >= 1f)
                ChangeState(_enemyCtrl.EnemyMoving.IsMoving ? EnemyState.Walk : EnemyState.Idle);
            return;
        }

        if (_enemyCtrl.EnemyMoving.IsMoving)
        {
            ChangeState(EnemyState.Walk);
        }
        else if (_timeCount >= _timeAttack)
        {
            _timeCount = 0;
            ChangeState(EnemyState.Attack);
        }
        else
        {
            _timeCount += Time.deltaTime;
            ChangeState(EnemyState.Idle);
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
