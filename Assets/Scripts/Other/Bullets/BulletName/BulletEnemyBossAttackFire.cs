using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletEnemyBossAttackFire : BulletCtrlAbstract
{
    [SerializeField] private float _speedBullet = 6f;

    [SerializeField] private float _maxDistance = 10f;
    [SerializeField] private Vector3 _startPos;

    private void OnEnable()
    {
        _startPos = transform.position;
    }

    private void OnTriggerEnter(Collider other)
    {
        PlayerController player = other.GetComponent<PlayerController>();
        if (player != null)
        {
            Observer.Notify(ObserverID.PlayerTakeDmg);
            DespawnBullet();
        }
    }

    protected override void OnUpdate()
    {
        transform.Translate(_speedBullet * Time.deltaTime * Vector3.forward);
        float sqrDistance = (transform.position - _startPos).sqrMagnitude;
        if (sqrDistance >= _maxDistance * _maxDistance)
        {
            DespawnBullet();
        }
    }

    private void DespawnBullet()
    {
        PoolManager<BulletCtrlAbstract>.Ins.Despawn(this);
    }

    public override string GetName()
    {
        return "BulletEnemyBossAttackFire";
    }

    protected override void LoadComponents()
    {
        //Nothing
    }
}
