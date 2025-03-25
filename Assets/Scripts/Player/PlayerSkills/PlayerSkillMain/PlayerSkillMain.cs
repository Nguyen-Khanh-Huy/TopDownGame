using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerSkillMain : PISMonoBehaviour
{
    [SerializeField] private PlayerSkillsCtrl _playerSkillCtrl;
    [SerializeField] private HitLightning _hitLightning;
    private int _shotCount = 0;

    private void Start()
    {
        Invoke(nameof(CheckLvSkillLightning), _playerSkillCtrl.PlayerSkillList.PlayerSkillLightning.TimeLightning);
    }

    public void SkillBulletMain()
    {
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

    private void ShootBullet(float angle)
    {
        Quaternion bulletRotation = Quaternion.Euler(0, angle, 0) * _playerSkillCtrl.transform.rotation;
        PoolManager<BulletCtrlAbstract>.Ins.Spawn(_playerSkillCtrl.PlayerSkillList.GetBullet(), _playerSkillCtrl.PlayerCtrl.FirePoint.position, bulletRotation);
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

    private void CheckLvSkillLightning()
    {
        Invoke(nameof(CheckLvSkillLightning), _playerSkillCtrl.PlayerSkillList.PlayerSkillLightning.TimeLightning);
        if (!UIGamePlayManager.Ins.CheckPlayTime) return;
        if (_playerSkillCtrl.PlayerSkillList.PlayerSkillLightning.LevelSkill <= 1) return;
        if (_playerSkillCtrl.PlayerCtrl.PlayerTarget.Target == null) return;
        foreach (EnemyCtrlAbstract enemy in _playerSkillCtrl.PlayerCtrl.PlayerTarget.ListEnemyTarget)
        {
            Observer.Notify(ObserverID.EnemyTakeDmg, enemy);
            PoolManager<EffectCtrlAbstract>.Ins.Spawn(_hitLightning, enemy.transform.position, Quaternion.identity);
        }
    }

    protected override void LoadComponents()
    {
        if (_playerSkillCtrl != null && _hitLightning != null) return;
        _playerSkillCtrl = GetComponentInParent<PlayerSkillsCtrl>();
        _hitLightning = Resources.Load<HitLightning>("Hits/HitLightning");
    }
}
