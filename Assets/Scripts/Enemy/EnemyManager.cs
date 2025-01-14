using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyManager : Singleton<EnemyManager>
{
    [SerializeField] private EnemyPool _enemyPool;
    [SerializeField] private EnemyPrefab _enemyPrefab;
    [SerializeField] private int _maxEnemyNear = 15;
    [SerializeField] private int _maxEnemyLong = 5;
    [SerializeField] private float _spawnSpeed = 2f;
    [SerializeField] private List<EnemyNearCtrlAbstract> _listEnemyNearSpawn = new();
    [SerializeField] private List<EnemyLongCtrlAbstract> _listEnemyLongSpawn = new();
    [SerializeField] private List<Vector3> _listPosSpawn = new();
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
            EnemyCtrlAbstract newEnemyNear = PoolManager<EnemyCtrlAbstract>.Ins.Spawn(_enemyPrefab.GetEnemyNear(), GetVector3ForSpawn(), Quaternion.identity);
            _listEnemyNearSpawn.Add((EnemyNearCtrlAbstract)newEnemyNear);
        }

        if (_listEnemyLongSpawn.Count < _maxEnemyLong)
        {
            EnemyCtrlAbstract newEnemyLong = PoolManager<EnemyCtrlAbstract>.Ins.Spawn(_enemyPrefab.GetEnemyLong(), GetVector3ForSpawn(), Quaternion.identity);
            _listEnemyLongSpawn.Add((EnemyLongCtrlAbstract)newEnemyLong);
        }
    }

    private Vector3 GetVector3ForSpawn()
    {
        int rd = Random.Range(0, _listPosSpawn.Count);
        return _listPosSpawn[rd];
    }

    protected override void LoadComponents()
    {
        if (_enemyPool != null && _enemyPrefab != null) return;
        _enemyPool = GetComponentInChildren<EnemyPool>();
        _enemyPrefab = GetComponentInChildren<EnemyPrefab>();
        _listPosSpawn = LoadListPosSpawn();
    }

    private List<Vector3> LoadListPosSpawn()
    {
        Vector3[] pos = new Vector3[]
        {
            new(6.5f, 1f, -16.5f),
            new(-7.5f, 1f, -16.5f),
            new(-17f, 1f, -6.5f),
            new(-23.5f, 1f, 6.5f),
            new(-21.5f, 1f, 20f),
            new(-7.5f, 1f, 21.5f),
            new(7.5f, 1f, 19.5f),
            new(16f, 1f, 15.5f),
            new(16.5f, 1f, 6.5f),
            new(16.5f, 1f, -8f)
        };

        return new List<Vector3>(pos);
    }
}
