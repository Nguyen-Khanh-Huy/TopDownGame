using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerCtrl : Singleton<PlayerCtrl>
{
    [SerializeField] private Rigidbody _rb;
    [SerializeField] private Animator _anim;
    [SerializeField] private Transform _firePoint;
    [SerializeField] private SpriteRenderer _playerSpriteAttackRange;
    [SerializeField] private BulletPlayer _bulletPlayer;
    [SerializeField] private PlayerMoving _playerMoving;
    [SerializeField] private PlayerTarget _playerTarget;
    [SerializeField] private PlayerShoot _playerShoot;
    [SerializeField] private PlayerMana _playerMana;
    [SerializeField] private PlayerSkillsCtrl _playerSkillsCtrl;
    [SerializeField] private PlayerSO _playerSO;
    [SerializeField] private PlayerSkillSO _playerSkillSO;
    [SerializeField] private int _hp;

    public Rigidbody Rb { get => _rb; set => _rb = value; }
    public Animator Anim { get => _anim; set => _anim = value; }
    public Transform FirePoint { get => _firePoint; }
    public SpriteRenderer PlayerSpriteAttackRange { get => _playerSpriteAttackRange; set => _playerSpriteAttackRange = value; }
    public BulletPlayer BulletPlayer { get => _bulletPlayer; }
    public PlayerMoving PlayerMoving { get => _playerMoving; }
    public PlayerTarget PlayerTarget { get => _playerTarget; }
    public PlayerShoot PlayerShoot { get => _playerShoot; }
    public PlayerMana PlayerMana { get => _playerMana; }
    public PlayerSkillsCtrl PlayerSkillsCtrl { get => _playerSkillsCtrl; }
    public PlayerSO PlayerSO { get => _playerSO; }
    public PlayerSkillSO PlayerSkillSO { get => _playerSkillSO; }
    public int Hp { get => _hp; set => _hp = value; }

    protected override void LoadComponents()
    {
        DontDestroy(false);
        if (_rb != null && _anim != null && _firePoint && _playerSpriteAttackRange != null && _bulletPlayer && _playerMoving != null && _playerTarget != null && _playerShoot != null && _playerMana != null && _playerSkillsCtrl != null && _playerSO != null && _playerSkillSO != null) return;
        _rb = GetComponent<Rigidbody>();
        _anim = GetComponent<Animator>();
        _firePoint = transform.Find("Model/Bip001/FirePoint").GetComponent<Transform>();
        _playerSpriteAttackRange = GetComponentInChildren<SpriteRenderer>();
        _bulletPlayer = Resources.Load<BulletPlayer>("Bullets/BulletPlayer");
        _playerMoving = GetComponentInChildren<PlayerMoving>();
        _playerTarget = GetComponentInChildren<PlayerTarget>();
        _playerShoot = GetComponentInChildren<PlayerShoot>();
        _playerMana = GetComponentInChildren<PlayerMana>();
        _playerSkillsCtrl = GetComponentInChildren<PlayerSkillsCtrl>();
        _playerSO = Resources.Load<PlayerSO>("SO/PlayerSO");
        _playerSkillSO = Resources.Load<PlayerSkillSO>("SO/PlayerSkillSO");
    }

    private void OnEnable()
    {
        Init();
        Observer.AddObserver(ObserverID.PlayerTakeDmg, PlayerTakeDamage);
    }

    private void OnDisable()
    {
        Observer.RemoveObserver(ObserverID.PlayerTakeDmg, PlayerTakeDamage);
    }

    public void PlayerTakeDamage()
    {
        if (_hp <= 0) return;
        _hp--;
        UIGamePlayManager.Ins.DisableImgHpOn();
    }

    private void Init()
    {
        _hp = _playerSO.Hp;
        _playerMana.CurLevel = _playerSO.CurLevel;
        _playerMana.CurMana = _playerSO.CurMana;
        _playerMana.ManaNextLevel = _playerSO.ManaNextLevel;
        _playerShoot.CountEnemyDead = _playerSO.CountEnemyDead;

        _playerMoving.MoveSpeed = _playerSkillSO.MoveSpeed;
        _playerShoot.ShootSpeed = _playerSkillSO.ShootSpeed;
        _playerTarget.ColliderTarget.radius = _playerSkillSO.ShootRange;
        _playerSpriteAttackRange.transform.localScale = new Vector3(_playerTarget.ColliderTarget.radius * 1.5f, _playerTarget.ColliderTarget.radius * 1.5f, 1);
    }
}
