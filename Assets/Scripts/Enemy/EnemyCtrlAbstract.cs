using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.UI;

public abstract class EnemyCtrlAbstract : PoolObj<EnemyCtrlAbstract>
{
    [SerializeField] protected int _hp;
    [SerializeField] protected PlayerController _playerCtrl;
    [SerializeField] protected EnemyMoving _enemyMoving;
    [SerializeField] protected EnemyAttack _enemyAttack;
    [SerializeField] protected EnemyFlashingEffect _enemyFlashingEffect;
    [SerializeField] protected EnemySO _enemySO;
    [SerializeField] protected Animator _anim;
    [SerializeField] protected CapsuleCollider _col;
    [SerializeField] protected NavMeshAgent _agent;
    [SerializeField] protected ItemDropMana _itemDropMana;
    [SerializeField] protected Slider _hpBar;

    public int Hp { get => _hp; set => _hp = value; }
    public PlayerController PlayerCtrl { get => _playerCtrl; }
    public EnemyMoving EnemyMoving { get => _enemyMoving; }
    public EnemyAttack EnemyAttack { get => _enemyAttack; }
    public EnemyFlashingEffect EnemyFlashingEffect { get => _enemyFlashingEffect; }
    public Animator Anim { get => _anim; }
    public NavMeshAgent Agent { get => _agent; set => _agent = value; }
    public ItemDropMana ItemDropMana { get => _itemDropMana; }
    public Slider HpBar { get => _hpBar; set => _hpBar = value; }
    public EnemySO EnemySO { get => _enemySO; }
    public CapsuleCollider Col { get => _col; set => _col = value; }

    protected override void LoadComponents()
    {
        if (_playerCtrl != null && _enemyMoving != null && _enemyAttack && _enemyFlashingEffect != null && _anim != null && _col != null && _agent != null && _itemDropMana != null && _hpBar != null) return;
        _playerCtrl = GameObject.Find("PlayerController").GetComponent<PlayerController>();
        _enemyMoving = GetComponentInChildren<EnemyMoving>();
        _enemyAttack = GetComponentInChildren<EnemyAttack>();
        _enemyFlashingEffect = GetComponentInChildren<EnemyFlashingEffect>();
        _anim = GetComponent<Animator>();
        _col = GetComponent<CapsuleCollider>();
        _agent = GetComponent<NavMeshAgent>();
        _itemDropMana = Resources.Load<ItemDropMana>("ItemsDrop/ItemDropMana");
        _hpBar = GetComponentInChildren<Slider>();
    }

    protected virtual void OnEnable()
    {
        Observer.AddObserver<EnemyCtrlAbstract>(ObserverID.EnemyTakeDmg, EnemyTakeDamage);
        Observer.AddObserver<EnemyCtrlAbstract>(ObserverID.EnemyTakeDmgSingle, EnemyTakeDamageSingle);
    }

    protected virtual void OnDisable()
    {
        Observer.RemoveObserver<EnemyCtrlAbstract>(ObserverID.EnemyTakeDmg, EnemyTakeDamage);
        Observer.RemoveObserver<EnemyCtrlAbstract>(ObserverID.EnemyTakeDmgSingle, EnemyTakeDamageSingle);
    }

    private void EnemyTakeDamage(EnemyCtrlAbstract enemy)
    {
        if (_hp <= 0) return;
        if (enemy == this || EnemyTakeDmgSkillAoeBullet(enemy))
        {
            _hp--;
            _hpBar.value = (float)_hp / EnemySO.Hp;
            _enemyFlashingEffect.StartFlash();
        }
    }

    private void EnemyTakeDamageSingle(EnemyCtrlAbstract enemy)
    {
        if (_hp <= 0) return;
        if (enemy == this)
        {
            _hp--;
            _hpBar.value = (float)_hp / EnemySO.Hp;
            _enemyFlashingEffect.StartFlash();
        }
    }

    private bool EnemyTakeDmgSkillAoeBullet(EnemyCtrlAbstract enemy)
    {
        return Vector3.Distance(transform.position, enemy.transform.position) <= _playerCtrl.PlayerSkillsCtrl.PlayerSkillList.PlayerSkillAoeDamage.AoeRange;
    }
}
