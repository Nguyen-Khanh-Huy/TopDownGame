using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyPrefab : PISMonoBehaviour
{
    [SerializeField] private List<EnemyCtrlAbstract> _listEnemyPrefabs = new();
    [SerializeField] private List<EnemyNearCtrlAbstract> _listEnemyNear = new();
    [SerializeField] private List<EnemyLongCtrlAbstract> _listEnemyLong = new();

    protected override void LoadComponents()
    {
        if (_listEnemyPrefabs.Count > 0 && _listEnemyNear.Count > 0 && _listEnemyLong.Count > 0) return;
        _listEnemyPrefabs.Clear();
        EnemyCtrlAbstract[] enemyPrefabs = Resources.LoadAll<EnemyCtrlAbstract>("Enemies");
        foreach (EnemyCtrlAbstract enemyPrefab in enemyPrefabs)
        {
            if (enemyPrefab != null)
            {
                _listEnemyPrefabs.Add(enemyPrefab);
            }
        }

        _listEnemyNear.Clear();
        _listEnemyLong.Clear();
        foreach (EnemyCtrlAbstract enemyPrefab in _listEnemyPrefabs)
        {
            if (enemyPrefab is EnemyNearCtrlAbstract enemyNear)
            {
                _listEnemyNear.Add(enemyNear);
            }
            else if (enemyPrefab is EnemyLongCtrlAbstract enemyLong)
            {
                _listEnemyLong.Add(enemyLong);
            }
        }
    }

    public EnemyNearCtrlAbstract GetEnemyNear()
    {
        int rd = Random.Range(0, _listEnemyNear.Count);
        return _listEnemyNear[rd];
    }

    public EnemyLongCtrlAbstract GetEnemyLong()
    {
        int rd = Random.Range(0, _listEnemyLong.Count);
        return _listEnemyLong[rd];
    }
}
