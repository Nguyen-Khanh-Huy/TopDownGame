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
    [SerializeField] private PlayerShoot _playerFire;
    [SerializeField] private PlayerSkillsCtrl _playerSkillsCtrl;
    [SerializeField] private PlayerSO _playerSO;
    [SerializeField] private PlayerSkillSO _playerSkillSO;
    [SerializeField] private int _hp;
    [SerializeField] private float _moveSpeed;
    [SerializeField] private float _shootSpeed;

    public Rigidbody Rb { get => _rb; set => _rb = value; }
    public Animator Anim { get => _anim; set => _anim = value; }
    public Transform FirePoint { get => _firePoint; }
    public BulletPlayer BulletPlayer { get => _bulletPlayer; }
    public PlayerMoving PlayerMoving { get => _playerMoving; }
    public PlayerTarget PlayerTarget { get => _playerTarget; }
    public PlayerShoot PlayerFire { get => _playerFire; }
    public PlayerSkillsCtrl PlayerSkillsCtrl { get => _playerSkillsCtrl; }
    public PlayerSO PlayerSO { get => _playerSO; }
    public PlayerSkillSO PlayerSkillSO { get => _playerSkillSO; }
    public int Hp { get => _hp; set => _hp = value; }
    public float MoveSpeed { get => _moveSpeed; set => _moveSpeed = value; }
    public float ShootSpeed { get => _shootSpeed; set => _shootSpeed = value; }

    protected override void LoadComponents()
    {
        if(_rb != null && _anim != null && _firePoint && _bulletPlayer && _playerMoving != null && _playerTarget != null && _playerFire != null && _playerSkillsCtrl != null && _playerSO != null && _playerSkillSO != null) return;
        _rb = GetComponent<Rigidbody>();
        _anim = GetComponent<Animator>();
        _firePoint = transform.Find("Model/Bip001/FirePoint").GetComponent<Transform>();
        _bulletPlayer = Resources.Load<BulletPlayer>("Bullets/BulletPlayer");
        _playerMoving = GetComponentInChildren<PlayerMoving>();
        _playerTarget = GetComponentInChildren<PlayerTarget>();
        _playerFire = GetComponentInChildren<PlayerShoot>();
        _playerSkillsCtrl = GetComponentInChildren<PlayerSkillsCtrl>();
        _playerSO = Resources.Load<PlayerSO>("SO/PlayerSO");
        _playerSkillSO = Resources.Load<PlayerSkillSO>("SO/PlayerSkillSO");
    }

    private void OnEnable()
    {
        Init();
        Observer.AddObserver(ObserverID.PlayerTakeDmg, _playerFire.PlayerTakeDamage);
    }

    private void Init()
    {
        _hp = _playerSO.Hp;
        _moveSpeed = _playerSO.MoveSpeed;
        _shootSpeed = _playerSO.ShootSpeed;
        _playerTarget.PlayerCollider.isTrigger = true;
        _playerTarget.PlayerCollider.radius = _playerSO.ShootRange;
    }
}
