using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletParabol : BulletCtrlAbstract
{
    [SerializeField] private PlayerController _playerCtrl;
    [SerializeField] private HitParabol _hitParabol;

    [SerializeField] private float _range = 0.8f;
    [SerializeField] private float _height = 1.5f;
    [SerializeField] private float _timeToTarget = 1.2f;
    [SerializeField] private float _timer = 0f;

    private Vector3 _startPos;
    private Vector3 _targetPos;

    private void OnEnable()
    {
        _startPos = transform.position;
        _targetPos = _playerCtrl.transform.position;
        _targetPos.y = 0.2f;
    }

    protected override void OnUpdate()
    {
        _timer += Time.deltaTime;
        float t = Mathf.Clamp01(_timer / _timeToTarget);
        Vector3 horizontalPos = Vector3.Lerp(_startPos, _targetPos, t);
        float y = _startPos.y + (_targetPos.y - _startPos.y) * t + _height * 4 * t * (1 - t);
        transform.position = new Vector3(horizontalPos.x, y, horizontalPos.z);

        if (_timer >= _timeToTarget)
        {
            _timer = 0f;
            CheckPlayerTakeDmg();
            PoolManager<EffectCtrlAbstract>.Ins.Spawn(_hitParabol, transform.position, Quaternion.identity);
            PoolManager<BulletCtrlAbstract>.Ins.Despawn(this);
        }
    }

    private void CheckPlayerTakeDmg()
    {
        Collider[] hits = Physics.OverlapSphere(transform.position, _range);
        foreach (var hit in hits)
        {
            if (hit.TryGetComponent<PlayerController>(out var player))
                Observer.Notify(ObserverID.PlayerTakeDmg);
        }
    }

    //private void OnDrawGizmos()
    //{
    //    Gizmos.color = Color.red;
    //    Gizmos.DrawWireSphere(transform.position, _range);
    //}

    //private Vector3 MoveParabol()
    //{
    //    Vector3 displacement = _target.position - transform.position;
    //    Vector3 displacementXZ = new(displacement.x, 0, displacement.z);

    //    float verticalDistance = displacement.y + _height;
    //    float gravity = Mathf.Abs(Physics.gravity.y);

    //    Vector3 velocityXZ = displacementXZ / _timeToTarget;
    //    float velocityY = (verticalDistance + 0.5f * gravity * Mathf.Pow(_timeToTarget, 2)) / _timeToTarget;

    //    return velocityXZ + Vector3.up * velocityY;
    //}

    public override string GetName()
    {
        return "BulletParabol";
    }

    protected override void LoadComponents()
    {
        if (_playerCtrl != null && _hitParabol != null) return;
        _playerCtrl = GameObject.Find("PlayerController").GetComponent<PlayerController>();
        _hitParabol = Resources.Load<HitParabol>("Hits/HitParabol");
    }
}
