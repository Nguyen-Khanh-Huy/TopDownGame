using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyLongCtrl : EnemyCtrlAbstract
{
    [SerializeField] private BulletParabol _bulletParabol;
    [SerializeField] private Transform _firePoint;

    protected override void LoadComponents()
    {
        base.LoadComponents();
        if (_enemySO != null && _bulletParabol != null && _firePoint != null) return;
        _enemySO = Resources.Load<EnemySO>("SO/EnemyLongSO");
        _bulletParabol = Resources.Load<BulletParabol>("Bullets/BulletParabol");
        _firePoint = transform.Find("Model/mixamorig:Hips/mixamorig:Spine/mixamorig:Spine1/mixamorig:Spine2/mixamorig:RightShoulder/mixamorig:RightArm/mixamorig:RightForeArm/mixamorig:RightHand/FirePoint");
    }

    protected override void OnEnable()
    {
        _hpBar.gameObject.SetActive(true);
        _hp = _enemySO.Hp;
        _hpBar.value = _hp / _enemySO.Hp;
        _agent.speed = _enemySO.MoveSpeed;
        base.OnEnable();
    }

    public void EventFireBulletParabol()
    {
        BulletCtrlAbstract newBullet = PoolManager<BulletCtrlAbstract>.Ins.Spawn(_bulletParabol, _firePoint.position, Quaternion.identity);
        newBullet.Attacker = this;
    }

    public override string GetName()
    {
        return "EnemyLong";
    }
}
