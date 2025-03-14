using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletPlayer : BulletCtrlAbstract
{
    [SerializeField] private float _speedBullet = 10f;
    [SerializeField] private float _despawnByTime = 2f;

    private void Update()
    {
        BulletMoving();
        BulletRayCast();
    }

    private void OnEnable()
    {
        Invoke(nameof(DespawnBullet), _despawnByTime);
    }

    private void OnDisable()
    {
        CancelInvoke(nameof(DespawnBullet));
    }

    private void BulletMoving()
    {
        transform.Translate(_speedBullet * Time.deltaTime * Vector3.forward);
    }

    private void BulletRayCast()
    {
        if (Physics.Raycast(transform.position - transform.forward * 0.7f, transform.forward, out RaycastHit hitInfo, 0.7f + _speedBullet * Time.deltaTime))
        {
            EnemyCtrlAbstract enemy = hitInfo.collider.GetComponent<EnemyCtrlAbstract>();
            if (enemy != null && enemy.Hp > 0)
            {
                UpdateHpEnemy(enemy);
                DespawnBullet();
            }
        }
    }

    //private void OnDrawGizmos()
    //{
    //    Gizmos.color = Color.green;
    //    Gizmos.DrawLine(transform.position, transform.position + transform.forward);
    //}

    private void DespawnBullet()
    {
        PoolManager<BulletCtrlAbstract>.Ins.Despawn(this);
    }

    private void UpdateHpEnemy(EnemyCtrlAbstract enemy)
    {
        if (enemy.Hp <= 0) return;
        enemy.Hp--;
        enemy.HpBar.value = (float)enemy.Hp / enemy.EnemySO.Hp;
    }

    public override string GetName()
    {
        return "BulletPlayer";
    }

    protected override void LoadComponents()
    {
        // Nothing
    }
}
