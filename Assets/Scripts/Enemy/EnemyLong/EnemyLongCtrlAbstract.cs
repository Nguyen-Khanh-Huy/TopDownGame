using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class EnemyLongCtrlAbstract : EnemyCtrlAbstract
{
    [SerializeField] private EnemyLongAttack _enemyLongAttack;
    public EnemyLongAttack EnemyLongAttack { get => _enemyLongAttack; set => _enemyLongAttack = value; }

    protected override void LoadComponents()
    {
        base.LoadComponents();
        if (_enemyLongAttack != null) return;
        _enemyLongAttack = GetComponentInChildren<EnemyLongAttack>();
    }

    private void OnEnable()
    {
        _hp = 3;
    }
}
