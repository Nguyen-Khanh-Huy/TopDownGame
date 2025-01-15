using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyDespawn : PISMonoBehaviour
{
    [SerializeField] private EnemyCtrlAbstract _enemyAbstract;
    [SerializeField] private float _despawnByTime = 3f;
    [SerializeField] private bool _isSpawnedItemDrop;

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
        SpawnItemDropCoin();
        StartCoroutine(DelayDespawnEnemy());
    }

    private IEnumerator DelayDespawnEnemy()
    {
        yield return new WaitForSeconds(_despawnByTime);
        _isSpawnedItemDrop = false;
        PoolManager<EnemyCtrlAbstract>.Ins.Despawn(_enemyAbstract);
    }

    private void SpawnItemDropCoin()
    {
        if (!_isSpawnedItemDrop)
        {
            _isSpawnedItemDrop = true;
            Vector3 randomPosDrop = _enemyAbstract.transform.position + new Vector3(Random.Range(-0.5f, 0.5f), 1f, Random.Range(-0.5f, 0.5f));
            PoolManager<ItemDropCtrlAbstract>.Ins.Spawn(_enemyAbstract.ItemDropCoin, randomPosDrop, Quaternion.identity);
        }
    }
}
