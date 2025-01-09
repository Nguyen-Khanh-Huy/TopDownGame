using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public abstract class EnemyCtrlAbstract : PoolObj<EnemyCtrlAbstract>
{
    [SerializeField] protected int _hp;
    [SerializeField] protected Animator _anim;
    [SerializeField] protected NavMeshAgent _agent;
    [SerializeField] protected EnemyMoving _enemyMoving;
    [SerializeField] protected PlayerController _player;

    public int Hp { get => _hp; set => _hp = value; }
    public Animator Anim { get => _anim; }
    public NavMeshAgent Agent { get => _agent; set => _agent = value; }
    public EnemyMoving EnemyMoving { get => _enemyMoving; }
    public PlayerController Player { get => _player; }

    protected override void LoadComponents()
    {
        if (_anim != null && _agent != null && _enemyMoving != null && _player != null) return;
        _anim = GetComponent<Animator>();
        _agent = GetComponent<NavMeshAgent>();
        _enemyMoving = GetComponentInChildren<EnemyMoving>();
        _player = GameObject.Find("PlayerController").GetComponent<PlayerController>();
    }
}
