using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpinBall : PISMonoBehaviour
{
    [SerializeField] private ParticleSystem _particle;
    private Coroutine _spinCoroutine;

    private void OnDisable()
    {
        if (_spinCoroutine != null)
        {
            StopCoroutine(_spinCoroutine);
            _spinCoroutine = null;
        }
    }

    private void Update()
    {
        if (!UIGamePlayManager.Ins.CheckPlayTime && _particle.isPlaying)
            _particle.Pause();
        else if (UIGamePlayManager.Ins.CheckPlayTime && _particle.isPaused)
            _particle.Play();
    }

    private void OnTriggerEnter(Collider other)
    {
        EnemyCtrlAbstract enemy = other.GetComponent<EnemyCtrlAbstract>();
        if (enemy != null)
        {
            if (enemy != null && enemy.Hp > 0)
                Observer.Notify(ObserverID.EnemyTakeDmgSingle, enemy);
        }
    }

    public void Spin(Transform spinBallRotation, float speed)
    {
        _spinCoroutine ??= StartCoroutine(StartSpin(spinBallRotation, speed));
    }

    private IEnumerator StartSpin(Transform spinBallRotation, float speed)
    {
        while (gameObject.activeSelf)
        {
            if (UIGamePlayManager.Ins.CheckPlayTime)
                transform.RotateAround(spinBallRotation.position, Vector3.up, speed * Time.deltaTime);
            yield return null;
        }
        _spinCoroutine = null;
    }

    protected override void LoadComponents()
    {
        if (_particle != null) return;
        _particle = GetComponent<ParticleSystem>();
    }
}
