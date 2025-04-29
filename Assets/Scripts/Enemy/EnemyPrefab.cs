using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyPrefab : PISMonoBehaviour
{
    [SerializeField] private List<EnemyCtrlAbstract> _listEnemyAll = new();
    [SerializeField] private List<EnemyCreepCtrl> _listEnemyCreep = new();
    [SerializeField] private List<EnemyNearCtrl> _listEnemyNear = new();
    [SerializeField] private List<EnemyLongCtrl> _listEnemyLong = new();
    [SerializeField] private List<EnemyBossCtrl> _listEnemyBoss = new();

    protected override void LoadComponents()
    {
        if (_listEnemyAll.Count > 0 && _listEnemyNear.Count > 0 && _listEnemyLong.Count > 0) return;
        _listEnemyAll.Clear();
        EnemyCtrlAbstract[] enemyPrefabs = Resources.LoadAll<EnemyCtrlAbstract>("Enemies");
        foreach (EnemyCtrlAbstract enemyPrefab in enemyPrefabs)
        {
            if (enemyPrefab != null)
                _listEnemyAll.Add(enemyPrefab);
        }

        _listEnemyCreep.Clear();
        _listEnemyNear.Clear();
        _listEnemyLong.Clear();
        _listEnemyBoss.Clear();
        foreach (EnemyCtrlAbstract enemyPrefab in _listEnemyAll)
        {
            if (enemyPrefab is EnemyCreepCtrl enemyCreep)
                _listEnemyCreep.Add(enemyCreep);
            else if (enemyPrefab is EnemyNearCtrl enemyNear)
                _listEnemyNear.Add(enemyNear);
            else if (enemyPrefab is EnemyLongCtrl enemyLong)
                _listEnemyLong.Add(enemyLong);
            else if (enemyPrefab is EnemyBossCtrl enemyBoss)
                _listEnemyBoss.Add(enemyBoss);
        }
    }

    public EnemyCreepCtrl GetEnemyCreep()
    {
        int rd = Random.Range(0, _listEnemyCreep.Count);
        return _listEnemyCreep[rd];
    }

    public EnemyNearCtrl GetEnemyNear()
    {
        int rd = Random.Range(0, _listEnemyNear.Count);
        return _listEnemyNear[rd];
    }

    public EnemyLongCtrl GetEnemyLong()
    {
        int rd = Random.Range(0, _listEnemyLong.Count);
        return _listEnemyLong[rd];
    }

    public EnemyBossCtrl GetEnemyBoss()
    {
        return _listEnemyBoss[0];
    }
}
