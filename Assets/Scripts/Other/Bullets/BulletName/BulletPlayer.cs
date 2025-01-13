using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletPlayer : BulletCtrlAbstract
{
    [SerializeField] private float _speedBullet = 10f;
    [SerializeField] private float _despawnByTime = 2f;
    private bool isCollided;

    private void Update()
    {
        BulletMoving();
        BulletSphereCast();
    }

    private void OnEnable()
    {
        isCollided = false;
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

    private void BulletSphereCast()
    {
        if (Physics.SphereCast(transform.position, 0.1f, Vector3.forward, out RaycastHit hitInfo, _speedBullet * Time.deltaTime))
        {
            EnemyCtrlAbstract enemy = hitInfo.collider.GetComponent<EnemyCtrlAbstract>();
            if (enemy != null && enemy.Hp > 0 && !isCollided)
            {
                isCollided = true;
                DespawnBullet();
                UpdateHpEnemy(enemy);
            }
        }
    }

    private void DespawnBullet()
    {
        PoolManager<BulletCtrlAbstract>.Ins.Despawn(this);
    }

    private void OnTriggerEnter(Collider other)
    {
        EnemyCtrlAbstract enemy = other.GetComponent<EnemyCtrlAbstract>();
        if (enemy != null)
        {
            UpdateHpEnemy(enemy);
        }
    }

    private void UpdateHpEnemy(EnemyCtrlAbstract enemy)
    {
        if (enemy.Hp <= 0) return;
        enemy.Hp--;
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
