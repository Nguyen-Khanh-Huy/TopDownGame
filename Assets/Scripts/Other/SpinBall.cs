using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpinBall : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        EnemyCtrlAbstract enemy = other.GetComponent<EnemyCtrlAbstract>();
        if (enemy != null)
        {
            if (enemy != null && enemy.Hp > 0)
            {
                Observer.Notify(ObserverID.EnemyTakeDmg, enemy);
            }
        }
    }
}
