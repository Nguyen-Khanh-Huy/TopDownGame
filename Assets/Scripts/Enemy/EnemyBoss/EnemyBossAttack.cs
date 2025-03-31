using System;
using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using UnityEngine.UIElements;

public class EnemyBossAttack : EnemyAttack
{
    [SerializeField] private EnemyBossState _curBossState;
    [SerializeField] private EnemyBossState _prevBossState;

    [SerializeField] private bool _isDash;
    [SerializeField] private ParticleSystem _vfxDash;
    [SerializeField] private HitEnemyBossAttackDash _hitEnemyBossAttackDash;
    private Dictionary<EnemyBossState, Action<AnimatorStateInfo>> _attackHandlers;

    public bool IsDash { get => _isDash; set => _isDash = value; }

    private void Start()
    {
        _timeAttack = 3f;
        ResetInforAttackDash();
        _attackHandlers = new(){{ EnemyBossState.AttackDash, HandleAttackDash },
                                { EnemyBossState.AttackRain, HandleAttackRain },
                                { EnemyBossState.AttackLaser, HandleAttackLaser }};
    }

    private void ChangeState(EnemyBossState newState)
    {
        if (_curBossState == newState) return;
        _curBossState = newState;
        _enemyCtrl.Anim.SetInteger("State", (int)newState);
    }

    protected override void Attack()
    {
        AnimatorStateInfo stateInfo = _enemyCtrl.Anim.GetCurrentAnimatorStateInfo(0);
        if (_enemyCtrl.Hp <= 0)
        {
            ChangeState(EnemyBossState.Dying);
            return;
        }

        if (_attackHandlers.TryGetValue(_curBossState, out Action<AnimatorStateInfo> handler))
        {
            if (stateInfo.IsName(_curBossState.ToString()))
            {
                if (stateInfo.normalizedTime >= 1f)
                {
                    ResetInforAttackDash();
                    ChangeState(_enemyCtrl.EnemyMoving.IsMoving ? EnemyBossState.Walk : EnemyBossState.Idle);
                }
                else handler(stateInfo);
            }
            return;
        }

        if (_enemyCtrl.EnemyMoving.IsMoving)
        {
            ChangeState(EnemyBossState.Walk);
        }
        else if (_timeCount >= _timeAttack)
        {
            _timeCount = 0;
            //GetRandomAttack();
            ChangeState(EnemyBossState.AttackDash);
            //ChangeState(EnemyBossState.AttackRain);
            //ChangeState(EnemyBossState.AttackLaser);
        }
        else
        {
            _timeCount += Time.deltaTime;
            ChangeState(EnemyBossState.Idle);
        }
    }

    private void ResetInforAttackDash()
    {
        if (!_isDash && _enemyCtrl.Col.radius == 0.3f && !_vfxDash.gameObject.activeSelf) return;
        _isDash = false;
        _enemyCtrl.Col.radius = 0.3f;
        _vfxDash.gameObject.SetActive(false);
        PoolManager<EffectCtrlAbstract>.Ins.Spawn(_hitEnemyBossAttackDash, _enemyCtrl.transform.position + new Vector3(0f, 0.1f, 0f), Quaternion.Euler(90f, 0f, 0f));
    }

    private void SetInforAttackDash()
    {
        _enemyCtrl.Col.radius = 1f;
        _vfxDash.gameObject.SetActive(true);
    }
    private void HandleAttackDash(AnimatorStateInfo stateInfo)
    {
        if (_isDash)
        {
            _enemyCtrl.transform.Translate(8f * Time.deltaTime * Vector3.forward);
            SetInforAttackDash();
            if (Physics.Raycast(_enemyCtrl.transform.position + Vector3.up + _enemyCtrl.transform.forward, _enemyCtrl.transform.forward, 0.5f, LayerMask.GetMask("BG")))
            {
                ResetInforAttackDash();
            }
        }
    }

    private void HandleAttackRain(AnimatorStateInfo stateInfo)
    {
        Debug.Log("AttackRain");
    }

    private void HandleAttackLaser(AnimatorStateInfo stateInfo)
    {
        Debug.Log("AttackLaser");
    }

    private void GetRandomAttack()
    {
        List<EnemyBossState> attackStates = new() { EnemyBossState.AttackDash, EnemyBossState.AttackRain, EnemyBossState.AttackLaser };
        if (attackStates.Contains(_prevBossState))
        {
            attackStates.Remove(_prevBossState);
        }
        EnemyBossState newAttack = attackStates[UnityEngine.Random.Range(0, attackStates.Count)];
        ChangeState(newAttack);
        _prevBossState = _curBossState;
    }

    protected override void LoadComponents()
    {
        base.LoadComponents();
        if (_vfxDash != null && _hitEnemyBossAttackDash != null) return;
        _vfxDash = GetComponentInChildren<ParticleSystem>();
        _hitEnemyBossAttackDash = Resources.Load<HitEnemyBossAttackDash>("Hits/HitEnemyBossAttackDash");
    }
}
