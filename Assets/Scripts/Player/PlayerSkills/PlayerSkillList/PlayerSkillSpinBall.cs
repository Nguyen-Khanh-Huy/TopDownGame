using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerSkillSpinBall : PlayerSkillAbstract
{
    public int SpinBallCount = 1;
    [SerializeField] private float _speed = 120f;
    [SerializeField] private Transform _spinBallRotation;
    [SerializeField] private List<SpinBall> _listBallSpin = new();

    private void Update()
    {
        if (_levelSkill < 2 || _levelSkill > _maxLevel || !UIGamePlayManager.Ins.CheckPlayTime) return;
        _spinBallRotation.position = PlayerCtrl.Ins.transform.position;
    }

    public override void Upgrade()
    {
        base.Upgrade();
        if (SpinBallCount >= 5) return;
        SpinBallCount += 2;
        UpdateBallPos();
    }

    public void UpdateBallPos()
    {
        float angleStep = 360f / SpinBallCount;
        float radius = PlayerCtrl.Ins.PlayerTarget.ColliderTarget.radius / 2.5f;
        for (int i = 0; i < _listBallSpin.Count; i++)
        {
            _listBallSpin[i].gameObject.SetActive(i < SpinBallCount);
            if (i < SpinBallCount)
            {
                float angle = Mathf.Deg2Rad * (angleStep * i);
                Vector3 offset = new Vector3(Mathf.Cos(angle), 0.2f, Mathf.Sin(angle)) * radius;
                _listBallSpin[i].transform.position = _spinBallRotation.position + offset;
                _listBallSpin[i].Spin(_spinBallRotation, _speed);
            }
        }
    }

    protected override void LoadComponents()
    {
        base.LoadComponents();
        if (_spinBallRotation != null && _listBallSpin.Count > 0) return;
        _spinBallRotation = GameObject.Find("SpinBalls").GetComponent<Transform>();

        _listBallSpin.Clear();
        foreach (Transform child in _spinBallRotation)
        {
            if (child.TryGetComponent<SpinBall>(out var ball))
                _listBallSpin.Add(ball);
        }
    }
}
