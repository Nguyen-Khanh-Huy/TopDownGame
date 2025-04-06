using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletParabol : BulletCtrlAbstract
{
    [SerializeField] private PlayerController _player;
    [SerializeField] private HitParabol _hitParabol;
    [SerializeField] private SphereCollider _colliderAttack;

    [SerializeField] private float _height = 1.5f;
    [SerializeField] private float _timeToTarget = 1.2f;
    [SerializeField] private float _timer = 0f;

    private Vector3 _startPosition;
    private Vector3 _targetPosition;

    private void OnEnable()
    {
        _startPosition = transform.position;
        _targetPosition = _player.transform.position;
        _targetPosition.y = 0.2f;
    }

    private void OnDisable()
    {
        _colliderAttack.enabled = false;
    }

    protected override void OnUpdate()
    {
        _timer += Time.deltaTime;
        float t = Mathf.Clamp01(_timer / _timeToTarget);
        Vector3 horizontalPos = Vector3.Lerp(_startPosition, _targetPosition, t);
        float y = _startPosition.y + (_targetPosition.y - _startPosition.y) * t + _height * 4 * t * (1 - t);
        transform.position = new Vector3(horizontalPos.x, y, horizontalPos.z);

        if (!_colliderAttack.enabled && _timer >= _timeToTarget - 0.1f)
            _colliderAttack.enabled = true;

        if (_timer >= _timeToTarget)
        {
            _timer = 0f;
            DespawnBulletParabol();
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        PlayerController player = other.GetComponent<PlayerController>();
        if (player != null)
        {
            Observer.Notify(ObserverID.PlayerTakeDmg);
        }
    }

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

    private void DespawnBulletParabol()
    {
        PoolManager<EffectCtrlAbstract>.Ins.Spawn(_hitParabol, transform.position, Quaternion.identity);
        PoolManager<BulletCtrlAbstract>.Ins.Despawn(this);
    }

    public override string GetName()
    {
        return "BulletParabol";
    }

    protected override void LoadComponents()
    {
        if (_player != null && _hitParabol != null && _colliderAttack != null) return;
        _player = GameObject.Find("PlayerController").GetComponent<PlayerController>();
        _hitParabol = Resources.Load<HitParabol>("Hits/HitParabol");
        _colliderAttack = GetComponent<SphereCollider>();
    }
}
