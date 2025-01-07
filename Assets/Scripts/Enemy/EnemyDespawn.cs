using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyDespawn : PISMonoBehaviour
{
    [SerializeField] private EnemyCtrlAbstract _enemyAbstract;
    [SerializeField] private float _despawnByTime = 3f;
    //[SerializeField] private bool _isSpawnedItem;

    protected override void LoadComponents()
    {
        if (_enemyAbstract != null) return;
        _enemyAbstract = GetComponentInParent<EnemyCtrlAbstract>();
        Debug.Log("Load: " + transform.name);
    }

    private void FixedUpdate()
    {
        DespawnEnemy();
    }

    private void DespawnEnemy()
    {
        if (_enemyAbstract.Hp > 0) return;
        //SpawnItems();
        StartCoroutine(DelayDespawnEnemy());
    }

    private IEnumerator DelayDespawnEnemy()
    {
        yield return new WaitForSeconds(_despawnByTime);
        //_isSpawnedItem = false;
        PoolManager<EnemyCtrlAbstract>.Ins.Despawn(_enemyAbstract);
    }

    //private void SpawnItems()
    //{
    //    if (!_isSpawnedItem)
    //    {
    //        _isSpawnedItem = true;
    //        _enemyAbstract.ItemManager.SpawnItems(_enemyAbstract);
    //    }
    //}
}
