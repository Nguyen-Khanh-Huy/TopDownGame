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
        if (_levelSkill < 2 || _levelSkill > 3) return;
        _spinBallRotation.position = _playerSkillCtrl.PlayerCtrl.transform.position;
        foreach (SpinBall ball in _listBallSpin)
        {
            if (ball.gameObject.activeSelf)
                ball.transform.RotateAround(_spinBallRotation.position, Vector3.up, _speed * Time.deltaTime);
        }
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
        float angleStep = (SpinBallCount == 3) ? 120f : 72f;
        float radius = _playerSkillCtrl.PlayerCtrl.PlayerTarget.PlayerCollider.radius / 2.5f;
        for (int i = 0; i < _listBallSpin.Count; i++)
        {
            _listBallSpin[i].gameObject.SetActive(i < SpinBallCount);
            if (i < SpinBallCount)
            {
                float angle = Mathf.Deg2Rad * (angleStep * i);
                Vector3 offset = new Vector3(Mathf.Cos(angle), 0.2f, Mathf.Sin(angle)) * radius;
                _listBallSpin[i].transform.position = _playerSkillCtrl.PlayerCtrl.transform.position + offset;
            }
        }
    }

    protected override void LoadComponents()
    {
        base.LoadComponents();
        if (_spinBallRotation != null && _listBallSpin.Count > 0) return;
        _spinBallRotation = GameObject.Find("SpinBalls").GetComponent<Transform>();

        foreach (Transform child in _spinBallRotation)
        {
            SpinBall ball = child.GetComponent<SpinBall>();
            if (ball != null)
                _listBallSpin.Add(ball);
        }
    }
}
