using UnityEngine;

public abstract class PoolObj<T> : PISMonoBehaviour where T : PoolObj<T>
{
    [SerializeField] private string _poolKey;

    public string PoolKey
    {
        get
        {
            _poolKey = string.IsNullOrEmpty(_poolKey) ? GetName() : _poolKey;
            return _poolKey;
        }
    }

    public abstract string GetName();

    internal void SetPoolKey(string poolKey)
    {
        _poolKey = poolKey;
    }
}
