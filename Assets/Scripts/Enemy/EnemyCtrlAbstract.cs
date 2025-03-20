using System.Collections;
using System.Collections.Generic;
using System.Security.Cryptography;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.UI;

public abstract class EnemyCtrlAbstract : PoolObj<EnemyCtrlAbstract>
{
    [SerializeField] protected int _hp;
    [SerializeField] protected EnemySO _enemySO;
    [SerializeField] protected Animator _anim;
    [SerializeField] protected NavMeshAgent _agent;
    [SerializeField] protected EnemyMoving _enemyMoving;
    [SerializeField] protected PlayerController _playerCtrl;
    [SerializeField] protected ItemDropMana _itemDropMana;
    [SerializeField] protected Slider _hpBar;

    public int Hp { get => _hp; set => _hp = value; }
    public Animator Anim { get => _anim; }
    public NavMeshAgent Agent { get => _agent; set => _agent = value; }
    public EnemyMoving EnemyMoving { get => _enemyMoving; }
    public PlayerController PlayerCtrl { get => _playerCtrl; }
    public ItemDropMana ItemDropMana { get => _itemDropMana; }
    public Slider HpBar { get => _hpBar; set => _hpBar = value; }
    public EnemySO EnemySO { get => _enemySO; }

    protected override void LoadComponents()
    {
        if (_anim != null && _agent != null && _enemyMoving != null && _playerCtrl != null && _itemDropMana != null && _hpBar != null) return;
        _anim = GetComponent<Animator>();
        _agent = GetComponent<NavMeshAgent>();
        _enemyMoving = GetComponentInChildren<EnemyMoving>();
        _playerCtrl = GameObject.Find("PlayerController").GetComponent<PlayerController>();
        _itemDropMana = Resources.Load<ItemDropMana>("ItemsDrop/ItemDropMana");
        _hpBar = GetComponentInChildren<Slider>();
    }

    protected virtual void OnEnable()
    {
        Observer.AddObserver<EnemyCtrlAbstract>(ObserverID.EnemyTakeDmg, EnemyTakeDamage);
    }

    protected virtual void OnDisable()
    {
        Observer.RemoveObserver<EnemyCtrlAbstract>(ObserverID.EnemyTakeDmg, EnemyTakeDamage);
    }

    private void EnemyTakeDamage(EnemyCtrlAbstract enemy)
    {
        if (_hp <= 0) return;
        if (enemy == this || Vector3.Distance(enemy.transform.position, transform.position) <= _playerCtrl.PlayerSkillsCtrl.PlayerSkillList.PlayerSkillAoeBullet.AoeBullet)
        {
            _hp--;
            _hpBar.value = (float)_hp / EnemySO.Hp;
        }
    }
}
