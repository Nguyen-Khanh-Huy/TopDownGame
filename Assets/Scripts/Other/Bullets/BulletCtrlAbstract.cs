using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class BulletCtrlAbstract : PoolObj<BulletCtrlAbstract>
{
    [SerializeField] private EnemyCtrlAbstract _attacker;
    public EnemyCtrlAbstract Attacker { get => _attacker; set => _attacker = value; }

    private void Update()
    {
        if (!UIGamePlayManager.Ins.CheckPlayTime || (_attacker != null && _attacker.EnemyMoving.IsFreeze)) return;
        OnUpdate();
    }
    protected abstract void OnUpdate();
}