using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyManager : Singleton<EnemyManager>
{
    [SerializeField] private EnemyPool _enemyPool;
    [SerializeField] private EnemyPrefab _enemyPrefab;
    [SerializeField] private int _maxEnemyNear = 10;
    [SerializeField] private int _maxEnemyLong = 3;
    [SerializeField] private float _spawnSpeed = 2f;
    [SerializeField] private List<EnemyNearCtrlAbstract> _listEnemyNearSpawn = new();
    [SerializeField] private List<EnemyLongCtrlAbstract> _listEnemyLongSpawn = new();
    public List<EnemyNearCtrlAbstract> ListEnemyNearSpawn { get => _listEnemyNearSpawn; set => _listEnemyNearSpawn = value; }
    public List<EnemyLongCtrlAbstract> ListEnemyLongSpawn { get => _listEnemyLongSpawn; set => _listEnemyLongSpawn = value; }

    protected override void Awake()
    {
        DontDestroy(false);
    }

    private void Start()
    {
        Invoke(nameof(SpawnEnemy), _spawnSpeed);
    }

    private void SpawnEnemy()
    {
        Invoke(nameof(SpawnEnemy), _spawnSpeed);
        if (_listEnemyNearSpawn.Count < _maxEnemyNear)
        {
            EnemyCtrlAbstract newEnemyNear = PoolManager<EnemyCtrlAbstract>.Ins.Spawn(_enemyPrefab.GetEnemyNear(), new Vector3(-15, 1, 15), Quaternion.identity);
            _listEnemyNearSpawn.Add((EnemyNearCtrlAbstract)newEnemyNear);
        }

        if (_listEnemyLongSpawn.Count < _maxEnemyLong)
        {
            EnemyCtrlAbstract newEnemyLong = PoolManager<EnemyCtrlAbstract>.Ins.Spawn(_enemyPrefab.GetEnemyLong(), new Vector3(0, 1, 15), Quaternion.identity);
            _listEnemyLongSpawn.Add((EnemyLongCtrlAbstract)newEnemyLong);
        }
    }

    protected override void LoadComponents()
    {
        if (_enemyPool != null && _enemyPrefab != null) return;
        _enemyPool = GetComponentInChildren<EnemyPool>();
        _enemyPrefab = GetComponentInChildren<EnemyPrefab>();
    }
}
