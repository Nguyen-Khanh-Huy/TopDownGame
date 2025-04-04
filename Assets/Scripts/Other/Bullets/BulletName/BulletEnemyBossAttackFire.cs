using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletEnemyBossAttackFire : BulletCtrlAbstract
{
    [SerializeField] private float _speedBullet = 6f;
    [SerializeField] private float _despawnByTime = 2f;

    private void OnEnable()
    {
        Invoke(nameof(DespawnBullet), _despawnByTime);
    }

    private void OnDisable()
    {
        CancelInvoke(nameof(DespawnBullet));
    }

    protected override void Update()
    {
        base.Update();
        transform.Translate(_speedBullet * Time.deltaTime * Vector3.forward);
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
