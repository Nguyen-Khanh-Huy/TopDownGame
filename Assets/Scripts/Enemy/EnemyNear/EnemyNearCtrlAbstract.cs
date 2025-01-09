using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class EnemyNearCtrlAbstract : EnemyCtrlAbstract
{
    [SerializeField] private EnemyNearAttack _enemyNearAttack;
    public EnemyNearAttack EnemyNearAttack { get => _enemyNearAttack; set => _enemyNearAttack = value; }

    protected override void LoadComponents()
    {
        base.LoadComponents();
        if (_enemyNearAttack != null) return;
        _enemyNearAttack = GetComponentInChildren<EnemyNearAttack>();
    }

    private void OnEnable()
    {
        _hp = 5;
    }
}
