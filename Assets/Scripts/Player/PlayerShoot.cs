using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerShoot : MonoBehaviour
{
    [SerializeField] private float _shootSpeed;
    [SerializeField] private int _countEnemyDead;

    private int _shotCount = 0;

    public float ShootSpeed { get => _shootSpeed; set => _shootSpeed = value; }
    public int CountEnemyDead { get => _countEnemyDead; set => _countEnemyDead = value; }

    private void Start()
    {
        FireBullet();
    }

    private void FireBullet()
    {
        Invoke(nameof(FireBullet), _shootSpeed);
        if (!UIGamePlayManager.Ins.CheckPlayTime) return;
        if (PlayerCtrl.Ins.PlayerTarget.Target == null) return;

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
        if (_shotCount >= PlayerCtrl.Ins.PlayerSkillsCtrl.PlayerSkillMultiShot.MultiShotCount) return;

        CheckLvSkillMultiDirection();

        _shotCount++;
        Invoke(nameof(SkillShooting), 0.2f);
    }

    private void CheckLvSkillMultiDirection()
    {
        int check = PlayerCtrl.Ins.PlayerSkillsCtrl.PlayerSkillMultiDirection.MultiDirCount;
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
        Quaternion bulletRotation = Quaternion.Euler(0, angle, 0) * PlayerCtrl.Ins.transform.rotation;
        PoolManager<BulletCtrlAbstract>.Ins.Spawn(PlayerCtrl.Ins.PlayerSkillsCtrl.GetBullet(), PlayerCtrl.Ins.FirePoint.position, bulletRotation);
    }
}
