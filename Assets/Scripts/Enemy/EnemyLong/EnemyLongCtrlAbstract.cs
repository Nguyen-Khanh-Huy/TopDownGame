using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class EnemyLongCtrlAbstract : EnemyCtrlAbstract
{

    [SerializeField] private EnemyLongAttack _enemyLongAttack;
    [SerializeField] private BulletParabol _bulletParabol;
    [SerializeField] private Transform _firePoint;
    public EnemyLongAttack EnemyLongAttack { get => _enemyLongAttack; set => _enemyLongAttack = value; }

    protected override void LoadComponents()
    {
        base.LoadComponents();
        if (_enemySO != null && _enemyLongAttack != null && _bulletParabol != null && _firePoint != null) return;
        _enemySO = Resources.Load<EnemySO>("SO/EnemyLongSO");
        _enemyLongAttack = GetComponentInChildren<EnemyLongAttack>();
        _bulletParabol = Resources.Load<BulletParabol>("Bullets/BulletParabol");
        _firePoint = transform.Find("Model/mixamorig:Hips/mixamorig:Spine/mixamorig:Spine1/mixamorig:Spine2/mixamorig:RightShoulder/mixamorig:RightArm/mixamorig:RightForeArm/mixamorig:RightHand/FirePoint");
    }

    protected override void OnEnable()
    {
        base.OnEnable();
        _hp = _enemySO.Hp;
        _hpBar.value = _hp / _enemySO.Hp;
    }

    protected override void OnDisable()
    {
        EnemyManager.Ins.ListEnemyLongSpawn.Remove(this);
        base.OnDisable();
    }

    public void EventFireBulletParabol()
    {
        PoolManager<BulletCtrlAbstract>.Ins.Spawn(_bulletParabol, _firePoint.position, Quaternion.identity);
    }
}
