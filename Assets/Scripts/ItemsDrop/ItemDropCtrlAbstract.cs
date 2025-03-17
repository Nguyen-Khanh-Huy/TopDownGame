using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class ItemDropCtrlAbstract : PoolObj<ItemDropCtrlAbstract>
{
    protected void DespawnItem()
    {
        PoolManager<ItemDropCtrlAbstract>.Ins.Despawn(this);
    }
}
