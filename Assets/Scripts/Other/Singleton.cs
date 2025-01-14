using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Singleton<T> : PISMonoBehaviour where T : PISMonoBehaviour
{
    private static T _ins;

    public static T Ins { get { return _ins; } }

    protected override void Awake()
    {
        DontDestroy(true);
    }

    public void DontDestroy(bool dontDestroyOnLoad)
    {
        if (_ins == null)
        {
            _ins = this as T;
            if (dontDestroyOnLoad)
            {
                DontDestroyOnLoad(this.gameObject);
            }
        }
        else
        {
            Destroy(gameObject);
        }
    }
}