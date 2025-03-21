using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class EnemyAttack : PISMonoBehaviour
{
    [SerializeField] private EnemyCtrlAbstract _enemyCtrlAbstract;
    [SerializeField] private EnemyState _curState;
    [SerializeField] protected float _timeAttack;
    protected float _timeCount;

    public EnemyState CurState { get => _curState; }

    private void Update()
    {
        if (!UIGamePlayManager.Ins.CheckPlayTime)
        {
            PauseGame();
            return;
        }
        if (Input.GetKeyDown(KeyCode.Space))
        {
            ChangeState(EnemyState.Hit);
            if (_curState == EnemyState.Hit)
            {
                Debug.Log("zzzzzzzz");
            }
        }
        Attack();
    }

    private void PauseGame()
    {
        ChangeState(EnemyState.Idle);
    }

    public void ChangeState(EnemyState newState)
    {
        if (_curState != newState)
        {
            _curState = newState;
            _enemyCtrlAbstract.Anim.SetInteger("State", (int)newState);
        }
    }

    private void Attack()
    {
        AnimatorStateInfo stateInfo = _enemyCtrlAbstract.Anim.GetCurrentAnimatorStateInfo(0);
        if (_enemyCtrlAbstract.Hp <= 0)
        {
            ChangeState(EnemyState.Dying);
            return;
        }

        if (_curState == EnemyState.Hit)
        {
            if (stateInfo.IsName(EnemyState.Hit.ToString()) && stateInfo.normalizedTime >= 1f)
                ChangeState(_enemyCtrlAbstract.EnemyMoving.IsMoving ? EnemyState.Walk : EnemyState.Idle);
            return;
        }

        if (_curState == EnemyState.Attack)
        {
            if (stateInfo.IsName(EnemyState.Attack.ToString()) && stateInfo.normalizedTime >= 1f)
                ChangeState(_enemyCtrlAbstract.EnemyMoving.IsMoving ? EnemyState.Walk : EnemyState.Idle);
            return;
        }

        if (_curState != EnemyState.Attack && _enemyCtrlAbstract.EnemyMoving.IsMoving)
        {
            ChangeState(EnemyState.Walk);
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
                    ChangeState(EnemyState.Idle);
            }
        }
    }

    protected override void LoadComponents()
    {
        if (_enemyCtrlAbstract != null) return;
        _enemyCtrlAbstract = GetComponentInParent<EnemyCtrlAbstract>();
    }
}
