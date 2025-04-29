using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletRocket : BulletCtrlAbstract
{
    [SerializeField] private PlayerController _playerCtrl;
    [SerializeField] private HitRocket _hitRocket;

    [SerializeField] private float _range = 1.6f;
    [SerializeField] private float _height = 1.5f;
    [SerializeField] private float _timeToTarget = 0.6f;

    private Vector3 _startPosition;
    private Vector3 _target;
    private float _timer;

    private void OnEnable()
    {
        _startPosition = transform.position;
        _target = GetTargetPos();
    }

    private Vector3 GetTargetPos()
    {
        List<EnemyCtrlAbstract> enemies = _playerCtrl.PlayerTarget.ListEnemyTarget;
        Vector3 target = Vector3.zero;
        float sqrRange = _range * _range;
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
        target.y = 0.2f;
        return target;
        //if (maxCount > 0) 
        //    return bestCenter;
    }

    protected override void OnUpdate()
    {
        _timer += Time.deltaTime;
        float t = Mathf.Clamp01(_timer / _timeToTarget);
        Vector3 hozPos = Vector3.Lerp(_startPosition, _target, t);
        float y = _startPosition.y + (_target.y - _startPosition.y) * t + _height * 4 * t * (1 - t);
        Vector3 currentPosition = new(hozPos.x, y, hozPos.z);
        Vector3 velocity = (currentPosition - transform.position) / Time.deltaTime;
        if (velocity.sqrMagnitude > 0.01f)
            transform.rotation = Quaternion.LookRotation(velocity.normalized) * Quaternion.Euler(90, 0, 0);
        transform.position = currentPosition;

        if (_timer >= _timeToTarget)
        {
            _timer = 0f;
            CheckEnemyTakeDmg();
            PoolManager<EffectCtrlAbstract>.Ins.Spawn(_hitRocket, transform.position, Quaternion.identity);
            PoolManager<BulletCtrlAbstract>.Ins.Despawn(this);
        }
    }

    private void CheckEnemyTakeDmg()
    {
        Collider[] hits = Physics.OverlapSphere(transform.position, _range);
        foreach (var hit in hits)
        {
            if (hit.TryGetComponent<EnemyCtrlAbstract>(out var enemy))
                Observer.NotifyObserver(ObserverID.EnemyTakeDmgSingle, enemy);
        }
    }

    //private void OnDrawGizmos()
    //{
    //    Gizmos.color = Color.red;
    //    Gizmos.DrawWireSphere(transform.position, _range);
    //}

    protected override void LoadComponents()
    {
        if (_playerCtrl != null && _hitRocket != null) return;
        _playerCtrl = GameObject.Find("PlayerController").GetComponent<PlayerController>();
        _hitRocket = Resources.Load<HitRocket>("Hits/HitRocket");
    }

    public override string GetName()
    {
        return "BulletRocket";
    }
}
