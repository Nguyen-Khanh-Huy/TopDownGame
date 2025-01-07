using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class PoolManager<T> : Singleton<PoolManager<T>> where T : PoolObj<T>
{
    protected int _spawnCount = 0;
    [SerializeField] public List<T> _listPool = new();

    protected override void Awake()
    {
        DontDestroy(false);
    }

    public virtual T Spawn(T prefab, Vector3 postion, Quaternion rotation)
    {
        T newObj = GetObjectFromPool(prefab);
        if (newObj == null)
        {
            newObj = Instantiate(prefab, postion, rotation);
            UpdateName(prefab, newObj);
            newObj.transform.SetParent(transform);
        }
        else
        {
            newObj.transform.SetPositionAndRotation(postion, rotation);
            newObj.gameObject.SetActive(true);
        }
        return newObj;
    }

    public virtual T GetObjectFromPool(T prefab)
    {
        foreach (T inPoolObj in _listPool)
        {
            if (prefab.GetName() == inPoolObj.GetName())
            {
                RemoveObjFromPool(inPoolObj);
                return inPoolObj;
            }
        }
        return null;
    }

    public virtual void Despawn(T prefab)
    {
        if (_listPool.Contains(prefab) && prefab == null) return;
        prefab.gameObject.SetActive(false);
        AddObjToPool(prefab);
    }

    public virtual void UpdateName(T prefab, T newObject)
    {
        _spawnCount++;
        newObject.name = _spawnCount + "_" + prefab.GetName();
    }

    protected virtual void AddObjToPool(T obj)
    {
        _listPool.Add(obj);
    }

    protected virtual void RemoveObjFromPool(T obj)
    {
        _listPool.Remove(obj);
    }
}
