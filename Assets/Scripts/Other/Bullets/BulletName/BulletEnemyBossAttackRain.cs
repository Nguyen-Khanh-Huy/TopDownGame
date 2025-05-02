using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletEnemyBossAttackRain : BulletCtrlAbstract
{
    [SerializeField] private HitEnemyBossAttackRain _hitEnemyBossAttackRain;
    [SerializeField] private bool _isSpawnHit;
    [SerializeField] private float _speedBullet = 5f;

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.layer == LayerMask.NameToLayer("Player"))
            Observer.NotifyObserver(ObserverID.PlayerTakeDmg);
    }

    protected override void OnUpdate()
    {
        transform.Translate(_speedBullet * Time.deltaTime * Vector3.down);
        if (transform.position.y <= 0.3f && !_isSpawnHit)
        {
            _isSpawnHit = true;
            SpawnHitParabol();
        }

        if (transform.position.y >= 0f) return;
        _isSpawnHit = false;
        PoolManager<BulletCtrlAbstract>.Ins.Despawn(this);
    }

    private void SpawnHitParabol()
    {
        PoolManager<EffectCtrlAbstract>.Ins.Spawn(_hitEnemyBossAttackRain, transform.position, Quaternion.identity);
    }

    public override string GetName()
    {
        return "BulletEnemyBossAttackRain";
    }

    protected override void LoadComponents()
    {
        if (_hitEnemyBossAttackRain != null) return;
        _hitEnemyBossAttackRain = Resources.Load<HitEnemyBossAttackRain>("Hits/HitEnemyBossAttackRain");
    }
}
