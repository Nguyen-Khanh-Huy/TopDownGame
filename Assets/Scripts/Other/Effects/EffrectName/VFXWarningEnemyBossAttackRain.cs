using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VFXWarningEnemyBossAttackRain : EffectCtrlAbstract
{
    [SerializeField] private ParticleSystem _particle;

    protected override void OnEnable() {}
    protected override void OnDisable() {}

    private void Update()
    {
        bool isGamePaused = !UIGamePlayManager.Ins.CheckPlayTime;
        bool isAttackerFrozen = _attacker != null && _attacker.EnemyMoving.IsFreeze;

        if ((isGamePaused || isAttackerFrozen) && _particle.isPlaying)
            _particle.Pause();
        else if (!isGamePaused && !isAttackerFrozen && _particle.isPaused)
            _particle.Play();

        if (!_particle.IsAlive())
            DespawnEffect();
    }

    protected override void LoadComponents()
    {
        if (_particle != null) return;
        _particle = GetComponent<ParticleSystem>();
    }

    public override string GetName()
    {
        return "VFXWarningEnemyBossAttackRain";
    }
}

