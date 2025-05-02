using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyNearAttack : EnemyAttack
{
    private void Start()
    {
        _timeAttack = 0.1f;
    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.layer == LayerMask.NameToLayer("Player"))
        {
            Observer.NotifyObserver(ObserverID.PlayerTakeDmg);
        }
    }
}
