using System;
using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEditor.PackageManager;
using UnityEngine;
using UnityEngine.UIElements;
using static UnityEngine.EventSystems.EventTrigger;

public class EnemyBossAttack : EnemyAttack
{
    [SerializeField] private EnemyBossState _curBossState;
    [SerializeField] private EnemyBossState _prevBossState;

    [Header("Attack Dash:")]
    [SerializeField] private bool _isAttackDash;
    [SerializeField] private bool _isSpawnVfxDash;
    [SerializeField] private VFXDashEnemyBossAttackDash _vfxDash;
    [SerializeField] private VFXSmokeEnemyBossAttackDash _vfxSmokeAttackDash;
    [SerializeField] private Transform _pointAttackDash;
    private Dictionary<EnemyBossState, Action<AnimatorStateInfo>> _attackHandlers;

    [Header("Attack Rain:")]
    [SerializeField] private bool _isAttackRain;
    [SerializeField] private bool _isGetPosPlayer;
    [SerializeField] private int _rainCount;
    [SerializeField] private float _timeRainCount;
    [SerializeField] private Vector3 _targetPos;
    [SerializeField] private BulletEnemyBossAttackRain _bulletAttackRain;
    [SerializeField] private VFXWarningEnemyBossAttackRain _vfxWarningRain;

    public bool IsAttackDash { get => _isAttackDash; set => _isAttackDash = value; }
    public bool IsSpawnVfxDash { get => _isSpawnVfxDash; set => _isSpawnVfxDash = value; }
    public bool IsAttackRain { get => _isAttackRain; set => _isAttackRain = value; }

    private void Start()
    {
        _timeAttack = 3f;
        _attackHandlers = new(){{ EnemyBossState.AttackDash, HandleAttackDash },
                                { EnemyBossState.AttackRain, HandleAttackRain },
                                { EnemyBossState.AttackLaser, HandleAttackLaser }};
    }

    private void OnEnable()
    {
        ResetInforAttackDash();
        ResetInforAttackRain();
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
            PoolManager<EffectCtrlAbstract>.Ins.Despawn(transform.GetComponentInChildren<EffectCtrlAbstract>());

            PoolManager<EffectCtrlAbstract>.Ins.DespawnAll(_vfxWarningRain, "EffectPool");
            PoolManager<BulletCtrlAbstract>.Ins.DespawnAll(_bulletAttackRain, "BulletPool");
            return;
        }

        if (_attackHandlers.TryGetValue(_curBossState, out Action<AnimatorStateInfo> handler))
        {
            if (stateInfo.IsName(_curBossState.ToString()))
            {
                if (stateInfo.normalizedTime >= 1f) ChangeState(_enemyCtrl.EnemyMoving.IsMoving ? EnemyBossState.Walk : EnemyBossState.Idle);
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

            GetRandomAttack();
            //ChangeState(EnemyBossState.AttackDash);
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
        _isSpawnVfxDash = false;
        _enemyCtrl.Col.radius = 0.4f;
    }

    public void StopAttackDash()
    {
        _isSpawnVfxDash = false;
        _enemyCtrl.Col.radius = 0.4f;
        PoolManager<EffectCtrlAbstract>.Ins.Despawn(transform.GetComponentInChildren<EffectCtrlAbstract>());
        PoolManager<EffectCtrlAbstract>.Ins.Spawn(_vfxSmokeAttackDash, _enemyCtrl.transform.position + new Vector3(0f, 0.1f, 0f), Quaternion.Euler(90f, 0f, 0f));
    }

    private void StartAttackDash()
    {
        _enemyCtrl.Col.radius = 1f;
        EffectCtrlAbstract newVfxDash = PoolManager<EffectCtrlAbstract>.Ins.Spawn(_vfxDash, _pointAttackDash.transform.position, Quaternion.identity);
        newVfxDash.transform.SetParent(transform);
    }
    private void HandleAttackDash(AnimatorStateInfo stateInfo)
    {
        if (_isAttackDash)
        {
            if (!_isSpawnVfxDash)
            {
                StartAttackDash();
                _isSpawnVfxDash = true;
            }
            _enemyCtrl.transform.Translate(8f * Time.deltaTime * Vector3.forward);
            if (Physics.Raycast(_enemyCtrl.transform.position + Vector3.up + _enemyCtrl.transform.forward, _enemyCtrl.transform.forward, 0.2f, LayerMask.GetMask("BG")))
            {
                ChangeState(_enemyCtrl.EnemyMoving.IsMoving ? EnemyBossState.Walk : EnemyBossState.Idle);
                StopAttackDash();
                _isAttackDash = false;
            }
        }
    }

    public void ResetInforAttackRain()
    {
        _timeRainCount = 0f;
        _isGetPosPlayer = false;
        _rainCount = 0;
    }
    private Vector3 GetPosPlayer()
    {
        if (!_isGetPosPlayer)
        {
            _isGetPosPlayer = true;
            _targetPos = _enemyCtrl.PlayerCtrl.transform.position;
        }
        return _targetPos;
    }

    private void HandleAttackRain(AnimatorStateInfo stateInfo)
    {
        if (_isAttackRain)
        {
            if (_rainCount >= 5) return;
            _timeRainCount += Time.deltaTime;
            if (_timeRainCount >= 0.5f)
            {
                PoolManager<EffectCtrlAbstract>.Ins.Spawn(_vfxWarningRain, GetPosPlayer() + new Vector3(0f, 0.1f, 0f), Quaternion.identity);
                PoolManager<BulletCtrlAbstract>.Ins.Spawn(_bulletAttackRain, GetPosPlayer() + new Vector3(0f, 10f, 0f), Quaternion.identity);
                _timeRainCount = 0;
                _isGetPosPlayer = false;
                _rainCount++;
            }
        }
    }

    private void HandleAttackLaser(AnimatorStateInfo stateInfo)
    {
        Debug.Log("AttackLaser");
    }

    private void GetRandomAttack()
    {
        List<EnemyBossState> listAttack = new() { EnemyBossState.AttackDash, EnemyBossState.AttackRain, EnemyBossState.AttackLaser };
        if (listAttack.Contains(_prevBossState))
            listAttack.Remove(_prevBossState);

        float distance = Vector3.Distance(_enemyCtrl.PlayerCtrl.transform.position, _enemyCtrl.transform.position);
        if (Physics.Raycast(_enemyCtrl.transform.position + Vector3.up + _enemyCtrl.transform.forward, _enemyCtrl.transform.forward, distance, LayerMask.GetMask("BG")))
            listAttack.Remove(EnemyBossState.AttackDash);

        EnemyBossState newAttack = listAttack[UnityEngine.Random.Range(0, listAttack.Count)];
        ChangeState(newAttack);
        _prevBossState = _curBossState;
    }

    protected override void LoadComponents()
    {
        base.LoadComponents();
        if (_vfxDash != null && _vfxSmokeAttackDash != null && _pointAttackDash != null && _bulletAttackRain != null && _vfxWarningRain != null) return;
        _vfxDash = Resources.Load<VFXDashEnemyBossAttackDash>("VFX/VFXDashEnemyBossAttackDash");
        _vfxSmokeAttackDash = Resources.Load<VFXSmokeEnemyBossAttackDash>("VFX/VFXSmokeEnemyBossAttackDash");
        _pointAttackDash = transform.GetChild(0);

        _bulletAttackRain = Resources.Load<BulletEnemyBossAttackRain>("Bullets/BulletEnemyBossAttackRain");
        _vfxWarningRain = Resources.Load<VFXWarningEnemyBossAttackRain>("VFX/VFXWarningEnemyBossAttackRain");
    }
}
