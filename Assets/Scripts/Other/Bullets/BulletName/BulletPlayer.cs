using System.Collections;
using System.Collections.Generic;
using UnityEditor.PackageManager;
using UnityEngine;

public class BulletPlayer : BulletCtrlAbstract
{
    [SerializeField] private float _speedBullet = 10f;
    [SerializeField] private float _despawnByTime = 2f;
    //[SerializeField] private bool isCollided;

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
        //isCollided = false;
        CancelInvoke(nameof(DespawnBullet));
    }

    private void BulletMoving()
    {
        transform.Translate(_speedBullet * Time.deltaTime * Vector3.forward);
    }

    private void BulletRayCast()
    {
        //if (Physics.SphereCast(transform.position, 0.2f, Vector3.forward, out RaycastHit hitInfo, _speedBullet * Time.deltaTime))
        if (Physics.Raycast(transform.position, transform.forward, out RaycastHit hitInfo, 0.1f))
        {
            EnemyCtrlAbstract enemy = hitInfo.collider.GetComponent<EnemyCtrlAbstract>();
            if (enemy != null && enemy.Hp > 0)// && !isCollided)
            {
                //isCollided = true;
                UpdateHpEnemy(enemy);
                DespawnBullet();
            }
        }
    }

    //private void OnDrawGizmos()
    //{
    //    Gizmos.color = Color.green;
    //    Gizmos.DrawLine(transform.position, transform.position + transform.forward * 0.1f);
    //}

    private void DespawnBullet()
    {
        PoolManager<BulletCtrlAbstract>.Ins.Despawn(this);
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
