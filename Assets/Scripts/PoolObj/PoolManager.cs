using System.Collections.Generic;
using UnityEngine;
using System;

public abstract class PoolManager<T> : Singleton<PoolManager<T>> where T : PoolObj<T>
{
    [SerializeField] private List<T> _listPool = new();
    [SerializeField] private List<PrewarmConfig> _prewarmAmount = new();
    private int _spawnCount = 0;
    private readonly Dictionary<string, Stack<T>> _poolByKey = new();
    private readonly Dictionary<string, HashSet<T>> _activeByKey = new();
    private readonly HashSet<T> _inactiveLookup = new();
    private readonly List<T> _despawnBuffer = new();

    protected override void Awake()
    {
        DontDestroy(false);
        RebuildPoolLookup();
        PrewarmFromInspector();
    }

    public virtual T Spawn(T prefab, Vector3 postion, Quaternion rotation)
    {
        if (prefab == null) return null;

        string poolKey = prefab.PoolKey;
        T newObj = GetObjectFromPool(prefab);
        if (newObj == null)
        {
            newObj = Instantiate(prefab, postion, rotation, transform);
            newObj.SetPoolKey(poolKey);
            UpdateName(prefab, newObj);
        }
        else
        {
            newObj.SetPoolKey(poolKey);
            newObj.transform.SetPositionAndRotation(postion, rotation);
            newObj.gameObject.SetActive(true);
        }

        AddActiveObj(newObj);
        return newObj;
    }

    public virtual void Despawn(T prefab)
    {
        if (prefab == null || _inactiveLookup.Contains(prefab)) return;

        RemoveActiveObj(prefab);
        prefab.gameObject.SetActive(false);
        AddObjToPool(prefab);
    }

    public virtual void DespawnAll(T prefab, string parentName = null)
    {
        if (prefab == null) return;

        string poolKey = prefab.PoolKey;
        if (!_activeByKey.TryGetValue(poolKey, out HashSet<T> activeObjects) || activeObjects.Count == 0) return;

        _despawnBuffer.Clear();
        foreach (T obj in activeObjects)
            _despawnBuffer.Add(obj);

        foreach (T obj in _despawnBuffer)
        {
            if (obj == null || !obj.gameObject.activeSelf) continue;

            RemoveActiveObj(obj);
            obj.gameObject.SetActive(false);
            AddObjToPool(obj);
        }

        _despawnBuffer.Clear();
    }

    private void Prewarm(T prefab, int amount)
    {
        if (prefab == null || amount <= 0) return;

        string poolKey = prefab.PoolKey;
        for (int i = 0; i < amount; i++)
        {
            T newObj = Instantiate(prefab, transform);
            newObj.SetPoolKey(poolKey);
            UpdateName(prefab, newObj);
            newObj.gameObject.SetActive(false);
            AddObjToPool(newObj);
        }
    }

    private T GetObjectFromPool(T prefab)
    {
        if (prefab == null) return null;

        string poolKey = prefab.PoolKey;
        if (!_poolByKey.TryGetValue(poolKey, out Stack<T> poolStack)) return null;

        while (poolStack.Count > 0)
        {
            T inPoolObj = poolStack.Pop();
            if (inPoolObj == null) continue;

            RemoveObjFromPool(inPoolObj);
            return inPoolObj;
        }

        return null;
    }

    private void UpdateName(T prefab, T newObject)
    {
        _spawnCount++;
        newObject.name = _spawnCount + "_" + prefab.PoolKey;
    }

    private void AddObjToPool(T obj)
    {
        if (obj == null || _inactiveLookup.Contains(obj)) return;

        string poolKey = obj.PoolKey;
        if (!_poolByKey.TryGetValue(poolKey, out Stack<T> poolStack))
        {
            poolStack = new Stack<T>();
            _poolByKey.Add(poolKey, poolStack);
        }

        poolStack.Push(obj);
        _inactiveLookup.Add(obj);
    }

    private void RemoveObjFromPool(T obj)
    {
        if (obj == null) return;

        _inactiveLookup.Remove(obj);
    }

    private void AddActiveObj(T obj)
    {
        if (obj == null) return;

        string poolKey = obj.PoolKey;
        if (!_activeByKey.TryGetValue(poolKey, out HashSet<T> activeObjects))
        {
            activeObjects = new HashSet<T>();
            _activeByKey.Add(poolKey, activeObjects);
        }

        activeObjects.Add(obj);
    }

    private void RemoveActiveObj(T obj)
    {
        if (obj == null) return;

        if (_activeByKey.TryGetValue(obj.PoolKey, out HashSet<T> activeObjects))
            activeObjects.Remove(obj);
    }

    private void RebuildPoolLookup()
    {
        _poolByKey.Clear();
        _activeByKey.Clear();
        _inactiveLookup.Clear();

        for (int i = _listPool.Count - 1; i >= 0; i--)
        {
            T obj = _listPool[i];
            if (obj == null)
            {
                _listPool.RemoveAt(i);
                continue;
            }

            obj.gameObject.SetActive(false);
            AddObjToPool(obj);
        }
    }

    private void PrewarmFromInspector()
    {
        foreach (PrewarmConfig config in _prewarmAmount)
        {
            if (config == null || config.Prefab == null || config.Amount <= 0) continue;

            Prewarm(config.Prefab, config.Amount);
        }
    }

    [Serializable] private class PrewarmConfig
    {
        public T Prefab = null;
        [Min(0)] public int Amount = 0;
    }
}
