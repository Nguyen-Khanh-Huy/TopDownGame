using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class BulletCtrlAbstract : PoolObj<BulletCtrlAbstract>
{
    protected virtual void Update()
    {
        if (!UIGamePlayManager.Ins.CheckPlayTime)
            PoolManager<BulletCtrlAbstract>.Ins.Despawn(this);
    }
}