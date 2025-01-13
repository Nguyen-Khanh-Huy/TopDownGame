using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : PISMonoBehaviour
{
    [SerializeField] private Rigidbody _rb;
    [SerializeField] private Animator _anim;
    [SerializeField] private Transform _firePoint;
    [SerializeField] private BulletPlayer _bulletPlayer;
    [SerializeField] private PlayerMoving _playerMoving;
    [SerializeField] private PlayerTarget _playerTarget;
    [SerializeField] private int _hp = 10;

    public Rigidbody Rb { get => _rb; set => _rb = value; }
    public Animator Anim { get => _anim; set => _anim = value; }
    public Transform FirePoint { get => _firePoint; }
    public BulletPlayer BulletPlayer { get => _bulletPlayer; }
    public PlayerMoving PlayerMoving { get => _playerMoving; }
    public PlayerTarget PlayerTarget { get => _playerTarget; }
    public int Hp { get => _hp; set => _hp = value; }

    protected override void LoadComponents()
    {
        if(_rb != null && _anim != null && _firePoint && _bulletPlayer && _playerMoving != null && _playerTarget != null) return;
        _rb = GetComponent<Rigidbody>();
        _anim = GetComponent<Animator>();
        _firePoint = transform.Find("Model/Bip001/FirePoint").GetComponent<Transform>();
        _bulletPlayer = Resources.Load<BulletPlayer>("Bullets/BulletPlayer");
        _playerMoving = GetComponentInChildren<PlayerMoving>();
        _playerTarget = GetComponentInChildren<PlayerTarget>();
    }
}
