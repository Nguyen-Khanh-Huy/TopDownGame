using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Unity.VisualScripting;
using UnityEngine;

public class EnemyDespawn : PISMonoBehaviour
{
    [SerializeField] private EnemyCtrlAbstract _enemyAbstract;
    [SerializeField] private float _despawnByTime = 3f;
    [SerializeField] private bool _isDespawn;

    private void FixedUpdate()
    {
        if (!UIGamePlayManager.Ins.CheckPlayTime) return;
        DespawnEnemy();
    }

    private void DespawnEnemy()
    {
        if (_enemyAbstract.Hp > 0 || _isDespawn) return;
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
        PoolManager<EnemyCtrlAbstract>.Ins.Despawn(_enemyAbstract);
    }

    private void SetActiveEnemyHpBar()
    {
        if (_enemyAbstract.HpBar.gameObject.activeSelf)
            _enemyAbstract.HpBar.gameObject.SetActive(false);
    }

    private void CountEnemyDead()
    {
        _enemyAbstract.PlayerCtrl.PlayerShoot.CountEnemyDead++;
        UIGamePlayManager.Ins.TxtCountEnemyDead.text = _enemyAbstract.PlayerCtrl.PlayerShoot.CountEnemyDead.ToString();
    }
    private void SpawnItemDropMana()
    {
        Vector3 changeDropPos = _enemyAbstract.transform.position + new Vector3(0, 1f, 0);
        PoolManager<ItemDropCtrlAbstract>.Ins.Spawn(_enemyAbstract.ItemDropMana, changeDropPos, Quaternion.identity);
    }

    private void RemoveEnemyInListPlayerTarget()
    {
        if (_enemyAbstract.PlayerCtrl.PlayerTarget.ListEnemyTarget.Contains(_enemyAbstract))
            _enemyAbstract.PlayerCtrl.PlayerTarget.ListEnemyTarget.Remove(_enemyAbstract);
    }

    private void RemoveEnemyInListEnemyManager()
    {
        if (_enemyAbstract is EnemyLongCtrlAbstract longEnemy && EnemyManager.Ins.ListEnemyLongSpawn.Contains(longEnemy))
            EnemyManager.Ins.ListEnemyLongSpawn.Remove(longEnemy);

        else if (_enemyAbstract is EnemyNearCtrlAbstract nearEnemy && EnemyManager.Ins.ListEnemyNearSpawn.Contains(nearEnemy))
            EnemyManager.Ins.ListEnemyNearSpawn.Remove(nearEnemy);
    }

    protected override void LoadComponents()
    {
        if (_enemyAbstract != null) return;
        _enemyAbstract = GetComponentInParent<EnemyCtrlAbstract>();
    }
}
