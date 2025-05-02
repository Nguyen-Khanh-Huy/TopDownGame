using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyDespawn : PISMonoBehaviour
{
    [SerializeField] private EnemyCtrlAbstract _enemyCtrl;
    [SerializeField] private float _despawnByTime = 3f;
    [SerializeField] private bool _isDespawn;

    private void Update()
    {
        //if (!UIGamePlayManager.Ins.CheckPlayTime) return;
        DespawnEnemy();
    }

    private void DespawnEnemy()
    {
        if (_enemyCtrl.Hp > 0 || _isDespawn) return;
        _isDespawn = true;
        SetActiveEnemyHpBar();
        CountEnemyDead();
        SpawnItemDropMana();
        RemoveEnemyInListPlayerTarget();
        RemoveEnemyInListEnemyManager();
        Invoke(nameof(DelayDespawnEnemy), _despawnByTime);
    }

    private void DelayDespawnEnemy()
    {
        _isDespawn = false;
        PoolManager<EnemyCtrlAbstract>.Ins.Despawn(_enemyCtrl);
    }

    private void SetActiveEnemyHpBar()
    {
        if (_enemyCtrl.HpBar.gameObject.activeSelf)
            _enemyCtrl.HpBar.gameObject.SetActive(false);
    }

    private void CountEnemyDead()
    {
        PlayerCtrl.Ins.PlayerShoot.CountEnemyDead++;
        UIGamePlayManager.Ins.TxtCountEnemyDead.text = PlayerCtrl.Ins.PlayerShoot.CountEnemyDead.ToString();
    }
    private void SpawnItemDropMana()
    {
        Vector3 changeDropPos = _enemyCtrl.transform.position + Vector3.up;
        PoolManager<ItemDropCtrlAbstract>.Ins.Spawn(_enemyCtrl.ItemDropMana, changeDropPos, Quaternion.identity);
    }

    private void RemoveEnemyInListPlayerTarget()
    {
        if (PlayerCtrl.Ins.PlayerTarget.ListEnemyTarget.Contains(_enemyCtrl))
            PlayerCtrl.Ins.PlayerTarget.ListEnemyTarget.Remove(_enemyCtrl);
    }

    private void RemoveEnemyInListEnemyManager()
    {
        if (_enemyCtrl is EnemyCreepCtrl enemyCreep && EnemyManager.Ins.ListEnemyCreepSpawn.Contains(enemyCreep))
            EnemyManager.Ins.ListEnemyCreepSpawn.Remove(enemyCreep);

        else if (_enemyCtrl is EnemyNearCtrl enemyNear && EnemyManager.Ins.ListEnemyNearSpawn.Contains(enemyNear))
            EnemyManager.Ins.ListEnemyNearSpawn.Remove(enemyNear);
        
        else if (_enemyCtrl is EnemyLongCtrl enemyLong && EnemyManager.Ins.ListEnemyLongSpawn.Contains(enemyLong))
            EnemyManager.Ins.ListEnemyLongSpawn.Remove(enemyLong);
    }

    protected override void LoadComponents()
    {
        if (_enemyCtrl != null) return;
        _enemyCtrl = GetComponentInParent<EnemyCtrlAbstract>();
    }
}
