using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyManager : Singleton<EnemyManager>
{
    [SerializeField] private EnemyPool _enemyPool;
    [SerializeField] private EnemyPrefab _enemyPrefab;
    [SerializeField] private int _maxEnemyCreep = 20;
    [SerializeField] private int _maxEnemyNear = 5;
    [SerializeField] private int _maxEnemyLong = 2;
    [SerializeField] private float _spawnSpeed = 1f;
    [SerializeField] private List<EnemyCreepCtrl> _listEnemyCreepSpawn = new();
    [SerializeField] private List<EnemyNearCtrl> _listEnemyNearSpawn = new();
    [SerializeField] private List<EnemyLongCtrl> _listEnemyLongSpawn = new();
    [SerializeField] private List<Vector3> _listPosSpawn = new();

    private int _lastUpdateTime = 0;

    public List<EnemyCreepCtrl> ListEnemyCreepSpawn { get => _listEnemyCreepSpawn; set => _listEnemyCreepSpawn = value; }
    public List<EnemyNearCtrl> ListEnemyNearSpawn { get => _listEnemyNearSpawn; set => _listEnemyNearSpawn = value; }
    public List<EnemyLongCtrl> ListEnemyLongSpawn { get => _listEnemyLongSpawn; set => _listEnemyLongSpawn = value; }

    protected override void Awake()
    {
        DontDestroy(false);
    }

    private void Start()
    {
        SpawnEnemy();
    }

    private void SpawnEnemy()
    {
        Invoke(nameof(SpawnEnemy), _spawnSpeed);
        if (!UIGamePlayManager.Ins.CheckPlayTime) return;
        QuantityOverTime();
        if (_listEnemyCreepSpawn.Count < _maxEnemyCreep)
        {
            EnemyCtrlAbstract newEnemyCreep = PoolManager<EnemyCtrlAbstract>.Ins.Spawn(_enemyPrefab.GetEnemyCreep(), GetRandomPos(), Quaternion.identity);
            _listEnemyCreepSpawn.Add((EnemyCreepCtrl)newEnemyCreep);
        }

        if (_listEnemyNearSpawn.Count < _maxEnemyNear)
        {
            EnemyCtrlAbstract newEnemyNear = PoolManager<EnemyCtrlAbstract>.Ins.Spawn(_enemyPrefab.GetEnemyNear(), GetRandomPos(), Quaternion.identity);
            _listEnemyNearSpawn.Add((EnemyNearCtrl)newEnemyNear);
        }

        if (_listEnemyLongSpawn.Count < _maxEnemyLong)
        {
            EnemyCtrlAbstract newEnemyLong = PoolManager<EnemyCtrlAbstract>.Ins.Spawn(_enemyPrefab.GetEnemyLong(), GetRandomPos(), Quaternion.identity);
            _listEnemyLongSpawn.Add((EnemyLongCtrl)newEnemyLong);
        }

        if (UIGamePlayManager.Ins.GamePlayTime >= 300f)
        {
            CancelInvoke(nameof(SpawnEnemy));
            EnemyCtrlAbstract newEnemyBoss = PoolManager<EnemyCtrlAbstract>.Ins.Spawn(_enemyPrefab.GetEnemyBoss(), GetRandomPos(), Quaternion.identity);
        }
    }

    private void QuantityOverTime()
    {
        int[] timeThresholds = { 60, 120, 180, 240 };
        foreach (int t in timeThresholds)
        {
            if (UIGamePlayManager.Ins.GamePlayTime >= t && _lastUpdateTime < t)
            {
                _maxEnemyNear++;
                _maxEnemyLong++;
                _lastUpdateTime = t;
                break;
            }
        }
    }

    private Vector3 GetRandomPos()
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
