using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class EffectCtrlAbstract : PoolObj<EffectCtrlAbstract>
{
    [SerializeField] protected EnemyCtrlAbstract _attacker;
    public EnemyCtrlAbstract Attacker { get => _attacker; set => _attacker = value; }

    protected virtual void OnEnable()
    {
        Invoke(nameof(DespawnEffect), 0.5f);
    }

    protected virtual void OnDisable()
    {
        CancelInvoke(nameof(DespawnEffect));
    }

    protected void DespawnEffect()
    {
        PoolManager<EffectCtrlAbstract>.Ins.Despawn(this);
    }

    protected override void LoadComponents()
    {
        // Nothing
    }
}
