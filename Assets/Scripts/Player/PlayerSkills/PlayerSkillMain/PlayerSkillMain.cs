using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerSkillMain : PISMonoBehaviour
{
    [SerializeField] private PlayerSkillsCtrl _playerSkillCtrl;
    
    private int _shotCount = 0;

    public void SkillBulletMain()
    {
        CancelInvoke(nameof(StartShooting));
        _shotCount = 0;
        StartShooting();
    }

    private void StartShooting()
    {
        if (_shotCount >= _playerSkillCtrl.PlayerSkillList.PlayerSkillMultiShot.MultiShotCount) return;

        CheckLvSkillMultiDirection();

        _shotCount++;
        Invoke(nameof(StartShooting), 0.2f);
    }

    private void CheckLvSkillMultiDirection()
    {
        int check = _playerSkillCtrl.PlayerSkillList.PlayerSkillMultiDirection.MultiDirCount;
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
        Quaternion bulletRotation = Quaternion.Euler(0, angle, 0) * _playerSkillCtrl.transform.rotation;
        PoolManager<BulletCtrlAbstract>.Ins.Spawn(_playerSkillCtrl.PlayerSkillList.GetBullet(), _playerSkillCtrl.PlayerCtrl.FirePoint.position, bulletRotation);
    }

    protected override void LoadComponents()
    {
        if (_playerSkillCtrl != null) return;
        _playerSkillCtrl = GetComponentInParent<PlayerSkillsCtrl>();
        
    }
}
