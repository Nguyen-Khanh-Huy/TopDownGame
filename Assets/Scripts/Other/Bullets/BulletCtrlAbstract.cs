using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class BulletCtrlAbstract : PoolObj<BulletCtrlAbstract>
{
    private void Update()
    {
        if (!UIGamePlayManager.Ins.CheckPlayTime) return;
        OnUpdate();
            //PoolManager<BulletCtrlAbstract>.Ins.Despawn(this);
    }
    protected virtual void OnUpdate() { }
}