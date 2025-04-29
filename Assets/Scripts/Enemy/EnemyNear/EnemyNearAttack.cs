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
        PlayerController player = other.GetComponent<PlayerController>();
        if(player != null)
        {
            Observer.NotifyObserver(ObserverID.PlayerTakeDmg);
        }
    }
}
