using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class EnemyNearCtrlAbstract : EnemyCtrlAbstract
{
    [SerializeField] private EnemyNearMoving _enemyNearMoving;
    public EnemyNearMoving EnemyNearMoving { get => _enemyNearMoving; set => _enemyNearMoving = value; }

    protected override void LoadComponents()
    {
        base.LoadComponents();
        if (_enemyNearMoving != null) return;
        _enemyNearMoving = GetComponentInChildren<EnemyNearMoving>();
    }

    private void OnEnable()
    {
        _hp = 5;
    }
}
