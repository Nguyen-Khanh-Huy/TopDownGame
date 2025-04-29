using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Observer : MonoBehaviour
{
    private static Dictionary<ObserverID, List<Delegate>> _listObserver = new();

    //--------------------------------------------------------------------------

    public static void AddObserver(ObserverID id, Action action)
    {
        if (action == null) return;
        if (!_listObserver.ContainsKey(id))
            _listObserver[id] = new List<Delegate>();

        _listObserver[id].Add(action);
    }

    public static void RemoveObserver(ObserverID id, Action action)
    {
        if (action == null || !_listObserver.ContainsKey(id)) return;
        _listObserver[id].Remove(action);
        if (_listObserver[id].Count == 0)
            _listObserver.Remove(id);
    }

    public static void NotifyObserver(ObserverID id)
    {
        if (!_listObserver.ContainsKey(id)) return;
        foreach (var listener in _listObserver[id])
        {
            if (listener is Action action)
                action?.Invoke();
        }
    }

    //--------------------------------------------------------------------------

    public static void AddObserver<T>(ObserverID id, Action<T> action)
    {
        if (action == null) return;
        if (!_listObserver.ContainsKey(id))
            _listObserver[id] = new List<Delegate>();

        _listObserver[id].Add(action);
    }

    public static void RemoveObserver<T>(ObserverID id, Action<T> action)
    {
        if (action == null || !_listObserver.ContainsKey(id)) return;
        _listObserver[id].Remove(action);
        if (_listObserver[id].Count == 0)
            _listObserver.Remove(id);
    }

    public static void NotifyObserver<T>(ObserverID id, T parameter)
    {
        if (!_listObserver.ContainsKey(id)) return;
        foreach (var listener in _listObserver[id])
        {
            if (listener is Action<T> action)
                action?.Invoke(parameter);
        }
    }

    //--------------------------------------------------------------------------

    public static void AddObserver(ObserverID id, Action<object[]> action)
    {
        if (action == null) return;
        if (!_listObserver.ContainsKey(id))
            _listObserver[id] = new List<Delegate>();

        _listObserver[id].Add(action);
    }
    public static void RemoveObserver(ObserverID id, Action<object[]> action)
    {
        if (action == null || !_listObserver.ContainsKey(id)) return;
        _listObserver[id].Remove(action);
        if (_listObserver[id].Count == 0)
            _listObserver.Remove(id);
    }

    public static void NotifyObserver(ObserverID id, params object[] parameters)
    {
        if (!_listObserver.ContainsKey(id)) return;
        foreach (var listener in _listObserver[id])
        {
            if (listener is Action<object[]> action)
                action?.Invoke(parameters);
        }
    }

    //--------------------------------------------------------------------------
}
