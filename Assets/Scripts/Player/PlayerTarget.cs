using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerTarget : PISMonoBehaviour
{
    [SerializeField] private PlayerController _playerCtrl;
    [SerializeField] private EnemyCtrlAbstract _target;
    [SerializeField] private List<EnemyCtrlAbstract> _listEnemyTarget;

    public EnemyCtrlAbstract Target { get => _target; }

    private void Update()
    {
        RemoveEnemyDeadInList();
    }

    private void RemoveEnemyDeadInList()
    {
        if (_target == null) return;
        _listEnemyTarget.RemoveAll(enemy => enemy.Hp <= 0);
    }

    private void OnTriggerStay(Collider other)
    {
        EnemyCtrlAbstract enemy = other.GetComponent<EnemyCtrlAbstract>();
        if (enemy != null)
        {
            AddAndRemoveEnemy(enemy);
            if (_listEnemyTarget.Count > 0)
            {
                float minDistance = Mathf.Infinity;
                foreach (EnemyCtrlAbstract enemyInList in _listEnemyTarget)
                {
                    float distance = Vector3.Distance(_playerCtrl.transform.position, enemyInList.transform.position);
                    if (minDistance > distance)
                    {
                        minDistance = distance;
                        _target = enemyInList;
                    }
                }
            }
            else
            {
                _target = null;
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
    private void AddAndRemoveEnemy(EnemyCtrlAbstract enemy)
    {
        Vector3 playerPosition = _playerCtrl.transform.position;
        Vector3 enemyPosition = enemy.transform.position;
        playerPosition.y += 1;
        enemyPosition.y += 1;
        Vector3 direction = enemyPosition - playerPosition;
        if (Physics.Raycast(playerPosition, direction, out RaycastHit hitInfo))
        {
            if (hitInfo.collider.gameObject.layer != LayerMask.NameToLayer("BG"))
            {
                if (enemy.Hp > 0 && !_listEnemyTarget.Contains(enemy))
                {
                    _listEnemyTarget.Add(enemy);
                }
            }
            else
            {
                if (_listEnemyTarget.Contains(enemy))
                {
                    _listEnemyTarget.Remove(enemy);
                }
            }
        }
    }

    //private void OnDrawGizmos()
    //{
    //    if (_target != null)
    //    {
    //        Vector3 playerPosition = _playerCtrl.transform.position;
    //        Vector3 targetPosition = _target.transform.position;
    //        playerPosition.y += 1;
    //        targetPosition.y += 1;
    //        Gizmos.color = Color.green;
    //        Gizmos.DrawLine(playerPosition, targetPosition);
    //    }
    //}

    protected override void LoadComponents()
    {
        if (_playerCtrl != null) return;
        _playerCtrl = GetComponentInParent<PlayerController>();
    }
}
