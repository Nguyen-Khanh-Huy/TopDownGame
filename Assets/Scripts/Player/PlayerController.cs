using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : PISMonoBehaviour
{
    [SerializeField] private Rigidbody _rb;
    [SerializeField] private Animator _anim;
    [SerializeField] private PlayerMoving _playerMoving;
    [SerializeField] private PlayerTarget _playerTarget;
    [SerializeField] private int _hp = 10;

    public Rigidbody Rb { get => _rb; set => _rb = value; }
    public Animator Anim { get => _anim; set => _anim = value; }
    public PlayerMoving PlayerMoving { get => _playerMoving; set => _playerMoving = value; }
    public PlayerTarget PlayerTarget { get => _playerTarget; }
    public int Hp { get => _hp; set => _hp = value; }

    protected override void LoadComponents()
    {
        if(_rb != null && _anim != null && _playerMoving != null && _playerTarget != null) return;
        _rb = GetComponent<Rigidbody>();
        _anim = GetComponent<Animator>();
        _playerMoving = GetComponentInChildren<PlayerMoving>();
        _playerTarget = GetComponentInChildren<PlayerTarget>();
    }
}
