using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class EnemyCtrlAbstract : PoolObj<EnemyCtrlAbstract>
{
    [SerializeField] protected int _hp;
    [SerializeField] protected Rigidbody _rb;
    [SerializeField] protected EnemyTarget _enemyTarget;
    [SerializeField] protected PlayerController _player;

    public int Hp { get => _hp; set => _hp = value; }
    public Rigidbody Rb { get => _rb; set => _rb = value; }
    public EnemyTarget EnemyTarget { get => _enemyTarget; }
    public PlayerController Player { get => _player; }

    protected override void LoadComponents()
    {
        if (_rb != null && _enemyTarget != null && _player != null) return;
        _rb = GetComponent<Rigidbody>();
        _enemyTarget = GetComponentInChildren<EnemyTarget>();
        _player = GameObject.Find("PlayerController").GetComponent<PlayerController>();
    }
}
