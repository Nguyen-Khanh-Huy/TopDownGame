using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerSkillRocket : PlayerSkillAbstract
{
    public int TimeRoket = 8;

    [SerializeField] private BulletRocket _bulletRocket;
    private float _timeCount;

    private void Update()
    {
        if (_levelSkill < 2 || _levelSkill > _maxLevel || !UIGamePlayManager.Ins.CheckPlayTime || PlayerCtrl.Ins.PlayerTarget.Target == null) return;
        _timeCount += Time.deltaTime;
        if (_timeCount >= TimeRoket)
        {
            _timeCount = 0;
            PoolManager<BulletCtrlAbstract>.Ins.Spawn(_bulletRocket, PlayerCtrl.Ins.transform.position + new Vector3(0f, 1.5f, 0f), Quaternion.identity);
        }
    }

    public override void Upgrade()
    {
        base.Upgrade();
        if (TimeRoket <= 4) return;
        TimeRoket -= 2;

        //if (_rocketCoroutine != null) 
        //    StopCoroutine(_rocketCoroutine);
        //_rocketCoroutine = StartCoroutine(RocketCoroutine());
    }

    //private IEnumerator RocketCoroutine()
    //{
    //    float timeElapsed = 0f;
    //    float startTime = 0f;

    //    while (_levelSkill > 1)
    //    {
    //        startTime = Time.time;
    //        timeElapsed = 0f;

    //        while (timeElapsed < TimeRoket)
    //        {
    //            timeElapsed = Time.time - startTime;
    //            if (!UIGamePlayManager.Ins.CheckPlayTime) startTime -= timeElapsed;
    //            yield return null;
    //        }

    //        if (UIGamePlayManager.Ins.CheckPlayTime)
    //        {
    //            PoolManager<BulletCtrlAbstract>.Ins.Spawn(_bulletRocket, _playerSkillCtrl.PlayerCtrl.transform.position + new Vector3(0f, 1.5f, 0f), Quaternion.identity);
    //        }
    //    }
    //}

    protected override void LoadComponents()
    {
        base.LoadComponents();
        if (_bulletRocket != null) return;
        _bulletRocket = Resources.Load<BulletRocket>("Bullets/BulletRocket");
    }
}
