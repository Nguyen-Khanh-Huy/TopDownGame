using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerTarget : MonoBehaviour
{
    [SerializeField] private EnemyCtrlAbstract _target;
    [SerializeField] private List<EnemyCtrlAbstract> _listEnemyTarget;

    public EnemyCtrlAbstract Target { get => _target; }

    private void FixedUpdate()
    {
        RemoveEnemyDeadInList();
    }

    public void RemoveEnemyDeadInList()
    {
        if (_target == null) return;
        _listEnemyTarget.RemoveAll(enemy => enemy.Hp <= 0);
    }

    private void OnTriggerEnter(Collider other)
    {
        EnemyCtrlAbstract enemy = other.GetComponent<EnemyCtrlAbstract>();
        if (enemy != null && !_listEnemyTarget.Contains(enemy))
        {
            _listEnemyTarget.Add(enemy);
        }
    }

    private void OnTriggerStay(Collider other)
    {
        EnemyCtrlAbstract enemy = other.GetComponent<EnemyCtrlAbstract>();
        if (enemy != null)
        {
            float minDistance = Mathf.Infinity;
            foreach (EnemyCtrlAbstract enemyInList in _listEnemyTarget)
            {
                float distance = Vector3.Distance(transform.position, enemyInList.transform.position);
                if (minDistance > distance)
                {
                    minDistance = distance;
                    _target = enemyInList;
                }
            }
        }
    }

    private void OnTriggerExit(Collider other)
    {
        EnemyCtrlAbstract enemy = other.GetComponent<EnemyCtrlAbstract>();
        if (enemy != null)
        {
            _listEnemyTarget.Remove(enemy);
        }

        if (_listEnemyTarget.Count == 0)
        {
            _target = null;
        }
    }
}
