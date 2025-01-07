using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class PoolObj<T> : PISMonoBehaviour where T : PoolObj<T>
{
    public abstract string GetName();
}
