using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletRocket : BulletCtrlAbstract
{
    [SerializeField] private Rigidbody _rb;
    [SerializeField] private SphereCollider _col;
    [SerializeField] private HitRocket _hitRocket;

    [SerializeField] private float _timeToTarget = 1.1f;
    [SerializeField] private Vector3 _target = new(-3, 0.5f, 5);

    private float _flyTime = 0f;
    private bool _isPaused;
    private Vector3 _savedVelocity;

    private void OnEnable()
    {
        _col.enabled = false;
        LaunchRocket();
    }
    private void OnDisable()
    {
        _col.enabled = false;
    }

    private void Update()
    {
        if (!UIGamePlayManager.Ins.CheckPlayTime)
        {
            PauseRocket();
            return;
        }
        ResumeRocket();
    }

    private void FixedUpdate()
    {
        if (_rb.velocity.sqrMagnitude > 0.01f)
            transform.rotation = Quaternion.LookRotation(_rb.velocity.normalized) * Quaternion.Euler(90, 0, 0);

        _flyTime += Time.fixedDeltaTime;
        if (_flyTime >= _timeToTarget - 0.1f)
            _col.enabled = true;

        if (_flyTime >= _timeToTarget)
        {
            _flyTime = 0f;
            PoolManager<EffectCtrlAbstract>.Ins.Spawn(_hitRocket, transform.position, Quaternion.identity);
            PoolManager<BulletCtrlAbstract>.Ins.Despawn(this);
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        EnemyCtrlAbstract enemy = other.GetComponent<EnemyCtrlAbstract>();
        if (enemy != null && enemy.Hp > 0)
        {
            Observer.Notify(ObserverID.EnemyTakeDmgSingle, enemy);
        }
    }

    protected override void OnUpdate() { }

    private void LaunchRocket()
    {
        _rb.isKinematic = false;
        Vector3 displacement = _target - transform.position;
        Vector3 displacementXZ = new(displacement.x, 0, displacement.z);

        float verticalDistance = displacement.y;
        float gravity = Mathf.Abs(Physics.gravity.y);

        Vector3 velocityXZ = displacementXZ / _timeToTarget;
        float velocityY = (verticalDistance + 0.5f * gravity * Mathf.Pow(_timeToTarget, 2)) / _timeToTarget;

        _rb.velocity = velocityXZ + Vector3.up * velocityY;
    }

    private void PauseRocket()
    {
        if (_isPaused) return;
        _savedVelocity = _rb.velocity;
        _rb.isKinematic = true;
        _isPaused = true;
    }

    private void ResumeRocket()
    {
        if (!_isPaused) return;
        _rb.isKinematic = false;
        _rb.velocity = _savedVelocity;
        _isPaused = false;
    }

    protected override void LoadComponents()
    {
        if (_rb != null && _col != null && _hitRocket != null) return;
        _rb = GetComponent<Rigidbody>();
        _col = GetComponent<SphereCollider>();
        _hitRocket = Resources.Load<HitRocket>("Hits/HitRocket");
    }

    public override string GetName()
    {
        return "BulletRocket";
    }
}
