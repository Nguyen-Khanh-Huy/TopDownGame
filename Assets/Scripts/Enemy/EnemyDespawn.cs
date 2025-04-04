using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyDespawn : PISMonoBehaviour
{
    [SerializeField] private EnemyCtrlAbstract _enemyAbstract;
    [SerializeField] private float _despawnByTime = 3f;
    [SerializeField] private bool _isSpawnedItemDrop;
    [SerializeField] private bool _isDead;

    private void FixedUpdate()
    {
        if (!UIGamePlayManager.Ins.CheckPlayTime) return;
        DespawnEnemy();
    }

    private void DespawnEnemy()
    {
        if (_enemyAbstract.Hp > 0) return;
        SetActiveEnemyHpBar();
        CountEnemyDead();
        SpawnItemDropMana();
        RemoveEnemyInPlayerTarget();
        StartCoroutine(DelayDespawnEnemy());
    }

    private IEnumerator DelayDespawnEnemy()
    {
        yield return new WaitForSeconds(_despawnByTime);
        _isSpawnedItemDrop = false;
        _isDead = false;
        PoolManager<EnemyCtrlAbstract>.Ins.Despawn(_enemyAbstract);
    }

    private void SetActiveEnemyHpBar()
    {
        if (_enemyAbstract.HpBar.gameObject.activeSelf)
            _enemyAbstract.HpBar.gameObject.SetActive(false);
    }

    private void CountEnemyDead()
    {
        if (!_isDead)
        {
            _isDead = true;
            _enemyAbstract.PlayerCtrl.PlayerShoot.CountEnemyDead++;
            UIGamePlayManager.Ins.TxtCountEnemyDead.text = _enemyAbstract.PlayerCtrl.PlayerShoot.CountEnemyDead.ToString();
        }
    }
    private void SpawnItemDropMana()
    {
        if (!_isSpawnedItemDrop)
        {
            _isSpawnedItemDrop = true;
            Vector3 changeDropPos = _enemyAbstract.transform.position + new Vector3(0, 1f, 0);
            PoolManager<ItemDropCtrlAbstract>.Ins.Spawn(_enemyAbstract.ItemDropMana, changeDropPos, Quaternion.identity);
        }
    }

    private void RemoveEnemyInPlayerTarget()
    {
        _enemyAbstract.PlayerCtrl.PlayerTarget.ListEnemyTarget.Remove(_enemyAbstract);
    }

    protected override void LoadComponents()
    {
        if (_enemyAbstract != null) return;
        _enemyAbstract = GetComponentInParent<EnemyCtrlAbstract>();
    }
}
