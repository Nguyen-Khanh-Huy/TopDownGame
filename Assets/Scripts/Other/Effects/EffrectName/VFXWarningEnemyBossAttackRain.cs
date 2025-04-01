using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VFXWarningEnemyBossAttackRain : EffectCtrlAbstract
{
    protected override void OnEnable()
    {
        Invoke(nameof(DespawnEffect), 2.2f);
    }

    public override string GetName()
    {
        return "VFXWarningEnemyBossAttackRain";
    }
}

