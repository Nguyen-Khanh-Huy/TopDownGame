using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyNearCtrl : EnemyCtrlAbstract
{
    [SerializeField] private SphereCollider _colliderAttack;
    [SerializeField] private GameObject _model01;
    [SerializeField] private GameObject _model02;
    [SerializeField] private Avatar _near01Avatar;
    [SerializeField] private Avatar _near02Avatar;
    protected override void OnEnable()
    {
        _hpBar.gameObject.SetActive(true);
        _hp = _enemySO.Hp;
        _hpBar.value = _hp / _enemySO.Hp;
        _agent.speed = _enemySO.MoveSpeed;
        base.OnEnable();
    }

    private void Start()
    {
        SetModel(_near02Avatar);
    }

    private void SetModel(Avatar avatar)
    {
        if(avatar == _near01Avatar)
        {
            _model02.SetActive(false);
            _anim.avatar = _near01Avatar;
        }
        else if(avatar == _near02Avatar)
        {
            _model01.SetActive(false);
            _anim.avatar = _near02Avatar;
        }
    }

    public void EventOnColliderAttack()
    {
        _colliderAttack.enabled = true;
    }

    public void EventOffColliderAttack()
    {
        _colliderAttack.enabled = false;
    }

    protected override void LoadComponents()
    {
        base.LoadComponents();
        if (_enemySO != null && _colliderAttack != null && _model01 != null && _model02 != null && _near01Avatar != null && _near02Avatar != null) return;
        _enemySO = Resources.Load<EnemySO>("SO/EnemyNearSO");
        _colliderAttack = GetComponentInChildren<SphereCollider>();
        _model01 = transform.Find("Model01")?.gameObject;
        _model02 = transform.Find("Model02")?.gameObject;
        _near01Avatar = Resources.Load<Avatar>("Avatar/Near01Avatar");
        _near02Avatar = Resources.Load<Avatar>("Avatar/Near02Avatar");
    }

    public override string GetName()
    {
        return "EnemyNear";
    }
}
