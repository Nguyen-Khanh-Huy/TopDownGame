using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerTarget : PISMonoBehaviour
{
    [SerializeField] private SphereCollider _colliderTarget;
    [SerializeField] private EnemyCtrlAbstract _target;
    [SerializeField] private List<EnemyCtrlAbstract> _listEnemyTarget;

    public SphereCollider ColliderTarget { get => _colliderTarget; set => _colliderTarget = value; }
    public EnemyCtrlAbstract Target { get => _target; }
    public List<EnemyCtrlAbstract> ListEnemyTarget { get => _listEnemyTarget; set => _listEnemyTarget = value; }

    private void OnTriggerEnter(Collider other)
    {
        if (other.TryGetComponent<EnemyCtrlAbstract>(out var enemy))
            if (enemy.Hp > 0 && !_listEnemyTarget.Contains(enemy))
                _listEnemyTarget.Add(enemy);
    }

    private void OnTriggerStay(Collider other)
    {
        if (_listEnemyTarget.Count <= 0)
        {
            _target = null;
            return;
        }

        if (other.TryGetComponent<EnemyCtrlAbstract>(out var enemy))
        {
            float minDistance = Mathf.Infinity;
            foreach (EnemyCtrlAbstract enemyInList in _listEnemyTarget)
            {
                float distance = (PlayerCtrl.Ins.transform.position - enemyInList.transform.position).sqrMagnitude;
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
        if (other.TryGetComponent<EnemyCtrlAbstract>(out var enemy))
            if (_listEnemyTarget.Contains(enemy))
                _listEnemyTarget.Remove(enemy);

        if (_listEnemyTarget.Count == 0)
            _target = null;
    }

    //private void AddAndRemoveEnemy(EnemyCtrlAbstract enemy)
    //{
    //    Vector3 playerPos = _playerCtrl.transform.position;
    //    Vector3 enemyPos = enemy.transform.position;
    //    playerPos.y += 1;
    //    enemyPos.y += 1;
    //    Vector3 direction = enemyPos - playerPos;
    //    if (Physics.Raycast(playerPos, direction, out RaycastHit hitInfo))
    //    {
    //        if (hitInfo.collider.gameObject.layer != LayerMask.NameToLayer("BG"))
    //        {
    //            if (enemy.Hp > 0 && !_listEnemyTarget.Contains(enemy))
    //                _listEnemyTarget.Add(enemy);
    //        }
    //        else
    //        {
    //            if (_listEnemyTarget.Contains(enemy))
    //                _listEnemyTarget.Remove(enemy);
    //        }
    //    }
    //}

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
        if (_colliderTarget != null) return;
        _colliderTarget = GetComponent<SphereCollider>();
    }
}
