using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class PISMonoBehaviour : MonoBehaviour
{
    protected virtual void Awake()
    {
        LoadComponents();
    }

    protected virtual void Reset()
    {
        LoadComponents();
    }
    protected abstract void LoadComponents();
}
