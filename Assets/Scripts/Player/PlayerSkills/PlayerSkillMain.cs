using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerSkillMain : PlayerSkillAbstract
{
    private int _shotCount = 0;

    public void SkillBulletMain()
    {
        _shotCount = 0;
        FireTripleShot();
    }

    private void FireTripleShot()
    {
        if (_shotCount >= _playerCtrl.PlayerSkillsCtrl.PlayerSkillBulletFiveShots.ShotCount) return;

        CheckLevelSkillThreeTime();

        _shotCount++;
        Invoke(nameof(FireTripleShot), 0.2f);
    }

    private void ShootBullet(float angle)
    {
        Quaternion bulletRotation = Quaternion.Euler(0, angle, 0) * _playerCtrl.transform.rotation;
        PoolManager<BulletCtrlAbstract>.Ins.Spawn(GetBullet(), _playerCtrl.FirePoint.position, bulletRotation);
    }

    private void CheckLevelSkillThreeTime()
    {
        int Check = _playerCtrl.PlayerSkillsCtrl.PlayerSkillBulletTripleBeam.TripleBeam;
        if (Check == 0)
        {
            ShootBullet(0);
        }
        else if(Check == 1)
        {
            ShootBullet(0);
            ShootBullet(-15f);
            ShootBullet(15f);
        }
        else if (Check == 2)
        {
            ShootBullet(0);
            ShootBullet(-15f);
            ShootBullet(15f);
            ShootBullet(-30f);
            ShootBullet(30f);
        }
    }
}
