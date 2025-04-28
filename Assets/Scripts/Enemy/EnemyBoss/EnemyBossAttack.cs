using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

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

    [Header("Attack Rain:")]
    [SerializeField] private bool _isAttackRain;
    [SerializeField] private bool _isGetPosPlayer;
    [SerializeField] private int _rainCount;
    [SerializeField] private float _timeRainCount;
    [SerializeField] private Vector3 _targetPos;
    [SerializeField] private BulletEnemyBossAttackRain _bulletAttackRain;
    [SerializeField] private VFXWarningEnemyBossAttackRain _vfxWarningRain;

    [Header("Attack Laser:")]
    [SerializeField] private bool _isAttackLaser;
    [SerializeField] private bool _isSpawnVfxLaser;
    [SerializeField] private VFXLaserEnemyBossAttackLaser _vfxLaser;
    [SerializeField] private CapsuleCollider _colAttackLaser;
    [SerializeField] private Transform _pointAttackLaser;

    [Header("Attack Fire:")]
    [SerializeField] private bool _isAttackFire;
    [SerializeField] private BulletEnemyBossAttackFire _bulletAttackFire;
    [SerializeField] private Transform _pointAttackFire;

    private Dictionary<EnemyBossState, Action<AnimatorStateInfo>> _attackHandlers;

    public bool IsAttackDash { get => _isAttackDash; set => _isAttackDash = value; }
    public bool IsAttackRain { get => _isAttackRain; set => _isAttackRain = value; }
    public bool IsAttackLaser { get => _isAttackLaser; set => _isAttackLaser = value; }
    public bool IsAttackFire { get => _isAttackFire; set => _isAttackFire = value; }

    private void Start()
    {
        _timeAttack = 1.5f;
        _attackHandlers = new(){{ EnemyBossState.AttackDash, HandleAttackDash },
                                { EnemyBossState.AttackRain, HandleAttackRain },
                                { EnemyBossState.AttackLaser, HandleAttackLaser },
                                { EnemyBossState.AttackFire, HandleAttackFire }};
    }

    private void OnEnable()
    {
        ResetInforAttackDash();
        ResetInforAttackRain();
        ResetInforAttackLaser();
        ResetInforAttackFire();
    }

    private void OnTriggerEnter(Collider other)
    {
        PlayerController player = other.GetComponent<PlayerController>();
        if (player != null)
        {
            Observer.Notify(ObserverID.PlayerTakeDmg);
        }
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
            StopAttackDash();
            StopAttackRain();
            StopAttackLaser();
            StopAttackFire();
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
            //ChangeState(EnemyBossState.AttackFire);
        }
        else
        {
            _timeCount += Time.deltaTime;
            ChangeState(EnemyBossState.Idle);
        }
    }

    //-----------------------------------------------------------------------

    private void ResetInforAttackDash()
    {
        _isSpawnVfxDash = false;
        GetComponentInParent<CapsuleCollider>().radius = 0.4f;
    }

    public void StopAttackDash()
    {
        if (!_isSpawnVfxDash) return;
        _isAttackDash = false;
        _isSpawnVfxDash = false;
        GetComponentInParent<CapsuleCollider>().radius = 0.4f;
        PoolManager<EffectCtrlAbstract>.Ins.Despawn(transform.GetComponentInChildren<VFXDashEnemyBossAttackDash>());
        PoolManager<EffectCtrlAbstract>.Ins.Spawn(_vfxSmokeAttackDash, _enemyCtrl.transform.position + new Vector3(0f, 0.1f, 0f), Quaternion.Euler(90f, 0f, 0f));
    }

    private void HandleAttackDash(AnimatorStateInfo stateInfo)
    {
        if (_isAttackDash)
        {
            if (!_isSpawnVfxDash)
            {
                _isSpawnVfxDash = true;
                GetComponentInParent<CapsuleCollider>().radius = 1f;
                EffectCtrlAbstract newVfxDash = PoolManager<EffectCtrlAbstract>.Ins.Spawn(_vfxDash, _pointAttackDash.transform.position, Quaternion.identity);
                newVfxDash.Attacker = _enemyCtrl;
                newVfxDash.transform.SetParent(transform);
            }
            _enemyCtrl.transform.Translate(8f * Time.deltaTime * Vector3.forward);
            if (Physics.Raycast(_enemyCtrl.transform.position + Vector3.up + _enemyCtrl.transform.forward, _enemyCtrl.transform.forward, 0.2f, LayerMask.GetMask("BG")))
            {
                ChangeState(_enemyCtrl.EnemyMoving.IsMoving ? EnemyBossState.Walk : EnemyBossState.Idle);
                StopAttackDash();
            }
        }
    }

    //-----------------------------------------------------------------------

    public void ResetInforAttackRain()
    {
        _timeRainCount = 0f;
        _isGetPosPlayer = false;
        _rainCount = 0;
    }

    private void StopAttackRain()
    {
        PoolManager<EffectCtrlAbstract>.Ins.DespawnAll(_vfxWarningRain, "EffectPool");
        PoolManager<BulletCtrlAbstract>.Ins.DespawnAll(_bulletAttackRain, "BulletPool");
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
                EffectCtrlAbstract newVfx = PoolManager<EffectCtrlAbstract>.Ins.Spawn(_vfxWarningRain, GetPosPlayer() + new Vector3(0f, 0.1f, 0f), Quaternion.identity);
                newVfx.Attacker = _enemyCtrl;

                BulletCtrlAbstract newBullet = PoolManager<BulletCtrlAbstract>.Ins.Spawn(_bulletAttackRain, GetPosPlayer() + new Vector3(0f, 10f, 0f), Quaternion.identity);
                newBullet.Attacker = _enemyCtrl;

                _timeRainCount = 0;
                _isGetPosPlayer = false;
                _rainCount++;
            }
        }
    }

    //-----------------------------------------------------------------------

    private void ResetInforAttackLaser()
    {
        _colAttackLaser.enabled = false;
        _isSpawnVfxLaser = false;
    }

    public void StopAttackLaser()
    {
        _colAttackLaser.enabled = false;
        _isSpawnVfxLaser = false;
        PoolManager<EffectCtrlAbstract>.Ins.Despawn(transform.GetComponentInChildren<VFXLaserEnemyBossAttackLaser>());
    }

    private void HandleAttackLaser(AnimatorStateInfo stateInfo)
    {
        if (_isAttackLaser)
        {
            if (!_isSpawnVfxLaser)
            {
                _isSpawnVfxLaser = true;
                _colAttackLaser.enabled = true;
                EffectCtrlAbstract newVfxLaser = PoolManager<EffectCtrlAbstract>.Ins.Spawn(_vfxLaser, _pointAttackLaser.transform.position, _pointAttackLaser.transform.rotation);
                newVfxLaser.transform.SetParent(transform);
            }
        }
    }

    //-----------------------------------------------------------------------

    private void ResetInforAttackFire()
    {
        _isAttackFire = false;
    }

    private void StopAttackFire()
    {
        _isAttackFire = false;
        PoolManager<BulletCtrlAbstract>.Ins.DespawnAll(_bulletAttackFire, "BulletPool");
    }

    private void HandleAttackFire(AnimatorStateInfo stateInfo)
    {
        if (_isAttackFire)
        {
            _isAttackFire = false;
            BulletCtrlAbstract newBullet = PoolManager<BulletCtrlAbstract>.Ins.Spawn(_bulletAttackFire, _pointAttackFire.position, _pointAttackFire.rotation);
            newBullet.Attacker = _enemyCtrl;
        }
    }

    //-----------------------------------------------------------------------

    private void GetRandomAttack()
    {
        List<EnemyBossState> listAttack = new() { EnemyBossState.AttackDash, EnemyBossState.AttackRain, EnemyBossState.AttackLaser, EnemyBossState.AttackFire };
        if (listAttack.Contains(_prevBossState))
            listAttack.Remove(_prevBossState);

        float distance = Vector3.Distance(_enemyCtrl.transform.position, _enemyCtrl.PlayerCtrl.transform.position);
        if (Physics.Raycast(_enemyCtrl.transform.position + Vector3.up, _enemyCtrl.transform.forward, distance, LayerMask.GetMask("BG")))
            listAttack.Remove(EnemyBossState.AttackDash);

        EnemyBossState newAttack = listAttack[UnityEngine.Random.Range(0, listAttack.Count)];
        ChangeState(newAttack);
        _prevBossState = _curBossState;
    }

    protected override void LoadComponents()
    {
        base.LoadComponents();
        if (_vfxDash != null && _vfxSmokeAttackDash != null && _pointAttackDash != null && _bulletAttackRain != null && _vfxWarningRain != null && _vfxLaser != null && _colAttackLaser != null && _pointAttackLaser != null && _bulletAttackFire != null && _pointAttackFire != null) return;
        _vfxDash = Resources.Load<VFXDashEnemyBossAttackDash>("VFX/VFXDashEnemyBossAttackDash");
        _vfxSmokeAttackDash = Resources.Load<VFXSmokeEnemyBossAttackDash>("VFX/VFXSmokeEnemyBossAttackDash");
        _pointAttackDash = transform.GetChild(0);

        _bulletAttackRain = Resources.Load<BulletEnemyBossAttackRain>("Bullets/BulletEnemyBossAttackRain");
        _vfxWarningRain = Resources.Load<VFXWarningEnemyBossAttackRain>("VFX/VFXWarningEnemyBossAttackRain");

        _vfxLaser = Resources.Load<VFXLaserEnemyBossAttackLaser>("VFX/VFXLaserEnemyBossAttackLaser");
        _colAttackLaser = GetComponent<CapsuleCollider>();
        _pointAttackLaser = transform.GetChild(1);

        _bulletAttackFire = Resources.Load<BulletEnemyBossAttackFire>("Bullets/BulletEnemyBossAttackFire");
        _pointAttackFire = transform.GetChild(2);
    }
}
