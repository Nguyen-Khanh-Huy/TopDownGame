using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletPlayer : BulletCtrlAbstract
{
    [SerializeField] private float _speedBullet = 10f;

    [SerializeField] private float _maxDistance = 10f;
    [SerializeField] private Vector3 _startPos;

    private void OnEnable()
    {
        _startPos = transform.position;
    }

    protected override void OnUpdate()
    {
        BulletMoving();
        BulletRayCast();
    }

    private void BulletMoving()
    {
        transform.Translate(_speedBullet * Time.deltaTime * Vector3.forward);
        float sqrDistance = (transform.position - _startPos).sqrMagnitude;
        if (sqrDistance >= _maxDistance * _maxDistance)
            DespawnBullet();
    }

    private void BulletRayCast()
    {
        if (Physics.Raycast(transform.position - transform.forward * 0.7f, transform.forward, out RaycastHit hitInfo, 0.7f + _speedBullet * Time.deltaTime))
        {
            if (hitInfo.collider.TryGetComponent<EnemyCtrlAbstract>(out var enemy) && enemy.Hp > 0)
            {
                Observer.NotifyObserver(ObserverID.EnemyTakeDmg, enemy);
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

    public override string GetName()
    {
        return "BulletPlayer";
    }

    protected override void LoadComponents()
    {
        // Nothing
    }
}
