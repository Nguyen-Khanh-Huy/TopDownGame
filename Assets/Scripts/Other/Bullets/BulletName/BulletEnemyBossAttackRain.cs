using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletEnemyBossAttackRain : BulletCtrlAbstract
{
    protected override void Update()
    {
        base.Update();
        transform.Translate(5f * Time.deltaTime * Vector3.down);

        if (transform.position.y > 0f) return;
        PoolManager<BulletCtrlAbstract>.Ins.Despawn(this);
    }

    private void OnTriggerEnter(Collider other)
    {
        PlayerController player = other.GetComponent<PlayerController>();
        if (player != null)
        {
            Observer.Notify(ObserverID.PlayerTakeDmg);
        }
    }

    public override string GetName()
    {
        return "BulletEnemyBossAttackRain";
    }

    protected override void LoadComponents()
    {
        // nothing
    }
}
