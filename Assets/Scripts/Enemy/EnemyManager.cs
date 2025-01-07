using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyManager : PISMonoBehaviour
{
    [SerializeField] private EnemyPool _enemyPool;
    [SerializeField] private EnemyPrefab _enemyPrefab;
    protected override void LoadComponents()
    {
        if(_enemyPool != null && _enemyPrefab != null) return;
        _enemyPool = GetComponentInChildren<EnemyPool>();
        _enemyPrefab = GetComponentInChildren<EnemyPrefab>();
    }
}
