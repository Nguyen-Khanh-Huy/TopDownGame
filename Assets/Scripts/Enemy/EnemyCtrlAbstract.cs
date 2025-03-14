using System.Collections;
using System.Collections.Generic;
using System.Security.Cryptography;
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
    [SerializeField] protected PlayerController _player;
    [SerializeField] protected ItemDropCoin _itemDropCoin;
    [SerializeField] protected Slider _hpBar;

    public int Hp { get => _hp; set => _hp = value; }
    public Animator Anim { get => _anim; }
    public NavMeshAgent Agent { get => _agent; set => _agent = value; }
    public EnemyMoving EnemyMoving { get => _enemyMoving; }
    public PlayerController Player { get => _player; }
    public ItemDropCoin ItemDropCoin { get => _itemDropCoin; }
    public Slider HpBar { get => _hpBar; set => _hpBar = value; }
    public EnemySO EnemySO { get => _enemySO; }

    protected override void LoadComponents()
    {
        if (_anim != null && _agent != null && _enemyMoving != null && _player != null && _itemDropCoin != null && _hpBar != null) return;
        _anim = GetComponent<Animator>();
        _agent = GetComponent<NavMeshAgent>();
        _enemyMoving = GetComponentInChildren<EnemyMoving>();
        _player = GameObject.Find("PlayerController").GetComponent<PlayerController>();
        _itemDropCoin = Resources.Load<ItemDropCoin>("ItemsDrop/ItemDropCoin");
        _hpBar = GetComponentInChildren<Slider>();
    }

    protected virtual void OnEnable()
    {
        Observer.AddObserver(ObserverID.EnemyTakeDmg, EnemyTakeDamage);
    }

    protected virtual void OnDisable()
    {
        Observer.RemoveObserver(ObserverID.EnemyTakeDmg, EnemyTakeDamage);
    }
    private void EnemyTakeDamage(object[] parameters)
    {
        if (parameters.Length < 1) return;
        EnemyCtrlAbstract enemy = (EnemyCtrlAbstract)parameters[0];
        if (enemy != this) return;
        if (_hp <= 0) return;
        _hp--;
        _hpBar.value = (float)_hp / EnemySO.Hp;
    }
}
