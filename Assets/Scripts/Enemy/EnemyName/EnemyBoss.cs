using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyBoss : EnemyBossCtrlAbstract
{
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
        return "Enemy Boss";
    }
}
