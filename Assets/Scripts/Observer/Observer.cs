using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Observer : MonoBehaviour
{
    private static Dictionary<ObserverID, List<Delegate>> _listeners = new();

    public static void AddObserver(ObserverID id, Action action)
    {
        if (action == null) return;
        if (!_listeners.ContainsKey(id))
            _listeners[id] = new List<Delegate>();

        _listeners[id].Add(action);
    }

    public static void RemoveObserver(ObserverID id, Action action)
    {
        if (action == null || !_listeners.ContainsKey(id)) return;
        _listeners[id].Remove(action);
        if (_listeners[id].Count == 0)
            _listeners.Remove(id);
    }

    public static void Notify(ObserverID id)
    {
        if (!_listeners.ContainsKey(id)) return;
        foreach (var listener in _listeners[id])
        {
            if (listener is Action action)
                action?.Invoke();
        }
    }

    //--------------------------------------------------------------------------

    public static void AddObserver<T>(ObserverID id, Action<T> action)
    {
        if (action == null) return;
        if (!_listeners.ContainsKey(id))
            _listeners[id] = new List<Delegate>();

        _listeners[id].Add(action);
    }

    public static void RemoveObserver<T>(ObserverID id, Action<T> action)
    {
        if (action == null || !_listeners.ContainsKey(id)) return;
        _listeners[id].Remove(action);
        if (_listeners[id].Count == 0)
            _listeners.Remove(id);
    }

    public static void Notify<T>(ObserverID id, T parameter)
    {
        if (!_listeners.ContainsKey(id)) return;
        foreach (var listener in _listeners[id])
        {
            if (listener is Action<T> action)
                action?.Invoke(parameter);
        }
    }

    //--------------------------------------------------------------------------

    public static void AddObserver(ObserverID id, Action<object[]> action)
    {
        if (action == null) return;
        if (!_listeners.ContainsKey(id))
            _listeners[id] = new List<Delegate>();

        _listeners[id].Add(action);
    }
    public static void RemoveObserver(ObserverID id, Action<object[]> action)
    {
        if (action == null || !_listeners.ContainsKey(id)) return;
        _listeners[id].Remove(action);
        if (_listeners[id].Count == 0)
            _listeners.Remove(id);
    }

    public static void Notify(ObserverID id, params object[] parameters)
    {
        if (!_listeners.ContainsKey(id)) return;
        foreach (var listener in _listeners[id])
        {
            if (listener is Action<object[]> action)
                action?.Invoke(parameters);
        }
    }
}
