using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyBoss : EnemyBossCtrlAbstract
{
    private void OnTriggerEnter(Collider other)
    {
        if (other.TryGetComponent<PlayerController>(out var player))
        {
            Observer.Notify(ObserverID.PlayerTakeDmg);
        }
    }

    public override string GetName()
    {
        return "Enemy Boss";
    }
}
