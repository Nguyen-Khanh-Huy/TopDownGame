using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletParabol : PISMonoBehaviour
{
    [SerializeField] private PlayerController _player;
    [SerializeField] private Rigidbody _rb;
    [SerializeField] private SphereCollider _sphereCollider;

    [SerializeField] private Transform _target;
    [SerializeField] private float height = 0.5f;
    [SerializeField] private float flightTime = 1.2f;

    protected override void LoadComponents()
    {
        if (_player != null && _rb != null && _sphereCollider != null) return;
        _player = GameObject.Find("PlayerController").GetComponent<PlayerController>();
        _rb = GetComponent<Rigidbody>();
        _sphereCollider = GetComponent<SphereCollider>();
    }
    private void OnEnable()
    {
        _target = _player.transform;
        MoveParabol();
    }

    private void MoveParabol()
    {
        Vector3 displacement = _target.position - transform.position;
        Vector3 displacementXZ = new(displacement.x, 0, displacement.z);

        float verticalDistance = displacement.y + height;
        float gravity = Mathf.Abs(Physics.gravity.y);

        Vector3 velocityXZ = displacementXZ / flightTime;
        float velocityY = (verticalDistance + 0.5f * gravity * Mathf.Pow(flightTime, 2)) / flightTime;

        _rb.velocity = velocityXZ + Vector3.up * velocityY;
    }
}
