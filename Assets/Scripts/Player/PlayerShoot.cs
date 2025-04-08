using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerShoot : PISMonoBehaviour
{
    [SerializeField] private PlayerController _playerCtrl;
    [SerializeField] private float _shootSpeed;
    [SerializeField] private int _countEnemyDead;

    private int _shotCount = 0;

    public float ShootSpeed { get => _shootSpeed; set => _shootSpeed = value; }
    public int CountEnemyDead { get => _countEnemyDead; set => _countEnemyDead = value; }

    protected override void LoadComponents()
    {
        if (_playerCtrl != null) return;
        _playerCtrl = GetComponentInParent<PlayerController>();
    }

    private void Start()
    {
        FireBullet();
    }

    private void FireBullet()
    {
        Invoke(nameof(FireBullet), _shootSpeed);
        if (!UIGamePlayManager.Ins.CheckPlayTime) return;
        if (_playerCtrl.PlayerTarget.Target == null) return;

        StartSkillShooting();
    }

    public void StartSkillShooting()
    {
        CancelInvoke(nameof(SkillShooting));
        _shotCount = 0;
        SkillShooting();
    }

    private void SkillShooting()
    {
        if (_shotCount >= _playerCtrl.PlayerSkillsCtrl.PlayerSkillMultiShot.MultiShotCount) return;

        CheckLvSkillMultiDirection();

        _shotCount++;
        Invoke(nameof(SkillShooting), 0.2f);
    }

    private void CheckLvSkillMultiDirection()
    {
        int check = _playerCtrl.PlayerSkillsCtrl.PlayerSkillMultiDirection.MultiDirCount;
        switch (check)
        {
            case 0:
                ShootBullet(0);
                break;
            case 1:
                ShootBullet(0);
                ShootBullet(-15f);
                ShootBullet(15f);
                break;
            case 2:
                ShootBullet(0);
                ShootBullet(-15f);
                ShootBullet(15f);
                ShootBullet(-30f);
                ShootBullet(30f);
                break;
        }
    }
    private void ShootBullet(float angle)
    {
        Quaternion bulletRotation = Quaternion.Euler(0, angle, 0) * _playerCtrl.transform.rotation;
        PoolManager<BulletCtrlAbstract>.Ins.Spawn(_playerCtrl.PlayerSkillsCtrl.GetBullet(), _playerCtrl.FirePoint.position, bulletRotation);
    }
}
