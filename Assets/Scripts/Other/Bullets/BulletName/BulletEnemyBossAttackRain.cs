using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static UnityEditor.Experimental.GraphView.GraphView;

public class BulletEnemyBossAttackRain : BulletCtrlAbstract
{
    [SerializeField] private HitEnemyBossAttackRain _hitEnemyBossAttackRain;
    [SerializeField] private bool _isSpawnHit;
    [SerializeField] private float _speedBullet = 5f;

    protected override void Update()
    {
        base.Update();
        transform.Translate(_speedBullet * Time.deltaTime * Vector3.down);

        if (transform.position.y <= 0.3f && !_isSpawnHit)
        {
            _isSpawnHit = true;
            SpawnHitParabol();
        }

        if (transform.position.y >= 0f) return;
        PoolManager<BulletCtrlAbstract>.Ins.Despawn(this);
    }

    private void OnEnable()
    {
        _isSpawnHit = false;
    }

    private void OnTriggerEnter(Collider other)
    {
        PlayerController player = other.GetComponent<PlayerController>();
        if (player != null)
        {
            Observer.Notify(ObserverID.PlayerTakeDmg);
        }
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
