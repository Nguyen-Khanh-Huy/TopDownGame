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
    [SerializeField] protected PlayerController _playerCtrl;
    [SerializeField] protected EnemyMoving _enemyMoving;
    [SerializeField] protected EnemyAttack _enemyAttack;
    [SerializeField] protected EnemySO _enemySO;
    [SerializeField] protected Animator _anim;
    [SerializeField] protected NavMeshAgent _agent;
    [SerializeField] protected ItemDropMana _itemDropMana;
    [SerializeField] protected Slider _hpBar;

    public int Hp { get => _hp; set => _hp = value; }
    public PlayerController PlayerCtrl { get => _playerCtrl; }
    public EnemyMoving EnemyMoving { get => _enemyMoving; }
    public EnemyAttack EnemyAttack { get => _enemyAttack; }
    public Animator Anim { get => _anim; }
    public NavMeshAgent Agent { get => _agent; set => _agent = value; }
    public ItemDropMana ItemDropMana { get => _itemDropMana; }
    public Slider HpBar { get => _hpBar; set => _hpBar = value; }
    public EnemySO EnemySO { get => _enemySO; }

    protected override void LoadComponents()
    {
        if (_playerCtrl != null && _enemyMoving != null && _enemyAttack && _anim != null && _agent != null && _itemDropMana != null && _hpBar != null) return;
        _playerCtrl = GameObject.Find("PlayerController").GetComponent<PlayerController>();
        _enemyMoving = GetComponentInChildren<EnemyMoving>();
        _enemyAttack = GetComponentInChildren<EnemyAttack>();
        _anim = GetComponent<Animator>();
        _agent = GetComponent<NavMeshAgent>();
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
        if (enemy == this || EnemyTakeDmgSkillAoeBullet(enemy))
        {
            _hp--;
            _hpBar.value = (float)_hp / EnemySO.Hp;
            //_enemyAttack.ChangeState(EnemyState.Hit);
        }
    }

    private bool EnemyTakeDmgSkillAoeBullet(EnemyCtrlAbstract enemy)
    {
        return Vector3.Distance(enemy.transform.position, transform.position) <= _playerCtrl.PlayerSkillsCtrl.PlayerSkillList.PlayerSkillAoeBullet.AoeBullet;
    }
}
