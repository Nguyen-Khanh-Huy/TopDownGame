using UnityEngine;

public class EnemyCreepCtrl : EnemyCtrlAbstract
{
    protected override void OnEnable()
    {
        base.OnEnable();

        _hp = _enemySO.Hp;
        _hpBar.value = 1f;
        _hpBar.gameObject.SetActive(true);

        _agent.speed = _enemySO.MoveSpeed;
        _agent.isStopped = false;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.layer == LayerMask.NameToLayer("Player"))
        {
            Observer.NotifyObserver(ObserverID.PlayerTakeDmg);
            (EnemyMoving as EnemyCreepMoving)?.StartKnockBack(other.transform);
        }
    }

    protected override void LoadComponents()
    {
        base.LoadComponents();
        if (_enemySO == null)
            _enemySO = Resources.Load<EnemySO>("SO/EnemyCreepSO");
    }

    public override string GetName() => "EnemyCreep";
}
