using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMoving : PISMonoBehaviour
{
    public bool IsOnMobile;
    //public Joystick Joystick;

    private bool _canMoveLeft;
    private bool _canMoveRight;
    private bool _canMoveUp;
    private bool _canMoveDown;
    private bool _canJump;

    [SerializeField] private PlayerController _playerController;
    [SerializeField] private float _moveSpeed = 3f;
    [SerializeField] private float _jumpSpeed = 5f;

    private float _hozCheck, _vertCheck;

    private bool IsIdle
    {
        get => !_canMoveLeft && !_canMoveRight && !_canMoveUp && !_canMoveDown && !_canJump;
    }

    //private void Start()
    //{
    //    //if (IsOnMobile)
    //    //{ UIGamePlayManager.Ins.UIMobileGamepad.SetActive(true); }
    //    //else
    //    //{ UIGamePlayManager.Ins.UIMobileGamepad.SetActive(false); }
    //}

    private void Update()
    {
        if (!IsOnMobile)
        {
            _hozCheck = Input.GetAxisRaw("Horizontal");
            _vertCheck = Input.GetAxisRaw("Vertical");

            _canMoveLeft = _hozCheck < 0;
            _canMoveRight = _hozCheck > 0;
            _canMoveUp = _vertCheck > 0;
            _canMoveDown = _vertCheck < 0;

            _canJump = Input.GetKeyDown(KeyCode.Space);
        }
        else
        {
            //if (Joystick == null) return;
            //_canMoveLeft = Joystick.xValue < 0 ? true : false;
            //_canMoveRight = Joystick.xValue > 0 ? true : false;
            //_canMoveUp = Joystick.yValue > 0 ? true : false;
            //_canMoveDown = Joystick.yValue < 0 ? true : false;
        }
        Moving();
        LookAtTarget();
    }
    protected override void LoadComponents()
    {
        if (_playerController != null) return;
        _playerController = GetComponentInParent<PlayerController>();
    }

    private void ChangeState(PlayerState State)
    {
        _playerController.Anim.SetInteger("State", (int)State);
    }

    private void LookAtTarget()
    {
        if (_playerController.PlayerTarget.Target == null)
        {
            if (!_canMoveLeft && !_canMoveRight && !_canMoveUp && !_canMoveDown) return;
            Vector3 moveDirection = new Vector3(_hozCheck, 0, _vertCheck).normalized;
            Quaternion toRotation = Quaternion.LookRotation(moveDirection);
            _playerController.transform.rotation = Quaternion.RotateTowards(_playerController.transform.rotation, toRotation, 540f * Time.deltaTime);
        }
        else
        {
            _playerController.transform.LookAt(_playerController.PlayerTarget.Target.transform);
        }
    }

    private void Moving()
    {
        int _hoz = _canMoveLeft ? -1 : _canMoveRight ? 1 : 0;
        int _vert = _canMoveDown ? -1 : _canMoveUp ? 1 : 0;
        if (_hoz != 0)
        {
            ChangeState(PlayerState.Walk);
            _playerController.Rb.velocity = new Vector3(_hoz * _moveSpeed, _playerController.Rb.velocity.y, _playerController.Rb.velocity.z);
        }
        if (_vert != 0)
        {
            ChangeState(PlayerState.Walk);
            _playerController.Rb.velocity = new Vector3(_playerController.Rb.velocity.x, _playerController.Rb.velocity.y, _vert * _moveSpeed);
        }
        if (_canJump && Mathf.Abs(_playerController.Rb.velocity.y) < 0.01f)
        {
            _canJump = false;
            _playerController.Rb.velocity = new Vector3(_playerController.Rb.velocity.x, _jumpSpeed, _playerController.Rb.velocity.z);
        }
        if (IsIdle && Mathf.Abs(_playerController.Rb.velocity.y) < 0.01f)
        {
            ChangeState(PlayerState.Idle);
            _playerController.Rb.velocity = Vector3.zero;
        }
    }
}
