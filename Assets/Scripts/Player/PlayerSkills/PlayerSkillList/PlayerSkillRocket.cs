using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerSkillRocket : PlayerSkillAbstract
{
    public int TimeRoket = 8;

    [SerializeField] private BulletRocket _bulletRocket;

    public override void Upgrade()
    {
        base.Upgrade();
        if (TimeRoket <= 4) return;
        TimeRoket -= 2;
        CancelInvoke(nameof(CheckLvSkillRocket));
        CheckLvSkillRocket();
    }

    private void CheckLvSkillRocket()
    {
        Invoke(nameof(CheckLvSkillRocket), TimeRoket);
        if (_levelSkill <= 1) return;
        if (!UIGamePlayManager.Ins.CheckPlayTime) return;
        PoolManager<BulletCtrlAbstract>.Ins.Spawn(_bulletRocket, _playerSkillCtrl.PlayerCtrl.transform.position + new Vector3(0f, 1.5f, 0f), Quaternion.identity);
    }

    protected override void LoadComponents()
    {
        base.LoadComponents();
        if (_bulletRocket != null) return;
        _bulletRocket = Resources.Load<BulletRocket>("Bullets/BulletRocket");
    }
}
