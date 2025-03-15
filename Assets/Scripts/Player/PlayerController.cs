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
    [SerializeField] private PlayerFire _playerFire;
    [SerializeField] private PlayerSkillsCtrl _playerSkillsCtrl;
    [SerializeField] private PlayerSO _playerSO;
    [SerializeField] private int _hp;
    [SerializeField] private float _moveSpeed;
    [SerializeField] private float _fireSpeed;

    public Rigidbody Rb { get => _rb; set => _rb = value; }
    public Animator Anim { get => _anim; set => _anim = value; }
    public Transform FirePoint { get => _firePoint; }
    public BulletPlayer BulletPlayer { get => _bulletPlayer; }
    public PlayerMoving PlayerMoving { get => _playerMoving; }
    public PlayerTarget PlayerTarget { get => _playerTarget; }
    public PlayerFire PlayerFire { get => _playerFire; }
    public PlayerSkillsCtrl PlayerSkillsCtrl { get => _playerSkillsCtrl; }
    public int Hp { get => _hp; set => _hp = value; }
    public float MoveSpeed { get => _moveSpeed; set => _moveSpeed = value; }
    public float FireSpeed { get => _fireSpeed; set => _fireSpeed = value; }

    protected override void LoadComponents()
    {
        if(_rb != null && _anim != null && _firePoint && _bulletPlayer && _playerMoving != null && _playerTarget != null && _playerFire != null && _playerSkillsCtrl != null && _playerSO != null) return;
        _rb = GetComponent<Rigidbody>();
        _anim = GetComponent<Animator>();
        _firePoint = transform.Find("Model/Bip001/FirePoint").GetComponent<Transform>();
        _bulletPlayer = Resources.Load<BulletPlayer>("Bullets/BulletPlayer");
        _playerMoving = GetComponentInChildren<PlayerMoving>();
        _playerTarget = GetComponentInChildren<PlayerTarget>();
        _playerFire = GetComponentInChildren<PlayerFire>();
        _playerSkillsCtrl = GetComponentInChildren<PlayerSkillsCtrl>();
        _playerSO = Resources.Load<PlayerSO>("SO/PlayerSO");
    }

    private void OnEnable()
    {
        StartInfor();
        Observer.AddObserver(ObserverID.PlayerTakeDmg, _playerFire.PlayerTakeDamage);
    }

    private void StartInfor()
    {
        _hp = _playerSO.Hp;
        _moveSpeed = _playerSO.MoveSpeed;
        _fireSpeed = _playerSO.FireSpeed;
    }
}
