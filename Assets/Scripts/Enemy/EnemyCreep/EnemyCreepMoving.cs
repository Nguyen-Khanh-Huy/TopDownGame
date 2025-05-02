using UnityEngine;

public class EnemyCreepMoving : EnemyMoving
{
    [SerializeField] private bool _isKnockBack;
    [SerializeField] private float _knockBackSpeed = 5f;
    [SerializeField] private float _knockBackTime = 0.3f;

    private Vector3 _knockBackDir;
    private float _knockBackTimer;
    private float _destinationTimer;

    private void OnEnable()
    {
        _enemyCtrl.Agent.isStopped = false;
        _isKnockBack = false;
        _knockBackTimer = 0f;
        _destinationTimer = 0f;
    }

    protected override void EnemyMove()
    {
        if (_enemyCtrl.Hp <= 0f)
        {
            if (_enemyCtrl.Anim.speed != 1) _enemyCtrl.Anim.speed = 1;
            if (!_enemyCtrl.Agent.isStopped) _enemyCtrl.Agent.isStopped = true;

            ChangeState(EnemyCreepState.Dying);
            return;
        }

        if (!UIGamePlayManager.Ins.CheckPlayTime || IsFreeze)
        {
            if (_enemyCtrl.Anim.speed != 0) _enemyCtrl.Anim.speed = 0;
            if (!_enemyCtrl.Agent.isStopped) _enemyCtrl.Agent.isStopped = true;
            return;
        }

        if (_enemyCtrl.Anim.speed != 1) _enemyCtrl.Anim.speed = 1;
        if (_enemyCtrl.Agent.isStopped) _enemyCtrl.Agent.isStopped = false;

        if (_isKnockBack)
            HandleKnockBack();
        else
            HandleMoving();
    }

    private void HandleKnockBack()
    {
        ChangeState(EnemyCreepState.Hit);

        _knockBackTimer += Time.deltaTime;
        _enemyCtrl.Agent.isStopped = true;
        _enemyCtrl.transform.position += _knockBackDir * (_knockBackSpeed * Time.deltaTime);

        if (_knockBackTimer >= _knockBackTime)
            StopKnockBack();
    }

    private void HandleMoving()
    {
        ChangeState(EnemyCreepState.Walk);

        _destinationTimer += Time.deltaTime;
        if (_destinationTimer >= 0.5f)
        {
            _destinationTimer = 0f;
            _enemyCtrl.Agent.SetDestination(PlayerCtrl.Ins.transform.position);
        }
    }

    public void StartKnockBack(Transform player)
    {
        if (_enemyCtrl.Agent == null) return;

        _isKnockBack = true;
        _knockBackTimer = 0f;
        _enemyCtrl.Agent.isStopped = true;
        _knockBackDir = (_enemyCtrl.transform.position - player.position).normalized;
    }

    private void StopKnockBack()
    {
        _isKnockBack = false;
        _knockBackTimer = 0f;
        _enemyCtrl.Agent.isStopped = false;
    }

    private void ChangeState(EnemyCreepState newState)
    {
        if (_enemyCtrl.Anim.GetInteger("State") != (int)newState)
            _enemyCtrl.Anim.SetInteger("State", (int)newState);
    }
}
