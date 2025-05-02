using System.Collections;
using System.Collections.Generic;
using Unity.Burst.CompilerServices;
using UnityEditor.PackageManager;
using UnityEngine;

public class BulletRocket : BulletCtrlAbstract
{
    [SerializeField] private HitRocket _hitRocket;
    [SerializeField] private SphereCollider _collider;

    [SerializeField] private float _height = 1.5f;
    [SerializeField] private float _timeToTarget = 0.6f;

    private Vector3 _startPos;
    private Vector3 _targetPos;
    private float _timer;

    private void OnEnable()
    {
        _startPos = transform.position;
        _targetPos = GetTargetPos();
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.TryGetComponent<EnemyCtrlAbstract>(out var enemy))
            Observer.NotifyObserver(ObserverID.EnemyTakeDmgSingle, enemy);
    }

    private Vector3 GetTargetPos()
    {
        List<EnemyCtrlAbstract> enemies = PlayerCtrl.Ins.PlayerTarget.ListEnemyTarget;
        Vector3 target = Vector3.zero;
        float sqrRange = 1.6f * 1.6f;
        int maxCount = 0;
        foreach (EnemyCtrlAbstract centerEnemy in enemies)
        {
            Vector3 enemyCenter = centerEnemy.transform.position;
            Vector3 posCenter = Vector3.zero;
            int count = 0;
            foreach (EnemyCtrlAbstract enemy in enemies)
            {
                if ((enemy.transform.position - enemyCenter).sqrMagnitude <= sqrRange)
                {
                    posCenter += enemy.transform.position;
                    count++;
                }
            }
            if (count > 0 && count > maxCount)
            {
                maxCount = count;
                target = posCenter / count;
            }
        }
        target.y = 0.3f;
        return target;
        //if (maxCount > 0) 
        //    return bestCenter;
    }

    protected override void OnUpdate()
    {
        _timer += Time.deltaTime;
        float t = Mathf.Clamp01(_timer / _timeToTarget);
        Vector3 hozPos = Vector3.Lerp(_startPos, _targetPos, t);
        float y = _startPos.y + (_targetPos.y - _startPos.y) * t + _height * 4 * t * (1 - t);
        Vector3 currentPosition = new(hozPos.x, y, hozPos.z);
        Vector3 velocity = (currentPosition - transform.position) / Time.deltaTime;
        if (velocity.sqrMagnitude > 0.01f)
            transform.rotation = Quaternion.LookRotation(velocity.normalized) * Quaternion.Euler(90, 0, 0);
        transform.position = currentPosition;

        if (_timer >= _timeToTarget - 0.05f)
        {
            if (!_collider.enabled) _collider.enabled = true;
            if (_timer >= _timeToTarget)
            {
                _timer = 0f;
                _collider.enabled = false;
                PoolManager<EffectCtrlAbstract>.Ins.Spawn(_hitRocket, transform.position, Quaternion.identity);
                PoolManager<BulletCtrlAbstract>.Ins.Despawn(this);
            }
        }
    }

    //private void CheckEnemyTakeDmg()
    //{
    //    Collider[] hits = Physics.OverlapSphere(transform.position, _range);
    //    foreach (var hit in hits)
    //    {
    //        if (hit.TryGetComponent<EnemyCtrlAbstract>(out var enemy))
    //            Observer.NotifyObserver(ObserverID.EnemyTakeDmgSingle, enemy);
    //    }
    //}

    //private void OnDrawGizmos()
    //{
    //    Gizmos.color = Color.red;
    //    Gizmos.DrawWireSphere(transform.position, 1.6f);
    //}

    protected override void LoadComponents()
    {
        if (_hitRocket != null && _collider != null) return;
        _hitRocket = Resources.Load<HitRocket>("Hits/HitRocket");
        _collider = GetComponent<SphereCollider>();
    }

    public override string GetName()
    {
        return "BulletRocket";
    }
}
