using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletParabol : BulletCtrlAbstract
{
    [SerializeField] private PlayerController _player;
    [SerializeField] private HitParabol _hitParabol;
    [SerializeField] private Rigidbody _rb;
    [SerializeField] private SphereCollider _colliderAttack;

    [SerializeField] private Transform _target;
    [SerializeField] private float _height = 0.5f;
    [SerializeField] private float _timeToTarget = 1.2f;


    private void OnEnable()
    {
        _target = _player.transform;
        MoveParabol();
        Invoke(nameof(EnabledColliderAttack), _timeToTarget - 0.1f);
        Invoke(nameof(DespawnBulletParabol), _timeToTarget);
    }

    private void OnDisable()
    {
        _colliderAttack.enabled = false;
        CancelInvoke(nameof(EnabledColliderAttack));
        CancelInvoke(nameof(DespawnBulletParabol));
    }

    private void OnTriggerEnter(Collider other)
    {
        PlayerController player = other.GetComponent<PlayerController>();
        if (player != null)
        {
            Observer.Notify(ObserverID.PlayerTakeDmg);
        }
    }

    private void MoveParabol()
    {
        Vector3 displacement = _target.position - transform.position;
        Vector3 displacementXZ = new(displacement.x, 0, displacement.z);

        float verticalDistance = displacement.y + _height;
        float gravity = Mathf.Abs(Physics.gravity.y);

        Vector3 velocityXZ = displacementXZ / _timeToTarget;
        float velocityY = (verticalDistance + 0.5f * gravity * Mathf.Pow(_timeToTarget, 2)) / _timeToTarget;

        _rb.velocity = velocityXZ + Vector3.up * velocityY;
    }

    private void DespawnBulletParabol()
    {
        SpawnHitParabol();
        PoolManager<BulletCtrlAbstract>.Ins.Despawn(this);
    }

    private void EnabledColliderAttack()
    {
        _colliderAttack.enabled = true;
    }

    private void SpawnHitParabol()
    {
        PoolManager<EffectCtrlAbstract>.Ins.Spawn(_hitParabol, transform.position, Quaternion.identity);
    }

    public override string GetName()
    {
        return "BulletParabol";
    }

    protected override void LoadComponents()
    {
        if (_player != null && _hitParabol != null && _rb != null && _colliderAttack != null) return;
        _player = GameObject.Find("PlayerController").GetComponent<PlayerController>();
        _hitParabol = Resources.Load<HitParabol>("Hits/HitParabol");
        _rb = GetComponent<Rigidbody>();
        _colliderAttack = GetComponent<SphereCollider>();
    }
}
