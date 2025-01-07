using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class EnemyLongCtrlAbstract : EnemyCtrlAbstract
{
    [SerializeField] private EnemyLongMoving _enemyLongMoving;
    public EnemyLongMoving EnemyLongMoving { get => _enemyLongMoving; set => _enemyLongMoving = value; }

    protected override void LoadComponents()
    {
        base.LoadComponents();
        if (_enemyLongMoving != null) return;
        _enemyLongMoving = GetComponentInChildren<EnemyLongMoving>();
    }

    private void OnEnable()
    {
        _hp = 3;
    }
}
